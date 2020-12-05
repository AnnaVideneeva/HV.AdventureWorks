using Microsoft.Extensions.DependencyInjection;
using HV.AdventureWorks.Services.Interfaces;
using HV.AdventureWorks.Services.Services;
using HV.AdventureWorks.Data.Configurations;
using HV.AdventureWorks.Data.Contexts;
using HV.AdventureWorks.Core.Data.Interfaces;
using HV.AdventureWorks.Core.Data;

namespace HV.AdventureWorks.Services.Configurations
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection
                .ConfigureUnitOfWork(connectionString)
                .ConfigureProviders();

            return serviceCollection
                .AddTransient<IProductsService, ProductsService>()
                .AddTransient<IDocumentsService, DocumentsService>();
        }

        private static IServiceCollection ConfigureUnitOfWork(this IServiceCollection serviceCollection, string connectionString)
        {
            return serviceCollection
                .AddScoped<IUnitOfWork, UnitOfWork<AdventureWorksContext>>()
                .AddScoped(s => new AdventureWorksContext(connectionString));
        }
    }
}
