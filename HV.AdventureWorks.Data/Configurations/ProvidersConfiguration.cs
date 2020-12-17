using Microsoft.Extensions.DependencyInjection;
using HV.AdventureWorks.Data.Interfaces;
using HV.AdventureWorks.Data.Providers;

namespace HV.AdventureWorks.Data.Configurations
{
    public static class ProvidersConfiguration
    {
        public static IServiceCollection ConfigureProviders(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddTransient<IProductsProvider, ProductsProvider>()
                .AddTransient<IDocumentsProvider, DocumentsProvider>();
        }
    }
}
