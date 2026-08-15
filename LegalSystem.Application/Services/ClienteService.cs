using AutoMapper;
using LegalSystem.Application.DTOs.Cliente;
using LegalSystem.Application.Interfaces;
using LegalSystem.Application.Interfaces.Repositorio;
using LegalSystem.Application.Response;
using LegalSystem.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LegalSystem.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepo;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClienteService(IClienteRepository clienteRepo, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _clienteRepo = clienteRepo;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IEnumerable<ClienteDtos>> GetAllAsync()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            string? usuarioIdFiltro = user?.FindFirstValue("id") ?? user?.FindFirstValue(ClaimTypes.NameIdentifier);

            var clientes = await _clienteRepo.GetAllAsync(usuarioIdFiltro);
            return _mapper.Map<IEnumerable<ClienteDtos>>(clientes);
        }
        // 1. Paginación y Listado
        public async Task<RespuestaPaginada<ClienteDtos>> GetAllPagedAsync(int pagina, int tamano)
        {
            var user = _httpContextAccessor.HttpContext?.User;
            string? usuarioIdFiltro = null;
                
            usuarioIdFiltro = user?.FindFirstValue("id") ?? user?.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(usuarioIdFiltro))
            {
                throw new UnauthorizedAccessException("BLOQUEO DE SEGURIDAD: No se pudo identificar tu ID en el Token. Vuelve a iniciar sesión.");
            }



            var total = await _clienteRepo.CountAsync(usuarioIdFiltro);
            var items = await _clienteRepo.GetAllPagedAsync(pagina, tamano, usuarioIdFiltro);

            
            var dtos = _mapper.Map<IEnumerable<ClienteDtos>>(items);

            return new RespuestaPaginada<ClienteDtos>(dtos, total, pagina, tamano);
        }
        

        // 2. Búsqueda con paginación
        public async Task<RespuestaPaginada<ClienteDtos>> SearchPagedAsync(string valor, int pagina, int tamano)
        {
            var total = await _clienteRepo.CountSearchAsync(valor);
            var items = await _clienteRepo.SearchPagedAsync(valor, pagina, tamano);
            var dtos = _mapper.Map<IEnumerable<ClienteDtos>>(items);

            return new RespuestaPaginada<ClienteDtos>(dtos, total, pagina, tamano);
        }

        // 3. CRUD 
        public async Task<ClienteDtos?> GetByIdAsync(int id)
        {
            if (id <= 0) throw new ArgumentException("El ID del cliente debe ser válido.");

            var cliente = await _clienteRepo.GetByIdAsync(id);
            if (cliente == null) return null;


            return _mapper.Map<ClienteDtos>(cliente);
        }


        public async Task<bool> AddAsync(CrearClienteDtos dto)
        {
            // 1. Obtener el ID del abogado del token
            var user = _httpContextAccessor.HttpContext?.User;
            var userId = user?.FindFirstValue("id") ?? user?.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("Error: No se pudo identificar al usuario. ¿Iniciaste sesión?");
            }

            // 2. Mapear y asignar (SOLO UNA VEZ)
            var cliente = _mapper.Map<Clientes>(dto);
            cliente.UsuarioId = userId;

            // 3. Guardar
            await _clienteRepo.AddAsync(cliente);
            return true;
        }

        public async Task<bool> UpdateAsync(int id, ActualizarClienteDtos dto)
        {
            // 1. Buscamos el cliente por el ID de la URL
            var clienteExistente = await _clienteRepo.GetByIdAsync(id);
            if (clienteExistente == null) return false;

            // 2. Extraemos el ID del abogado del Token (Seguridad)
            var user = _httpContextAccessor.HttpContext?.User;
            var userId = user?.FindFirstValue("id") ?? user?.FindFirstValue(ClaimTypes.NameIdentifier);

            if (clienteExistente.UsuarioId != userId) return false;

         
            _mapper.Map(dto, clienteExistente);

            await _clienteRepo.UpdateAsync(clienteExistente);
            return true;
        }


        public async Task<bool> DeleteAsync(int id)
        {
            //  Validamos que el ID sea un número válido
            if (id <= 0)
            {
                throw new ArgumentException("El ID del cliente debe ser mayor que cero.", nameof(id));
            }

            //  Buscamos si el cliente existe antes de intentar borrarlo
            var cliente = await _clienteRepo.GetByIdAsync(id);

            //  Si no existe, lanzamos un mensaje de error claro
            if (cliente == null)
            {
                throw new InvalidOperationException($"No se puede eliminar porque el cliente con ID {id} no existe en el sistema.");
            }

            // 4. Si existe, lo borramos
            await _clienteRepo.DeleteAsync(id);
            return true;


        }

       
    }
}