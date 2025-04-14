using Microsoft.AspNetCore.Mvc;
using TecBankApi.Models;
using TecBankApi.Services;

namespace TecBankApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportesController : ControllerBase
    {
        private readonly ReporteService _reporteService;

        public ReportesController(ReporteService reporteService)
        {
            _reporteService = reporteService;
        }

        #region Reportes de Mora

        [HttpGet("mora")]
        public ActionResult<IEnumerable<ReporteMora>> GetReporteMora()
        {
            var reportes = _reporteService.ObtenerReportesMora();
            return Ok(reportes);
        }

        [HttpPost("mora")]
        public ActionResult CrearReporteMora([FromBody] ReporteMora reporte)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _reporteService.AgregarReporteMora(reporte);
            return CreatedAtAction(nameof(GetReporteMora), new { id = reporte.ID_ReporteMora }, reporte);
        }

        #endregion

        #region Reportes de Asesores

        [HttpGet("asesores")]
        public ActionResult<IEnumerable<ReporteAsesoresCredito>> GetReporteAsesores()
        {
            var reportes = _reporteService.ObtenerReportesAsesores();
            return Ok(reportes);
        }

        [HttpPost("asesores")]
        public ActionResult CrearReporteAsesores([FromBody] ReporteAsesoresCredito reporte)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _reporteService.AgregarReporteAsesor(reporte);
            return CreatedAtAction(nameof(GetReporteAsesores), new { id = reporte.Id }, reporte);
        }

        #endregion
    }
}
