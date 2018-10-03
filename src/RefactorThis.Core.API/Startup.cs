﻿using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RefactorThis.Core.Api.Configurations;
using RefactorThis.Core.Domain.Interfaces;
using RefactorThis.Core.Infra.CrossCutting.IoC;
using RefactorThis.Core.Infra.Data.Context;
using RefactorThis.Core.Infra.Data.Repository;
using RefactorThis.Core.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace RefactorThis.Core
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
            //  services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

/*            services.AddWebApi(options =>
            {
                options.OutputFormatters.Remove(new XmlDataContractSerializerOutputFormatter());
                options.UseCentralRoutePrefix(new RouteAttribute("api/v{version}"));
            });*/


            services.AddAutoMapperSetup();
            //  services.AddScoped<ILogger, Logger>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Products API", Version = "v1" });
            });

            //   var connection = @"data source=(LocalDB)\\MSSQLLocalDB;attachdbfilename=C:\\Users\\KoganTV\\source\\repos\\RefactorThis.Core\\RefactorThis.Core\\App_Data\\Database.mdf;integrated security=True;connect timeout=30;MultipleActiveResultSets=True;App=EntityFramework"  ;
            services.AddDbContext<ProductsContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            // Adding MediatR for Domain Events and Notifications
            services.AddMediatR(typeof(Startup));
            RegisterServices(services);
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Products V1");
            });

            app.UseMvc();
        }
        private static void RegisterServices(IServiceCollection services)
        {
            // Adding dependencies from another layers (isolated from Presentation)
            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}