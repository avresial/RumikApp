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
        AvailableTables NotYetApprovedTESTDataTable { get; set; }
        ObservableCollection<Beverage> GetAllData();
        ObservableCollection<Beverage> GetAllPiratesBeverages();
        ObservableCollection<Beverage> GetDataFromDatabaseWithConditions(PollPurpose pollPurpose, int pollPurposeWeight, PollMixes pollMixes, List<Flavour> Flavours, PollPricePoints pollPricePoints);
        Beverage GetRandomRow(Random random = null);
        bool TestConnectionToDatabase();
        bool TestConnectionToTable(AvailableTables availableTables);
        string SaveBevreageToDatabase(Beverage beverage, byte[] img);
    }
}

