using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using API.Middleware;
using StackExchange.Redis;

namespace API {
    public class Startup {

        private readonly IConfiguration Configuration;
        private readonly string CorsPolicyName = "CorsPolicy";

        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddScoped<StoreContextSeed>();
            services.AddAutoMapper(this.GetType().Assembly);

            services.AddControllers();

            services.AddServicesToApi();

            services.AddCors(option =>
                option.AddPolicy(CorsPolicyName, policy =>{
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins(Configuration["FrontEndClient"]);
                })
            );

            services.AddDbContext<StoreContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            services.AddSingleton<ConnectionMultiplexer>(c=>{
                var configuration = ConfigurationOptions
                    .Parse(Configuration.GetConnectionString("Redis"),true);

                return ConnectionMultiplexer.Connect(configuration);
            });

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, StoreContextSeed seeder) {

            seeder.Seed();

            app.UseMiddleware<ExceptionMiddleware>();

            if (env.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseStaticFiles();

            app.UseCors(CorsPolicyName);

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
