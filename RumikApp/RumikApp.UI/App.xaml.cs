using Autofac;
using RumikApp.UI.ViewModel;
using System.Windows;

namespace RumikApp.UI
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
            MainWindow mainWindow = new MainWindow(mainViewModel);
            mainWindow.Show();
        }
    }
}