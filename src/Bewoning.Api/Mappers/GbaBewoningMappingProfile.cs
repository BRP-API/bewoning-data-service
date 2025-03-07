using AutoMapper;
using Bewoning.Api.ApiModels.Bewoning;

namespace Bewoning.Api.Mappers
{
    public class GbaBewoningMappingProfile : Profile
    {

        public GbaBewoningMappingProfile()
        {
            CreateMap<GbaBewoning, Generated.Partials.GbaBewoning>()
                .ReverseMap();
        }
    }
}
