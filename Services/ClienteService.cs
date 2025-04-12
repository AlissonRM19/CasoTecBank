using TecBankApi.Models;                // Se importan los modelos, incluyendo Cliente.
using TecBankApi.Helpers;              // Se importan utilidades como el servicio de almacenamiento y hasheo de contraseñas.
using System.Text.RegularExpressions;  // Para validar patrones con expresiones regulares.

namespace TecBankApi.Services
{
    /// <summary>
    /// Servicio que administra la lógica relacionada con clientes:
    /// creación, actualización, eliminación y validación de datos.
    /// </summary>
    public class ClienteService
    {
        // Servicio de almacenamiento JSON que se encarga de cargar y guardar datos de clientes.
        private readonly JsonStorageService _storage;

        // Lista en memoria de todos los clientes cargados desde el archivo.
        private List<Cliente> _clientes;

        // Expresión regular para validar formato de cédula (ej: 3).
        private const string CedulaPattern = @"^[1-7]\d{8}$";

        /// <summary>
        /// Constructor: carga la lista de clientes desde el archivo JSON.
        /// Si no existe, inicia con una lista vacía.
        /// </summary>
        public ClienteService(JsonStorageService storage)
        {
            _storage = storage;

            // Carga inicial de clientes; si no hay datos previos, se crea una nueva lista vacía.
            _clientes = _storage.LoadData<List<Cliente>>().Result ?? new List<Cliente>();
        }

        /// <summary>
        /// Retorna todos los clientes registrados.
        /// </summary>
        public async Task<List<Cliente>> ObtenerTodosClientes() =>
            await Task.FromResult(_clientes);

        /// <summary>
        /// Busca un cliente por su ID único.
        /// </summary>
        public async Task<Cliente?> ObtenerClientePorId(int id) =>
            await Task.FromResult(_clientes.FirstOrDefault(c => c.ClienteId == id));

        /// <summary>
        /// Registra un nuevo cliente si pasa las validaciones.
        /// </summary>
        /// <param name="cliente">Cliente nuevo a registrar</param>
        /// <returns>True si se guarda exitosamente, False si hay errores de validación</returns>
        public async Task<bool> CrearCliente(Cliente cliente)
        {
            // Verifica que la cédula tenga el formato correcto usando expresión regular.
            if (!Regex.IsMatch(cliente.Cedula, CedulaPattern))
                return false;

            // Verifica si ya existe una cédula o usuario duplicado.
            if (_clientes.Any(c => c.Cedula == cliente.Cedula || c.Usuario == cliente.Usuario))
                return false;

            // Hashea la contraseña antes de almacenarla y elimina el texto plano por seguridad.
            cliente.PasswordHash = PasswordHasher.HashPassword(cliente.Password);
            cliente.Password = string.Empty;

            // Asigna un nuevo ID autoincremental al cliente.
            cliente.ClienteId = _clientes.Any() ? _clientes.Max(c => c.ClienteId) + 1 : 1;

            // Agrega el cliente a la lista y guarda los cambios.
            _clientes.Add(cliente);
            return await GuardarCambios();
        }

        /// <summary>
        /// Actualiza los datos de un cliente existente si no hay conflictos.
        /// </summary>
        /// <param name="cliente">Cliente con datos actualizados</param>
        /// <returns>True si se actualiza correctamente, False si ocurre error</returns>
        public async Task<bool> ActualizarCliente(Cliente cliente)
        {
            // Busca el cliente original en la lista por su ID.
            var existente = _clientes.FirstOrDefault(c => c.ClienteId == cliente.ClienteId);
            if (existente == null)
                return false;

            // Si se cambió la cédula, valida que la nueva no esté repetida.
            if (existente.Cedula != cliente.Cedula &&
                _clientes.Any(c => c.Cedula == cliente.Cedula))
                return false;

            // Si se actualizó la contraseña, se hashea nuevamente. Si no, se conserva el hash anterior.
            if (!string.IsNullOrEmpty(cliente.Password))
                cliente.PasswordHash = PasswordHasher.HashPassword(cliente.Password);
            else
                cliente.PasswordHash = existente.PasswordHash;

            // Elimina al cliente original y agrega la nueva versión.
            _clientes.Remove(existente);
            _clientes.Add(cliente);

            // Guarda los cambios persistentes.
            return await GuardarCambios();
        }

        /// <summary>
        /// Elimina un cliente del sistema por su ID.
        /// </summary>
        /// <param name="id">ID del cliente a eliminar</param>
        /// <returns>True si se eliminó exitosamente, False si no se encontró</returns>
        public async Task<bool> EliminarCliente(int id)
        {
            // Busca el cliente por ID.
            var cliente = _clientes.FirstOrDefault(c => c.ClienteId == id);
            if (cliente == null)
                return false;

            // Lo elimina de la lista y guarda los cambios.
            _clientes.Remove(cliente);
            return await GuardarCambios();
        }

        /// <summary>
        /// Guarda la lista actual de clientes en el archivo JSON.
        /// </summary>
        /// <returns>True si se guarda sin errores, False si hay una excepción</returns>
        private async Task<bool> GuardarCambios()
        {
            try
            {
                await _storage.SaveData(_clientes);
                return true;
            }
            catch
            {
                // Si ocurre un error durante el guardado, se retorna false.
                return false;
            }
        }
    }
}
