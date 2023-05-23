using Autofac;
using BLL;
using Microsoft.OpenApi.Models;
using DAL;
using Microsoft.EntityFrameworkCore;

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
            services.AddHttpContextAccessor();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.UseAllOfForInheritance();
                c.CustomSchemaIds(type => type.FullName);
                c.SelectSubTypesUsing(baseType => baseType.Assembly.GetTypes().Where(type => type.IsSubclassOf(baseType)));
            });
            services.AddTransient<IService, ShowService>();
            services.AddTransient<IService, TicketService>();

            services.AddTransient<IService>(provider =>
            {
                var context = provider.GetService<IHttpContextAccessor>();
                var url = context.HttpContext.Request.Path.Value.ToLower();

                if (url.Contains("/show") || url.Contains("/tickets/show"))
                {
                    return provider.GetService<ShowService>();
                }
                else if (url.Contains("/ticket") || url.Contains("/shows/ticket"))
                {
                    return provider.GetService<TicketService>();
                }
                return provider.GetService<ShowService>();
            });
            services.AddDbContext<ShowContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MyDatabase")));
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacModule());
            builder.RegisterType<ShowService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<TicketService>().AsSelf().InstancePerLifetimeScope();
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
