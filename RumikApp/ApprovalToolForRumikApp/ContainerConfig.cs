using Autofac;
using ApprovalToolForRumikApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ApprovalToolForRumikApp
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
                                  
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            .Where(t => t.Name.EndsWith("Service")).As(t=> t.GetInterfaces().FirstOrDefault(i=>i.Name=="I"+t.Name)).SingleInstance();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            .Where(t => t.Name.EndsWith("ViewModel")).SingleInstance();

        
            return builder.Build();
        }
    }
}
