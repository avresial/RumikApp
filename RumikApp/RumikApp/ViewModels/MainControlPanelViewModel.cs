using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using RumikApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RumikApp.ViewModels
{
    public class MainControlPanelViewModel : ViewModelBase
    {
        private MainViewModel mainViewModel;
        public MainControlPanelViewModel(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
        }

        private Visibility _Visibility = Visibility.Visible;
        public Visibility Visibility
        {
            get { return _Visibility; }
            set
            {
                if (_Visibility == value)
                    return;

                _Visibility = value;
                RaisePropertyChanged("Visibility");

            }
        }

      
        private RelayCommand _GetMeARum;
        public RelayCommand GetMeARum
        {
            get
            {
                if (_GetMeARum == null)
                {
                    _GetMeARum = new RelayCommand(
                    () =>
                    {
                        Visibility = Visibility.Collapsed;
                        mainViewModel.PollViewModel.Visibility = Visibility.Visible;
                    },
                    () =>
                    {
                        return true;
                    });
                }

                return _GetMeARum;
            }
        }

    }
}
