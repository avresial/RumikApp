using GalaSoft.MvvmLight;
using RumikApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RumikApp.UserControls
{
    public class PollViewModel : ViewModelBase
    {
        private MainViewModel mainViewModel;
        private Visibility _Visibility = Visibility.Collapsed;
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
        public PollViewModel(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
        }
    }
}
