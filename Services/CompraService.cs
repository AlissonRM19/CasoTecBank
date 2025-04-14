// Importa el modelo CompraTarjeta para manejar las compras
using TecBankApi.Models;

namespace TecBankApi.Services
{
    /// <summary>
    /// Servicio encargado de la lógica relacionada con las compras hechas con tarjeta
    /// </summary>
    public class CompraService
    {
        // Servicio para manejo de almacenamiento en archivos JSON
        private readonly JsonStorageService _storage;

        // Lista que contiene todas las compras cargadas en memoria
        private List<CompraTarjeta> _compras;

        /// <summary>
        /// Constructor que inicializa el servicio de almacenamiento y carga las compras desde almacenamiento persistente
        /// </summary>
        public CompraService(JsonStorageService storage)
        {
            _storage = storage;
            // Carga los datos desde el archivo JSON o crea una lista vacía si no existen
            _compras = _storage.ComprasTarjeta ?? new List<CompraTarjeta>();


        }

        /// <summary>
        /// Registra una nueva compra y la guarda en almacenamiento
        /// </summary>
        /// <param name="compra">Objeto CompraTarjeta con los datos de la compra</param>
        /// <returns>La compra registrada o null si ocurre un error</returns>
        public async Task<CompraTarjeta?> CrearCompra(CompraTarjeta compra)
        {
            try
            {
                // Asigna un ID único a la compra
                compra.CompraId = _compras.Any() ? _compras.Max(c => c.CompraId) + 1 : 1;

                // Agrega la compra a la lista
                _compras.Add(compra);

                // Guarda la lista actualizada en el almacenamiento
                await _storage.GuardarEntidad(_compras);

                // Retorna la compra registrada
                return compra;
            }
            catch
            {
                // Si ocurre un error, retorna null
                return null;
            }
        }

        /// <summary>
        /// Obtiene las compras realizadas con una tarjeta específica dentro de un rango de fechas
        /// </summary>
        /// <param name="numeroTarjeta">Número de la tarjeta</param>
        /// <param name="desde">Fecha de inicio del rango</param>
        /// <param name="hasta">Fecha de fin del rango</param>
        /// <returns>Lista de compras ordenadas por fecha descendente</returns>
        public async Task<List<CompraTarjeta>> ObtenerComprasPorTarjeta(string numeroTarjeta, DateTime desde, DateTime hasta)
        {
            return await Task.FromResult(
                _compras
                    .Where(c => c.NumeroTarjeta == numeroTarjeta &&
                                c.Fecha >= desde &&
                                c.Fecha <= hasta)
                    .OrderByDescending(c => c.Fecha) // Ordena las compras de más reciente a más antigua
                    .ToList()
            );
        }
    }
}