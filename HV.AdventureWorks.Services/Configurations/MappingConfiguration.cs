using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using HV.AdventureWorks.Services.Mappings;

namespace HV.AdventureWorks.Services.Configurations
{
    public static class MappingConfiguration
    {
        public static IServiceCollection ConfigureMapper(this IServiceCollection serviceCollection)
        {
            var mapper = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()))
                .CreateMapper();

            return serviceCollection.AddSingleton(mapper);
        }
    }
}
