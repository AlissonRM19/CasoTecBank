using System.ComponentModel.DataAnnotations;

namespace TecBankApi.Models
{
    /// <summary>
    /// Modelo que representa un movimiento bancario (depósito o retiro).
    /// Cada instancia refleja una transacción asociada a una cuenta.
    /// </summary>
    public class Movimiento
    {
        /// <summary>
        /// Identificador único del movimiento.
        /// </summary>
        [Key]
        public int MovimientoId { get; set; }

        /// <summary>
        /// Tipo de movimiento: "Depósito" o "Retiro".
        /// Valor por defecto: "Depósito".
        /// </summary>
        [Required(ErrorMessage = "Tipo de movimiento requerido")]
        [RegularExpression("Depósito|Retiro", ErrorMessage = "Tipos válidos: Depósito/Retiro")]
        public string Tipo { get; set; } = "Depósito";

        /// <summary>
        /// Monto del movimiento.
        /// Debe ser mayor a 0.01.
        /// </summary>
        [Required(ErrorMessage = "Monto requerido")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Monto mínimo: 0.01")]
        public decimal Monto { get; set; }

        /// <summary>
        /// Fecha en la que se realiza el movimiento.
        /// Valor por defecto: fecha y hora actual.
        /// </summary>
        [Required(ErrorMessage = "Fecha requerida")]
        public DateTime Fecha { get; set; } = DateTime.Now;

        /// <summary>
        /// Descripción opcional del movimiento (máximo 100 caracteres).
        /// </summary>
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string Descripcion { get; set; } = string.Empty;

        /// <summary>
        /// Identificador de la cuenta asociada al movimiento.
        /// </summary>
        [Required(ErrorMessage = "Cuenta asociada requerida")]
        public int CuentaId { get; set; }

        /// <summary>
        /// Moneda del movimiento. Solo se permiten: CRC, USD o EUR.
        /// Valor por defecto: CRC.
        /// </summary>
        [Required(ErrorMessage = "Moneda requerida")]
        [RegularExpression("CRC|USD|EUR", ErrorMessage = "Moneda inválida")]
        public string Moneda { get; set; } = "CRC";
    }
}
