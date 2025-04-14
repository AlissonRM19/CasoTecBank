using System.Text.Json;
using TecBankApi.Data;
using TecBankApi.Models;

namespace TecBankApi.Services
{
    /// <summary>
    /// Servicio responsable de cargar y guardar los datos de la aplicación desde/hacia un archivo JSON.
    /// </summary>
    public class JsonStorageService
    {
        private readonly string _filePath;         // Ruta del archivo JSON donde se almacenan los datos
        private DatosTecBank _datos;               // Objeto que representa todos los datos de la aplicación

        /// <summary>
        /// Constructor que inicializa la ruta del archivo y carga los datos desde el archivo JSON.
        /// </summary>
        public JsonStorageService(IConfiguration config)
        {
            // Obtiene la ruta del archivo desde la configuración, con un valor por defecto
            _filePath = config["Data:FilePath"] ?? "Data/data.json";

            // Inicializa el objeto contenedor de datos
            _datos = new DatosTecBank();

            // Carga los datos desde el archivo
            CargarDatos().Wait();
        }

        /// <summary>
        /// Carga los datos desde el archivo JSON si existe, de lo contrario crea uno nuevo vacío.
        /// </summary>
        private async Task CargarDatos()
        {
            // Si el archivo no existe, lo crea con datos vacíos
            if (!File.Exists(_filePath))
            {
                await GuardarDatos();
                return;
            }

            // Lee el contenido del archivo JSON
            var jsonData = await File.ReadAllTextAsync(_filePath);

            // Intenta deserializar el contenido, si falla usa datos vacíos
            _datos = JsonSerializer.Deserialize<DatosTecBank>(jsonData) ?? new DatosTecBank();
        }

        /// <summary>
        /// Guarda los datos actuales en el archivo JSON con formato indentado.
        /// </summary>
        public async Task GuardarDatos()
        {
            var options = new JsonSerializerOptions { WriteIndented = true }; // Formato bonito
            var jsonData = JsonSerializer.Serialize(_datos, options);         // Convierte a JSON
            await File.WriteAllTextAsync(_filePath, jsonData);                // Escribe el archivo
        }

        // Propiedades de acceso a las listas de datos principales

        public List<Cliente> Clientes => _datos.Clientes;
        public List<Cuenta> Cuentas => _datos.Cuentas;
        public List<Tarjeta> Tarjetas => _datos.Tarjetas;
        public List<Prestamo> Prestamos => _datos.Prestamos;
        public List<Movimiento> Movimientos => _datos.Movimientos;
        public List<AsesorCredito> Asesores => _datos.Asesores;
        public List<Rol> Roles => _datos.Roles;
        public List<CompraTarjeta> ComprasTarjeta => _datos.ComprasTarjeta;


        // Propiedades para acceder y modificar el tipo de cambio
        public decimal TipoCambioUSD
        {
            get => _datos.TipoCambioUSD;
            set => _datos.TipoCambioUSD = value;
        }

        public decimal TipoCambioEUR
        {
            get => _datos.TipoCambioEUR;
            set => _datos.TipoCambioEUR = value;
        }
        public async Task GuardarEntidad<T>(List<T> entidad)
        {
            if (typeof(T) == typeof(Cliente))
                _datos.Clientes = entidad.Cast<Cliente>().ToList();
            else if (typeof(T) == typeof(Cuenta))
                _datos.Cuentas = entidad.Cast<Cuenta>().ToList();
            else if (typeof(T) == typeof(Tarjeta))
                _datos.Tarjetas = entidad.Cast<Tarjeta>().ToList();
            else if (typeof(T) == typeof(Prestamo))
                _datos.Prestamos = entidad.Cast<Prestamo>().ToList();
            else if (typeof(T) == typeof(Movimiento))
                _datos.Movimientos = entidad.Cast<Movimiento>().ToList();
            else if (typeof(T) == typeof(AsesorCredito))
                _datos.Asesores = entidad.Cast<AsesorCredito>().ToList();
            else if (typeof(T) == typeof(Rol))
                _datos.Roles = entidad.Cast<Rol>().ToList();
            else if (typeof(T) == typeof(CompraTarjeta)) 
                _datos.ComprasTarjeta = entidad.Cast<CompraTarjeta>().ToList();
            else
                throw new InvalidOperationException($"Tipo {typeof(T).Name} no soportado.");

            await GuardarDatos();
        }

    }
}