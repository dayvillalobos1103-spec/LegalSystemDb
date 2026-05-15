using LegalSystem.Application.DTOs.Cita;
using LegalSystem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LegalSystem.API.Request;

namespace LegalSystem.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CitasController : ControllerBase
    {
        private readonly ICitaService _citaService;

        public CitasController(ICitaService citaService)
        {
            _citaService = citaService;
        }

        // 1. CREAR CITA
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CrearCitaDtos dto)
        {
            if (dto == null) return BadRequest("Los datos son obligatorios.");
            var resultado = await _citaService.AddAsync(dto);
            return resultado ? Ok("Cita programada.") : BadRequest("No se pudo crear.");
        }

        // 2. OBTENER TODAS (PAGINADAS)
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pagina = 1, [FromQuery] int tamano = 10)
        {
            // Llamamos al servicio de citas con los parámetros de paginación
            var resultado = await _citaService.GetAllPagedAsync(pagina, tamano);

            // Verificamos si la respuesta es nula o si el conteo de elementos es 0
            if (resultado == null || resultado.TotalElementos == 0)
            {
                // Enviamos el mensaje personalizado con un código 404
                return NotFound(new { mensaje = "No hay citas registradas en el sistema actualmente." });
            }

            // Si hay datos, devolvemos el objeto paginado con un 200 OK
            return Ok(resultado);
        }
        

        // 3. ACTUALIZAR CITA 
        [HttpPut("{id}")] 
        public async Task<IActionResult> Update(int id, [FromBody] ActualizarCitaDtos dto)
        {
            if (id <= 0 || dto == null)
                return BadRequest("Datos o ID no válidos.");
            
            var resultado = await _citaService.UpdateAsync(id, dto);

            if (resultado) return Ok("Cita actualizada correctamente.");

            return NotFound("No se encontró la cita para actualizar.");
        }

        // 4. ELIMINAR CITA
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var resultado = await _citaService.DeleteAsync(id);
            if (resultado) return Ok("Cita eliminada.");
            return NotFound("La cita no existe.");
        }
    }
}