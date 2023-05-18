using Autofac;
using AutoMapper;
using BLL;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Infrastructure;

namespace AtaPPZlab6_7
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(ctx => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UserMappingProfile());
            })).As<MapperConfiguration>().SingleInstance();

            builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>().InstancePerLifetimeScope();

            builder.RegisterType<ShowService>().InstancePerLifetimeScope();
        }
    }
}
