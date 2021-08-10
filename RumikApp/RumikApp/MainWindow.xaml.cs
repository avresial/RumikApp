using RumikApp.ViewModel;
using System.Windows;

namespace RumikApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
    //    public MainWindow()
    //    {
    //        InitializeComponent();
    //        //this.DataContext = new MainViewModel();
            
    //        this.Show();
    //    }

        public MainWindow(MainViewModel mainViewModel)
        {
            InitializeComponent();
            this.DataContext = mainViewModel;

            this.Show();
        }
    }
}
