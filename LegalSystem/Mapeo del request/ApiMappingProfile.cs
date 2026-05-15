using AutoMapper;
using LegalSystem.API.Request; 
using LegalSystem.Application.DTOs.CasoJuridico;
using LegalSystem.Application.DTOs.Cita;
using LegalSystem.Application.DTOs.Cliente;
using LegalSystem.Domain;


namespace LegalSystem.API.Mapping
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<ClienteRequest, ClienteDtos>();
            CreateMap<CasoJuridicoRequest,CasoDtos>();
            CreateMap<CitaRequest, CitaDtos>();
            CreateMap<ActualizarClienteRequest, ActualizarClienteDtos>();
            CreateMap<CrearCasoRequest,CrearCasoDtos>();
            CreateMap<ActualizarCitaRequest, ActualizarCitaDtos>();

        }
    }
}