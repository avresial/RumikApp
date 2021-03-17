using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MySql.Data.MySqlClient;
using RumikApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

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

        private ObservableCollection<Beverage> _Users = new ObservableCollection<Beverage>();
        public ObservableCollection<Beverage> Users
        {
            get { return _Users; }
            set
            {
                if (_Users == value)
                    return;

                _Users = value;
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
        private bool _Vanily;
        public bool Vanily
        {
            get { return _Vanily; }
            set
            {
                if (_Vanily == value)
                    return;

                _Vanily = value;
                RaisePropertyChanged("Vanily");
            }
        }

        private bool _Honey;
        public bool Honey
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

        private bool _Smoked;
        public bool Smoked
        {
            get { return _Smoked; }
            set
            {
                if (_Smoked == value)
                    return;

                _Smoked = value;
                RaisePropertyChanged("Smoked");
            }
        }

        private bool _Other;
        public bool Other
        {
            get { return _Other; }
            set
            {
                if (_Other == value)
                    return;

                _Other = value;
                RaisePropertyChanged("Other");
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
                        mainViewModel.DataGridViewModel2.Users = Users;
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
                for (int i = 0; i < conditions.Count; i++)
                {
                    if (i != 0)
                        oString += " and ";

                    oString += conditions[i];
                }
            }
            else
            {
                oString = $"SELECT * FROM RumsBase";
            }

            Users = mainViewModel.DatabaseConnectionService.GetData(oString);

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

            if (Vanily)
                flavoursList.Add("Vanilly = 1");

            if (Honey)
                flavoursList.Add("Honey = 1");

            if (Smoked)
                flavoursList.Add("Smoky = 1");


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

        public string CnnVal(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        public PollViewModel(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
        }
    }
}
