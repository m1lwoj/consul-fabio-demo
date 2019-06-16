using System;
using System.Threading;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Common
{
    public static class ConsulRegister
    {
        private static readonly ILogger _logger = ApplicationLogging.CreateLogger(nameof(ConsulRegister));

        public static IServiceCollection AddConsul(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();

            var settingsSection = configuration.GetSection("serviceDiscovery");
            var settings = settingsSection.Get<ServiceDiscoveryOptions>();

            services.Configure<ServiceDiscoveryOptions>(configuration.GetSection("serviceDiscovery"));

            return services.AddSingleton<IConsulClient>(p => new ConsulClient(cfg =>
            {
                var serviceConfiguration = p.GetService<IOptions<ServiceDiscoveryOptions>>().Value;

                if (!string.IsNullOrEmpty(serviceConfiguration.Consul.Address))
                {
                    cfg.Address = new Uri($"http://{serviceConfiguration.Consul.Address}:{serviceConfiguration.Consul.Port}");
                }
            }));
        }

        public static string UseConsul(this IApplicationBuilder app, IApplicationLifetime applicationLifetime)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var serviceDiscoveryOptions = scope.ServiceProvider.GetService<IOptions<ServiceDiscoveryOptions>>();
                var consulOptions = serviceDiscoveryOptions.Value.Consul;

                var uniqueId = Guid.NewGuid().ToString();
                var client = scope.ServiceProvider.GetService<IConsulClient>();

                var serviceName = serviceDiscoveryOptions.Value.ServiceName;
                var serviceId = $"{serviceName}:{uniqueId}";
                var servicePort = serviceDiscoveryOptions.Value.ServicePort;
                var serviceAddress = serviceDiscoveryOptions.Value.ServiceAddress;
                var healthCheckEndpoint = serviceDiscoveryOptions.Value.HealthCheckEndPoint;

                var check = new AgentServiceCheck
                {
                    Interval = TimeSpan.FromSeconds(5),
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(10),
                    HTTP = $"http://{serviceAddress}:{servicePort}/{healthCheckEndpoint}"
                };

                var registration = new AgentServiceRegistration
                {
                    Name = serviceName,
                    ID = serviceId,
                    Address = serviceAddress,
                    Port = servicePort,
                    Checks = new[] { check },
                    Tags = new[] { $"urlprefix-/{serviceName} strip=/{serviceName}" }
                };

                _logger.LogDebug($"Registering {serviceName} to consul");
                _logger.LogDebug($"{serviceAddress}:{servicePort} with id: {serviceId}");

                client.Agent.ServiceRegister(registration).GetAwaiter().GetResult();

                applicationLifetime.ApplicationStopping.Register(() =>
               {
                   _logger.LogDebug($"Deregistering {serviceId} from consul");
                   client.Agent.ServiceDeregister(serviceId).GetAwaiter().GetResult();
               });

                return serviceId;
            }
        }
    }
}
