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
                        beverageTMP.Name = reader.GetString(0);
                        beverageTMP.Capacity = reader.GetInt32(1);
                        beverageTMP.AlcoholPercentage = reader.GetFloat(2);
                        beverageTMP.Price = reader.GetFloat(3);
                        beverageTMP.Grade = reader.GetInt32(4);
                        beverageTMP.GradeWithCoke = reader.GetInt32(5);
                        beverageTMP.Color = reader.GetString(6);

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
