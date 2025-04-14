using System.ComponentModel.DataAnnotations;

namespace TecBankApi.Models
{
    /// <summary>
    /// Modelo que representa un préstamo solicitado por un cliente.
    /// </summary>
    public class Prestamo
    {
        /// <summary>
        /// Identificador único del préstamo.
        /// </summary>
        [Key]
        public int Id_Prestamo { get; set; }

        /// <summary>
        /// Monto original del préstamo.
        /// </summary>
        [Required(ErrorMessage = "El monto original es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "El monto debe ser mayor que 0")]
        public int Monto_Original { get; internal set; }

        /// <summary>
        /// Saldo pendiente del préstamo.
        /// </summary>
        [Required(ErrorMessage = "El saldo es requerido")]
        [Range(0, int.MaxValue, ErrorMessage = "El saldo no puede ser negativo")]
        public int Saldo { get; set; }

        /// <summary>
        /// Cédula del cliente que solicitó el préstamo.
        /// </summary>
        [Required(ErrorMessage = "La cédula del cliente es requerida")]
        public int Ced_Cliente { get; set; }

        /// <summary>
        /// Porcentaje de interés aplicado al préstamo.
        /// </summary>
        [Required(ErrorMessage = "El interés es requerido")]
        [Range(0, 100, ErrorMessage = "El interés debe estar entre 0 y 100")]
        public int Interes { get; set; }

        /// <summary>
        /// Cédula del asesor que gestionó el préstamo.
        /// </summary>
        [Required(ErrorMessage = "La cédula del asesor es requerida")]
        public int Ced_acesor { get; internal set; }
        public DateTime FechaAprobacion { get; internal set; }

        public required string Moneda { get; set; } // "CRC" o "USD"

    }
}
