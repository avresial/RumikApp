using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumikApp.Services
{
    public class SortingService : ViewModelBase ,ISortingService
    {
        private IList<Beverage> _SortedBeverages = new ObservableCollection<Beverage>();
        public IList<Beverage> SortedBeverages
        {
            get { return _SortedBeverages; }
            set
            {
                if (_SortedBeverages == value)
                    return;

                _SortedBeverages = value;
                RaisePropertyChanged(nameof(SortedBeverages));
            }
        }

        private IList<Beverage> _OriginalBeverages = new ObservableCollection<Beverage>();
        public IList<Beverage> OriginalBeverages
        {
            get { return _OriginalBeverages; }
            set
            {
                if (_OriginalBeverages == value)
                    return;

                _OriginalBeverages = value;
                RaisePropertyChanged(nameof(OriginalBeverages));
            }
        }

        public void SortByName()
        {
            SortedBeverages = new ObservableCollection<Beverage>(OriginalBeverages.OrderBy(x => x.Name));
        }
    }
}
