using Microsoft.Extensions.DependencyInjection;
using System;

namespace Stock.API.Configuration
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
