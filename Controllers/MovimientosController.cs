using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TecBankApi.Models;
using TecBankApi.Services;

namespace TecBankApi.Controllers
{
    /// <summary>
    /// Controlador que gestiona las operaciones relacionadas con los movimientos bancarios.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class MovimientosController : ControllerBase
    {
        // Dependencias de servicios inyectadas mediante el constructor.
        private readonly MovimientoService _movimientoService;
        private readonly CuentaService _cuentaService;
        private readonly ClienteService _clienteService;

        /// <summary>
        /// Constructor del controlador con inyección de dependencias.
        /// </summary>
        public MovimientosController(
            MovimientoService movimientoService,
            CuentaService cuentaService,
            ClienteService clienteService)
        {
            _movimientoService = movimientoService;
            _cuentaService = cuentaService;
            _clienteService = clienteService;
        }

        /// <summary>
        /// Obtiene los movimientos de una cuenta dentro de un rango de fechas específico.
        /// Solo accesible para clientes autenticados.
        /// </summary>
        /// <param name="cuentaId">Identificador de la cuenta</param>
        /// <param name="desde">Fecha de inicio del rango</param>
        /// <param name="hasta">Fecha de fin del rango</param>
        /// <returns>Lista de movimientos y resumen de totales</returns>
        [HttpGet("{cuentaId}")]
        [Authorize]
        public async Task<IActionResult> GetMovimientos(
            int cuentaId,
            [FromQuery] DateTime desde,
            [FromQuery] DateTime hasta)
        {
            // Obtiene el ID del cliente autenticado desde el token JWT
            var clienteId = int.Parse(User.FindFirst("ClienteId")?.Value);

            // Verifica que la cuenta consultada pertenezca al cliente autenticado
            if (!await _cuentaService.ValidarPropiedadCuenta(cuentaId, clienteId))
                return Forbid(); // Retorna 403 si el cliente no es dueño de la cuenta

            // Obtiene los movimientos en el rango especificado
            var movimientos = await _movimientoService.ObtenerMovimientos(cuentaId, desde, hasta);

            // Obtiene el saldo actual de la cuenta
            var saldoActual = await _cuentaService.ObtenerSaldo(cuentaId);

            // Retorna los movimientos junto con totales de depósitos, retiros y el saldo actual
            return Ok(new
            {
                success = true,
                data = movimientos,
                totales = new
                {
                    depositos = movimientos.Where(m => m.Tipo == "Depósito").Sum(m => m.Monto),
                    retiros = movimientos.Where(m => m.Tipo == "Retiro").Sum(m => m.Monto),
                    saldoActual
                }
            });
        }

        /// <summary>
        /// Registra un nuevo movimiento en la base de datos.
        /// Solo accesible para usuarios con rol "Admin" o "Sistema".
        /// </summary>
        /// <param name="movimiento">Datos del movimiento a registrar</param>
        /// <returns>Resultado del registro</returns>
        [HttpPost]
        [Authorize(Roles = "Admin,Sistema")]
        public async Task<IActionResult> RegistrarMovimiento([FromBody] Movimiento movimiento)
        {
            // Valida el modelo recibido (ej: campos requeridos, formatos, etc.)
            if (!ModelState.IsValid)
            {
                // Retorna errores de validación
                return BadRequest(new
                {
                    success = false,
                    errors = ModelState.Values.SelectMany(v => v.Errors)
                });
            }

            // Llama al servicio para registrar el movimiento
            var resultado = await _movimientoService.CrearMovimiento(movimiento);

            // Retorna éxito si se registró correctamente, o error si falló
            return resultado != null
                ? Ok(new { success = true, data = resultado })
                : BadRequest(new { success = false, message = "Error al registrar movimiento" });
        }
    }
}
