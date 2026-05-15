using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AutoMapper;
using LegalSystem.Application.DTOs.Usuario;
using LegalSystem.Application.Interfaces;
using LegalSystem.Application.Interfaces.Dbcontex;
using LegalSystem.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;





namespace LegalSystem.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public UsuarioService(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, IMapper mapper, IApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _context = context;


        }

        // Registro con validaciones para el Middleware

        public async Task<bool> RegistrarUsuarioAsync(CrearUsuarioDtos dto)
        {
            var nuevoUsuario = new Usuario
            {
                Nombre = dto.Nombre,
                Email = dto.Email,

                
                UserName = dto.Email
            };

            var resultado = await _userManager.CreateAsync(nuevoUsuario, dto.Password);

            if (!resultado.Succeeded)
            {
                // Esto te ayudará a ver cualquier otro error de Identity en consola
                var error = resultado.Errors.FirstOrDefault()?.Description;
                throw new Exception($"Error al registrar el usuario: {error}");
            }

            return true;
        }


        //  Login

        public async Task<string?> LoginAsync(string email, string password)
        {
           
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);

            if (usuario == null) return null;

            // 2. Verificamos si la contraseña coincide con el Hash de la base de datos
            var resultado = await _signInManager.CheckPasswordSignInAsync(usuario, password, false);

            if (!resultado.Succeeded) return null;

            // 3. Si la contraseña es correcta, generamos los Claims
            var claims = new[]
            {
             new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, usuario.Nombre!),
             new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Email, usuario.Email!),
             new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, usuario.Id)
            };

            // 4. Configuración de seguridad del Token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("EstaEsMiLlaveSuperSecretaYMuyLarga123456789"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "LegalSystem.API",
                audience: "LegalSystem.UI",
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            // 5. Devolvemos el token escrito como string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}