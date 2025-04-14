using TecBankApi.Models;        // Se importan los modelos necesarios
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TecBankApi.Services
{
    /// <summary>
    /// Servicio que administra la lógica relacionada con transferencias bancarias.
    /// Permite listar, agregar, buscar y eliminar transferencias.
    /// </summary>
    public class TransferenciaService
    {
        // Servicio de almacenamiento JSON compartido entre servicios
        private readonly JsonStorageService _storage;

        // Lista de transferencias cargadas desde el archivo JSON
        private List<Transferencia> _transferencias;

        /// <summary>
        /// Constructor: carga las transferencias existentes desde almacenamiento JSON.
        /// </summary>
        public TransferenciaService(JsonStorageService storage)
        {
            _storage = storage;
            _transferencias = _storage.transferencias?? new List<Transferencia>();
        }

        /// <summary>
        /// Retorna todas las transferencias registradas.
        /// </summary>
        public async Task<List<Transferencia>> ObtenerTodas()
        {
            return await Task.FromResult(_transferencias.ToList());
        }

        /// <summary>
        /// Busca una transferencia por su ID.
        /// </summary>
        public async Task<Transferencia?> ObtenerPorId(int id)
        {
            return await Task.FromResult(
                _transferencias.FirstOrDefault(t => t.ID_Transferencia == id)
            );
        }

        /// <summary>
        /// Agrega una nueva transferencia con ID autoincremental.
        /// </summary>
        public async Task<bool> AgregarTransferencia(Transferencia transferencia)
        {
            transferencia.ID_Transferencia = _transferencias.Any()
                ? _transferencias.Max(t => t.ID_Transferencia) + 1
                : 1;

            _transferencias.Add(transferencia);
            return await GuardarCambios();
        }

        /// <summary>
        /// Elimina una transferencia existente.
        /// </summary>
        public async Task<bool> EliminarTransferencia(int id)
        {
            var transferencia = _transferencias.FirstOrDefault(t => t.ID_Transferencia == id);
            if (transferencia == null)
                return false;

            _transferencias.Remove(transferencia);
            return await GuardarCambios();
        }

        /// <summary>
        /// Guarda la lista de transferencias en el almacenamiento JSON.
        /// </summary>
        private async Task<bool> GuardarCambios()
        {
            try
            {
                await _storage.GuardarEntidad(_transferencias);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

