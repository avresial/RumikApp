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

        /// <summary>
        /// Loads data from database and adds to it records from disc - only if they are unique 
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Beverage> GetAllData()
        {
            ObservableCollection<Beverage> FinalCollection = sQLDatabaseConnectionService.GetAllData();

            if (FinalCollection == null)
                return fileDatabaseConnectionService.GetAllData();

            return getUniqueBeverages(FinalCollection, fileDatabaseConnectionService.GetAllData());
        }

        public ObservableCollection<Beverage> GetAllPiratesBeverages()
        {
            ObservableCollection<Beverage> FinalCollection = sQLDatabaseConnectionService.GetAllPiratesBeverages();

            //here will be file data handled

            return FinalCollection;
        }

        public ObservableCollection<Beverage> GetDataFromDatabaseWithConditions(PollPurpose pollPurpose, int pollPurposeWeight, PollMixes pollMixes, List<Flavour> Flavours, PollPricePoints pollPricePoints)
        {
            ObservableCollection<Beverage> SQLDataCollection = sQLDatabaseConnectionService.GetDataFromDatabaseWithConditions(pollPurpose, pollPurposeWeight, pollMixes, Flavours, pollPricePoints);
            ObservableCollection<Beverage> FileDataCollection = fileDatabaseConnectionService.GetDataFromDatabaseWithConditions(pollPurpose, pollPurposeWeight, pollMixes, Flavours, pollPricePoints);

            return getUniqueBeverages(SQLDataCollection, FileDataCollection);
        }

        public Beverage GetRandomRow()
        {
            Beverage FinalCollection = sQLDatabaseConnectionService.GetRandomRow();
           
            if (FinalCollection == null)
                return FinalCollection = fileDatabaseConnectionService.GetRandomRow();
           
            return FinalCollection;
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

        private ObservableCollection<Beverage> getUniqueBeverages(ObservableCollection<Beverage> mainList, ObservableCollection<Beverage> aditionalList)
        {

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
