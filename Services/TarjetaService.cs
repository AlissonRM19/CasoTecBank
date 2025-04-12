// Se importan los espacios de nombres necesarios para el modelo, servicios, validaciones y utilidades generales
using TecBankApi.Models;
using TecBankApi.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TecBankApi.Services
{
    // Servicio encargado de gestionar la lógica relacionada con tarjetas (crédito y débito)
    public class TarjetaService
    {
        private readonly JsonStorageService _storage; // Servicio de almacenamiento en JSON
        private readonly ClienteService _clienteService; // Servicio para verificar datos del cliente
        private List<Tarjeta> _tarjetas; // Lista en memoria de tarjetas cargadas
        private const string NumeroTarjetaPattern = @"^\d{16}$"; // Expresión regular para validar números de tarjeta (16 dígitos)

        // Constructor que recibe servicios requeridos e inicializa la lista de tarjetas
        public TarjetaService(JsonStorageService storage, ClienteService clienteService)
        {
            _storage = storage;
            _clienteService = clienteService;
            _tarjetas = _storage.LoadData<List<Tarjeta>>().Result ?? new List<Tarjeta>();
        }

        /// <summary>
        /// Retorna todas las tarjetas registradas en el sistema. (Acceso restringido a administrador)
        /// </summary>
        public async Task<List<Tarjeta>> ObtenerTodasTarjetas() => await Task.FromResult(_tarjetas);

        /// <summary>
        /// Crea una nueva tarjeta después de aplicar todas las validaciones necesarias.
        /// </summary>
        public async Task<Tarjeta?> CrearTarjeta(Tarjeta tarjeta)
        {
            // Si no se proporciona un número de tarjeta, se genera automáticamente uno válido
            if (string.IsNullOrEmpty(tarjeta.Numero))
                tarjeta.Numero = GenerarNumeroTarjetaValido();
            // Si se proporciona, se valida que tenga formato de 16 dígitos
            else if (!Regex.IsMatch(tarjeta.Numero, NumeroTarjetaPattern))
                throw new ValidationException("Formato de tarjeta inválido");

            // Validación de la fecha de expiración: debe ser al menos un mes en el futuro
            if (tarjeta.FechaExpiracion < DateTime.Now.AddMonths(1))
                throw new ValidationException("La fecha de expiración debe ser al menos 1 mes en el futuro");

            // Se verifica si el cliente asociado existe
            if (!await _clienteService.ClienteExiste(tarjeta.ClienteId))
                throw new ValidationException("Cliente no registrado");

            // Se verifica que el número de tarjeta sea único
            if (_tarjetas.Any(t => t.Numero == tarjeta.Numero))
                return null;

            // Lógica particular según el tipo de tarjeta
            if (tarjeta.Tipo == "Crédito")
            {
                // Se valida que tenga un límite de crédito válido
                if (tarjeta.LimiteCredito <= 0)
                    throw new ValidationException("Límite de crédito inválido");

                // El saldo disponible se iguala al límite
                tarjeta.SaldoDisponible = tarjeta.LimiteCredito;
            }
            else if (tarjeta.Tipo == "Débito")
            {
                // Una tarjeta de débito requiere una cuenta asociada
                if (string.IsNullOrEmpty(tarjeta.CuentaAsociada))
                    throw new ValidationException("Cuenta asociada requerida para débito");
            }

            // Se asigna un ID único a la tarjeta
            tarjeta.TarjetaId = _tarjetas.Any() ? _tarjetas.Max(t => t.TarjetaId) + 1 : 1;

            // Se guarda la tarjeta en la lista y en almacenamiento
            _tarjetas.Add(tarjeta);
            await _storage.SaveData(_tarjetas);
            return tarjeta;
        }

        /// <summary>
        /// Elimina una tarjeta si cumple con las reglas de negocio.
        /// </summary>
        public async Task<bool> EliminarTarjeta(int id)
        {
            // Se busca la tarjeta por ID
            var tarjeta = _tarjetas.FirstOrDefault(t => t.TarjetaId == id);
            if (tarjeta == null) return false;

            // En tarjetas de crédito, sólo se puede eliminar si no ha sido usada
            if (tarjeta.Tipo == "Crédito" && tarjeta.SaldoDisponible != tarjeta.LimiteCredito)
                return false;

            _tarjetas.Remove(tarjeta);
            return await GuardarCambios();
        }

        /// <summary>
        /// Verifica si un cliente es dueño de una tarjeta específica.
        /// </summary>
        public async Task<bool> ValidarPropietario(string numeroTarjeta, int clienteId)
        {
            return await Task.FromResult(
                _tarjetas.Any(t =>
                    t.Numero == numeroTarjeta &&
                    t.ClienteId == clienteId
                )
            );
        }

        /// <summary>
        /// Actualiza el saldo disponible de una tarjeta de crédito después de una transacción.
        /// </summary>
        public async Task ActualizarSaldo(string numeroTarjeta, decimal monto)
        {
            var tarjeta = _tarjetas.FirstOrDefault(t => t.Numero == numeroTarjeta);
            if (tarjeta == null || monto <= 0) return;

            // Solo se actualiza el saldo si es tarjeta de crédito
            if (tarjeta.Tipo == "Crédito")
            {
                if (tarjeta.SaldoDisponible < monto)
                    throw new InvalidOperationException("Saldo insuficiente");

                tarjeta.SaldoDisponible -= monto;
            }

            await GuardarCambios();
        }

        /// <summary>
        /// Genera un número de tarjeta válido utilizando el algoritmo de Luhn.
        /// </summary>
        public string GenerarNumeroTarjetaValido()
        {
            var random = new Random();
            var numero = new int[15]; // Primeros 15 dígitos aleatorios

            for (int i = 0; i < 15; i++)
                numero[i] = random.Next(0, 10);

            // Se calcula el dígito verificador (16°) con el algoritmo Luhn
            int suma = numero.Select((d, i) => (i % 2 == 0 ? d * 2 : d))
                             .Sum(d => d > 9 ? d - 9 : d);

            int digitoVerificador = (10 - (suma % 10)) % 10;

            // Se retorna la concatenación como string
            return string.Concat(numero) + digitoVerificador.ToString();
        }

        /// <summary>
        /// Retorna todas las tarjetas registradas a un cliente específico.
        /// </summary>
        public async Task<List<Tarjeta>> ObtenerTarjetasPorCliente(int clienteId)
        {
            return await Task.FromResult(
                _tarjetas.Where(t => t.ClienteId == clienteId)
                         .OrderByDescending(t => t.TarjetaId)
                         .ToList()
            );
        }

        // Método privado que guarda los cambios en el almacenamiento persistente
        private async Task<bool> GuardarCambios()
        {
            try
            {
                await _storage.SaveData(_tarjetas);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    // Modelo que representa una tarjeta de crédito o débito
    public class Tarjeta
    {
        public int TarjetaId { get; set; } // ID único de la tarjeta

        [Required(ErrorMessage = "Tipo de tarjeta requerido")]
        [RegularExpression("Crédito|Débito", ErrorMessage = "Tipo inválido")]
        public string Tipo { get; set; } = "Débito"; // Tipo de tarjeta

        [Required(ErrorMessage = "Número de tarjeta requerido")]
        [CreditCard(ErrorMessage = "Número inválido")]
        public string Numero { get; set; } = string.Empty; // Número de tarjeta

        [Required(ErrorMessage = "Fecha de expiración requerida")]
        [FutureDate(ErrorMessage = "Debe ser fecha futura")]
        public DateTime FechaExpiracion { get; set; } // Fecha de expiración

        [Required(ErrorMessage = "CVV requerido")]
        [StringLength(4, MinimumLength = 3, ErrorMessage = "CVV inválido")]
        public string CVV { get; set; } = string.Empty; // Código de seguridad

        [Range(0, double.MaxValue, ErrorMessage = "Límite inválido")]
        public decimal LimiteCredito { get; set; } // Límite de crédito (solo tarjetas de crédito)

        [Range(0, double.MaxValue, ErrorMessage = "Saldo inválido")]
        public decimal SaldoDisponible { get; set; } // Saldo disponible (para crédito)

        [StringLength(20, ErrorMessage = "Cuenta inválida")]
        public string? CuentaAsociada { get; set; } // Cuenta asociada (para débito)

        [Required(ErrorMessage = "Cliente requerido")]
        public int ClienteId { get; set; } // ID del cliente propietario
    }

    // Atributo de validación personalizado para asegurar que la fecha sea futura
    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            return value is DateTime date && date > DateTime.Now.Date;
        }
    }
}