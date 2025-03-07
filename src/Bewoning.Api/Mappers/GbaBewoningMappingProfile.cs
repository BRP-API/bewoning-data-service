using AutoMapper;
using Bewoning.Api.ApiModels.Bewoning;

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
                .ReverseMap();
        }
    }
}
