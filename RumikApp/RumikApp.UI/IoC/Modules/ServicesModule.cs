using Autofac;
using RumikApp.Core.Services;

namespace RumikApp.UI.IoC.Modules
{
    class ServicesModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PanelVisibilityService>().As<IPanelVisibilityService>().SingleInstance();
        }
    }
}
