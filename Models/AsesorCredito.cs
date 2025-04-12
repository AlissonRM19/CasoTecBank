using System.ComponentModel.DataAnnotations;

namespace TecBankApi.Models
{
    /// <summary>
    /// Modelo de asesor de crédito con validaciones requeridas.
    /// Este modelo representa a un asesor y almacena su información personal y metas/comisiones de ventas.
    /// </summary>
    public class AsesorCredito
    {
        /// <summary>
        /// Identificador único del asesor (clave primaria).
        /// </summary>
        [Key]
        public int AsesorId { get; set; }

        /// <summary>
        /// Nombre completo del asesor.
        /// Campo requerido con longitud mínima de 5 y máxima de 100 caracteres.
        /// </summary>
        [Required(ErrorMessage = "Nombre completo requerido")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Mínimo 5 caracteres")]
        public string NombreCompleto { get; set; } = string.Empty;

        /// <summary>
        /// Cédula del asesor en formato nacional (ejemplo: 1-2345-6789).
        /// Campo requerido con validación mediante expresión regular.
        /// </summary>
        [Required(ErrorMessage = "Cédula requerida")]
        [RegularExpression(@"^[1-7]\d{8}$", ErrorMessage = "Formato de cédula inválido. Debe tener 9 dígitos sin guiones y empezar con 1-7.")]
        public string Cedula { get; set; } = string.Empty;

        /// <summary>
        /// Fecha de nacimiento del asesor.
        /// Campo requerido y validado como fecha.
        /// </summary>
        [Required(ErrorMessage = "Fecha de nacimiento requerida")]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }

        /// <summary>
        /// Meta de ventas del asesor en colones.
        /// Valor no puede ser negativo.
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "Meta en colones inválida")]
        public decimal MetaVentasColones { get; set; }

        /// <summary>
        /// Meta de ventas del asesor en dólares.
        /// Valor no puede ser negativo.
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "Meta en dólares inválida")]
        public decimal MetaVentasDolares { get; set; }

        /// <summary>
        /// Comisiones acumuladas en colones.
        /// Valor no puede ser negativo.
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "Comisiones inválidas")]
        public decimal ComisionesColones { get; set; }

        /// <summary>
        /// Comisiones acumuladas en dólares.
        /// Valor no puede ser negativo.
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "Comisiones inválidas")]
        public decimal ComisionesDolares { get; set; }
    }
}
