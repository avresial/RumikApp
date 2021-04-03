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
            ObservableCollection<Beverage> Beverages = new ObservableCollection<Beverage>();

            if (File.Exists(fileName))
            {
                using (StreamReader r = new StreamReader(fileName))
                {
                    string json2 = r.ReadToEnd();
                    List<JsonBeverage> items = JsonConvert.DeserializeObject<List<JsonBeverage>>(json2);
                    if (items != null)
                        foreach (JsonBeverage item in items)
                            Beverages.Add(JsonBeverage.TransFromJsonBeverageToBeverage(item));
                }
            }
            
            return Beverages;
        }

        private ObservableCollection<JsonBeverage> GetAllJsonData()
        {
            ObservableCollection<JsonBeverage> Beverages = new ObservableCollection<JsonBeverage>();

            if (File.Exists(fileName))
            {
                using (StreamReader r = new StreamReader(fileName))
                {
                    string json2 = r.ReadToEnd();
                    List<JsonBeverage> items = JsonConvert.DeserializeObject<List<JsonBeverage>>(json2);
                    if (items != null)
                        foreach (JsonBeverage item in items)
                            Beverages.Add(item);
                }
            }

            return Beverages;
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
            List<JsonBeverage> jsonBeverage = GetAllJsonData().ToList();
            jsonBeverage.Add(JsonBeverage.TransFromBeverageToJsonBeverage(beverage, img));

            //convert object to json string.
            string json = JsonConvert.SerializeObject(jsonBeverage);


            //export data to json file. 
            using (TextWriter tw = new StreamWriter(fileName))
            {
                tw.WriteLine(json);
            };

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
