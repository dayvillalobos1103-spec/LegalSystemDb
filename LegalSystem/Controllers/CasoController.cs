using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LegalSystem.Application.Interfaces;
using LegalSystem.Application.DTOs.CasoJuridico;
using LegalSystem.Application.DTOs.Casoluridico.LegalSystem.Application.DTOs.Casojuridico;
using LegalSystem.API.Request;
namespace LegalSystem.API.Controllers
{
    [Authorize] 
    [ApiController]
    [Route("api/[controller]")]
    public class CasosController : ControllerBase
    {
        private readonly ICasoService _casoService;

        public CasosController(ICasoService casoService)
        {
            _casoService = casoService;
        }

   
        // GET: api/Casos
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pagina = 1, [FromQuery] int tamano = 10)
        {
            // Llamamos al servicio con los parámetros de consulta
            var resultado = await _casoService.GetAllPagedAsync(pagina, tamano);

            // Verificamos si no hay registros, igual que en Clientes
            if (resultado == null || resultado.TotalElementos == 0)
            {
                return NotFound(new { mensaje = "No hay casos jurídicos registrados actualmente." });
            }

            return Ok(resultado);
        }

        // POST: api/Casos
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CrearCasoDtos dto)
        {
            if (dto == null) return BadRequest("Los datos del caso son nulos.");

            var success = await _casoService.AddAsync(dto);

            if (success)
            {
                return Ok(new { message = "Caso creado exitosamente con el abogado actual." });
            }

            return BadRequest("No se pudo crear el caso.");
        }

        // PUT: api/Casos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ActualizarCasoDtos dto)
        {
            if (dto == null || id != dto.Casoid)
            {
                return BadRequest("Los datos del caso son inconsistentes.");
            }

            var success = await _casoService.UpdateAsync(dto);

            if (success)
            {
                return Ok(new { message = "Caso actualizado correctamente." });
            }

            return BadRequest("No se pudo actualizar el caso. Verifica si el ID existe.");
        }

        // DELETE: api/Casos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _casoService.DeleteAsync(id);
            if (!success) return NotFound("El caso no existe.");

            return Ok(new { message = "Caso eliminado correctamente." });
        }
    }
}
