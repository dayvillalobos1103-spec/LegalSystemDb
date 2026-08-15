using System.Security.Claims;
using AutoMapper;
using LegalSystem.Application.DTOs.CasoJuridico;
using LegalSystem.Application.Interfaces;
using LegalSystem.Application.Interfaces.Repositorio; 
using LegalSystem.Application.Response;
using LegalSystem.Domain;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace LegalSystem.Application.Services
{
    public class CasoService : ICasoService
    {
        private readonly ICasoJuridicoRepository _casoRepo;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        // Inyectamos el repositorio de casos directamente
        public CasoService(ICasoJuridicoRepository casoRepo, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _casoRepo = casoRepo;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IEnumerable<CasoDtos>> GetAllAsync()
        {
            var casos = await _casoRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<CasoDtos>>(casos);
        }

        // 1. Obtener todos los casos paginados
        public async Task<RespuestaPaginada<CasoDtos>> GetAllPagedAsync(int pagina, int tamano)
        {
            // 1. Extraemos el usuario logueado desde el Token
            var user = _httpContextAccessor.HttpContext?.User;
            string? usuarioIdFiltro = user?.FindFirstValue("id") ?? user?.FindFirstValue(ClaimTypes.NameIdentifier);

            // 4. Enviamos el filtro al repositorio
            var total = await _casoRepo.CountAsync(usuarioIdFiltro);
            var items = await _casoRepo.GetAllPagedAsync(pagina, tamano, usuarioIdFiltro);

            var dtos = _mapper.Map<IEnumerable<CasoDtos>>(items);

            return new RespuestaPaginada<CasoDtos>(dtos, total, pagina, tamano);
        }
        // 2. Buscar casos paginados
        public async Task<RespuestaPaginada<CasoDtos>> SearchPagedAsync(string valor, int pagina, int tamano)
        {
            var total = await _casoRepo.CountSearchAsync(valor);
            var items = await _casoRepo.SearchPagedAsync(valor, pagina, tamano);
            var dtos = _mapper.Map<IEnumerable<CasoDtos>>(items);

            return new RespuestaPaginada<CasoDtos>(dtos, total, pagina, tamano);
        }

        // 3. Obtener caso por ID
        public async Task<CasoDtos?> GetByIdAsync(int id)
        {
            var caso = await _casoRepo.GetByIdAsync(id);
            if (caso == null) return null;

            return _mapper.Map<CasoDtos>(caso);
        }

        // 4. Crear un nuevo caso
        public async Task<bool> AddAsync(CrearCasoDtos dto)
        {
            
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "Los datos para crear el caso no pueden estar vacíos.");
            }

            if (string.IsNullOrWhiteSpace(dto.Titulo))
            {
                throw new ArgumentException("El título del caso es obligatorio.", nameof(dto.Titulo));
            }

            var user = _httpContextAccessor.HttpContext?.User;
            var userId = user?.FindFirstValue("id") ?? user?.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("Error: No se pudo identificar al abogado. ¿Iniciaste sesión?");
            }

            var caso = _mapper.Map<CasoJuridico>(dto);
            caso.UsuarioId = userId;
            caso.Estado = "Pendiente";
            await _casoRepo.AddAsync(caso);

            return true;
        }


        // 5. Actualizar los datos de un caso
        public async Task<bool> UpdateAsync(int id, ActualizarCasoDtos dto)
        {
            
            if (id <= 0)
            {
                throw new ArgumentException("El ID del caso debe ser mayor que cero.", nameof(id));
            }

          
            var casoExistente = await _casoRepo.GetByIdAsync(id);

         
            if (casoExistente == null)
            {
                throw new InvalidOperationException($"No se puede actualizar porque el caso con ID {id} no existe.");
            }

            // 4. Mapeamos y actualizamos
            _mapper.Map(dto, casoExistente);
            await _casoRepo.UpdateAsync(casoExistente);

            return true;
        }


        // 6. Eliminar un caso
        public async Task<bool> DeleteAsync(int id)
        {
            // 1. Validamos que el id sea válido
            if (id <= 0)
            {
                throw new ArgumentException("El ID del caso debe ser mayor que cero.", nameof(id));
            }

            // 2. Buscamos el caso antes de borrarlo
            var caso = await _casoRepo.GetByIdAsync(id);

            // 3. Si no existe, avisamos con un mensaje claro
            if (caso == null)
            {
                throw new InvalidOperationException($"No se puede eliminar porque el caso con ID {id} no existe en el sistema.");
            }

            // 4. Si existe, procedemos a borrarlo
            await _casoRepo.DeleteAsync(id);
            return true;
        }
    }
}