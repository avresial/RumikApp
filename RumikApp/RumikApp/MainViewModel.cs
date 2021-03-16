using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MySql.Data.MySqlClient;
using RumikApp.UserControls;
using RumikApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;

namespace RumikApp.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        private string connectionStringSosek;

        private MainControlPanelViewModel _MainControlPanelViewModel;
        public MainControlPanelViewModel MainControlPanelViewModel
        {
            get { return _MainControlPanelViewModel; }
            set
            {
                if (_MainControlPanelViewModel == value)
                    return;

                _MainControlPanelViewModel = value;
                RaisePropertyChanged("MainControlPanelViewModel");
            }
        }

        private PollViewModel _PollViewModel;
        public PollViewModel PollViewModel
        {
            get { return _PollViewModel; }
            set
            {
                if (_PollViewModel == value)
                    return;

                _PollViewModel = value;
                RaisePropertyChanged("PollViewModel");
            }
        }

        private DataGridViewModel _DataGridViewModel;
        public DataGridViewModel DataGridViewModel
        {
            get { return _DataGridViewModel; }
            set
            {
                if (_DataGridViewModel == value)
                    return;

                _DataGridViewModel = value;
                RaisePropertyChanged("DataGridViewModel");
            }
        }

        private DataGridViewModel _DataGridViewModel2;
        public DataGridViewModel DataGridViewModel2
        {
            get { return _DataGridViewModel2; }
            set
            {
                if (_DataGridViewModel2 == value)
                    return;

                _DataGridViewModel2 = value;
                RaisePropertyChanged("DataGridViewModel2");
            }
        }

        private DataGridViewModel _ItemsControl;
        public DataGridViewModel ItemsControl
        {
            get { return _ItemsControl; }
            set
            {
                if (_ItemsControl == value)
                    return;

                _ItemsControl = value;
                RaisePropertyChanged("ItemsControl");
            }
        }

        public MainViewModel()
        {
            PollViewModel = new PollViewModel(this);
            MainControlPanelViewModel = new MainControlPanelViewModel(this);
            DataGridViewModel = new DataGridViewModel(this);
            DataGridViewModel2 = new DataGridViewModel(this);

            ItemsControl = new DataGridViewModel(this);

            Random rand = new Random();
            for (int i = 0; i < 15; i++)
                ItemsControl.Users.Add(new Beverage().GetRandomBevrage(rand));


        }

        private RelayCommand _Test;
        public RelayCommand Test
        {
            get
            {
                if (_Test == null)
                {
                    _Test = new RelayCommand(
                    () =>
                    {
                        test();
                    },
                    () =>
                    {
                        return true;
                    });
                }

                return _Test;
            }
        }

        private List<string> list1 = new List<string>();
        private List<string> list2 = new List<string>();

        void test()
        {
            connectionStringSosek = CnnVal("sosek");

            using (MySqlConnection con = new MySqlConnection(CnnVal("sosek")))
            {
                string oString = "SELECT * FROM (SELECT * FROM Pomiar ORDER BY id DESC LIMIT 4) sub ORDER BY id ASC";


                MySqlCommand cmd0 = new MySqlCommand(oString, con);

                con.Open();

                ;

                using (MySqlDataReader reader = cmd0.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var xd = reader.GetString(2);
                        list1.Add(reader.GetString(0));
                        list2.Add(reader.GetString(0));
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