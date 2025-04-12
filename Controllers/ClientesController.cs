using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TecBankApi.Models;
using TecBankApi.Services;

namespace TecBankApi.Controllers
{
    // Marca esta clase como un controlador de API
    [ApiController]
    // Define la ruta base del controlador: api/clientes
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        // Dependencia del servicio de cliente, utilizado para acceder a la lógica de negocio
        private readonly ClienteService _clienteService;

        // Constructor que inyecta el servicio de cliente
        public ClientesController(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        /// <summary>
        /// Obtener todos los clientes (solo accesible por usuarios con rol Admin)
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin")] // Solo los administradores pueden acceder a esta ruta
        public async Task<IActionResult> GetAll()
        {
            // Llama al servicio para obtener todos los clientes
            var clientes = await _clienteService.ObtenerTodosClientes();

            // Devuelve una respuesta 200 OK con la lista de clientes
            return Ok(new { success = true, data = clientes });
        }

        /// <summary>
        /// Obtener cliente por su ID
        /// </summary>
        [HttpGet("{id}")] // Ruta: api/clientes/{id}
        [Authorize] // Requiere autenticación
        public async Task<IActionResult> GetById(int id)
        {
            // Busca el cliente por ID usando el servicio
            var cliente = await _clienteService.ObtenerClientePorId(id);

            // Si se encuentra, se retorna con 200 OK, si no, 404 NotFound
            return cliente != null
                ? Ok(new { success = true, data = cliente })
                : NotFound(new { success = false, message = "Cliente no encontrado" });
        }

        /// <summary>
        /// Crear un nuevo cliente (público, no requiere autenticación)
        /// </summary>
        [HttpPost]
        [AllowAnonymous] // Cualquier persona puede hacer esta petición
        public async Task<IActionResult> Create([FromBody] Cliente cliente)
        {
            // Verifica si el modelo recibido es válido
            if (!ModelState.IsValid)
                return BadRequest(new
                {
                    success = false,
                    errors = ModelState.Values.SelectMany(v => v.Errors)
                });

            // Intenta crear el cliente usando el servicio
            var resultado = await _clienteService.CrearCliente(cliente);

            // Si fue exitoso, retorna 201 Created con la ruta del nuevo cliente
            return resultado
                ? CreatedAtAction(nameof(GetById), new { id = cliente.ClienteId }, new { success = true })
                : BadRequest(new { success = false, message = "Error: Cédula o usuario ya existen" });
        }

        /// <summary>
        /// Actualizar un cliente existente
        /// </summary>
        [HttpPut("{id}")] // Ruta: api/clientes/{id}
        [Authorize] // Requiere autenticación
        public async Task<IActionResult> Update(int id, [FromBody] Cliente cliente)
        {
            // Verifica que el ID de la ruta coincida con el ID del objeto cliente
            if (id != cliente.ClienteId)
                return BadRequest(new { success = false, message = "ID no coincide" });

            // Intenta actualizar el cliente
            var resultado = await _clienteService.ActualizarCliente(cliente);

            // Si fue exitoso, retorna 204 NoContent, si no, 404 NotFound
            return resultado
                ? NoContent()
                : NotFound(new { success = false, message = "Cliente no encontrado" });
        }

        /// <summary>
        /// Eliminar un cliente (solo accesible por Admin)
        /// </summary>
        [HttpDelete("{id}")] // Ruta: api/clientes/{id}
        [Authorize(Roles = "Admin")] // Solo administradores pueden eliminar clientes
        public async Task<IActionResult> Delete(int id)
        {
            // Intenta eliminar el cliente
            var resultado = await _clienteService.EliminarCliente(id);

            // Si fue exitoso, retorna 204 NoContent, si no, 404 NotFound
            return resultado
                ? NoContent()
                : NotFound(new { success = false, message = "Cliente no encontrado" });
        }
    }
}
