using TecBankApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TecBankApi.Services
{
    /// <summary>
    /// Servicio que administra la lógica de negocio para los asesores de crédito.
    /// </summary>
    public class AsesorService
    {
        // Servicio de almacenamiento en formato JSON
        private readonly JsonStorageService _storage;

        // Lista de asesores en memoria
        private List<AsesorCredito> _asesores;

        /// <summary>
        /// Constructor que carga los datos almacenados al iniciar el servicio.
        /// </summary>
        public AsesorService(JsonStorageService storage)
        {
            _storage = storage;
            // Carga los datos desde almacenamiento; si no hay datos, crea una lista vacía
            _asesores = _storage.Asesores ?? new List<AsesorCredito>();

        }

        /// <summary>
        /// Retorna la lista completa de asesores.
        /// </summary>
        public async Task<List<AsesorCredito>> ObtenerTodosAsesores() => await Task.FromResult(_asesores);

        /// <summary>
        /// Crea un nuevo asesor si no existe otro con la misma cédula.
        /// </summary>
        /// <param name="asesor">Asesor a registrar</param>
        /// <returns>El asesor creado o null si ya existe uno con la misma cédula</returns>
        public async Task<AsesorCredito?> CrearAsesor(AsesorCredito asesor)
        {
            // Valida si ya existe un asesor con la misma cédula
            if (_asesores.Any(a => a.Cedula == asesor.Cedula))
                return null;

            // Asigna un nuevo ID de asesor de forma incremental
            asesor.AsesorId = _asesores.Any() ? _asesores.Max(a => a.AsesorId) + 1 : 1;

            // Agrega el asesor a la lista y guarda los datos
            _asesores.Add(asesor);
            await _storage.GuardarEntidad(_asesores);

            return asesor;
        }

        public async Task ActualizarComisiones(int asesorId, decimal monto, string moneda)
        {
            var asesor = _asesores.FirstOrDefault(a => a.AsesorId == asesorId);
            if (asesor == null) return;

            if (moneda == "CRC")
                asesor.ComisionesColones += monto * 0.03m;
            else if (moneda == "USD")
                asesor.ComisionesDolares += monto * 0.03m;

            await _storage.GuardarEntidad(_asesores);
        }

        /// <summary>
        /// Actualiza las metas de ventas de un asesor dado su ID.
        /// </summary>
        /// <param name="id">ID del asesor</param>
        /// <param name="metaColones">Nueva meta en colones</param>
        /// <param name="metaDolares">Nueva meta en dólares</param>
        /// <returns>true si se actualizó correctamente, false si el asesor no existe</returns>
        public async Task<bool> ActualizarMetas(int id, decimal metaColones, decimal metaDolares)
        {
            // Busca el asesor por su ID
            var asesor = _asesores.FirstOrDefault(a => a.AsesorId == id);
            if (asesor == null) return false;

            // Actualiza las metas
            asesor.MetaVentasColones = metaColones;
            asesor.MetaVentasDolares = metaDolares;

            // Guarda los cambios
            await _storage.GuardarEntidad(_asesores);
            return true;
        }

        /// <summary>
        /// Obtiene un asesor por su ID.
        /// </summary>
        /// <param name="id">ID del asesor a buscar</param>
        /// <returns>El asesor si se encuentra, o null si no existe</returns>
        public async Task<AsesorCredito?> ObtenerAsesorPorId(int id)
        {
            var asesor = _asesores.FirstOrDefault(a => a.AsesorId == id);
            return await Task.FromResult(asesor);
        }

    }
}
