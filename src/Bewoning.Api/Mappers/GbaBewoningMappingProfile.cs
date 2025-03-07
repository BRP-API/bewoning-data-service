using AutoMapper;
using Bewoning.Api.ApiModels.Bewoning;
using Bewoning.Api.ApiModels.PersonenHistorieBase;

namespace Bewoning.Api.Mappers
{
    public class GbaBewoningMappingProfile : Profile
    {

        public GbaBewoningMappingProfile()
        {
            CreateMap<GbaBewoning, Generated.GbaBewoning>()
                .ReverseMap();

            CreateMap<Periode, Generated.Periode>()
                .ReverseMap();

            CreateMap<GbaBewoner, Generated.GbaBewoner>()
                .ForMember(dest => dest.Naam, opt => opt.MapFrom(src => src.Naam))
                .ReverseMap();

            CreateMap<GbaNaamBasis, Generated.NaamBasis>()
                .ForMember(dest => dest.Voornamen, opt => opt.MapFrom(src => src.Voornamen))
                .ForMember(dest => dest.AdellijkeTitelPredicaat, opt => opt.MapFrom(src => src.AdellijkeTitelPredicaat))
                .ForMember(dest => dest.Voorvoegsel, opt => opt.MapFrom(src => src.Voorvoegsel))
                .ForMember(dest => dest.Geslachtsnaam, opt => opt.MapFrom(src => src.Geslachtsnaam))
                .ReverseMap();
        }
    }
}
