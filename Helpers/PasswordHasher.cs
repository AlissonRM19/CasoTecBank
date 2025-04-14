using System.Security.Cryptography;
using System.Text;

namespace TecBankApi.Utils
{
    public static class PasswordHasher
    {
        /// <summary>
        /// Genera un hash SHA256 de la contraseña en texto plano.
        /// </summary>
        public static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hashBytes = sha256.ComputeHash(bytes);
            return Convert.ToHexString(hashBytes); // Devuelve en hexadecimal (mayúsculas)
        }

        /// <summary>
        /// Verifica si una contraseña coincide con su hash almacenado.
        /// </summary>
        public static bool VerificarPassword(string password, string passwordHash)
        {
            var hashIngresado = HashPassword(password);
            return string.Equals(hashIngresado, passwordHash, StringComparison.OrdinalIgnoreCase);
        }
    }
}
