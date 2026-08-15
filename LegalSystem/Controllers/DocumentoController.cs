using LegalSystem.Application.DTOs;
using LegalSystem.Application.Interfaces.Services; // Ajusta según tu namespace
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LegalSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // 🔒 Protege todo el controlador pidiendo Token JWT
    public class DocumentoController : ControllerBase
    {
        private readonly IDocumentoService _documentoService;

        public DocumentoController(IDocumentoService documentoService)
        {
            _documentoService = documentoService;
        }



        [HttpGet("cliente/{clienteId}")]
        public async Task<IActionResult> GetDocumentosPorCliente(int clienteId)
        {
            try
            {
                var usuarioId = ObtenerUsuarioId();
                // Llamamos al servicio para obtener todos, y luego filtramos por ClienteId
                var todosLosDocumentos = await _documentoService.GetAllByUsuarioIdAsync(usuarioId);

                var documentosDelCliente = todosLosDocumentos
                                            .Where(d => d.ClienteId == clienteId)
                                            .ToList();

                return Ok(documentosDelCliente);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // 🟢 POST: api/Documento/subir
        [HttpPost("subir")]
        public async Task<IActionResult> SubirDocumento([FromForm] CrearDocumentoDto dto)
        {
            // Valida que el modelo DTO esté correcto (que traiga archivo y clienteId)
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var usuarioId = ObtenerUsuarioId();

                // Enviamos el DTO y el Id del abogado al servicio para que haga la magia
                var documentoCreado = await _documentoService.CrearDocumentoAsync(dto, usuarioId);

                return Ok(documentoCreado);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al subir el documento: {ex.Message}");
            }
        }

        // 🔴 DELETE: api/Documento/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarDocumento(int id)
        {
            try
            {
                var usuarioId = ObtenerUsuarioId();
                var exito = await _documentoService.DeleteAsync(id, usuarioId);

                if (!exito)
                    return NotFound(new { mensaje = "Documento no encontrado o no pertenece a este usuario." });

                return Ok(new { mensaje = "Documento eliminado correctamente de la base de datos." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // 🛠️ MÉTODO AUXILIAR: Extrae el ID del Abogado desde el Token JWT
        private string ObtenerUsuarioId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            return claim != null ? claim.Value : string.Empty;
        }
    }
}