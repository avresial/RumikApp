using RumikApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RumikApp.Tests
{
    public class FileDatabaseConnectionServiceTests
    {
        [Fact]
        public void GetAllData_When_There_Are_Not_File_To_Load()
        {
            // Arrange
            string newMainDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RumikApp\\GetAllDataTest";
            string newMainFile = newMainDirectory + "\\GetAllDataFileTest.json";

            if (File.Exists(newMainFile))
                File.Delete(newMainFile);

            if (Directory.Exists(newMainDirectory))
                Directory.Delete(newMainDirectory);


            FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();
            FileDatabaseConnectionService.MainDataDirectory = newMainDirectory;
            FileDatabaseConnectionService.FileName = newMainFile;

            ObservableCollection<Beverage> expectedResult = new ObservableCollection<Beverage>();
            expectedResult.Clear();

            // Act
            ObservableCollection<Beverage> actuallResule = FileDatabaseConnectionService.GetAllData();

            // Assert
            Assert.Equal(expectedResult, actuallResule);
        }

        [Fact]
        public void GetAllData_When_There_Are_Not_Data_To_Load()
        {
            // Arrange
            string newMainDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RumikApp\\GetAllDataTest2";
            string newMainFile = newMainDirectory + "\\GetAllDataFileTest2.json";

            if (!Directory.Exists(newMainDirectory))
                Directory.CreateDirectory(newMainDirectory);

            if (!File.Exists(newMainFile))
                File.Create(newMainFile).Close();


            FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();
            FileDatabaseConnectionService.MainDataDirectory = newMainDirectory;
            FileDatabaseConnectionService.FileName = newMainFile;

            ObservableCollection<Beverage> expectedResult = new ObservableCollection<Beverage>();
            expectedResult.Clear();

            // Act
            ObservableCollection<Beverage> actuallResule = FileDatabaseConnectionService.GetAllData();

            // Assert

            if (File.Exists(newMainFile))
                File.Delete(newMainFile);

            if (Directory.Exists(newMainDirectory))
                Directory.Delete(newMainDirectory);
            Assert.Equal(expectedResult, actuallResule);
        }

        [Fact]
        public void GetAllData_When_There_Is_Data_To_Load()
        {
            // Arrange
            string newMainDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RumikApp\\GetAllDataTest2";
            string newMainFile = newMainDirectory + "\\GetAllDataFileTest2.json";

            if (!Directory.Exists(newMainDirectory))
                Directory.CreateDirectory(newMainDirectory);

            if (!File.Exists(newMainFile))
                File.Create(newMainFile).Close();


            FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();
            FileDatabaseConnectionService.MainDataDirectory = newMainDirectory;
            FileDatabaseConnectionService.FileName = newMainFile;

            ObservableCollection<Beverage> expectedResult = new ObservableCollection<Beverage>();

            expectedResult.Add(new Beverage() { Name = "Test1", TestIcon = null });
            expectedResult.Add(new Beverage() { Name = "Test2", TestIcon = null });
            expectedResult.Add(new Beverage() { Name = "Test3", TestIcon = null });

            foreach (Beverage beverage in expectedResult)
                FileDatabaseConnectionService.SaveBevreageToDatabase(beverage, null);

            // Act

            ObservableCollection<Beverage> actuallResule = FileDatabaseConnectionService.GetAllData();

            // Assert

            if (File.Exists(newMainFile))
                File.Delete(newMainFile);

            if (Directory.Exists(newMainDirectory))
                Directory.Delete(newMainDirectory);

            //Assert.

            Assert.Equal(expectedResult[0].Name, actuallResule[0].Name);
        }


        [Fact]
        public void TestConnectionToDatabase_When_There_Is_No_Directory()
        {
            // Arrange

            string newMainDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RumikApp\\DirectoryTest1";

            if (Directory.Exists(newMainDirectory))
                Directory.Delete(newMainDirectory);

            FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();
            FileDatabaseConnectionService.MainDataDirectory = newMainDirectory;

            bool expectedResult = false;

            // Act

            bool actuallResule = FileDatabaseConnectionService.TestConnectionToDatabase();

            // Assert
            Assert.Equal(expectedResult, actuallResule);
        }

        [Fact]
        public void TestConnectionToDatabase_When_There_Is_A_Directory()
        {
            // Arrange

            string newMainDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RumikApp\\DirectoryTest2";

            if (!Directory.Exists(newMainDirectory))
                Directory.CreateDirectory(newMainDirectory);

            FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();
            FileDatabaseConnectionService.MainDataDirectory = newMainDirectory;

            bool expectedResult = true;

            // Act

            bool actuallResule = FileDatabaseConnectionService.TestConnectionToDatabase();

            // Assert
            Assert.Equal(expectedResult, actuallResule);

            if (Directory.Exists(newMainDirectory))
                Directory.Delete(newMainDirectory);
        }

        [Fact]
        public void TestConnectionToTable_There_Is_No_File()
        {
            // Arrange

            string newMainDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RumikApp\\FileTest1";
            string newMainFile = newMainDirectory + "\\FileTest1.json";

            if (!Directory.Exists(newMainDirectory))
                Directory.CreateDirectory(newMainDirectory);

            if (!File.Exists(newMainFile))
                File.Delete(newMainFile);

            FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();

            FileDatabaseConnectionService.MainDataDirectory = newMainDirectory;
            FileDatabaseConnectionService.FileName = newMainFile;

            bool expectedResult = false;

            // Act

            bool actuallResule = FileDatabaseConnectionService.TestConnectionToTable(Enums.AvailableTables.RumsBase);

            // Assert
            Assert.Equal(expectedResult, actuallResule);

            if (Directory.Exists(newMainDirectory))
                Directory.Delete(newMainDirectory);
        }

        [Fact]
        public void TestConnectionToTable_There_Is_A_File()
        {
            // Arrange

            string newMainDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RumikApp\\FileTest2";
            string newMainFile = newMainDirectory + "\\FileTest2.json";

            if (!Directory.Exists(newMainDirectory))
                Directory.CreateDirectory(newMainDirectory);

            if (!File.Exists(newMainFile))
                File.Create(newMainFile).Close();

            FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();

            FileDatabaseConnectionService.MainDataDirectory = newMainDirectory;
            FileDatabaseConnectionService.FileName = newMainFile;

            bool expectedResult = true;

            // Act

            bool actuallResule = FileDatabaseConnectionService.TestConnectionToTable(Enums.AvailableTables.RumsBase);

            // Assert

            if (File.Exists(newMainFile))
                File.Delete(newMainFile);

            if (Directory.Exists(newMainDirectory))
                Directory.Delete(newMainDirectory);

            Assert.Equal(expectedResult, actuallResule);
        }



    }
}
