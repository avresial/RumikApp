using Autofac;
using RumikApp.Core.Services;
using System.IO.Abstractions;

namespace RumikApp.UI.IoC.Modules
{
    class ServicesModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PanelVisibilityService>().As<IPanelVisibilityService>().SingleInstance();
            builder.RegisterType<FileSystem>().As<IFileSystem>().SingleInstance();
            builder.RegisterType<StreamReaderService>().As<IStreamReaderService>().SingleInstance();
             
        }
    }
}
