using RumikApp.Enums;
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

            Random random = new Random();

            for (int i = 0; i < 50; i++)
                expectedResult.Add(new Beverage() { Name = "Test" + random.Next(), TestIcon = null });

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

            Assert.Equal(expectedResult.Count, actuallResule.Count);

        }

        [Fact]
        public void GetAllPiratesBeverages_When_There_Is_Data_To_Load()
        {
            // Arrange
            int NonPirateBeverages = 25;
            int PirateBeverages = 25;

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

            Random random = new Random();

            for (int i = 0; i < NonPirateBeverages; i++)
                expectedResult.Add(new Beverage() { Name = "Test" + random.Next(), TestIcon = null });

            for (int i = 0; i < PirateBeverages; i++)
            {
                Flavour BeAPirate = new Flavour("/IMGs/PollIMG/BeAPirate.png", "BeAPirate");
                BeAPirate.IsSet = true;
                expectedResult.Add(new Beverage() { Name = "Test" + random.Next(), TestIcon = null, BeAPirate = BeAPirate });
            }


            foreach (Beverage beverage in expectedResult)
                FileDatabaseConnectionService.SaveBevreageToDatabase(beverage, null);

            // Act

            ObservableCollection<Beverage> actuallResule = FileDatabaseConnectionService.GetAllPiratesBeverages();

            // Assert

            if (File.Exists(newMainFile))
                File.Delete(newMainFile);

            if (Directory.Exists(newMainDirectory))
                Directory.Delete(newMainDirectory);

            //Assert.

            Assert.Equal(PirateBeverages, actuallResule.Count);

        }

        [Fact]
        public void DoesBeverageFulfillSetPurposeRequirement_When_It_Does_Make_None_Purpose()
        {
            // Arrange
            FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();
            // Act
            PollPurpose pollPurpose = PollPurpose.None;

            Beverage beverage = new Beverage() { Name = "NoneTest" };

            Beverage actuallResult = FileDatabaseConnectionService.DoesBeverageFulfillSetPurposeRequirement(pollPurpose, 0, beverage);
            // Assert

            Assert.Equal(beverage, actuallResult);
        }


        [Fact]
        public void DoesBeverageFulfillSetPurposeRequirement_When_It_Does_Make_ForPartyBool_Purpose()
        {
            // Arrange
            FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();
            // Act
            PollPurpose pollPurpose = PollPurpose.ForPartyBool;

            Beverage beverage = new Beverage() { Name = "ForPartyBoolTest", AlcoholPercentage = 500,Price = 5, Capacity = 6 };

            Beverage actuallResult = FileDatabaseConnectionService.DoesBeverageFulfillSetPurposeRequirement(pollPurpose, 5, beverage);
            // Assert

            Assert.Equal(beverage, actuallResult);
        }

        [Fact]
        public void DoesBeverageFulfillSetPurposeRequirement_When_It_Does_Not_Make_ForPartyBool_Purpose()
        {
            // Arrange
            FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();
            // Act
            PollPurpose pollPurpose = PollPurpose.ForPartyBool;

            Beverage beverage = new Beverage() { Name = "ForPartyBoolTest", AlcoholPercentage = 400, Price = 5, Capacity = 6 };

            Beverage actuallResult = FileDatabaseConnectionService.DoesBeverageFulfillSetPurposeRequirement(pollPurpose, 5, beverage);
            // Assert

            Assert.Equal(null, actuallResult);
        }


        [Fact]
        public void DoesBeverageFulfillSetPurposeRequirement_When_It_Does_Make_GoodButCheap_Purpose()
        {
            //((beverage.Grade + beverage.GradeWithCoke) / 2) / (100 * (beverage.Price / beverage.Capacity)) > pollPurposeWeight && (beverage.Grade + beverage.GradeWithCoke) / 2 > 5
            // Arrange
            FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();
            // Act
            PollPurpose pollPurpose = PollPurpose.GoodButCheap;

            Beverage beverage = new Beverage() { Name = "GoodButCheapTest", Price = 60, Capacity = 700,Grade = 9,GradeWithCoke=9 };

            Beverage actuallResult = FileDatabaseConnectionService.DoesBeverageFulfillSetPurposeRequirement(pollPurpose, 5, beverage);
            // Assert

            Assert.Equal(beverage, actuallResult);
        }

        [Fact]
        public void DoesBeverageFulfillSetPurposeRequirement_When_It_Does_Not_Make_GoodButCheap_Purpose()
        {
            // Arrange
            FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();
            // Act
            PollPurpose pollPurpose = PollPurpose.ForPartyBool;

            Beverage beverage = new Beverage() { Name = "GoodButCheapTest", Price = 60, Capacity = 700, Grade = 6, GradeWithCoke = 6 };

            Beverage actuallResult = FileDatabaseConnectionService.DoesBeverageFulfillSetPurposeRequirement(pollPurpose, 5, beverage);
            // Assert

            Assert.Equal(null, actuallResult);
        }


        [Fact]
        public void DoesBeverageFulfillSetPurposeRequirement_When_It_Does_Make_Exclusive_Purpose()
        {
            // Arrange
            FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();
            // Act
            PollPurpose pollPurpose = PollPurpose.Exclusive;

            Beverage beverage = new Beverage() { Name = "GoodButCheapTest", Price = 120, Capacity = 700, Grade = 9, GradeWithCoke = 9, AlcoholPercentage = 40 };

            Beverage actuallResult = FileDatabaseConnectionService.DoesBeverageFulfillSetPurposeRequirement(pollPurpose, 5, beverage);
            // Assert

            Assert.Equal(beverage, actuallResult);
        }

        [Fact]
        public void DoesBeverageFulfillSetPurposeRequirement_When_It_Does_Not_Make_Exclusive_Purpose()
        {
            // Arrange
            FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();
            // Act
            PollPurpose pollPurpose = PollPurpose.Exclusive;

            Beverage beverage = new Beverage() { Name = "GoodButCheapTest", Price = 60, Capacity = 700, Grade = 6, GradeWithCoke = 6, AlcoholPercentage = 40 };

            Beverage actuallResult = FileDatabaseConnectionService.DoesBeverageFulfillSetPurposeRequirement(pollPurpose, 5, beverage);
            // Assert

            Assert.Equal(null, actuallResult);
        }


        [Fact]
        public void DoesBeverageFulfillSetPurposeRequirement_When_It_Does_Make_ForPiratesFromCarabien_Purpose()
        {
            // Arrange
            FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();

            PollPurpose pollPurpose = PollPurpose.ForPiratesFromCarabien;

            Flavour BeAPirate = new Flavour("/IMGs/PollIMG/BeAPirate.png", "BeAPirate");
            BeAPirate.IsSet = true;

            Beverage beverage = new Beverage() { Name = "ForPiratesFromCarabienTest", Price = 120, Capacity = 700, Grade = 9, GradeWithCoke = 9, AlcoholPercentage = 40, BeAPirate = BeAPirate };


            // Act

            Beverage actuallResult = FileDatabaseConnectionService.DoesBeverageFulfillSetPurposeRequirement(pollPurpose, 5, beverage);
            // Assert

            Assert.Equal(beverage, actuallResult);
        }

        [Fact]
        public void DoesBeverageFulfillSetPurposeRequirement_When_It_Does_Not_Make_ForPiratesFromCarabien_Purpose()
        {
            // Arrange
            FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();
            // Act
            PollPurpose pollPurpose = PollPurpose.ForPiratesFromCarabien;

            Beverage beverage = new Beverage() { Name = "ForPiratesFromCarabienTest", Price = 60, Capacity = 700, Grade = 6, GradeWithCoke = 6, AlcoholPercentage = 40 };

            Beverage actuallResult = FileDatabaseConnectionService.DoesBeverageFulfillSetPurposeRequirement(pollPurpose, 5, beverage);
            // Assert

            Assert.Equal(null, actuallResult);
        }


        [Fact]
        public void DoesBeverageFulfillSetMixesRequirement_When_It_Does_Make_None_Purpose()
        {
            // Arrange
            FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();

            PollMixes pollMixes = PollMixes.None;

            Beverage beverage = new Beverage() { Name = "NoneTest" };

            // Act

            Beverage actuallResult = FileDatabaseConnectionService.DoesBeverageFulfillSetMixesRequirement(pollMixes, beverage);
            // Assert

            Assert.Equal(beverage, actuallResult);
        }


        [Fact]
        public void DoesBeverageFulfillSetMixesRequirement_When_It_Does_Make_Solo_Purpose()
        {
            // Arrange
            FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();

            PollMixes pollMixes = PollMixes.Solo;

            Beverage beverage = new Beverage() { Name = "SoloTest", Grade = 6};

            // Act

            Beverage actuallResult = FileDatabaseConnectionService.DoesBeverageFulfillSetMixesRequirement(pollMixes, beverage);
            // Assert

            Assert.Equal(beverage, actuallResult);
        }

        [Fact]
        public void DoesBeverageFulfillSetMixesRequirement_When_It_Does_Not_Make_Solo_Purpose()
        {
            // Arrange
            FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();

            PollMixes pollMixes = PollMixes.Solo;

            Beverage beverage = new Beverage() { Name = "SoloTest", Grade = 4};

            // Act

            Beverage actuallResult = FileDatabaseConnectionService.DoesBeverageFulfillSetMixesRequirement(pollMixes,  beverage);
            // Assert

            Assert.Equal(null, actuallResult);
        }

     
        [Fact]
        public void DoesBeverageFulfillSetMixesRequirement_When_It_Does_Make_WithCoke_Purpose()
        {
            // Arrange
            FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();

            PollMixes pollMixes = PollMixes.WithCoke;

            Beverage beverage = new Beverage() { Name = "WithCokeTest", GradeWithCoke = 6, };

            // Act

            Beverage actuallResult = FileDatabaseConnectionService.DoesBeverageFulfillSetMixesRequirement(pollMixes, beverage);
            // Assert

            Assert.Equal(beverage, actuallResult);
        }

        [Fact]
        public void DoesBeverageFulfillSetMixesRequirement_When_It_Does_Not_Make_WithCoke_Purpose()
        {
            // Arrange
            FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();

            PollMixes pollMixes = PollMixes.WithCoke;

            Beverage beverage = new Beverage() { Name = "WithCokeTest", GradeWithCoke = 4};
            // Act

            Beverage actuallResult = FileDatabaseConnectionService.DoesBeverageFulfillSetMixesRequirement(pollMixes, beverage);
            // Assert

            Assert.Equal(null, actuallResult);
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
