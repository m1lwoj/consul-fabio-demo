using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace PR.Payments
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            ApplicationLogging.RegisterFactory(_logFactory);

            services.AddConsul();
            services.AddFabio();
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
