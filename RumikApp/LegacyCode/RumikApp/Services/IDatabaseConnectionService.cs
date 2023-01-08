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
        Task<ObservableCollection<Beverage>> GetAllData();
        Task<ObservableCollection<Beverage>> GetAllPiratesBeverages();
        Task<ObservableCollection<Beverage>> GetDataFromDatabaseWithConditions(PollPurpose pollPurpose, int pollPurposeWeight, PollMixes pollMixes, List<Flavour> Flavours, PollPricePoints pollPricePoints);
        Task<Beverage> GetRandomRow(Random random = null);
        Task<bool> TestConnectionToDatabase();
        bool TestConnectionToTable(AvailableTables availableTables);
        Task<string> SaveBevreageToDatabase(Beverage beverage, byte[] img);
    }
}

