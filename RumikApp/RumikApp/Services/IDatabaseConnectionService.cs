using MySql.Data.MySqlClient;
using RumikApp.Enums;
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
        AvailableTables MainDataTable { get; set; }
        ObservableCollection<Beverage> GetData(string Query);
        ObservableCollection<Beverage> GetAllData();
        ObservableCollection<Beverage> GetAllPiratesBeverages();
        ObservableCollection<Beverage> GetDataFromDatabaseWithConditions(List<string> conditions);
        Beverage GetRandomRow();
        bool TestConnectionToDatabase();
        bool TestConnectionToTable(AvailableTables availableTables);
        string SaveBevreageToDatabase(Beverage beverage, byte[] img);
        void SaveImageToDatabase(byte[] img);
        string CnnVal(string name);
    }
}

