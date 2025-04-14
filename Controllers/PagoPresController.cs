using Microsoft.AspNetCore.Mvc;
using TecBankApi.Models;
using TecBankApi.Services;

namespace TecBankApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagoPresController : ControllerBase
    {
        private readonly PagoPrestamoService _pagoService;

        public PagoPresController(PagoPrestamoService pagoService)
        {
            _pagoService = pagoService;
        }

        // GET: api/PagoPres
        [HttpGet]
        public ActionResult<IEnumerable<PagoPrestamo>> GetAllPagos()
        {
            var pagos = _pagoService.GetAll();
            return Ok(pagos);
        }

        // GET: api/PagoPres/5
        [HttpGet("{id}")]
        public ActionResult<PagoPrestamo> GetPago(int id)
        {
            var pago = _pagoService.GetById(id);
            if (pago == null)
            {
                return NotFound();
            }
            return Ok(pago);
        }

        // POST: api/PagoPres
        [HttpPost]
        public ActionResult<PagoPrestamo> CreatePago([FromBody] PagoPrestamo nuevoPago)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _pagoService.Create(nuevoPago);
            return CreatedAtAction(nameof(GetPago), new { id = nuevoPago.Id_pago }, nuevoPago);
        }

        // PUT: api/PagoPres/5
        [HttpPut("{id}")]
        public IActionResult UpdatePago(int id, [FromBody] PagoPrestamo pagoActualizado)
        {
            if (id != pagoActualizado.Id_pago)
            {
                return BadRequest("El ID no coincide.");
            }

            var existente = _pagoService.GetById(id);
            if (existente == null)
            {
                return NotFound();
            }

            _pagoService.Update(pagoActualizado);
            return NoContent();
        }

        // DELETE: api/PagoPres/5
        [HttpDelete("{id}")]
        public IActionResult DeletePago(int id)
        {
            var existente = _pagoService.GetById(id);
            if (existente == null)
            {
                return NotFound();
            }

            _pagoService.Delete(id);
            return NoContent();
        }
    }
}

