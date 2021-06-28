using RumikApp.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumikApp.Services
{

    /// <summary>
    /// Connects data from database and disc to one stream.
    /// </summary>
    class GenerallDatabaseService : IGenerallDatabaseService
    {
        private ISQLDatabaseConnectionService sQLDatabaseConnectionService;
        private IFileDatabaseConnectionService fileDatabaseConnectionService;
        private bool doesSQLDatabaseConnectionServiceWasWarkingAtStart = false;

        /// <summary>
        /// Generall database service is a service that gets you required data from multiple sources at once
        /// </summary>
        /// <param name="sQLDatabaseConnectionService"></param>
        /// <param name="fileDatabaseConnectionService"></param>
        public GenerallDatabaseService(ISQLDatabaseConnectionService sQLDatabaseConnectionService, IFileDatabaseConnectionService fileDatabaseConnectionService)
        {
            this.sQLDatabaseConnectionService = sQLDatabaseConnectionService;
            this.fileDatabaseConnectionService = fileDatabaseConnectionService;

            MainDataTable = sQLDatabaseConnectionService.MainDataTable;
            NotYetApprovedTESTDataTable = sQLDatabaseConnectionService.NotYetApprovedTESTDataTable;

            doesSQLDatabaseConnectionServiceWasWarkingAtStart = sQLDatabaseConnectionService.TestConnectionToTable(MainDataTable);

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

        /// <summary>
        /// Loads data from database and adds to it records from disc - only if they are unique 
        /// </summary>
        /// <returns></returns>
        public async Task<ObservableCollection<Beverage>> GetAllData()
        {
            if (!doesSQLDatabaseConnectionServiceWasWarkingAtStart)
                return await fileDatabaseConnectionService.GetAllData();

            ObservableCollection<Beverage> FinalCollection = await sQLDatabaseConnectionService.GetAllData();

            if (FinalCollection == null)
                return await fileDatabaseConnectionService.GetAllData();

            return getUniqueBeverages(FinalCollection, await fileDatabaseConnectionService.GetAllData());
        }

        public async Task<ObservableCollection<Beverage>> GetAllPiratesBeverages()
        {
            if (!doesSQLDatabaseConnectionServiceWasWarkingAtStart)
                return await fileDatabaseConnectionService.GetAllPiratesBeverages();

            ObservableCollection<Beverage> FinalCollection = await sQLDatabaseConnectionService.GetAllPiratesBeverages();

            return getUniqueBeverages(FinalCollection, await fileDatabaseConnectionService.GetAllPiratesBeverages());
        }

        public async Task<ObservableCollection<Beverage>> GetDataFromDatabaseWithConditions(PollPurpose pollPurpose, int pollPurposeWeight, PollMixes pollMixes, List<Flavour> Flavours, PollPricePoints pollPricePoints)
        {
            if (!doesSQLDatabaseConnectionServiceWasWarkingAtStart)
                return await fileDatabaseConnectionService.GetDataFromDatabaseWithConditions(pollPurpose, pollPurposeWeight, pollMixes, Flavours, pollPricePoints);

            ObservableCollection<Beverage> SQLDataCollection = await sQLDatabaseConnectionService.GetDataFromDatabaseWithConditions(pollPurpose, pollPurposeWeight, pollMixes, Flavours, pollPricePoints);
            ObservableCollection<Beverage> FileDataCollection = await fileDatabaseConnectionService.GetDataFromDatabaseWithConditions(pollPurpose, pollPurposeWeight, pollMixes, Flavours, pollPricePoints);

            return getUniqueBeverages(SQLDataCollection, FileDataCollection);
        }

        public async Task<Beverage> GetRandomRow(Random random = null)
        {
            if (!doesSQLDatabaseConnectionServiceWasWarkingAtStart)
                return await fileDatabaseConnectionService.GetRandomRow();

            Beverage FinalCollection = await sQLDatabaseConnectionService.GetRandomRow();

            if (FinalCollection == null)
                return FinalCollection = await fileDatabaseConnectionService.GetRandomRow();

            return FinalCollection;
        }

        public async Task<string> SaveBevreageToDatabase(Beverage beverage, byte[] img)
        {
            if (!doesSQLDatabaseConnectionServiceWasWarkingAtStart)
                return await fileDatabaseConnectionService.SaveBevreageToDatabase(beverage, img);

            var task1 = fileDatabaseConnectionService.SaveBevreageToDatabase(beverage, img);
            var task2 = sQLDatabaseConnectionService.SaveBevreageToDatabase(beverage, img);

            Task.WaitAll(task1, task2);

            return task2.Result;
        }

        public async Task<bool> TestConnectionToDatabase()
        {
            return await sQLDatabaseConnectionService.TestConnectionToDatabase();
        }

        public bool TestConnectionToTable(AvailableTables availableTables)
        {
            return sQLDatabaseConnectionService.TestConnectionToTable(availableTables);
        }

        private ObservableCollection<Beverage> getUniqueBeverages(ObservableCollection<Beverage> mainList, ObservableCollection<Beverage> aditionalList)
        {
            if (mainList == null)
                return aditionalList;

            ObservableCollection<Beverage> UniqueBeverages = mainList;

            foreach (Beverage fileBeverage in aditionalList)
            {
                IEnumerable<Beverage> whereBeverage = mainList.Where(x => x.Name == fileBeverage.Name);

                if (whereBeverage.Count() == 0)
                    UniqueBeverages.Add(fileBeverage);

            }

            return UniqueBeverages;
        }

    }
}
