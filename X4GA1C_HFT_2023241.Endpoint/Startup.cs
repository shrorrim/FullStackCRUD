using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Numerics;
using X4GA1C_HFT_2023241.Repository;
using X4GA1C_HFT_2023241.Repository.Repositories;
using X4GA1C_HFT_2023241.Logic;
using Microsoft.AspNetCore.Diagnostics;

namespace X4GA1C_HFT_2023241.Endpoint
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //ha hibákat fog dobálni akkor singleton:
            services.AddTransient<LaptopWebShopDbContext>();

            services.AddTransient<IBrandRepository, BrandRepository>();
            services.AddTransient<ILaptopRepository, LaptopRepository>();
            services.AddTransient<IOrdererRepository, OrdererRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();

            services.AddTransient<IBrandLogic, BrandLogic>();
            services.AddTransient<ILaptopLogic, LaptopLogic>();
            services.AddTransient<IOrdererLogic, OrdererLogic>();
            services.AddTransient<IOrderLogic, OrderLogic>();

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "X4GA1C_HFT_2023241.Endpoint", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.RoutePrefix = string.Empty;
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "X4GA1C_HFT_2023241.Endpoint v1");
                }); 
                
            }



            app.UseExceptionHandler(c => c.Run(async context =>
            {
                var exception = context.Features
                .Get<IExceptionHandlerPathFeature>().Error;

                var response = new { Msg = exception.Message };
                await context.Response.WriteAsJsonAsync(response);
            }));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
