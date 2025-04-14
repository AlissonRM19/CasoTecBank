using System;
using System.ComponentModel.DataAnnotations;

namespace TecBankApi.Models
{
    /// <summary>
    /// Modelo que representa un pago realizado a un préstamo.
    /// </summary>
    public class PagoPrestamo
    {
        /// <summary>
        /// Identificador único del pago.
        /// </summary>
        [Key]
        public int Id_pago { get; set; }

        /// <summary>
        /// Monto pagado en el abono.
        /// </summary>
        [Required(ErrorMessage = "El monto pagado es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El monto debe ser mayor a 0.")]
        public int Monto_pagado { get; set; }

        /// <summary>
        /// Fecha del pago.
        /// </summary>
        [Required(ErrorMessage = "La fecha del pago es obligatoria.")]
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Tipo de pago realizado (efectivo, transferencia, etc.).
        /// </summary>
        [Required(ErrorMessage = "El tipo de pago es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El tipo de pago no debe exceder los 50 caracteres.")]
        public string? Tipo_pago { get; set; }

        /// <summary>
        /// Identificador del préstamo asociado al pago.
        /// </summary>
        [Required(ErrorMessage = "Debe indicar el préstamo al que corresponde el pago.")]
        public int Id_prestamo { get; set; }
    }
}
