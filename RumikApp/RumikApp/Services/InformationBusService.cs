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

        public InformationBusService()
        {
        }

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

                if (value != null)
                    if (value.Count > 0)
                    {
                        IsBeverageNotEmpty = Visibility.Visible;
                        IsBeverageEmpty = Visibility.Collapsed;
                        _OriginalBeverages = value; 
                        ShowDefault();
                    }
                    else
                    {
                        IsBeverageNotEmpty = Visibility.Collapsed;
                        IsBeverageEmpty = Visibility.Visible;
                    }
                
                RaisePropertyChanged(nameof(OriginalBeverages));
            }
        }
         
        public void ShowDefault()
        {
            SortedBeverages = _OriginalBeverages;
        }

        public void SortByNameAscending()
        {
            SortedBeverages = new ObservableCollection<Beverage>(OriginalBeverages.OrderBy(x => x.Name));
        }

        public void SortByNameDescending()
        {
            SortedBeverages = new ObservableCollection<Beverage>(OriginalBeverages.OrderByDescending(x => x.Name));
        }

        public void SortByPriceAscending()
        {
            SortedBeverages = new ObservableCollection<Beverage>(OriginalBeverages.OrderBy(x => x.Price));
        }

        public void SortByPriceDescending()
        {
            SortedBeverages = new ObservableCollection<Beverage>(OriginalBeverages.OrderByDescending(x => x.Price));
        }

        public void SortByGradeAscending()
        {
            SortedBeverages = new ObservableCollection<Beverage>(OriginalBeverages.OrderBy(x => x.Grade));
        }

        public void SortByGradeDescending()
        {
            SortedBeverages = new ObservableCollection<Beverage>(OriginalBeverages.OrderByDescending(x => x.Grade));
        }

        public void SortByGradeWithCokeAscending()
        {
            SortedBeverages = new ObservableCollection<Beverage>(OriginalBeverages.OrderBy(x => x.GradeWithCoke));
        }

        public void SortByGradeWithCokeDescending()
        {
            SortedBeverages = new ObservableCollection<Beverage>(OriginalBeverages.OrderByDescending(x => x.GradeWithCoke));
        }
    }
}
