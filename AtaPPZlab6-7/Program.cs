using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using BLL;
using DAL;
using Microsoft.AspNetCore.Hosting;

namespace AtaPPZlab6_7
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ShowService showService;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UserMappingProfile());
            });
            var mapper = config.CreateMapper();

            var serviceProvider = new ServiceCollection()
                .AddSingleton(mapper)
                .BuildServiceProvider();
            showService = new ShowService(serviceProvider.GetService<IMapper>());
            showService.GetShows();
            //CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
               .UseServiceProviderFactory(new AutofacServiceProviderFactory())
               .ConfigureWebHostDefaults(webBuilder =>
               {
                   webBuilder.UseStartup<Startup>();
               });
    }
}