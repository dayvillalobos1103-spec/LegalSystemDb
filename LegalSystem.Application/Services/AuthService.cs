using LegalSystem.Application.DTOs.Usuario;
using LegalSystem.Application.Interfaces;
using LegalSystem.Application.Response;
using LegalSystem.Domain; // Para usar tu clase Usuario
using Microsoft.AspNetCore.Identity; // Para usar UserManager
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LegalSystem.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<Usuario> _userManager;

   
        public AuthService(IConfiguration config, UserManager<Usuario> userManager)
        {
            _config = config;
            _userManager = userManager;
        }

        public async Task<AuthResponse> LoginAsync(LoginDto request)
        {

    
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return new AuthResponse { Success = false, Message = "Credenciales incorrectas" };
            }

            // Verificamos si la contraseña encriptada coincide
            var passwordValida = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!passwordValida)
            {
                return new AuthResponse { Success = false, Message = "Credenciales incorrectas" };
            }
            var claims = new List<Claim>
            {
                new Claim("id", user.Id.ToString()), 
                new Claim("user", user.Email),       
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT_KEY"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: _config["JWT_ISSUER"],
                audience: _config["JWT_AUDIENCE"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(4),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

          
            return new AuthResponse
            {
                Success = true,
                Token = tokenString, // Este es el Token real que usará el frontend
                Message = "Login exitoso"
            };
        }
    }
}