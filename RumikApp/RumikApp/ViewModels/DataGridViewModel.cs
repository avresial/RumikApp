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

namespace RumikApp.ViewModels
{
    public class DataGridViewModel : ViewModelBase
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

        public DataGridViewModel(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;

        }

        public void Reload()
        {
            GetData();
        }

        void GetData()
        {
            string connectionStringSosek = CnnVal("sosek");

            using (MySqlConnection con = new MySqlConnection(CnnVal("sosek")))
            {
                Users = new ObservableCollection<Beverage>();
                //string oString = "SELECT * FROM (SELECT * FROM RumsBase ORDER BY id DESC LIMIT 4) sub ORDER BY id ASC";
                string oString = "SELECT * FROM RumsBase ";

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
    }
}
