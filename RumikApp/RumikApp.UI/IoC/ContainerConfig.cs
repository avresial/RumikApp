using Autofac;
using RumikApp.Infrastructure.Respositories;
using RumikApp.Infrastructure.Services;
using RumikApp.UI.IoC.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RumikApp
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(typeof(ViewModelsModule).Assembly);
            builder.RegisterAssemblyModules(typeof(ServicesModule).Assembly);

         

            builder.RegisterType<BeverageService>().AsImplementedInterfaces();
#if DEBUG

            builder.RegisterType<InMemoryBeverageRepository>().AsImplementedInterfaces();
#endif

#if RELEASE
            builder.RegisterType<MySql>().AsImplementedInterfaces();
#endif
            //builder.RegisterType<SettingsService>().As<ISettingsService>().SingleInstance();
            //builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            //.Where(t => t.Name.EndsWith("Service"))
            //.As(t=> t.GetInterfaces().FirstOrDefault(i=>i.Name=="I"+t.Name)).SingleInstance();

            return builder.Build();
        }
    }
}
