using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common
{
    public static class FabioRegister
    {
        public static IServiceCollection AddFabio(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();

            services.Configure<FabioOptions>(configuration.GetSection("loadBalancer"));

            services.AddTransient<HttpMessageHandler>();

            return services;
        }
    }
}
