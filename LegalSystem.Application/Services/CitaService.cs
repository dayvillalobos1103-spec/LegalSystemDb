using System.Security.Claims;
using AutoMapper;
using LegalSystem.Application.DTOs.Cita;
using LegalSystem.Application.Interfaces;
using LegalSystem.Application.Interfaces.Repositorio; 
using LegalSystem.Application.Response;
using LegalSystem.Domain;
using Microsoft.AspNetCore.Http;


namespace LegalSystem.Application.Services
{
    public class CitaService : ICitaService
    {
        private readonly ICitaRepository _citaRepo;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
       

        // Inyectamos el repositorio de citas directamente
        public CitaService(ICitaRepository citaRepo, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _citaRepo = citaRepo;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
           
        }

        // 1. Obtener todas las citas paginadas
        public async Task<RespuestaPaginada<CitaDtos>> GetAllPagedAsync(int pagina, int tamano)
        {
            // 1. Identificamos quién es el usuario actual usando su Token
            var user = _httpContextAccessor.HttpContext?.User;
            string? usuarioIdFiltro = user?.FindFirstValue("id") ?? user?.FindFirstValue(ClaimTypes.NameIdentifier);

            // 4. Enviamos el filtro al repositorio
            var total = await _citaRepo.CountAsync(usuarioIdFiltro);
            var items = await _citaRepo.GetAllPagedAsync(pagina, tamano, usuarioIdFiltro);

            var dtos = _mapper.Map<IEnumerable<CitaDtos>>(items);

            return new RespuestaPaginada<CitaDtos>(dtos, total, pagina, tamano);
        }

        // 2. Buscar citas paginadas (Este lo dejas tal cual lo tienes en tu imagen, está perfecto)
        public async Task<RespuestaPaginada<CitaDtos>> SearchPagedAsync(string valor, int pagina, int tamano)
        {
            var total = await _citaRepo.CountSearchAsync(valor);
            var items = await _citaRepo.SearchPagedAsync(valor, pagina, tamano);
            var dtos = _mapper.Map<IEnumerable<CitaDtos>>(items);
            return new RespuestaPaginada<CitaDtos>(dtos, total, pagina, tamano);
        }

        // 3. Obtener cita por ID
        public async Task<CitaDtos?> GetByIdAsync(int id)
        {
            var cita = await _citaRepo.GetByIdAsync(id);
            if (cita == null) return null;

            return _mapper.Map<CitaDtos>(cita);
        }

        // 4. Crear nueva cita
        public async Task<bool> AddAsync(CrearCitaDtos dto)
        {
            // 1. Buscamos al abogado logueado
            var user = _httpContextAccessor.HttpContext?.User;
            var userId = user?.FindFirstValue("id") ?? user?.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("Debes iniciar sesión para agendar citas.");
            }

            // 2. Mapeamos el DTO a la entidad
            var cita = _mapper.Map<Cita>(dto);

            // 3. Asignamos los valores que NO vienen del Swagger
            cita.UsuarioId = userId;
            cita.Estado = "Programada"; 

            // 4. Guardamos
            await _citaRepo.AddAsync(cita);
            return true;
        }

        // 5. Actualizar cita
        public async Task<bool> UpdateAsync(int id, ActualizarCitaDtos dto)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID de la cita debe ser mayor que cero.", nameof(id));
            }

            var citaExistente = await _citaRepo.GetByIdAsync(id);
            if (citaExistente == null)
            {
                throw new InvalidOperationException($"No se puede actualizar porque la cita con ID {id} no existe en el sistema.");
            }

            _mapper.Map(dto, citaExistente);
            await _citaRepo.UpdateAsync(citaExistente);
            return true;
        }


        // 6. Eliminar cita
        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID de la cita debe ser mayor que cero.", nameof(id));
            }

            var cita = await _citaRepo.GetByIdAsync(id);
            if (cita == null)
            {
                throw new InvalidOperationException($"No se puede eliminar porque la cita con ID {id} no existe.");
            }

            await _citaRepo.DeleteAsync(id);
            return true;
        }


    }
}