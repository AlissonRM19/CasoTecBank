using TecBankApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TecBankApi.Services
{
    /// <summary>
    /// Servicio que maneja la lógica relacionada con los movimientos bancarios (depósitos y retiros).
    /// </summary>
    public class MovimientoService
    {
        private readonly JsonStorageService _storage; // Servicio para persistencia en almacenamiento JSON.
        private List<Movimiento> _movimientos;        // Lista en memoria de movimientos cargados.

        /// <summary>
        /// Constructor que carga los movimientos desde almacenamiento al iniciar el servicio.
        /// </summary>
        public MovimientoService(JsonStorageService storage)
        {
            _storage = storage;
            // Carga los movimientos existentes, si no hay ninguno, inicializa lista vacía.
            _movimientos = _storage.LoadData<List<Movimiento>>().Result ?? new List<Movimiento>();
        }

        /// <summary>
        /// Crea y guarda un nuevo movimiento en la lista y en el almacenamiento persistente.
        /// </summary>
        /// <param name="movimiento">Movimiento a registrar</param>
        /// <returns>El movimiento registrado si fue exitoso, null si ocurrió un error</returns>
        public async Task<Movimiento?> CrearMovimiento(Movimiento movimiento)
        {
            try
            {
                // Asigna un ID único autoincremental al nuevo movimiento.
                movimiento.MovimientoId = _movimientos.Any() ? _movimientos.Max(m => m.MovimientoId) + 1 : 1;

                // Agrega el nuevo movimiento a la lista.
                _movimientos.Add(movimiento);

                // Guarda la lista actualizada en el almacenamiento JSON.
                await _storage.SaveData(_movimientos);

                return movimiento;
            }
            catch
            {
                // Retorna null en caso de error (ej: error de escritura).
                return null;
            }
        }

        /// <summary>
        /// Obtiene los movimientos de una cuenta dentro de un rango de fechas específico.
        /// </summary>
        /// <param name="cuentaId">ID de la cuenta</param>
        /// <param name="desde">Fecha inicial del rango</param>
        /// <param name="hasta">Fecha final del rango</param>
        /// <returns>Lista de movimientos filtrados y ordenados por fecha descendente</returns>
        public async Task<List<Movimiento>> ObtenerMovimientos(int cuentaId, DateTime desde, DateTime hasta)
        {
            return await Task.FromResult(
                _movimientos.Where(m =>
                    m.CuentaId == cuentaId &&    // Coincide con la cuenta especificada
                    m.Fecha >= desde &&          // Fecha igual o posterior al inicio
                    m.Fecha <= hasta             // Fecha igual o anterior al final
                )
                .OrderByDescending(m => m.Fecha) // Ordenados por fecha más reciente primero
                .ToList()
            );
        }

        /// <summary>
        /// Calcula el saldo actual de una cuenta sumando depósitos y restando retiros.
        /// </summary>
        /// <param name="cuentaId">ID de la cuenta</param>
        /// <returns>Saldo total de la cuenta</returns>
        public async Task<decimal> ObtenerSaldoCuenta(int cuentaId)
        {
            // Filtra los movimientos de la cuenta especificada
            var movimientos = await Task.FromResult(
                _movimientos.Where(m => m.CuentaId == cuentaId).ToList()
            );

            // Calcula el saldo: suma los depósitos y resta los retiros
            return movimientos.Sum(m => m.Tipo == "Depósito" ? m.Monto : -m.Monto);
        }
    }
}
