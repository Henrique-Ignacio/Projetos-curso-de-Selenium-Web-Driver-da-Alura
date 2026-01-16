using Alura.LeilaoOnline.Core;
using Alura.LeilaoOnline.WebApp.Dados;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Alura.LeilaoOnline.WebApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration cfg)
        {
            Configuration = cfg;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var cnxString = Configuration.GetConnectionString("LeiloesDB");

            services.AddDbContext<LeiloesContext>(options =>
                options.UseSqlServer(cnxString));

            services.AddTransient<IModalidadeAvaliacao, MaiorValor>();
            services.AddTransient<IRepositorio<Leilao>, RepositorioLeilao>();
            services.AddTransient<IRepositorio<Interessada>, RepositorioInteressada>();
            services.AddTransient<IRepositorio<Usuario>, RepositorioUsuario>();

            services.AddSession();

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
