using System.ComponentModel.DataAnnotations;

namespace TecBankApi.Models
{
    /// <summary>
    /// Modelo de cuenta bancaria con validaciones requeridas
    /// </summary>
    public class Cuenta
    {
        // Clave primaria que identifica de forma única la cuenta
        [Key]
        public int CuentaId { get; set; }

        // Campo obligatorio: número de cuenta, entre 10 y 20 caracteres
        [Required(ErrorMessage = "Número de cuenta requerido")]
        [StringLength(20, MinimumLength = 10, ErrorMessage = "Entre 10-20 caracteres")]
        public string NumeroCuenta { get; set; } = string.Empty;

        // Campo obligatorio: descripción de la cuenta (ej:, "Cuenta personal")
        [Required(ErrorMessage = "Descripción requerida")]
        public string Descripcion { get; set; } = string.Empty;

        // Campo obligatorio: moneda de la cuenta, debe ser una de las siguientes: Colones, Dólares o Euros
        [Required(ErrorMessage = "Moneda requerida")]
        [RegularExpression("Colones|Dólares|Euros", ErrorMessage = "Moneda inválida")]
        public string Moneda { get; set; } = "Colones"; // Valor predeterminado: Colones

        // Campo obligatorio: tipo de cuenta, debe ser "Ahorros" o "Corriente"
        [Required(ErrorMessage = "Tipo de cuenta requerido")]
        [RegularExpression("Ahorros|Corriente", ErrorMessage = "Tipo de cuenta inválido")]
        public string TipoCuenta { get; set; } = "Ahorros"; // Valor predeterminado: Ahorros

        // Campo obligatorio: ID del cliente al que pertenece la cuenta
        [Required(ErrorMessage = "Cliente asociado requerido")]
        public int ClienteId { get; set; }

        // Campo opcional (pero validado): saldo debe ser mayor o igual a 0
        [Range(0, double.MaxValue, ErrorMessage = "Saldo inválido")]
        public decimal Saldo { get; set; } = 0;
    }
}
