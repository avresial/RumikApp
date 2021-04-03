using Autofac;
using RumikApp.Services;
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
            var builder = new ContainerBuilder();

            builder.RegisterType<PanelVisibilityService>().As<IPanelVisibilityService>().SingleInstance();

            builder.RegisterType<InformationBusService>().As<IInformationBusService>().SingleInstance();

            builder.RegisterType<SQLDatabaseConnectionService>().AsSelf().SingleInstance();

            builder.RegisterType<FileDatabaseConnectionService>().AsSelf().SingleInstance();

            builder.RegisterType<GenerallDatabaseService>().As<IDatabaseConnectionService>().SingleInstance();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
           .Where(t => t.Name.EndsWith("ViewModel")).SingleInstance();

            //builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            //.Where(t => t.Name.EndsWith("Service")).As(t=> t.GetInterfaces().FirstOrDefault(i=>i.Name=="I"+t.Name)).SingleInstance();

            return builder.Build();
        }
    }
}
