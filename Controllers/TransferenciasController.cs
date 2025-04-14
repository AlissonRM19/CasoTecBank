using Microsoft.AspNetCore.Mvc;
using TecBankApi.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TecBankApi.Services;

namespace TecBankApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferenciaController : ControllerBase
    {
        private readonly TransferenciaService _context;

        public TransferenciaController(TransferenciaService context)
        {
            _context = context;
        }

        // GET: api/Transferencia
        [HttpGet]
        public IActionResult GetTransferencias()
        {
            var transferencias = _context.ObtenerTodas();
            return Ok(transferencias);
        }

        // GET: api/Transferencia/5
        [HttpGet("{id}")]
        public IActionResult GetTransferencia(int id)
        {
            var transferencia = _context.ObtenerPorId(id);
            if (transferencia == null)
            {
                return NotFound();
            }
            return Ok(transferencia);
        }

        // POST: api/Transferencia
        [HttpPost]
        public IActionResult PostTransferencia(Transferencia transferencia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.AgregarTransferencia(transferencia);

            return CreatedAtAction(nameof(GetTransferencia), new { id = transferencia.ID_Transferencia }, transferencia);
        }

        // PUT: api/Transferencia/5
        [HttpPut("{id}")]
        public IActionResult PutTransferencia(int id, Transferencia transferencia)
        {
            if (id != transferencia.ID_Transferencia)
            {
                return BadRequest();
            }

            //_context.Entry(transferencia).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            return NoContent();
        }

        // DELETE: api/Transferencia/5
        [HttpDelete("{id}")]
        public IActionResult DeleteTransferencia(int id)
        {
            var transferencia = _context.ObtenerPorId(id);
            if (transferencia == null)
            {
                return NotFound();
            }

            _context.EliminarTransferencia(id);

            return NoContent();
        }
    }
}
