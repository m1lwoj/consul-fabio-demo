using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RestEase;
using System.Net.Http;

namespace Common
{
    public static class RestEaseRegister
    {
        public static void AddFabioService<T>(this IServiceCollection services, string serviceName)
            where T : class
        {
            //For named client name creates default HttpMessageHandler
            string clientName = typeof(T).ToString();
            services.AddHttpClient(clientName)
                .AddHttpMessageHandler(c =>
                    new HttpMessageHandler(c.GetService<IOptions<FabioOptions>>(), serviceName));

            services.AddTransient<T>(c => new RestClient(c.GetService<IHttpClientFactory>().CreateClient(clientName)).For<T>());
        }
    }
}
