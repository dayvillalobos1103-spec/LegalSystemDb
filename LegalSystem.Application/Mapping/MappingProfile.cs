using LegalSystem.Application.DTOs.Usuario;
using LegalSystem.Application.DTOs.Cliente;
using LegalSystem.Application.DTOs.CasoJuridico;
using LegalSystem.Application.DTOs.Cita;
using AutoMapper;
using LegalSystem.Domain;


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

            #region Clientes
            CreateMap<Clientes, ClienteDtos>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Clienteid))
                .ForMember(dest => dest.TituloCaso, opt => opt.MapFrom(src =>
                    src.CasosJuridicos != null && src.CasosJuridicos.Any()
                    ? src.CasosJuridicos.First().TituloCaso : ""))

                .ForMember(dest => dest.DescripcionCaso, opt => opt.MapFrom(src =>
                    src.CasosJuridicos != null && src.CasosJuridicos.Any()
                    ? src.CasosJuridicos.First().Descripcion : ""))

                .ForMember(dest => dest.DetalleCita, opt => opt.MapFrom(src =>
                    src.Citas != null && src.Citas.Any()
                    ? src.Citas.First().Motivo : ""));

            CreateMap<CrearClienteDtos, Clientes>();
            CreateMap<ActualizarClienteDtos, Clientes>();
            #endregion



            #region
            // 1. PARA CONSULTAR (GET)
            CreateMap<CasoJuridico, CasoDtos>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Casoid))
                .ForMember(dest => dest.ClienteId, opt => opt.MapFrom(src => src.ClienteId))
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
                .ForMember(dest => dest.Citaid, opt => opt.MapFrom(src => src.Citaid))
                .ForMember(dest => dest.ClienteId, opt => opt.MapFrom(src => src.ClienteId))
                .ForMember(dest => dest.Casoid, opt => opt.MapFrom(src => src.Casoid))
                .ForMember(dest => dest.NombreCliente, opt => opt.MapFrom(src => src.Cliente != null ? src.Cliente.Nombre : "Sin cliente"))
                .ForMember(dest => dest.NombreAbogado, opt => opt.MapFrom(src => src.Usuario != null ? src.Usuario.Nombre : "Sin abogado asignado"))
                .ForMember(dest => dest.TituloCaso, opt => opt.MapFrom(src => src.CasosJuridico != null ? src.CasosJuridico.TituloCaso : "Sin caso vinculado"));

            CreateMap<CrearCitaDtos, Cita>();
            CreateMap<ActualizarCitaDtos, Cita>();
            #endregion


        }
    }
}