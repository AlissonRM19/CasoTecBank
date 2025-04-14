using Microsoft.AspNetCore.Mvc;
using TecBankApi.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TecBankApi.Services;

namespace TecBankApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamoController : ControllerBase
    {
        private readonly PrestamoService _context;

        public PrestamoController(PrestamoService context)
        {
            _context = context;
        }

        // GET: api/Prestamo
        [HttpGet]
        public IActionResult GetPrestamos()
        {
            var prestamos = _context.Prestamos.ToList();
            return Ok(prestamos);
        }

        // GET: api/Prestamo/5
        [HttpGet("{id}")]
        public IActionResult GetPrestamo(int id)
        {
            var prestamo = _context.ObtenerPorId(id);

            if (prestamo == null)
            {
                return NotFound();
            }
            return Ok(prestamo);
        }

        // POST: api/Prestamo
        [HttpPost]
        public IActionResult PostPrestamo(Prestamo prestamo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Prestamos.Add(prestamo);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetPrestamo), new { id = prestamo.Id_Prestamo }, prestamo);
        }

        // PUT: api/Prestamo/5
        [HttpPut("{id}")]
        public IActionResult PutPrestamo(int id, Prestamo prestamo)
        {
            if (id != prestamo.Id_Prestamo)
            {
                return BadRequest();
            }

            _context.Entry(prestamo).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/Prestamo/5
        [HttpDelete("{id}")]
        public IActionResult DeletePrestamo(int id)
        {
            var prestamo = _context.ObtenerPorId(id);
            if (prestamo == null)
            {
                return NotFound();
            }

            _context.Prestamos.Remove(prestamo);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
