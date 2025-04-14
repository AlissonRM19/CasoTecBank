// Importa los espacios de nombres necesarios para MVC, autorización y los modelos y servicios usados
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TecBankApi.Models;
using TecBankApi.Services;

namespace TecBankApi.Controllers
{
    // Indica que esta clase es un controlador de API
    [ApiController]
    // Define la ruta base para todas las acciones de este controlador: "api/compras"
    [Route("api/[controller]")]
    public class ComprasController : ControllerBase
    {
        // Servicios inyectados para manejar compras y validación de tarjetas
        private readonly CompraService _compraService;
        private readonly TarjetaService _tarjetaService;

        // Constructor con inyección de dependencias
        public ComprasController(CompraService compraService, TarjetaService tarjetaService)
        {
            _compraService = compraService;
            _tarjetaService = tarjetaService;
        }

        /// <summary>
        /// Registrar nueva compra con tarjeta
        /// </summary>
        [HttpPost] // Ruta POST: api/compras
        [Authorize] // Requiere autenticación
        public async Task<IActionResult> RegistrarCompra([FromBody] CompraTarjeta compra)
        {
            // Verifica si el modelo recibido es válido según las anotaciones de validación
            if (!ModelState.IsValid)
                return BadRequest(new
                {
                    success = false,
                    errors = ModelState.Values.SelectMany(v => v.Errors)
                });

            // Verifica si la tarjeta existe en el sistema
            if (!await _tarjetaService.TarjetaExiste(compra.NumeroTarjeta))
                return BadRequest(new
                {
                    success = false,
                    message = "Tarjeta no registrada"
                });

            // Intenta registrar la compra
            var resultado = await _compraService.CrearCompra(compra);

            // Devuelve respuesta adecuada según el resultado
            return resultado != null
                ? Ok(new { success = true, data = resultado })
                : BadRequest(new { success = false, message = "Error al registrar compra" });
        }

        /// <summary>
        /// Obtener compras por tarjeta y rango de fechas
        /// </summary>
        [HttpGet("{numeroTarjeta}")] // Ruta GET: api/compras/{numeroTarjeta}?desde=...&hasta=...
        [Authorize] // Requiere autenticación
        public async Task<IActionResult> ObtenerCompras(
            int numeroTarjeta,
            [FromQuery] DateTime desde,
            [FromQuery] DateTime hasta)
        {
            // Extrae el ID del cliente autenticado desde el token JWT
            var clienteId = int.Parse(User.FindFirst("ClienteId")?.Value);

            // Verifica que la tarjeta pertenece al cliente autenticado
            if (!await _tarjetaService.ValidarPropietarioTarjeta(numeroTarjeta, clienteId))
                return Forbid(); // Retorna 403 si el cliente no es dueño de la tarjeta

            // Obtiene las compras hechas con esa tarjeta entre las fechas especificadas
            var compras = await _compraService.ObtenerComprasPorTarjeta(numeroTarjeta, desde, hasta);

            // Retorna las compras junto con el monto total de esas compras
            return Ok(new
            {
                success = true,
                data = compras,
                total = compras.Sum(c => c.Monto)
            });
        }
    }
}
