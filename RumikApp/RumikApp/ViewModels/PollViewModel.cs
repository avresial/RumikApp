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
        private Int16 _Vanily;
        public Int16 Vanily
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

        private Int16 _Honey;
        public Int16 Honey
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
                RaisePropertyChanged("LessThen50");
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
            string connectionStringSosek = CnnVal("sosek");

            string whers = null;

            if (PricePoint1)
                whers += " Price < 90";

            if (PricePoint2)
                whers += " Price >= 50 and Price < 70";

            if (PricePoint3)
                whers += " Price >= 70 and Price < 90";

            if (PricePoint4)
                whers += " Price >= 90";


            using (MySqlConnection con = new MySqlConnection(CnnVal("sosek")))
            {
                Users = new ObservableCollection<Beverage>();

                string oString = $"SELECT * FROM RumsBase WHERE Vanilly = {Vanily} and Honey = {Honey} and" + whers;

                oString = $"SELECT * FROM RumsBase";

                if (whers != null)
                    oString = $"SELECT * FROM RumsBase WHERE " + whers;

                MySqlCommand cmd0 = new MySqlCommand(oString, con);

                con.Open();

                using (MySqlDataReader reader = cmd0.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Beverage beverageTMP = new Beverage();
                        beverageTMP.ID = reader.GetInt32(0);
                        beverageTMP.Name = reader.GetString(1);
                        beverageTMP.Capacity = reader.GetInt32(2);
                        beverageTMP.AlcoholPercentage = reader.GetFloat(3);
                        beverageTMP.Price = reader.GetFloat(4);
                        beverageTMP.Grade = reader.GetInt32(5);
                        beverageTMP.GradeWithCoke = reader.GetInt32(6);
                        beverageTMP.Color = reader.GetString(7);

                        if (reader.GetInt16(8) == 1)
                            beverageTMP.Vanila = true;
                        else
                            beverageTMP.Vanila = false;

                        if (reader.GetInt16(9) == 1)
                            beverageTMP.Nuts = true;
                        else
                            beverageTMP.Nuts = false;

                        if (reader.GetInt16(10) == 1)
                            beverageTMP.Caramel = true;
                        else
                            beverageTMP.Caramel = false;

                        if (reader.GetInt16(11) == 1)
                            beverageTMP.Smoke = true;
                        else
                            beverageTMP.Smoke = false;

                        if (reader.GetInt16(12) == 1)
                            beverageTMP.Cinnamon = true;
                        else
                            beverageTMP.Cinnamon = false;

                        if (reader.GetInt16(13) == 1)
                            beverageTMP.Nutmeg = true;
                        else
                            beverageTMP.Nutmeg = false;

                        if (reader.GetInt16(14) == 1)
                            beverageTMP.Fruits = true;
                        else
                            beverageTMP.Fruits = false;

                        if (reader.GetInt16(15) == 1)
                            beverageTMP.Honey = true;
                        else
                            beverageTMP.Honey = false;


                        Users.Add(beverageTMP);

                    }
                }
                con.Close();
            }
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
