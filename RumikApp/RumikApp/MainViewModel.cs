using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace RumikApp.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        private string connectionStringSosek;
        public MainViewModel()
        {
            
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