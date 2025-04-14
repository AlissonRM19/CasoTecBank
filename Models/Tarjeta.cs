using System.ComponentModel.DataAnnotations;

namespace TecBankApi.Models
{
    /// <summary>
    /// Modelo que representa una tarjeta bancaria (crédito o débito).
    /// Cada tarjeta está asociada a un cliente y a una cuenta.
    /// </summary>
    public class Tarjeta
    {
        /// <summary>
        /// Número único de la tarjeta.
        /// </summary>
        [Key]
        public int N_Tarjeta { get; set; }

        /// <summary>
        /// Tipo de tarjeta: "Crédito" o "Débito".
        /// </summary>
        [Required(ErrorMessage = "Tipo de tarjeta requerido")]
        [RegularExpression("Crédito|Débito", ErrorMessage = "Tipos válidos: Crédito/Débito")]
        public string Tipo_Tarjeta { get; set; }

        /// <summary>
        /// Cédula del cliente dueño de la tarjeta.
        /// </summary>
        [Required(ErrorMessage = "Cédula del cliente requerida")]
        public int Ced_Cliente { get; set; }

        /// <summary>
        /// Fecha de vencimiento de la tarjeta.
        /// </summary>
        [Required(ErrorMessage = "Fecha de expiración requerida")]
        public DateTime Fecha_Expira { get; set; }

        /// <summary>
        /// Número de seguridad de la tarjeta (CVV).
        /// </summary>
        [Required(ErrorMessage = "Número de seguridad requerido")]
        [Range(100, 9999, ErrorMessage = "El número de seguridad debe tener entre 3 y 4 dígitos")]
        public int N_Seguridad { get; set; }

        /// <summary>
        /// Límite de crédito de la tarjeta (solo si es de crédito).
        /// </summary>
        public int? Credito { get; set; }

        /// <summary>
        /// Saldo actual de la tarjeta.
        /// </summary>
        public int? Saldo { get; set; }

        /// <summary>
        /// Número de cuenta asociada a la tarjeta.
        /// </summary>
        [Required(ErrorMessage = "Número de cuenta requerido")]
        public int N_Cuenta { get; set; }

        // Si querés manejar relaciones con otras entidades:
        public int Cuenta_aso { get; set; }

    }
}
