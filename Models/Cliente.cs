using System.ComponentModel.DataAnnotations;

namespace TecBankApi.Models
{
    /// <summary>
    /// Modelo de datos que representa un cliente del sistema TecBank.
    /// Incluye validaciones específicas para cédula, teléfono, nombre, etc.
    /// </summary>
    public class Cliente
    {
        /// <summary>
        /// Identificador único del cliente (clave primaria).
        /// </summary>
        [Key]
        public int ClienteId { get; set; }

        /// <summary>
        /// Nombre completo del cliente. Requiere entre 5 y 100 caracteres.
        /// </summary>
        [Required(ErrorMessage = "Nombre completo requerido")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Mínimo 5 caracteres")]
        public string NombreCompleto { get; set; } = string.Empty;

        /// <summary>
        /// Cédula costarricense sin guiones (formato: 9 dígitos, comenzando del 1 al 7).
        /// Ejemplo válido: 305780877
        /// </summary>
        [Required(ErrorMessage = "Cédula requerida")]
        [RegularExpression(@"^[1-7]\d{8}$", ErrorMessage = "Formato de cédula inválido. Debe tener 9 dígitos sin guiones y empezar con 1-7.")]
        public string Cedula { get; set; } = string.Empty;

        /// <summary>
        /// Dirección física del cliente.
        /// </summary>
        [Required(ErrorMessage = "Dirección requerida")]
        public string Direccion { get; set; } = string.Empty;

        /// <summary>
        /// Teléfono costarricense con exactamente 8 dígitos. 
        /// El prefijo +506 opcional.
        /// </summary>
        [Required(ErrorMessage = "Teléfono requerido")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "El teléfono debe tener exactamente 8 dígitos.")]
        // Alternativa aceptar el prefijo +506:
        // [RegularExpression(@"^(\+506)?\d{8}$", ErrorMessage = "Teléfono inválido. Debe tener 8 dígitos (opcional +506 al inicio).")]
        public string Telefono { get; set; } = string.Empty;

        /// <summary>
        /// Ingreso mensual del cliente en colones. Debe ser positivo.
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "Ingreso mensual inválido")]
        public decimal IngresoMensual { get; set; }

        /// <summary>
        /// Tipo de cliente: Físico o Jurídico. Solo se aceptan esos dos valores.
        /// </summary>
        [Required(ErrorMessage = "Tipo de cliente requerido")]
        [RegularExpression("Físico|Jurídico", ErrorMessage = "Valores permitidos: Físico/Jurídico")]
        public string TipoCliente { get; set; } = "Físico";

        /// <summary>
        /// Nombre de usuario para iniciar sesión. Requiere entre 5 y 20 caracteres.
        /// </summary>
        [Required(ErrorMessage = "Usuario requerido")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Entre 5-20 caracteres")]
        public string Usuario { get; set; } = string.Empty;

        /// <summary>
        /// Contraseña en texto plano (solo se utiliza temporalmente para registrar o actualizar).
        /// Será reemplazada por PasswordHash.
        /// </summary>
        [Required(ErrorMessage = "Contraseña requerida")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Contraseña encriptada (hash seguro). No debe ser expuesta al cliente.
        /// </summary>
        public string PasswordHash { get; set; } = string.Empty;
    }
}
