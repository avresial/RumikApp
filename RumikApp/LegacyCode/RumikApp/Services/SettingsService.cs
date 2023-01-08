using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RumikApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumikApp.Services
{
    public class SettingsService: ISettingsService
    {
        private readonly string SettingsFileName = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RumikApp" + "\\Settings.json";
        private string MainDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RumikApp";
        IStreamReaderService streamReader;
        IFileService fileService;
        public SettingsService(IStreamReaderService streamReader, IFileService fileService)
        {
            this.streamReader = streamReader;
            this.fileService = fileService;

        }

        public Settings ReadSettings()
        {
            string json;
            if (!fileService.DirectoryExists(MainDataDirectory))
                fileService.CreateDirectory(MainDataDirectory);

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
            if (!fileService.DirectoryExists(MainDataDirectory))
                fileService.CreateDirectory(MainDataDirectory);

            File.Create(SettingsFileName).Close();

            SaveSettings(new Settings());

        }
    }
}
