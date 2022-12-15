using Microsoft.Extensions.DependencyInjection;
using QuoteAPI.Domain.Interfaces.ExternalServices;
using QuoteAPI.Domain.Interfaces.Services;
using Stock.Domain.Services;
using Stock.Infra.ExternalServices;

namespace Stock.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<IStooqExternalService, StooqExternalService>();
        }
    }
}
