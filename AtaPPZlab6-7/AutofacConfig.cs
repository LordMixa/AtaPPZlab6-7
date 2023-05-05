using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using System.Web;
using System.Web.Mvc;

namespace AtaPPZlab6_7
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            // получаем экземпляр контейнера
            var builder = new ContainerBuilder();

            // регистрируем контроллер в текущей сборке
            builder.RegisterControllers(typeof(Program).Assembly);

            // регистрируем споставление типов
            //builder.RegisterType<Show>().As<IRepository>();

            // создаем новый контейнер с теми зависимостями, которые определены выше
            var container = builder.Build();

            // установка сопоставителя зависимостей
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
