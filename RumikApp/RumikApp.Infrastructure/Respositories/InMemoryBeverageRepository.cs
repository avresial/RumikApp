using Newtonsoft.Json;
using RumikApp.Core.Services;
using RumikApp.Infrastructure.Dto;
using RumikApp.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Threading.Tasks;

namespace RumikApp.Infrastructure.Respositories
{
    public class InMemoryBeverageRepository : IBeverageRepository
    {
        private List<BeverageDto> beverages;
        IStreamReaderService streamReader;
        IFileSystem fileSystem;

        private string _FileName = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RumikApp" + "\\LocalDatabaseTest.json";
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

        public InMemoryBeverageRepository(IFileSystem fileSystem, IStreamReaderService streamReader)
        {
            this.fileSystem = fileSystem;
            this.streamReader = streamReader;


            beverages = GetAllData().Result;
        }

        public async Task<IEnumerable<BeverageDto>> Browse(Func<BeverageDto, bool> selector)
        {
            return beverages.Where(selector);
        }

        //public Task<IEnumerable<BeverageDto>> BrowseAll()
        //    => Task.FromResult(beverages.AsEnumerable<BeverageDto>());
        public async Task<IEnumerable<BeverageDto>> BrowseAll()
        {
            List<BeverageDto> Beverages = new List<BeverageDto>();

            if (!fileSystem.File.Exists(FileName)) return Beverages;

            using (streamReader.Create(FileName))
            {
                string json2 = streamReader.ReadToEnd();
                if (!string.IsNullOrEmpty(json2))
                    Beverages.AddRange(JsonConvert.DeserializeObject<List<BeverageDto>>(json2));

            }

            return Beverages;

        }

        private async Task<List<BeverageDto>> GetAllData()
        {

            List<BeverageDto> Beverages = new List<BeverageDto>();

            if (!fileSystem.File.Exists(FileName)) return Beverages;

            using (streamReader.Create(FileName))
            {
                string json2 = streamReader.ReadToEnd();
                if (!string.IsNullOrEmpty(json2))
                    Beverages = JsonConvert.DeserializeObject<List<BeverageDto>>(json2);

            }

            return Beverages;

        }

        public async Task SaveToRepository(BeverageDto beverageDto)
        {
            List<BeverageDto> BeverageDtos = new List<BeverageDto>(GetAllData().Result);
            var InInDatabase = BeverageDtos.Where(x => x.ID == beverageDto.ID);

            if (InInDatabase.Any())  BeverageDtos.Remove(InInDatabase.FirstOrDefault());
            BeverageDtos.Add(beverageDto);

            string json = JsonConvert.SerializeObject(BeverageDtos);

            using (TextWriter tw = new StreamWriter(FileName))
            {
                tw.WriteLine(json);
                tw.Close();
            };
        }

        public async Task<bool> RemoveFromRepository(BeverageDto beverageDto)
        {
            string MainDataDirectory = Path.GetDirectoryName(FileName);
            if (!fileSystem.Directory.Exists(MainDataDirectory))
                fileSystem.Directory.CreateDirectory(MainDataDirectory);

            if (!fileSystem.File.Exists(FileName)) return false;

            List<BeverageDto> oldBeverages = await GetAllData();

            oldBeverages.Remove(oldBeverages.Where(x => x.ID == beverageDto.ID).FirstOrDefault());

            ;
            using (TextWriter tw = new StreamWriter(FileName))
            {
                tw.WriteLine(JsonConvert.SerializeObject(oldBeverages));
                tw.Close();
            };

            return true;
        }

        //public async Task<string> UpdateBeveragesDatabase(ObservableCollection<Beverage> updatedBeverages)
        //{
        //    if (!fileService.DirectoryExists(MainDataDirectory))
        //        fileService.CreateDirectory(MainDataDirectory);

        //    if (!fileService.FileExists(FileName))
        //        fileService.FileCreate(FileName);

        //    string result = "Task failed";

        //    ObservableCollection<Beverage> oldBeverages = await GetAllData();
        //    List<JsonBeverage> jsonBeverage = new List<JsonBeverage>();

        //    foreach (Beverage updatedBeverage in updatedBeverages)
        //        oldBeverages.Single(x => x.ID == updatedBeverage.ID).Update(updatedBeverage);

        //    foreach (Beverage oldBeverage in oldBeverages)
        //        jsonBeverage.Add(JsonBeverage.TransFromBeverageToJsonBeverage(oldBeverage));

        //    using (TextWriter tw = new StreamWriter(FileName))
        //    {
        //        tw.WriteLine(JsonConvert.SerializeObject(jsonBeverage));
        //        result = "done";
        //        tw.Close();
        //    };

        //    return result;
        //}

    }
}
