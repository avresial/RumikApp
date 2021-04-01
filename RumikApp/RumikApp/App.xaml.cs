using Autofac;
using RumikApp.Services;
using RumikApp.UserControls;
using RumikApp.ViewModel;
using RumikApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace RumikApp
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
            var MainControlPanelViewModel = scope.Resolve<MainControlPanelViewModel>();


            var mainViewModel = scope.Resolve<MainViewModel>();
            var mainWindow = new MainWindow(mainViewModel);
            mainWindow.Show();


            //base.OnStartup(e);
        }
    }
}
