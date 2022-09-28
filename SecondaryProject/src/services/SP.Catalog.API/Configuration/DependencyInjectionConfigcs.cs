using SP.Catalog.API.Data.Repository;
using SP.Catalog.API.Data;
using SP.Catalog.API.Models;

namespace SP.Catalog.API.Configuration
{
    public static class DependencyInjectionConfigcs
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<CatalogContext>();
        }
    }

}
