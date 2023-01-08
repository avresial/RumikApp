using RumikApp.UI.ViewModel;
using System.Windows;

namespace RumikApp.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel mainViewModel)
        {
            InitializeComponent();
            this.DataContext = mainViewModel;

            this.Show();
        }
    }
}
