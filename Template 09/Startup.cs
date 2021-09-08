using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Template_SQLite_AdoNet_EfCore.Models.Enums;
using Template_SQLite_EfCore.Models.Options;
using Template_SQLite_EfCore.Models.Services.Application;
using Template_SQLite_EfCore.Models.Services.Application.Profili;
using Template_SQLite_EfCore.Models.Services.Application.Utenti;
using Template_SQLite_EfCore.Models.Services.Infrastructure;

namespace Template_SQLite_EfCore
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

            //Servizi database per l'accesso ai dati: ADO.NET o Entity Framework Core?
            var persistence = Persistence.EfCore;
            switch (persistence)
            {
                case Persistence.AdoNet:
                    services.AddTransient<IUtenteService, AdoNetUtenteService>();
                    services.AddTransient<IProfiloService, AdoNetProfiloService>();
                    services.AddTransient<IDatabaseAccessor, SqliteDatabaseAccessor>();
                break;

                case Persistence.EfCore:

                    services.AddTransient<IUtenteService, EfCoreUtenteService>();
                    services.AddTransient<IProfiloService, EfCoreProfiloService>();
                    services.AddDbContextPool<MySQLiteEfCoreDbContext>(optionsBuilder => {
                        string connectionString = Configuration.GetSection("ConnectionStrings").GetValue<string>("Default");
                        optionsBuilder.UseSqlite(connectionString);
                    });
                break;
            }

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
