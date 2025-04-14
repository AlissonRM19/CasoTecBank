using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using TecBankApi.Models;
using TecBankApi.Services;

namespace TecBankApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarjetaController : ControllerBase
    {
        private readonly TarjetaService _tarjetaService;

        public TarjetaController(TarjetaService tarjetaService)
        {
            _tarjetaService = tarjetaService;
        }

        // GET: api/Tarjeta
        [HttpGet]
        public async Task<IActionResult> GetTarjetas()
        {
            var tarjetas = await _tarjetaService.ObtenerTodasTarjetas();
            return Ok(tarjetas);
        }

        // GET: api/Tarjeta/cliente/5
        [HttpGet("cliente/{clienteId}")]
        public async Task<IActionResult> GetTarjetasPorCliente(int clienteId)
        {
            var tarjetas = await _tarjetaService.ObtenerTarjetasPorCliente(clienteId);
            return Ok(tarjetas);
        }

        // GET: api/Tarjeta/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTarjeta(int id)
        {
            var tarjetas = await _tarjetaService.ObtenerTodasTarjetas();
            var tarjeta = tarjetas.FirstOrDefault(t => t.N_Tarjeta == id);
            if (tarjeta == null)
                return NotFound();
            return Ok(tarjeta);
        }

        // POST: api/Tarjeta
        [HttpPost]
        public async Task<IActionResult> PostTarjeta([FromBody] Tarjeta tarjeta)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var nuevaTarjeta = await _tarjetaService.CrearTarjeta(tarjeta);
                if (nuevaTarjeta == null)
                    return Conflict("El número de tarjeta ya existe.");

                return CreatedAtAction(nameof(GetTarjeta), new { id = nuevaTarjeta.N_Tarjeta }, nuevaTarjeta);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // DELETE: api/Tarjeta/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarjeta(int id)
        {
            var eliminado = await _tarjetaService.EliminarTarjeta(id);
            if (!eliminado)
                return NotFound("No se pudo eliminar la tarjeta. Asegúrese de que no haya sido usada.");

            return NoContent();
        }
    }
}

