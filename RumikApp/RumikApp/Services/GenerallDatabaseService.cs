using RumikApp.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumikApp.Services
{
    class GenerallDatabaseService : IDatabaseConnectionService
    {
        private SQLDatabaseConnectionService sQLDatabaseConnectionService;
        private FileDatabaseConnectionService fileDatabaseConnectionService;

        /// <summary>
        /// Generall database service is a service that gets you required data from multiple sources at once
        /// </summary>
        /// <param name="sQLDatabaseConnectionService"></param>
        /// <param name="fileDatabaseConnectionService"></param>
        public GenerallDatabaseService(SQLDatabaseConnectionService sQLDatabaseConnectionService, FileDatabaseConnectionService fileDatabaseConnectionService)
        {
            this.sQLDatabaseConnectionService = sQLDatabaseConnectionService;
            this.fileDatabaseConnectionService = fileDatabaseConnectionService;

            MainDataTable = sQLDatabaseConnectionService.MainDataTable;
            NotYetApprovedTESTDataTable = sQLDatabaseConnectionService.NotYetApprovedTESTDataTable;
        }

        private AvailableTables _MainDataTable;
        public AvailableTables MainDataTable
        {
            get { return _MainDataTable; }
            set
            {
                if (_MainDataTable == value)
                    return;
                _MainDataTable = value;
            }
        }

        private AvailableTables _NotYetApprovedTESTDataTable;
        public AvailableTables NotYetApprovedTESTDataTable
        {
            get { return _NotYetApprovedTESTDataTable; }
            set
            {
                if (_NotYetApprovedTESTDataTable == value)
                    return;
                _NotYetApprovedTESTDataTable = value;
            }
        }

        public string CnnVal(string name)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Beverage> GetAllData()
        {
            ObservableCollection<Beverage> fromFile = fileDatabaseConnectionService.GetAllData();
            ObservableCollection<Beverage> FromSQLDatabase = sQLDatabaseConnectionService.GetAllData();
            //return fromFile;
            return FromSQLDatabase;
        }

        public ObservableCollection<Beverage> GetAllPiratesBeverages()
        {
            return sQLDatabaseConnectionService.GetAllPiratesBeverages();
        }

        public ObservableCollection<Beverage> GetDataFromDatabaseWithConditions(PollPurpose pollPurpose, int pollPurposeWeight, PollMixes pollMixes, List<Flavour> Flavours, PollPricePoints pollPricePoints)
        {
            return sQLDatabaseConnectionService.GetDataFromDatabaseWithConditions(pollPurpose, pollPurposeWeight, pollMixes, Flavours, pollPricePoints);
        }

        public Beverage GetRandomRow()
        {
            return sQLDatabaseConnectionService.GetRandomRow();
        }

        public string SaveBevreageToDatabase(Beverage beverage, byte[] img)
        {
            fileDatabaseConnectionService.SaveBevreageToDatabase(beverage, img);

            return sQLDatabaseConnectionService.SaveBevreageToDatabase(beverage, img);
        }

        public bool TestConnectionToDatabase()
        {
            return sQLDatabaseConnectionService.TestConnectionToDatabase();
        }

        public bool TestConnectionToTable(AvailableTables availableTables)
        {
            return sQLDatabaseConnectionService.TestConnectionToTable(availableTables);
        }
    }
}
