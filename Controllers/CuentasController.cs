using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TecBankApi.Models;
using TecBankApi.Services;

namespace TecBankApi.Controllers
{
    // Define el controlador de API y la ruta base: api/cuentas
    [ApiController]
    [Route("api/[controller]")]
    public class CuentasController : ControllerBase
    {
        private readonly CuentaService _cuentaService;      // Servicio para lógica de negocio relacionada con cuentas
        private readonly ClienteService _clienteService;    // Servicio para verificar existencia de clientes

        // Constructor con inyección de dependencias
        public CuentasController(CuentaService cuentaService, ClienteService clienteService)
        {
            _cuentaService = cuentaService;
            _clienteService = clienteService;
        }

        /// <summary>
        /// Obtener todas las cuentas (solo para administradores)
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var cuentas = await _cuentaService.ObtenerTodasCuentas();  // Obtener todas las cuentas registradas
            return Ok(new { success = true, data = cuentas });         // Retornar con éxito y lista de cuentas
        }

        /// <summary>
        /// Obtener cuentas del cliente autenticado
        /// </summary>
        [HttpGet("mis-cuentas")]
        [Authorize]
        public async Task<IActionResult> GetByCliente()
        {
            // Obtener el ID del cliente a partir del token JWT
            var clienteId = int.Parse(User.FindFirst("ClienteId")?.Value);

            var cuentas = await _cuentaService.ObtenerCuentasPorCliente(clienteId); // Obtener cuentas del cliente
            return Ok(new { success = true, data = cuentas });
        }

        /// <summary>
        /// Crear nueva cuenta (solo para administradores)
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] Cuenta cuenta)
        {
            // Verifica si el modelo cumple las validaciones
            if (!ModelState.IsValid)
                return BadRequest(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors) });

            // Verifica si el cliente asociado a la cuenta existe
            if (!await _clienteService.ClienteExiste(cuenta.ClienteId))
                return BadRequest(new { success = false, message = "Cliente no existe" });

            var resultado = await _cuentaService.CrearCuenta(cuenta);

            // Si se creó exitosamente, devuelve 201 Created con ruta a acción de consulta
            return resultado
                ? CreatedAtAction(nameof(GetByCliente), new { clienteId = cuenta.ClienteId }, new { success = true })
                : BadRequest(new { success = false, message = "Error: Número de cuenta ya existe" });
        }

        /// <summary>
        /// Realizar depósito en una cuenta
        /// </summary>
        [HttpPost("{id}/deposito")]
        [Authorize]
        public async Task<IActionResult> Depositar(int id, [FromBody] decimal monto)
        {
            // Verifica que el monto sea positivo
            if (monto <= 0)
                return BadRequest(new { success = false, message = "Monto inválido" });

            // Intenta realizar el depósito usando el servicio
            var resultado = await _cuentaService.RealizarOperacion(id, monto, "Deposito");

            // Si se encuentra la cuenta, retornar el nuevo saldo
            return (resultado!=null)
                ? Ok(new { success = true, nuevoSaldo = resultado.Value })
                : NotFound(new { success = false, message = "Cuenta no encontrada" });
        }

        /// <summary>
        /// Realizar retiro de una cuenta
        /// </summary>
        [HttpPost("{id}/retiro")]
        [Authorize]
        public async Task<IActionResult> Retirar(int id, [FromBody] decimal monto)
        {
            // Validar que el monto sea positivo
            if (monto <= 0)
                return BadRequest(new { success = false, message = "Monto inválido" });

            // Realiza el retiro enviando el monto negativo
            var resultado = await _cuentaService.RealizarOperacion(id, -monto, "Retiro");

            // Si resultado tiene valor, el retiro fue exitoso
            return resultado.HasValue
                ? Ok(new { success = true, nuevoSaldo = resultado.Value })
                : NotFound(new { success = false, message = resultado == null ? "Cuenta no encontrada" : "Fondos insuficientes" });
        }

        /// <summary>
        /// Eliminar una cuenta existente (solo para administradores)
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            // Intenta eliminar la cuenta por ID
            var resultado = await _cuentaService.EliminarCuenta(id);
            return resultado
                ? NoContent()  // Código 204 si se eliminó correctamente
                : NotFound(new { success = false, message = "Cuenta no encontrada" });
        }
    }
}
