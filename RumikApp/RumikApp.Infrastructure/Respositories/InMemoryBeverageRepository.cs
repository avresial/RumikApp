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

            SaveBevreageToDatabase();
            beverages = GetAllData().Result;
        }

        public async Task<IEnumerable<BeverageDto>> Browse(Func<BeverageDto, bool> selector)
        {
            return beverages.Where(selector);
        }

        public Task<IEnumerable<BeverageDto>> BrowseAll()
            => Task.FromResult(beverages.AsEnumerable<BeverageDto>());
               

        private async Task<List<BeverageDto>> GetAllData()
        {

            List<BeverageDto> Beverages = new List<BeverageDto>();

            if (!fileSystem.File.Exists(FileName)) return Beverages;

            using (streamReader.Create(FileName))
            {
                string json2 = streamReader.ReadToEnd();
                Beverages = JsonConvert.DeserializeObject<List<BeverageDto>>(json2);

            }

            return Beverages;

        }

        public async Task SaveBevreageToDatabase()
        {
            List<BeverageDto> jsonBeverage = new List<BeverageDto>() { new BeverageDto() { Name = "XD" } };

            string json = JsonConvert.SerializeObject(jsonBeverage);

            using (TextWriter tw = new StreamWriter(FileName))
            {
                tw.WriteLine(json);
                tw.Close();
            };

        }

    }
}
