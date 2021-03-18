using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumikApp.Services
{
    public interface IDatabaseConnectionService
    {
        ObservableCollection<Beverage> GetData(string Query);
        ObservableCollection<Beverage> GetAllData();
   
        string CnnVal(string name);
    }
}

