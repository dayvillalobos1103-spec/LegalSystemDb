using LegalSystem.Application.DTOs.Cliente;
using LegalSystem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LegalSystem.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var resultado = await _clienteService.GetAllAsync();

            // Si la lista viene vacía o es nula
            if (resultado == null || !resultado.Any())
            {
                return NotFound(new { mensaje = "No hay clientes registrados en el sistema." });
            }

            return Ok(resultado);
        }
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearClienteDtos dto)
        {
            var resultado = await _clienteService.AddAsync(dto);
            return Ok(new { mensaje = "Cliente creado con éxito", data = resultado });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarClienteDtos dto)
        {
           
            bool actualizado = await _clienteService.UpdateAsync(id, dto);

            if (!actualizado)
            {
                return NotFound(new { mensaje = "No se pudo actualizar porque el cliente no existe." });
            }

            return Ok(new { mensaje = "Cliente actualizado con éxito" });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            await _clienteService.DeleteAsync(id);
            return Ok(new { mensaje = "Cliente eliminado correctamente" });
        }
    }
}