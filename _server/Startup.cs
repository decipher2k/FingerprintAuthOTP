using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Pomelo.EntityFrameworkCore.MySql;

namespace AOTA_Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

#if DEBUG 
            services.AddDbContext<Models.BuildingContext>(opt =>
                opt.UseMySql("server=localhost;userid=root;password=Bl4f4s3L;database=fpauth"), ServiceLifetime.Transient);
#else
            services.AddDbContext<Models.BuildingContext>(opt =>
                opt.UseMySql("server=localhost;userid=wordpress;password=Deskjet1;database=aota"), ServiceLifetime.Transient);
#endif

            /*
            services.AddDbContext<Models.BuildingTypeContext>(opt =>            
                opt.UseMySql("server=localhost;user id=root;database=aota"));
               
            services.AddDbContext<Models.SessionContext>(opt =>
                opt.UseMySql("server=localhost;user id=root;database=aota"));
               
            services.AddDbContext<Models.PlayerDataContext>(opt =>
                opt.UseMySql("server=localhost;user id=root;database=aota"));
                */

            services.AddHostedService<ResourcesWorker>();

            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
            });




            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
             //   app.UseHsts();
            }

            app.UseCors(options => options.AllowAnyOrigin());

            //  app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
