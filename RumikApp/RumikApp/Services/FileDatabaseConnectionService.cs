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

        Random rand = new Random();
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

        private ObservableCollection<JsonBeverage> getAllJsonData()
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
            ObservableCollection<Beverage> finallList = new ObservableCollection<Beverage>();

            foreach (Beverage beverage in GetAllData())
                if (beverage.BeAPirate.IsSet == true)
                    finallList.Add(beverage);

            return finallList;
        }

        public ObservableCollection<Beverage> GetDataFromDatabaseWithConditions(PollPurpose pollPurpose, int pollPurposeWeight, PollMixes pollMixes, List<Flavour> Flavours, PollPricePoints pollPricePoints)
        {
            ObservableCollection<Beverage> finallList = new ObservableCollection<Beverage>();

            foreach (Beverage beverage in GetAllData())
            {
                Beverage TMPBeverage = doesBeverageFulfillSetPurposeRequirement(pollPurpose, pollPurposeWeight, beverage);

                if (TMPBeverage != null)
                    TMPBeverage = doesBeverageFulfillSetMixesRequirement(pollMixes, beverage);

                if (TMPBeverage != null)
                    TMPBeverage = doesBeverageFulfillSetFlavoursRequirement(Flavours, beverage);

                if (TMPBeverage != null)
                    TMPBeverage = doesBeverageFulfillSetPriceRequirement(pollPricePoints, beverage);

                if (TMPBeverage != null)
                    finallList.Add(TMPBeverage);
            }

            return finallList;
        }

        private Beverage doesBeverageFulfillSetPurposeRequirement(PollPurpose pollPurpose, int pollPurposeWeight, Beverage beverage)
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
                    if (((beverage.Grade + beverage.GradeWithCoke) / 2) / (100 * (beverage.Price / beverage.Capacity)) > pollPurposeWeight && (beverage.Grade + beverage.GradeWithCoke) / 2 > 5)
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

        private Beverage doesBeverageFulfillSetMixesRequirement(PollMixes pollMixes, Beverage beverage, int minimalAllowedGrade = 5)
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

        private Beverage doesBeverageFulfillSetFlavoursRequirement(List<Flavour> Flavours, Beverage beverage)
        {
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

                if (flavour.Name == nameof(beverage.Carmel))
                    if (beverage.Carmel.IsSet)
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


        private Beverage doesBeverageFulfillSetPriceRequirement(PollPricePoints pollPricePoints, Beverage beverage)
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

        public Beverage GetRandomRow()
        {
            ObservableCollection<Beverage> alldata = GetAllData();
            if (alldata == null || alldata.Count == 0)
                return null;

            if (alldata.Count == 1)
                return alldata[0];

            return alldata[rand.Next(0, alldata.Count - 1)];
        }

        public string SaveBevreageToDatabase(Beverage beverage, byte[] img)
        {
            string result = "Task failed";

            List<JsonBeverage> jsonBeverage = getAllJsonData().ToList();

            jsonBeverage.Add(JsonBeverage.TransFromBeverageToJsonBeverage(beverage, img));

            string json = JsonConvert.SerializeObject(jsonBeverage);

            using (TextWriter tw = new StreamWriter(fileName))
            {
                tw.WriteLine(json);
                result = "done";
            };

            return result;
        }

        public bool TestConnectionToDatabase()
        {
            return Directory.Exists(directory);
        }

        public bool TestConnectionToTable(AvailableTables availableTables)
        {
            return File.Exists(fileName);
        }
    }
}
