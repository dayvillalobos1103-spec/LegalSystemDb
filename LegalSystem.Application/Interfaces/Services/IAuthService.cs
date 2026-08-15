using LegalSystem.Application.DTOs.Usuario;
using LegalSystem.Application.Response;

namespace LegalSystem.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> LoginAsync(LoginDto request);
    }
}