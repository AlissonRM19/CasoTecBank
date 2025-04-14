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
            //_tarjetas = _storage.LoadData<List<Tarjeta>>().Result ?? new List<Tarjeta>();
            _tarjetas = _storage.Tarjetas ?? new List<Tarjeta>();

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
            if (string.IsNullOrEmpty(tarjeta.N_Tarjeta.ToString()))
                tarjeta.N_Tarjeta = Convert.ToInt32(GenerarNumeroTarjetaValido());
            // Si se proporciona, se valida que tenga formato de 16 dígitos
            else if (!Regex.IsMatch(tarjeta.N_Tarjeta.ToString(), NumeroTarjetaPattern))
                throw new ValidationException("Formato de tarjeta inválido");

            // Validación de la fecha de expiración: debe ser al menos un mes en el futuro
            if (tarjeta.Fecha_Expira < DateTime.Now.AddMonths(1))
                throw new ValidationException("La fecha de expiración debe ser al menos 1 mes en el futuro");

            // Se verifica si el cliente asociado existe
            if (!await _clienteService.ClienteExiste(tarjeta.Ced_Cliente))
                throw new ValidationException("Cliente no registrado");

            // Se verifica que el número de tarjeta sea único
            if (_tarjetas.Any(t => t.N_Tarjeta == tarjeta.N_Tarjeta))
                return null;

            // Lógica particular según el tipo de tarjeta
            if (tarjeta.Tipo_Tarjeta == "Crédito")
            {
                // Se valida que tenga un límite de crédito válido
                if (tarjeta.Credito <= 0)
                    throw new ValidationException("Límite de crédito inválido");

                // El saldo disponible se iguala al límite
                tarjeta.Saldo = tarjeta.Credito;
            }
            else if (tarjeta.Tipo_Tarjeta == "Débito")
            {
                // Una tarjeta de débito requiere una cuenta asociada
                if (string.IsNullOrEmpty(tarjeta.Cuenta_aso.ToString()))
                    throw new ValidationException("Cuenta asociada requerida para débito");
            }

            // Se asigna un ID único a la tarjeta
            tarjeta.N_Tarjeta = _tarjetas.Any() ? _tarjetas.Max(t => t.N_Tarjeta) + 1 : 1;

            // Se guarda la tarjeta en la lista y en almacenamiento
            _tarjetas.Add(tarjeta);
            await _storage.GuardarEntidad(_tarjetas); 
            return tarjeta;
        }

        /// <summary>
        /// Elimina una tarjeta si cumple con las reglas de negocio.
        /// </summary>
        public async Task<bool> EliminarTarjeta(int id)
        {
            // Se busca la tarjeta por ID
            var tarjeta = _tarjetas.FirstOrDefault(t => t.N_Tarjeta == id);
            if (tarjeta == null) return false;

            // En tarjetas de crédito, sólo se puede eliminar si no ha sido usada
            if (tarjeta.Tipo_Tarjeta == "Crédito" && tarjeta.Saldo != tarjeta.Credito)
                return false;

            _tarjetas.Remove(tarjeta);
            return await GuardarCambios();
        }

        /// <summary>
        /// Verifica si un cliente es dueño de una tarjeta específica.
        /// </summary>
        public async Task<bool> ValidarPropietario(int numeroTarjeta, int clienteId)
        {
            return await Task.FromResult(
                _tarjetas.Any(t =>
                    t.N_Tarjeta == numeroTarjeta &&
                    t.Ced_Cliente == clienteId
                )
            );
        }

        /// <summary>
        /// Actualiza el saldo disponible de una tarjeta de crédito después de una transacción.
        /// </summary>
        public async Task ActualizarSaldo(int numeroTarjeta, decimal monto)
        {
            var tarjeta = _tarjetas.FirstOrDefault(t => t.N_Tarjeta == numeroTarjeta);
            if (tarjeta == null || monto <= 0) return;

            // Solo se actualiza el saldo si es tarjeta de crédito
            if (tarjeta.Tipo_Tarjeta == "Crédito")
            {
                if (tarjeta.Saldo < monto)
                    throw new InvalidOperationException("Saldo insuficiente");

                tarjeta.Saldo -= Convert.ToInt32(monto);
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
                _tarjetas.Where(t => t.Ced_Cliente == clienteId)
                         .OrderByDescending(t => t.N_Tarjeta)
                         .ToList()
            );
        }

        // Método privado que guarda los cambios en el almacenamiento persistente
        private async Task<bool> GuardarCambios()
        {
            try
            {
                await _storage.GuardarEntidad(_tarjetas);
                return true;
            }
            catch
            {
                return false;
            }
        }
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