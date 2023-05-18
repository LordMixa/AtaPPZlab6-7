using AtaPPZlab6_7;
using AtaPPZlab6_7.Controllers;
using Autofac;
using BLL;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace UI
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
            services.AddScoped<ShowService>();
            services.AddControllersWithViews();
            services.AddControllers();
            services.AddRazorPages();
            services.AddDbContext<ShowContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MyDatabase")));
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacModule());
            builder.RegisterType<ShowService>().AsSelf().InstancePerLifetimeScope();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseDefaultFiles();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
