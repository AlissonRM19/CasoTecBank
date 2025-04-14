using TecBankApi.Models;

namespace TecBankApi.Data
{
    /// <summary>
    /// Clase contenedora principal para todos los datos del sistema.
    /// Se utiliza como estructura de almacenamiento para serialización y deserialización JSON.
    /// </summary>
    public class DatosTecBank
    {
        // Lista de clientes registrados en el sistema
        public List<Cliente> Clientes { get; set; } = new List<Cliente>();

        // Lista de cuentas bancarias asociadas a los clientes
        public List<Cuenta> Cuentas { get; set; } = new List<Cuenta>();

        // Lista de tarjetas de débito o crédito emitidas
        public List<Tarjeta> Tarjetas { get; set; } = new List<Tarjeta>();

        // Lista de préstamos solicitados por los clientes
        public List<Prestamo> Prestamos { get; set; } = new List<Prestamo>();

        // Lista de movimientos financieros realizados en cuentas o tarjetas
        public List<Movimiento> Movimientos { get; set; } = new List<Movimiento>();

        // Lista de asesores de crédito que gestionan préstamos y clientes
        public List<AsesorCredito> Asesores { get; set; } = new List<AsesorCredito>();
        public List<CompraTarjeta> ComprasTarjeta { get; set; } = new();
        public List<Transferencia> transferencias { get; set; } = new();


        // Lista de roles del sistema (Admin, Asesor, etc.)
        public List<Rol> Roles { get; set; } = new List<Rol>();

        // Tipo de cambio actual del dólar (USD) en colones
        public decimal TipoCambioUSD { get; set; } = 570;

        // Tipo de cambio actual del euro (EUR) en colones
        public decimal TipoCambioEUR { get; set; } = 620;
    }
}