using RumikApp.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumikApp.Services
{
    class FileDatabaseConnectionService : IDatabaseConnectionService
    {
        public AvailableTables MainDataTable { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public AvailableTables NotYetApprovedTESTDataTable { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string CnnVal(string name)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Beverage> GetAllData()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Beverage> GetAllPiratesBeverages()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Beverage> GetDataFromDatabaseWithConditions(PollPurpose pollPurpose, int pollPurposeWeight, PollMixes pollMixes, List<Flavour> Flavours, PollPricePoints pollPricePoints)
        {
            throw new NotImplementedException();
        }

        public Beverage GetRandomRow()
        {
            throw new NotImplementedException();
        }

        public string SaveBevreageToDatabase(Beverage beverage, byte[] img)
        {
            throw new NotImplementedException();
        }

        public bool TestConnectionToDatabase()
        {
            throw new NotImplementedException();
        }

        public bool TestConnectionToTable(AvailableTables availableTables)
        {
            throw new NotImplementedException();
        }
    }
}
