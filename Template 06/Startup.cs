using System;
using System.IO;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Template_SQLite_AdoNet_Crud_AutoMapper.Models.Options;
using Template_SQLite_AdoNet_Crud_AutoMapper.Models.Services.Application.Profili;
using Template_SQLite_AdoNet_Crud_AutoMapper.Models.Services.Application.Utenti;
using Template_SQLite_AdoNet_Crud_AutoMapper.Models.Services.Infrastructure;

namespace Template_SQLite_AdoNet_Crud_AutoMapper
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
            services.AddMvc(options => 
            {
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
            #if DEBUG
            .AddRazorRuntimeCompilation()
            #endif
            ;

            //Services
            services.AddTransient<IUtenteService, AdoNetUtenteService>();
            services.AddTransient<IProfiloService, AdoNetProfiloService>();
            services.AddTransient<IDatabaseAccessor, SqliteDatabaseAccessor>();
            
            services.AddAutoMapper(typeof(Startup));

            //Options
            services.Configure<ConnectionStringsOptions>(Configuration.GetSection("ConnectionStrings"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)
        {
            if (env.IsEnvironment("Development"))
            {
                app.UseDeveloperExceptionPage();

                lifetime.ApplicationStarted.Register(() =>
                {
                    string filePath = Path.Combine(env.ContentRootPath, "bin/reload.txt");
                    File.WriteAllText(filePath, DateTime.Now.ToString());
                });
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(routeBuilder => {
                routeBuilder.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}