// Importa el espacio de nombres necesario para las validaciones de datos
using System.ComponentModel.DataAnnotations;

namespace TecBankApi.Models
{
    /// <summary>
    /// Modelo que representa una compra realizada con tarjeta
    /// </summary>
    public class CompraTarjeta
    {
        // Identificador único de la compra (clave primaria)
        [Key]
        public int CompraId { get; set; }

        // Número de la tarjeta utilizada para la compra
        // Se requiere y debe tener formato válido de tarjeta de crédito
        [Required(ErrorMessage = "Número de tarjeta requerido")]
        [CreditCard(ErrorMessage = "Número de tarjeta inválido")]
        public int NumeroTarjeta { get; set; }

        // Monto de la compra
        // Se requiere y debe ser al menos 0.01
        [Required(ErrorMessage = "Monto requerido")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Monto mínimo: 0.01")]
        public decimal Monto { get; set; }

        // Fecha en que se realizó la compra
        // Se requiere y se inicializa con la fecha y hora actual por defecto
        [Required(ErrorMessage = "Fecha requerida")]
        public DateTime Fecha { get; set; } = DateTime.Now;

        // Descripción de la compra
        // Se requiere y debe tener entre 3 y 100 caracteres
        [Required(ErrorMessage = "Descripción requerida")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "3-100 caracteres")]
        public string Descripcion { get; set; } = string.Empty;

        // Nombre del establecimiento donde se realizó la compra
        // Campo requerido
        [Required(ErrorMessage = "Establecimiento requerido")]
        public string Establecimiento { get; set; } = string.Empty;

        // Código de categoría MCC (Merchant Category Code)
        // Debe estar entre 0 y 9999
        [Range(0, 9999, ErrorMessage = "Código MCC inválido")]
        public int CodigoCategoria { get; set; }

        // Moneda utilizada para la compra
        // Campo requerido, con valor por defecto "CRC" (colones costarricenses)
        [Required]
        public string Moneda { get; set; } = "CRC";
    }
}
