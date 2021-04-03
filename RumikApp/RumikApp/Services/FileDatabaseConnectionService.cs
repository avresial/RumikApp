using RumikApp.Enums;
using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Newtonsoft.Json;

namespace RumikApp.Services
{
    class FileDatabaseConnectionService : IDatabaseConnectionService
    {

        private string directory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RumikApp";
        private string fileName = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RumikApp" + "\\LocalDatabase.json";
        public AvailableTables MainDataTable { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public AvailableTables NotYetApprovedTESTDataTable { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public ObservableCollection<Beverage> GetAllData()
        {
            if (File.Exists(fileName))
            {
                using (StreamReader r = new StreamReader(fileName))
                {
                    string json2 = r.ReadToEnd();
                    List<Beverage> items = JsonConvert.DeserializeObject<List<Beverage>>(json2);
                }

            }
            return new ObservableCollection<Beverage>();
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

            List<Beverage> Beverages = new List<Beverage>();
            Beverages.Add(beverage);
            Beverages.Add(beverage);
            Beverages.Add(beverage);


            //convert object to json string.
            string json = JsonConvert.SerializeObject(Beverages);


            //export data to json file. 
            using (TextWriter tw = new StreamWriter(fileName))
            {
                tw.WriteLine(json);
            };



            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            if (!File.Exists(fileName))
                File.Create(fileName);

            return "done";
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
