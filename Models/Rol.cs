using System.ComponentModel.DataAnnotations;

namespace TecBankApi.Models
{
    /// <summary>
    /// Modelo que representa un rol de usuario dentro del sistema.
    /// </summary>
    public class Rol
    {
        /// <summary>
        /// Nombre del rol. Es el identificador principal.
        /// </summary>
        [Key]
        [Required(ErrorMessage = "Nombre del rol requerido")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string Nombre { get; set; }

        /// <summary>
        /// Descripción del rol.
        /// </summary>
        [StringLength(200, ErrorMessage = "Máximo 200 caracteres")]
        public string Descripcion { get; set; } = string.Empty;
    }
}
