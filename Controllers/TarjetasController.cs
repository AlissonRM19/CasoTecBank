using Microsoft.AspNetCore.Mvc;
using TecBankApi.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TecBankApi.Services;

namespace TecBankApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarjetaController : ControllerBase
    {
        private readonly TarjetaService _context;

        public TarjetaController(TarjetaService context)
        {
            _context = context;
        }

        // GET: api/Tarjeta
        [HttpGet]
        public IActionResult GetTarjetas()
        {
            var tarjetas = _context.Tarjetas.ToList();
            return Ok(tarjetas);
        }

        // GET: api/Tarjeta/5
        [HttpGet("{id}")]
        public IActionResult GetTarjeta(int id)
        {
            var tarjeta = _context.Tarjetas.Find(id);
            if (tarjeta == null)
            {
                return NotFound();
            }
            return Ok(tarjeta);
        }

        // POST: api/Tarjeta
        [HttpPost]
        public IActionResult PostTarjeta(Tarjeta tarjeta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Tarjetas.Add(tarjeta);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetTarjeta), new { id = tarjeta.N_Tarjeta }, tarjeta);
        }

        // PUT: api/Tarjeta/5
        [HttpPut("{id}")]
        public IActionResult PutTarjeta(int id, Tarjeta tarjeta)
        {
            if (id != tarjeta.N_Tarjeta)
            {
                return BadRequest();
            }

            _context.Entry(tarjeta).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/Tarjeta/5
        [HttpDelete("{id}")]
        public IActionResult DeleteTarjeta(int id)
        {
            var tarjeta = _context.Tarjetas.Find(id);
            if (tarjeta == null)
            {
                return NotFound();
            }

            _context.Tarjetas.Remove(tarjeta);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
