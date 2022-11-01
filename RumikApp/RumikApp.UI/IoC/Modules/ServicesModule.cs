using Autofac;
using RumikApp.Core.Models;
using RumikApp.Core.Services;
using RumikApp.Infrastructure.Repositories;
using RumikApp.Infrastructure.Respositories;
using System.IO.Abstractions;

namespace RumikApp.UI.IoC.Modules
{
    class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PanelVisibilityService>().As<IPanelVisibilityService>().SingleInstance();
            builder.RegisterType<FileSystem>().As<IFileSystem>().SingleInstance();
            builder.RegisterType<StreamReaderService>().As<IStreamReaderService>().SingleInstance();
            builder.RegisterType<BeverageContainer>().AsSelf().SingleInstance();


#if DEBUG

           // builder.RegisterType<RandomBeverageRepository>().AsImplementedInterfaces();
            builder.RegisterType<InMemoryBeverageRepository>().AsImplementedInterfaces();
          
#endif

#if RELEASE
            builder.RegisterType<InMemoryBeverageRepository>().AsImplementedInterfaces();
            builder.RegisterType<MySql>().AsImplementedInterfaces();
#endif

        }
    }
}
