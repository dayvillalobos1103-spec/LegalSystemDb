using LegalSystem.Application.DTOs.Usuario; 
using LegalSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LegalSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

       
        // 1. ENDPOINT PARA REGISTRAR UN USUARIO
       
        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] CrearUsuarioDtos dto)
        {
            // El Middleware atrapará cualquier ArgumentException si los datos vienen vacíos
            var resultado = await _usuarioService.RegistrarUsuarioAsync(dto);
            
            return Ok(new 
            { 
                mensaje = "Usuario registrado con éxito", 
                status = resultado 
            });
        }


        // 2. ENDPOINT PARA INICIAR SESIÓN (LOGIN)

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
            {
                return BadRequest(new { mensaje = "El correo y la contraseña son obligatorios." }); 
            }

            
            var token = await _usuarioService.LoginAsync(dto.Email, dto.Password);

            
            if (token == null)
            {
                return Unauthorized(new { mensaje = "Correo o contraseña incorrectos." }); 
            }

            
            return Ok(new
            {
                mensaje = "¡Bienvenido Abogado!", 
                token = token
            });
        }
    }
    
}