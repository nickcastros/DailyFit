using AutoMapper;
using DailyFit.Data.Dtos;

namespace DailyFit.Models.Profiles;

public class UsuarioProfile : Profile
{
    public UsuarioProfile()
    {
        CreateMap<CreateUsuarioDto, Usuario>();
        CreateMap<UpdateUsuarioDto, Usuario>();
        CreateMap<Usuario,ReadUsuarioDto>();
    }
}
