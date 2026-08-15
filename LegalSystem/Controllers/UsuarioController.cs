using LegalSystem.Application.DTOs.Usuario;
using LegalSystem.Application.Interfaces;
using LegalSystem.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LegalSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly UserManager<Usuario> _userManager;

        // 1. Añadimos el IAuthService
        private readonly IAuthService _authService;

        public UsuarioController(
            IUsuarioService usuarioService,
            UserManager<Usuario> userManager,
            IAuthService authService) // Lo inyectamos aquí
        {
            _usuarioService = usuarioService;
            _userManager = userManager;
            _authService = authService;
        }

      
        // ENDPOINT PARA REGISTRAR UN USUARIO
    
        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] CrearUsuarioDtos dto)
        {
            var resultado = await _usuarioService.RegistrarUsuarioAsync(dto);

            return Ok(new
            {
                mensaje = "Usuario registrado con éxito",
                status = resultado
            });
        }

        // ENDPOINT PARA INICIAR SESIÓN (LOGIN)
 
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            // Las validaciones se hacen solas gracias a los atributos de tu LoginDto
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Delegamos todo el trabajo sucio al AuthService
            var response = await _authService.LoginAsync(dto);

            // Si el Success es falso (credenciales inválidas)
            if (!response.Success)
            {
                return Unauthorized(new { mensaje = response.Message });
            }

            // Si todo sale bien, devolvemos el token generado
            return Ok(new
            {
                mensaje = response.Message,
                token = response.Token
            });
        }



        // ==========================================
        // ENDPOINT PARA ELIMINAR UN USUARIO
        // ==========================================
        [HttpDelete("eliminar")]
        public async Task<IActionResult> EliminarUsuario([FromQuery] string email)
        {
            var resultado = await _usuarioService.EliminarUsuarioAsync(email);
            if (!resultado) return NotFound(new { mensaje = "Usuario no encontrado o no se pudo eliminar." });

            return Ok(new { mensaje = $"Usuario {email} eliminado con éxito." });
        }

        // ==========================================
        // ENDPOINT PARA ACTUALIZAR UN USUARIO
        // ==========================================
        [HttpPut("actualizar")]
        public async Task<IActionResult> ActualizarUsuario([FromQuery] string email, [FromQuery] string nuevoNombre)
        {
            var resultado = await _usuarioService.ActualizarUsuarioAsync(email, nuevoNombre);
            if (!resultado) return BadRequest(new { mensaje = "No se pudo actualizar el usuario." });

            return Ok(new { mensaje = "Usuario actualizado con éxito." });
        }
    }
}