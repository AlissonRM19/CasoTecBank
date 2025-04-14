using TecBankApi.Models;

namespace TecBankApi.Services
{
    public class ReporteService
    {
        private static List<ReporteMora> _reportesMora = new();
        private static List<ReporteAsesoresCredito> _reportesAsesores = new();

        // Reportes de Mora
        public List<ReporteMora> ObtenerReportesMora() => _reportesMora;

        public void AgregarReporteMora(ReporteMora reporte)
        {
            reporte.ID_ReporteMora = _reportesMora.Count + 1;
            _reportesMora.Add(reporte);
        }

        // Reportes de Asesores
        public List<ReporteAsesoresCredito> ObtenerReportesAsesores() => _reportesAsesores;

        public void AgregarReporteAsesor(ReporteAsesoresCredito reporte)
        {
            reporte.Id = _reportesAsesores.Count + 1;
            _reportesAsesores.Add(reporte);
        }
    }
}
