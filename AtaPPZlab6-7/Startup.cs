using Autofac;
using BLL;
using Microsoft.OpenApi.Models;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;

namespace AtaPPZlab6_7
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
            services.AddControllers();
            services.AddRazorPages();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.UseAllOfForInheritance();
                c.CustomSchemaIds(type => type.FullName);
                c.SelectSubTypesUsing(baseType => baseType.Assembly.GetTypes().Where(type => type.IsSubclassOf(baseType)));
            });
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

            app.UseRouting();

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
