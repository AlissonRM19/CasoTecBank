
/// Funcionalidad para reportes, necesaria para Asesores 
/// (FALTA EL RESTO DEL CODIGO PrestamoService.cs)
public class PrestamoService
{
    public async Task<ReporteComisiones> GenerarReporteComisiones(int asesorId, int mes, int año)
    {
        var prestamos = _prestamos
            .Where(p => p.AsesorId == asesorId &&
                       p.FechaAprobacion.Month == mes &&
                       p.FechaAprobacion.Year == año)
            .ToList();

        return new ReporteComisiones
        {
            TotalColones = prestamos.Where(p => p.Moneda == "CRC").Sum(p => p.Monto),
            TotalDolares = prestamos.Where(p => p.Moneda == "USD").Sum(p => p.Monto),
            ComisionesColones = prestamos.Where(p => p.Moneda == "CRC").Sum(p => p.Monto * 0.03m),
            ComisionesDolares = prestamos.Where(p => p.Moneda == "USD").Sum(p => p.Monto * 0.03m)
        };
    }

    public class ReporteComisiones
    {
        public decimal TotalColones { get; set; }
        public decimal TotalDolares { get; set; }
        public decimal ComisionesColones { get; set; }
        public decimal ComisionesDolares { get; set; }
    }
}