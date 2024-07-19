using AutoMapper;
using DailyFit.Data.Dtos;

namespace DailyFit.Models.Profiles;

public class DadosPessoaisProfile : Profile
{
    public DadosPessoaisProfile()
    {
        CreateMap<CreateDadosPessoaisDto, DadosPessoais>().ForMember(dest => dest.DataNascimento, opt => opt.MapFrom(src => DateTime.Parse(src.DataNascimento)));
        CreateMap<UpdateDadosPessoaisDto, DadosPessoais>().ForMember(dest => dest.DataNascimento, opt => opt.MapFrom(src => DateTime.Parse(src.DataNascimento)));
        CreateMap<DadosPessoais,UpdateDadosPessoaisDto>();
        CreateMap<DadosPessoais, ReadDadosPessoaisDto>();
    }
}
