using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TecBankApi.Models;
using TecBankApi.Services;

namespace TecBankApi.Controllers
{
    /// <summary>
    /// Controlador para manejar operaciones relacionadas con los asesores de crédito.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AsesoresController : ControllerBase
    {
        // Dependencias necesarias para las operaciones con asesores y préstamos
        private readonly AsesorService _asesorService;
        private readonly PrestamoService _prestamoService;

        /// <summary>
        /// Constructor que recibe los servicios necesarios mediante inyección de dependencias.
        /// </summary>
        public AsesoresController(AsesorService asesorService, PrestamoService prestamoService)
        {
            _asesorService = asesorService;
            _prestamoService = prestamoService;
        }

        /// <summary>
        /// Obtener todos los asesores (solo rol Admin).
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var asesores = await _asesorService.ObtenerTodosAsesores();
            return Ok(new { success = true, data = asesores });
        }

        /// <summary>
        /// Crear un nuevo asesor de crédito (solo rol Admin).
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] AsesorCredito asesor)
        {
            // Verifica que el modelo sea válido
            if (!ModelState.IsValid)
                return BadRequest(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors) });

            // Intenta crear el asesor
            var resultado = await _asesorService.CrearAsesor(asesor);

            // Devuelve resultado de acuerdo a si la creación fue exitosa o si hubo error (como cédula duplicada)
            return resultado != null
                ? CreatedAtAction(nameof(GetById), new { id = resultado.AsesorId }, new { success = true })
                : BadRequest(new { success = false, message = "Error: Cédula ya registrada" });
        }

        /// <summary>
        /// Generar reporte de comisiones por asesor, mes y año (Admin y Asesor).
        /// </summary>
        [HttpGet("{id}/reporte-comisiones")]
        [Authorize(Roles = "Admin,Asesor")]
        public async Task<IActionResult> GenerarReporteComisiones(
            int id,
            [FromQuery] int mes,
            [FromQuery] int año)
        {
            var reporte = await _prestamoService.GenerarReporteComisiones(id, mes, año);
            return Ok(new
            {
                success = true,
                data = new
                {
                    reporte.TotalColones,
                    reporte.TotalDolares,
                    reporte.ComisionesColones,
                    reporte.ComisionesDolares
                }
            });
        }

        /// <summary>
        /// Actualiza las metas de ventas del asesor (solo Admin).
        /// </summary>
        [HttpPut("{id}/metas")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ActualizarMetas(int id, [FromBody] MetasVentas metas)
        {
            var resultado = await _asesorService.ActualizarMetas(id, metas.MetaColones, metas.MetaDolares);

            return resultado
                ? NoContent()
                : NotFound(new { success = false, message = "Asesor no encontrado" });
        }

        /// <summary>
        /// Clase interna que representa el cuerpo esperado al actualizar metas de ventas.
        /// </summary>
        public class MetasVentas
        {
            public decimal MetaColones { get; set; }
            public decimal MetaDolares { get; set; }
        }
    }
}
