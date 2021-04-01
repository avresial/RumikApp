using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumikApp.Services
{
    public class InformationBusService : ViewModelBase, IInformationBusService
    {
        private ObservableCollection<Beverage> _Beverages;
        public ObservableCollection<Beverage> Beverages
        {
            get { return _Beverages; }
            set
            {
                if (_Beverages == value)
                    return;

                _Beverages = value;
                RaisePropertyChanged(nameof(Beverages));
            }
        }
    }
}
