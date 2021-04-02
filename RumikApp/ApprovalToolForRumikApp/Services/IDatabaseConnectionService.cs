using MySql.Data.MySqlClient;
using ApprovalToolForRumikApp.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprovalToolForRumikApp.Services
{
    public interface IDatabaseConnectionService
    {
        AvailableTables MainDataTable { get; set; }
        AvailableTables NotYetApprovedTESTDataTable { get; set; }
        ObservableCollection<Beverage> GetAllData();
        ObservableCollection<Beverage> GetAllPiratesBeverages();
        ObservableCollection<Beverage> GetDataFromDatabaseWithConditions(List<string> conditions);
        Beverage GetRandomRow();
        int GetNumberOfRows();
        bool TestConnectionToDatabase();
        bool TestConnectionToTable(AvailableTables availableTables);
        string SaveBevreageToDatabase(Beverage beverage, byte[] img);
        string DeleteBevreageFromDatabase(Beverage beverage);
        string CnnVal(string name);
    }
}

