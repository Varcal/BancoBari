using System;
using System.IO;
using System.Reflection;
using BancoBari.Infra.CrossCutting;
using BancoBari.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;


namespace BancoBari.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            IocConfig.RegisterServices(Configuration, services);
            services.Configure<MessageSettings>(Configuration.GetSection("RabbitMQSettings"));


            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Banco Bari Api",
                    Description = "Projeto prático - Desenvolvedor Back-end",
                    Contact = new OpenApiContact
                    {
                        Name = "Cleber Varçal",
                        Email = "cleber.varcal@outlook.com",
                        Url = new Uri("https://github.com/varcal/BancoBari")
                    }

                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                config.IncludeXmlComments(xmlPath);
            });
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Banco Bari API V1");
                c.RoutePrefix = string.Empty;
            });
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
