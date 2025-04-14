using System.ComponentModel.DataAnnotations;

namespace TecBankApi.Models
{
    /// <summary>
    /// Modelo que representa un reporte de mora de un cliente.
    /// </summary>
    public class ReporteMora
    {
        /// <summary>
        /// Identificador único del reporte de mora.
        /// </summary>
        [Key]
        public int ID_ReporteMora { get; set; }

        /// <summary>
        /// Cédula del cliente asociada al reporte (valor 1).
        /// </summary>
        [Required(ErrorMessage = "Campo ced_Cliente requerido")]
        public int Ced_Cliente { get; set; }

        /// <summary>
        /// Cédula del cliente asociada al reporte (valor 2, si corresponde).
        /// </summary>
        public int Cedula_Cliente { get; set; }

        /// <summary>
        /// Número del préstamo que tiene cuotas vencidas.
        /// </summary>
        [Required(ErrorMessage = "Número de préstamo requerido")]
        public int Numero_Prestamo { get; set; }

        /// <summary>
        /// Cantidad de cuotas vencidas en el préstamo.
        /// </summary>
        [Required(ErrorMessage = "Cantidad de cuotas vencidas requerida")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe haber al menos una cuota vencida")]
        public int Cuotas_Vencidas { get; set; }

        /// <summary>
        /// Monto que se adeuda por el préstamo vencido.
        /// </summary>
        [Required(ErrorMessage = "Monto adeudado requerido")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto adeudado debe ser mayor a 0")]
        public int Monto_Adecuado { get; set; }
    }
}
