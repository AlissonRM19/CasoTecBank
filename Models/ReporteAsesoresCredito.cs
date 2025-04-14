using System.ComponentModel.DataAnnotations;

namespace TecBankApi.Models
{
    /// <summary>
    /// Modelo que representa el reporte de desempeño de un asesor de crédito.
    /// </summary>
    public class ReporteAsesoresCredito
    {
        /// <summary>
        /// Identificador único del reporte.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Cédula del asesor de crédito.
        /// </summary>
        [Required(ErrorMessage = "Cédula del asesor requerida")]
        public int Asesor { get; set; }

        /// <summary>
        /// Meta de créditos colocados en colones.
        /// </summary>
        [Required(ErrorMessage = "Meta en colones requerida")]
        [Range(0, int.MaxValue, ErrorMessage = "La meta debe ser positiva")]
        public int Meta_Colones { get; set; }

        /// <summary>
        /// Meta de créditos colocados en dólares.
        /// </summary>
        [Required(ErrorMessage = "Meta en dólares requerida")]
        [Range(0, int.MaxValue, ErrorMessage = "La meta debe ser positiva")]
        public int Meta_Dolares { get; set; }

        /// <summary>
        /// Total colocado en colones.
        /// </summary>
        [Required(ErrorMessage = "Total en colones requerido")]
        [Range(0, int.MaxValue, ErrorMessage = "El total debe ser positivo")]
        public int Total_Colones { get; set; }

        /// <summary>
        /// Total colocado en dólares.
        /// </summary>
        [Required(ErrorMessage = "Total en dólares requerido")]
        [Range(0, int.MaxValue, ErrorMessage = "El total debe ser positivo")]
        public int Total_Dolares { get; set; }

        /// <summary>
        /// Comisión ganada en colones.
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "La comisión debe ser positiva")]
        public int Comision_Colones { get; set; }

        /// <summary>
        /// Comisión ganada en dólares.
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "La comisión debe ser positiva")]
        public int Comision_Dolares { get; set; }
    }
}
