using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using RumikApp.ViewModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
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

        private ObservableCollection<Beverage> _Beverages = new ObservableCollection<Beverage>();
        public ObservableCollection<Beverage> Beverages
        {
            get { return _Beverages; }
            set
            {
                if (_Beverages == value)
                    return;

                _Beverages = value;
                RaisePropertyChanged("users");
            }
        }

        // first section - potrzebuję w celu

        private bool _ForPartyBool;
        public bool ForPartyBool
        {
            get { return _ForPartyBool; }
            set
            {
                if (_ForPartyBool == value)
                    return;

                if (value)
                {
                    GoodButCheap = false;
                    Exclusive = false;
                    ForPiratesFromCarabien = false;
                }

                _ForPartyBool = value;
                RaisePropertyChanged("ForPartyBool");
            }
        }

        private bool _GoodButCheap;
        public bool GoodButCheap
        {
            get { return _GoodButCheap; }
            set
            {
                if (_GoodButCheap == value)
                    return;

                if (value)
                {
                    ForPartyBool = false;
                    Exclusive = false;
                    ForPiratesFromCarabien = false;
                }

                _GoodButCheap = value;
                RaisePropertyChanged("GoodButCheap");
            }
        }

        private bool _Exclusive;
        public bool Exclusive
        {
            get { return _Exclusive; }
            set
            {
                if (_Exclusive == value)
                    return;

                if (value)
                {
                    ForPartyBool = false;
                    GoodButCheap = false;
                    ForPiratesFromCarabien = false;
                }

                _Exclusive = value;
                RaisePropertyChanged("Exclusive");
            }
        }

        private bool _ForPiratesFromCarabien;
        public bool ForPiratesFromCarabien
        {
            get { return _ForPiratesFromCarabien; }
            set
            {
                if (_ForPiratesFromCarabien == value)
                    return;

                if (value)
                {
                    ForPartyBool = false;
                    GoodButCheap = false;
                    Exclusive = false;
                }

                _ForPiratesFromCarabien = value;
                RaisePropertyChanged("ForPiratesFromCarabien");
            }
        }

        // second section - do picia
        private bool _solo;
        public bool solo
        {
            get { return _solo; }
            set
            {
                if (_solo == value)
                    return;
                if (value)
                    WithCoke = false;
                _solo = value;
                RaisePropertyChanged("solo");
            }
        }

        private bool _WithCoke;
        public bool WithCoke
        {
            get { return _WithCoke; }
            set
            {
                if (_WithCoke == value)
                    return;
                if (value)
                    solo = false;
                _WithCoke = value;
                RaisePropertyChanged("WithCoke");
            }
        }

        // third section - chce poczuc smak

        private Flavour _Vanila = new Flavour("/IMGs/PollIMG/Vanila.png", "Vanila");
        public Flavour Vanila
        {
            get { return _Vanila; }
            set
            {
                if (_Vanila == value)
                    return;

                _Vanila = value;
                RaisePropertyChanged("Vanila");
            }
        }

        private Flavour _Nuts = new Flavour("/IMGs/PollIMG/Nuts.png", "Nuts");
        public Flavour Nuts
        {
            get { return _Nuts; }
            set
            {
                if (_Nuts == value)
                    return;

                _Nuts = value;
                RaisePropertyChanged("Nuts");
            }
        }

        private Flavour _Carmel = new Flavour("/IMGs/PollIMG/Carmel.png", "Carmel");
        public Flavour Carmel
        {
            get { return _Carmel; }
            set
            {
                if (_Carmel == value)
                    return;

                _Carmel = value;
                RaisePropertyChanged("Caramel");
            }
        }

        private Flavour _Smoke = new Flavour("/IMGs/PollIMG/Smoked.png", "Smoke");
        public Flavour Smoke
        {
            get { return _Smoke; }
            set
            {
                if (_Smoke == value)
                    return;

                _Smoke = value;
                RaisePropertyChanged("Smoke");
            }
        }

        private Flavour _Cinnamon = new Flavour("/IMGs/PollIMG/Cinamon.png", "Cinnamon");
        public Flavour Cinnamon
        {
            get { return _Cinnamon; }
            set
            {
                if (_Cinnamon == value)
                    return;

                _Cinnamon = value;
                RaisePropertyChanged("Cinnamon");
            }
        }

        private Flavour _Spirit = new Flavour("/IMGs/PollIMG/Spirit.png", "Spirit");
        public Flavour Spirit
        {
            get { return _Spirit; }
            set
            {
                if (_Spirit == value)
                    return;

                _Spirit = value;
                RaisePropertyChanged("Spirit");
            }
        }

        private Flavour _Fruits = new Flavour("/IMGs/PollIMG/Fruits.png", "Fruits");
        public Flavour Fruits
        {
            get { return _Fruits; }
            set
            {
                if (_Fruits == value)
                    return;

                _Fruits = value;
                RaisePropertyChanged("Fruits");
            }
        }

        private Flavour _Honey = new Flavour("/IMGs/PollIMG/Honey.png", "Honey");
        public Flavour Honey
        {
            get { return _Honey; }
            set
            {
                if (_Honey == value)
                    return;

                _Honey = value;

                RaisePropertyChanged("Honey");
            }
        }

        // fourth section - price
        private bool _PricePoint1;
        public bool PricePoint1
        {
            get { return _PricePoint1; }
            set
            {
                if (_PricePoint1 == value)
                    return;
                if (value)
                {
                    PricePoint2 = false;
                    PricePoint3 = false;
                    PricePoint4 = false;
                }

                _PricePoint1 = value;
                RaisePropertyChanged("PricePoint1");
            }
        }

        private bool _PricePoint2;
        public bool PricePoint2
        {
            get { return _PricePoint2; }
            set
            {
                if (_PricePoint2 == value)
                    return;

                if (value)
                {
                    PricePoint1 = false;
                    PricePoint3 = false;
                    PricePoint4 = false;
                }

                _PricePoint2 = value;
                RaisePropertyChanged("PricePoint2");
            }
        }

        private bool __PricePoint3;
        public bool PricePoint3
        {
            get { return __PricePoint3; }
            set
            {
                if (__PricePoint3 == value)
                    return;

                if (value)
                {
                    PricePoint1 = false;
                    PricePoint2 = false;
                    PricePoint4 = false;
                }

                __PricePoint3 = value;
                RaisePropertyChanged("PricePoint3");
            }
        }

        private bool __PricePoint4;
        public bool PricePoint4
        {
            get { return __PricePoint4; }
            set
            {
                if (__PricePoint4 == value)
                    return;

                if (value)
                {
                    PricePoint1 = false;
                    PricePoint2 = false;
                    PricePoint3 = false;
                }

                __PricePoint4 = value;
                RaisePropertyChanged("PricePoint4");
            }
        }

        private RelayCommand _GoToMainMenu;
        public RelayCommand GoToMainMenu
        {
            get
            {
                if (_GoToMainMenu == null)
                {
                    _GoToMainMenu = new RelayCommand(
                    () =>
                    {
                        mainViewModel.MainControlPanelViewModel.Visibility = Visibility.Visible;
                        Visibility = Visibility.Collapsed;
                    },
                    () =>
                    {
                        return true;
                    });
                }

                return _GoToMainMenu;
            }
        }

        private RelayCommand _GetMeThatRum;
        public RelayCommand GetMeThatRum
        {
            get
            {
                if (_GetMeThatRum == null)
                {
                    _GetMeThatRum = new RelayCommand(
                    () =>
                    {
                        GetDataFromDatabase();
                        Visibility = Visibility.Collapsed;
                        mainViewModel.DataGridViewModel2.Visibility = Visibility.Visible;
                        mainViewModel.DataGridViewModel2.Beverages = Beverages;
                    },
                    () =>
                    {
                        return true;
                    });
                }

                return _GetMeThatRum;
            }
        }

        void GetDataFromDatabase()
        {

            List<string> conditions = getListOfConditions();
            string oString;

            if (conditions.Count > 0)
            {
                oString = $"SELECT * FROM RumsBase WHERE ";
                oString = $"SELECT * FROM RumsBaseTEST WHERE ";
                for (int i = 0; i < conditions.Count; i++)
                {
                    if (i != 0)
                        oString += " and ";

                    oString += conditions[i];
                }

                if (solo)
                    oString += " ORDER BY Grade DESC";
                else if (WithCoke)
                    oString += " ORDER BY GradeWithCoke DESC";

            }
            else
            {
                oString = $"SELECT * FROM RumsBase";
                oString = "SELECT * FROM RumsBaseTEST";
            }

            Beverages = mainViewModel.DatabaseConnectionService.GetData(oString);
        }

        List<string> getListOfConditions()
        {
            List<string> conditions = new List<string>();

            string price = getStringPrice();
            string soloOrCoke = getSoloOrWithCoke();
            string flavours = getFlavours();

            if (price != null && price != "")
                conditions.Add(price);

            if (soloOrCoke != null && soloOrCoke != "")
                conditions.Add(soloOrCoke);

            if (flavours != null && flavours != "")
                conditions.Add(flavours);

            return conditions;
        }

        string getStringPrice()
        {

            if (PricePoint1)
                return " Price < 50";

            if (PricePoint2)
                return " Price >= 50 and Price < 70";

            if (PricePoint3)
                return " Price >= 70 and Price < 90";

            if (PricePoint4)
                return " Price >= 90";

            return null;
        }

        string getSoloOrWithCoke()
        {
            int minimalAllowedGrade = 5;

            if (solo)
                return $"grade > {minimalAllowedGrade}";
            if (WithCoke)
                return $"GradeWithCoke > {minimalAllowedGrade}";

            return null;
        }

        string getFlavours()
        {

            List<string> flavoursList = new List<string>();
            string flavoursString = "";

            if (Vanila.IsSet)
                flavoursList.Add("Vanilly = 1");

            if (Nuts.IsSet)
                flavoursList.Add("Nuts = 1");

            if (Carmel.IsSet)
                flavoursList.Add("Carmel = 1");

            if (Smoke.IsSet)
                flavoursList.Add("Smoky = 1");

            if (Cinnamon.IsSet)
                flavoursList.Add("Cinnamon = 1");

            if (Spirit.IsSet)
                flavoursList.Add("Spirit = 1");

            if (Fruits.IsSet)
                flavoursList.Add("Fruits = 1");

            if (Honey.IsSet)
                flavoursList.Add("Honey = 1");


            if (flavoursList.Count > 0)
            {
                for (int i = 0; i < flavoursList.Count; i++)
                {
                    if (i != 0)
                        flavoursString += " and ";

                    flavoursString += flavoursList[i];
                }
                return flavoursString;
            }

            return null;
        }

        public PollViewModel(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
        }
    }
}
