using ApprovalToolForRumikApp.ViewModels;
using Autofac;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ApprovalToolForRumikApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var container = ContainerConfig.Configure();
            var scope = container.BeginLifetimeScope();
            var mainViewModel = scope.Resolve<MainViewModel>();
            var mainWindow = new MainWindow(mainViewModel);
            mainWindow.Show();


            base.OnStartup(e);
        }
    }
}
