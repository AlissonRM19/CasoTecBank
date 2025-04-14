using Microsoft.AspNetCore.Mvc;
using TecBankApi.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TecBankApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferenciaController : ControllerBase
    {
        private readonly DbContext _context;

        public TransferenciaController(DbContext context)
        {
            _context = context;
        }

        // GET: api/Transferencia
        [HttpGet]
        public IActionResult GetTransferencias()
        {
            var transferencias = _context.Transferencias.ToList();
            return Ok(transferencias);
        }

        // GET: api/Transferencia/5
        [HttpGet("{id}")]
        public IActionResult GetTransferencia(int id)
        {
            var transferencia = _context.Transferencias.Find(id);
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

            _context.Transferencias.Add(transferencia);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetTransferencia), new { id = transferencia.ID_transferencia }, transferencia);
        }

        // PUT: api/Transferencia/5
        [HttpPut("{id}")]
        public IActionResult PutTransferencia(int id, Transferencia transferencia)
        {
            if (id != transferencia.ID_transferencia)
            {
                return BadRequest();
            }

            _context.Entry(transferencia).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/Transferencia/5
        [HttpDelete("{id}")]
        public IActionResult DeleteTransferencia(int id)
        {
            var transferencia = _context.Transferencias.Find(id);
            if (transferencia == null)
            {
                return NotFound();
            }

            _context.Transferencias.Remove(transferencia);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
