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
using Newtonsoft.Json.Linq;
using RumikApp.Models;

namespace RumikApp.Services
{
    /// <summary>
    /// Class is used to save and load data from file located on local machine.
    /// Currently data is stored in Json files, maybe in future it will be changed binary files using binaryformatter
    /// </summary>
    public class FileDatabaseConnectionService : IFileDatabaseConnectionService
    {
        private readonly IFileService fileService;
        private readonly IStreamReaderService streamReader;
        private readonly string SettingsFileName = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RumikApp" + "\\Settings.json";

        #region CTOR
        public FileDatabaseConnectionService(IFileService fileService, IStreamReaderService streamReader)
        {
            this.fileService = fileService;
            this.streamReader = streamReader;
        }
        #endregion

        private string _MainDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RumikApp";
        public string MainDataDirectory
        {
            get { return _MainDataDirectory; }
            set
            {
                if (_MainDataDirectory == value)
                    return;

                _MainDataDirectory = value;

            }
        }

        private string _FileName = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RumikApp" + "\\LocalDatabase.json";
        public string FileName
        {
            get { return _FileName; }
            set
            {
                if (_FileName == value)
                    return;

                _FileName = value;

            }
        }

        Random rand = new Random();

        public AvailableTables MainDataTable { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public AvailableTables NotYetApprovedTESTDataTable { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public async Task<ObservableCollection<Beverage>> GetAllData()
        {

            ObservableCollection<Beverage> Beverages = new ObservableCollection<Beverage>();

            if (fileService.FileExists(FileName))
            {
                using (streamReader.Create(FileName))
                {
                    string json2 = streamReader.ReadToEnd();
                    List<JsonBeverage> items = JsonConvert.DeserializeObject<List<JsonBeverage>>(json2);
                    if (items != null)
                        foreach (JsonBeverage item in items)
                            Beverages.Add(JsonBeverage.TransFromJsonBeverageToBeverage(item));
                }
            }

            return Beverages;

        }

        public async Task<ObservableCollection<Beverage>> GetAllPiratesBeverages()
        {
            ObservableCollection<Beverage> finallList = new ObservableCollection<Beverage>();

            foreach (Beverage beverage in await GetAllData())
                if (beverage.BeAPirate.IsSet == true)
                    finallList.Add(beverage);

            return finallList;
        }

        public async Task<ObservableCollection<Beverage>> GetDataFromDatabaseWithConditions(PollPurpose pollPurpose, int pollPurposeWeight, PollMixes pollMixes, List<Flavour> Flavours, PollPricePoints pollPricePoints)
        {
            ObservableCollection<Beverage> finallList = new ObservableCollection<Beverage>();

            foreach (Beverage beverage in await GetAllData())
            {
                Beverage TMPBeverage = DoesBeverageFulfillSetPurposeRequirement(pollPurpose, pollPurposeWeight, beverage);

                if (TMPBeverage != null)
                    TMPBeverage = DoesBeverageFulfillSetMixesRequirement(pollMixes, beverage);

                if (TMPBeverage != null)
                    TMPBeverage = DoesBeverageFulfillSetFlavoursRequirement(Flavours, beverage);

                if (TMPBeverage != null)
                    TMPBeverage = DoesBeverageFulfillSetPriceRequirement(pollPricePoints, beverage);

                if (TMPBeverage != null)
                    finallList.Add(TMPBeverage);
            }

            return finallList;
        }

        public Beverage DoesBeverageFulfillSetPurposeRequirement(PollPurpose pollPurpose, int pollPurposeWeight, Beverage beverage)
        {
            switch (pollPurpose)
            {
                case PollPurpose.None:
                    return beverage;
                //break;

                case PollPurpose.ForPartyBool:
                    if (beverage.AlcoholPercentage / (100 * (beverage.Price / beverage.Capacity)) > pollPurposeWeight)
                        return beverage;

                    break;

                case PollPurpose.GoodButCheap:
                    if (((beverage.Grade + beverage.GradeWithCoke) / 2) / (100 * (beverage.Price / beverage.Capacity)) > 0.8 && (beverage.Grade + beverage.GradeWithCoke) / 2 > 5)
                        return beverage;
                    break;

                case PollPurpose.Exclusive:
                    if (beverage.AlcoholPercentage / (100 * (beverage.Price / beverage.Capacity)) < pollPurposeWeight && beverage.Grade > 6)
                        return beverage;
                    break;

                case PollPurpose.ForPiratesFromCarabien:
                    if (beverage.BeAPirate.IsSet)
                        return beverage;
                    break;
            }
            return null;
        }

        public Beverage DoesBeverageFulfillSetMixesRequirement(PollMixes pollMixes, Beverage beverage, int minimalAllowedGrade = 5)
        {

            switch (pollMixes)
            {
                case PollMixes.None:
                    return beverage;
                //break;

                case PollMixes.Solo:
                    if (beverage.Grade > minimalAllowedGrade)
                        return beverage;

                    break;

                case PollMixes.WithCoke:
                    if (beverage.GradeWithCoke > minimalAllowedGrade)
                        return beverage;
                    break;

            }

            return null;
        }

        public Beverage DoesBeverageFulfillSetFlavoursRequirement(List<Flavour> Flavours, Beverage beverage)
        {
            if (beverage == null)
                return null;

            if (Flavours.Count < 1)
                return beverage;

            int flavoursFound = 0;

            foreach (Flavour flavour in Flavours)
            {
                if (flavour.Name == nameof(beverage.Vanila))
                    if (beverage.Vanila.IsSet)
                        flavoursFound++;

                if (flavour.Name == nameof(beverage.Nuts))
                    if (beverage.Nuts.IsSet)
                        flavoursFound++;

                if (flavour.Name == nameof(beverage.Caramel))
                    if (beverage.Caramel.IsSet)
                        flavoursFound++;

                if (flavour.Name == nameof(beverage.Smoke))
                    if (beverage.Smoke.IsSet)
                        flavoursFound++;

                if (flavour.Name == nameof(beverage.Cinnamon))
                    if (beverage.Cinnamon.IsSet)
                        flavoursFound++;

                if (flavour.Name == nameof(beverage.Spirit))
                    if (beverage.Spirit.IsSet)
                        flavoursFound++;

                if (flavour.Name == nameof(beverage.Fruits))
                    if (beverage.Fruits.IsSet)
                        flavoursFound++;

                if (flavour.Name == nameof(beverage.Honey))
                    if (beverage.Honey.IsSet)
                        flavoursFound++;

                if (flavour.Name == nameof(beverage.BeAPirate))
                    if (beverage.BeAPirate.IsSet)
                        flavoursFound++;
            }

            if (flavoursFound == Flavours.Count)
                return beverage;

            return null;
        }

        public Beverage DoesBeverageFulfillSetPriceRequirement(PollPricePoints pollPricePoints, Beverage beverage)
        {
            switch (pollPricePoints)
            {
                case PollPricePoints.None:
                    return beverage;
                //break;

                case PollPricePoints.PricePoint1:
                    if (beverage.Price < 50)
                        return beverage;
                    break;

                case PollPricePoints.PricePoint2:
                    if (beverage.Price >= 50 && beverage.Price < 70)
                        return beverage;
                    break;

                case PollPricePoints.PricePoint3:
                    if (beverage.Price >= 70 && beverage.Price < 90)
                        return beverage;
                    break;

                case PollPricePoints.PricePoint4:
                    if (beverage.Price >= 90)
                        return beverage;
                    break;

            }

            return null;
        }

        public async Task<Beverage> GetRandomRow(Random random = null)
        {
            if (random == null)
                random = rand;

            ObservableCollection<Beverage> alldata = await GetAllData();

            if (alldata == null || alldata.Count == 0)
                return null;

            if (alldata.Count == 1)
                return alldata[0];

            return alldata[random.Next(0, alldata.Count - 1)];
        }

        public async Task<string> SaveBevreageToDatabase(Beverage beverage, byte[] img)
        {
            if (!fileService.DirectoryExists(MainDataDirectory))
                fileService.CreateDirectory(MainDataDirectory);

            if (!fileService.FileExists(FileName))
                fileService.FileCreate(FileName);

            string result = "Task failed";

            List<JsonBeverage> jsonBeverage = getAllJsonData().ToList();

            jsonBeverage.Add(JsonBeverage.TransFromBeverageToJsonBeverage(beverage, img));

            string json = JsonConvert.SerializeObject(jsonBeverage);

            using (TextWriter tw = new StreamWriter(FileName))
            {
                tw.WriteLine(json);
                result = "done";
                tw.Close();
            };

            return result;

        }

        public async Task<string> UpdateBeveragesDatabase(ObservableCollection<Beverage> updatedBeverages)
        {
            if (!fileService.DirectoryExists(MainDataDirectory))
                fileService.CreateDirectory(MainDataDirectory);

            if (!fileService.FileExists(FileName))
                fileService.FileCreate(FileName);

            string result = "Task failed";

            ObservableCollection<Beverage> oldBeverages = await GetAllData();
            List<JsonBeverage> jsonBeverage = new List<JsonBeverage>();

            foreach (Beverage updatedBeverage in updatedBeverages)
                oldBeverages.Single(x => x.ID == updatedBeverage.ID).Update(updatedBeverage);

            foreach (Beverage oldBeverage in oldBeverages)
                jsonBeverage.Add(JsonBeverage.TransFromBeverageToJsonBeverage(oldBeverage));

            using (TextWriter tw = new StreamWriter(FileName))
            {
                tw.WriteLine(JsonConvert.SerializeObject(jsonBeverage));
                result = "done";
                tw.Close();
            };

            return result;
        }

        public async Task<string> DeleteBeverageDatabase(Beverage updatedBeverages)
        {
            if (!fileService.DirectoryExists(MainDataDirectory))
                fileService.CreateDirectory(MainDataDirectory);

            if (!fileService.FileExists(FileName))
                fileService.FileCreate(FileName);

            string result = "Task failed";

            ObservableCollection<Beverage> oldBeverages = await GetAllData();
            List<JsonBeverage> jsonBeverage = new List<JsonBeverage>();

            oldBeverages.Remove(oldBeverages.Where(x => x.ID == updatedBeverages.ID).FirstOrDefault());
            foreach (Beverage beverage in oldBeverages)
                jsonBeverage.Add(JsonBeverage.TransFromBeverageToJsonBeverage(beverage));

            using (TextWriter tw = new StreamWriter(FileName))
            {
                tw.WriteLine(JsonConvert.SerializeObject(jsonBeverage));
                result = "done";
                tw.Close();
            };

            return result;
        }

        public async Task<bool> TestConnectionToDatabase()
        {

            return fileService.DirectoryExists(MainDataDirectory);

        }

        public bool TestConnectionToTable(AvailableTables availableTables)
        {
            return fileService.FileExists(FileName);
        }

        public Settings ReadSettings()
        {
            string json;
            if (!File.Exists(SettingsFileName))
                CreateSettingsFile();

            using (streamReader.Create(SettingsFileName))
                json = streamReader.ReadToEnd();


            if (json == null || !IsValidJson(json) || json == "")
            {
                File.Delete(SettingsFileName);
                CreateSettingsFile();
                using (streamReader.Create(SettingsFileName))
                    json = streamReader.ReadToEnd();
            }


            Settings test = JsonConvert.DeserializeObject<Settings>(json);
            return test;

        }

        public void SaveSettings(Settings newSettings)
        {
            string json = JsonConvert.SerializeObject(newSettings);
            var test = IsValidJson(json);
            using (TextWriter tw = new StreamWriter(SettingsFileName))
            {
                tw.WriteLine(json);
                tw.Close();
            };
        }

        private ObservableCollection<JsonBeverage> getAllJsonData()
        {
            ObservableCollection<JsonBeverage> Beverages = new ObservableCollection<JsonBeverage>();

            if (File.Exists(FileName))
            {
                using (StreamReader r = new StreamReader(FileName))
                {
                    string json2 = r.ReadToEnd();
                    List<JsonBeverage> items = JsonConvert.DeserializeObject<List<JsonBeverage>>(json2);
                    if (items != null)
                        foreach (JsonBeverage item in items)
                            Beverages.Add(item);

                    r.Close();
                }
            }

            return Beverages;
        }
        private bool IsValidJson(string strInput)
        {
            if (string.IsNullOrWhiteSpace(strInput)) { return false; }
            strInput = strInput.Trim();

            bool isValid;

            try
            {
                var obj = JToken.Parse(strInput);
                return true;
            }
            catch (JsonReaderException jex)
            {
                //Exception in parsing json
                Console.WriteLine(jex.Message);


                isValid = false;
            }
            catch (Exception ex) //some other exception
            {
                Console.WriteLine(ex.ToString());
                isValid = false;
            }

            if (!isValid)
                if (File.Exists(SettingsFileName))
                    File.Delete(SettingsFileName);

            return isValid;
        }

        private void CreateSettingsFile()
        {

            File.Create(SettingsFileName).Close();

            SaveSettings(new Settings());

        }
    }
}