using System.ComponentModel.DataAnnotations;

namespace TecBankApi.Models
{
    /// <summary>
    /// Modelo que representa una transferencia bancaria entre dos cuentas.
    /// </summary>
    public class Transferencia
    {
        /// <summary>
        /// Identificador único de la transferencia.
        /// </summary>
        [Key]
        public int ID_Transferencia { get; set; }

        /// <summary>
        /// Número de cuenta destino de la transferencia.
        /// </summary>
        [Required(ErrorMessage = "Cuenta destino requerida")]
        public int Cuenta_Destino { get; set; }

        /// <summary>
        /// Número de cuenta origen de la transferencia.
        /// </summary>
        [Required(ErrorMessage = "Cuenta origen requerida")]
        public int Cuenta_Origen { get; set; }

        /// <summary>
        /// Monto de la transferencia.
        /// Debe ser mayor a 0.01.
        /// </summary>
        [Required(ErrorMessage = "Monto requerido")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Monto mínimo: 0.01")]
        public decimal Monto { get; set; }

        /// <summary>
        /// Fecha en que se realizó la transferencia.
        /// Valor por defecto: fecha y hora actual.
        /// </summary>
        [Required(ErrorMessage = "Fecha requerida")]
        public DateTime Fecha { get; set; } = DateTime.Now;

        // Opcionalmente podrías incluir relaciones a entidades Cuenta si las tenés
        // public Cuenta CuentaOrigen { get; set; }
        // public Cuenta CuentaDestino { get; set; }
    }
}
