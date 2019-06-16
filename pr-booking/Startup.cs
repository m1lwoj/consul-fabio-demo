using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Common;
using Consul;
using System.ComponentModel;
using PR.Booking.Services;
using System.Threading;

namespace PR.Booking
{
    public class Startup
    {
        private ILoggerFactory _logFactory;

        public IConfiguration Configuration { get; }

        public IContainer Container { get; private set; }

        public Startup(IConfiguration configuration, ILoggerFactory logFactory)
        {
            Configuration = configuration;
            _logFactory = logFactory;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddOptions();

            ApplicationLogging.RegisterFactory(_logFactory);

            services.AddConsul();
            services.AddFabio();

            services.AddFabioService<IPaymentsService>("pr-payments-service");
            services.AddFabioService<IAccommodationService>("pr-accommodation-service");
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
          IApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseConsul(applicationLifetime);
        }
    }
}
