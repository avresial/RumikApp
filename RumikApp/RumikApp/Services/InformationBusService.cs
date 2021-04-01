using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RumikApp.Services
{
    public class InformationBusService : ViewModelBase, IInformationBusService
    {

        private Visibility _IsBeverageEmpty;
        public Visibility IsBeverageEmpty
        {
            get { return _IsBeverageEmpty; }
            set
            {
                if (_IsBeverageEmpty == value)
                    return;

                _IsBeverageEmpty = value;
                RaisePropertyChanged(nameof(IsBeverageEmpty));
            }
        }

        private Visibility _IsBeverageNotEmpty;
        public Visibility IsBeverageNotEmpty
        {
            get { return _IsBeverageNotEmpty; }
            set
            {
                if (_IsBeverageNotEmpty == value)
                    return;

                _IsBeverageNotEmpty = value;
                RaisePropertyChanged(nameof(IsBeverageNotEmpty));
            }
        }

        private ObservableCollection<Beverage> _Beverages;
        public ObservableCollection<Beverage> Beverages
        {
            get { return _Beverages; }
            set
            {
                if (_Beverages == value)
                    return;

                if (value != null)
                    if (value.Count > 0)
                    {
                        IsBeverageNotEmpty = Visibility.Visible;
                        IsBeverageEmpty = Visibility.Collapsed;
                    }
                    else
                    {
                        IsBeverageNotEmpty = Visibility.Collapsed;
                        IsBeverageEmpty = Visibility.Visible;
                    }


                _Beverages = value;
                RaisePropertyChanged(nameof(Beverages));
            }
        }
    }
}
