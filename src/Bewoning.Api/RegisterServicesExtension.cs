using Bewoning.Api.Mappers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bewoning.Api;

public static class RegisterServicesExtension
{
    public static void ConfigureRvigApiSharedServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(cfg => { }, typeof(GbaBewoningMappingProfile).Assembly);
    }
}
