using TecBankApi.Models;

namespace TecBankApi.Services
{
    public class AuthService
    {
        private readonly JsonStorageService _storage;

        public AuthService(JsonStorageService storage)
        {
            _storage = storage;
        }

        /// <summary>
        /// Verifica si las credenciales del usuario son válidas.
        /// </summary>
        public bool ValidarCredenciales(string cedula, string contrasena)
        {
            var cliente = _storage.Clientes.FirstOrDefault(c => c.Cedula.ToString() == cedula);

            if (cliente == null)
                return false;

            // Aquí podrías usar hash en vez de texto plano si querés mayor seguridad
            return cliente.Password == contrasena;
        }

        /// <summary>
        /// Obtiene un cliente autenticado (para usar luego en sesión, tokens, etc).
        /// </summary>
        public Cliente? AutenticarCliente(string cedula, string contrasena)
        {
            return _storage.Clientes.FirstOrDefault(c =>
                c.Cedula.ToString() == cedula && c.Password == contrasena);
        }

        // Si implementás roles o JWT, podrías tener métodos como:
        // string GenerarToken(Cliente cliente)
    }
}
