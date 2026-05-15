using LegalSystem.Application.DTOs.Usuario;
using LegalSystem.Application.DTOs.Cliente;
using LegalSystem.Application.DTOs.CasoJuridico;
using LegalSystem.Application.DTOs.Cita;
using AutoMapper;
using LegalSystem.Domain;
using LegalSystem.Application.DTOs.Casoluridico.LegalSystem.Application.DTOs.Casojuridico;


namespace LegalSystem.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Usuarios
            CreateMap<Usuario, UsuarioDtos>();
            CreateMap<CrearUsuarioDtos, Usuario>();
            #endregion

            #region
            CreateMap<Clientes, ClienteDtos>()
                .ForMember(dest => dest.UsuarioId, opt => opt.MapFrom(src => src.UsuarioId))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Clienteid))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
            CreateMap<CrearClienteDtos, Clientes>();
            CreateMap<ActualizarClienteDtos, Clientes>();

            #endregion
            #region
            // 1. PARA CONSULTAR (GET)
            CreateMap<CasoJuridico, CasoDtos>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Casoid))
                .ForMember(dest => dest.NombreCliente, opt => opt.MapFrom(src => src.Cliente.Nombre ?? "Sin nombre"))
                .ForMember(dest => dest.UsuarioId, opt => opt.MapFrom(src => src.UsuarioId))
                .ForMember(dest => dest.Titulo, opt => opt.MapFrom(src => src.TituloCaso))
                .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
                .ForMember(dest => dest.FechaInicio, opt => opt.MapFrom(src => src.FechaInicio));

            // 2. PARA CREAR (POST) - Este es el que faltaba en tu imagen
            CreateMap<CrearCasoDtos, CasoJuridico>()
                .ForMember(dest => dest.TituloCaso, opt => opt.MapFrom(src => src.Titulo))
                .ForMember(dest => dest.ClienteId, opt => opt.MapFrom(src => src.ClienteId));

            // 3. PARA ACTUALIZAR (PUT)
            CreateMap<ActualizarCasoDtos, CasoJuridico>()
                .ForMember(dest => dest.TituloCaso, opt => opt.MapFrom(src => src.Titulo));

            #endregion

            #region Citas
            CreateMap<Cita, CitaDtos>()
               
                .ForMember(dest => dest.NombreCliente, opt => opt.MapFrom(src => src.Cliente.Nombre ?? "Sin nombre"))
                .ForMember(dest => dest.TituloCaso, opt => opt.MapFrom(src => src.CasosJuridico.TituloCaso ?? "Sin título"))
                .ForMember(dest => dest.Motivo, opt => opt.MapFrom(src => src.Motivo))
                .ForMember(dest => dest.Lugar, opt => opt.MapFrom(src => src.Lugar))
                .ForMember(dest => dest.FechaHora, opt => opt.MapFrom(src => src.FechaHora));

            CreateMap<CrearCitaDtos, Cita>();
            CreateMap<ActualizarCitaDtos, Cita>();
            #endregion


        }
    }
}