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
        public void GetDataFromDatabaseWithConditions_When_There_Are_No_Conditions()
        {
            // Arrange
            string newMainDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RumikApp\\GetDataFromDatabaseWithConditions1";
            string newMainFile = newMainDirectory + "\\GetDataFromDatabaseWithConditions1.json";

            if (!Directory.Exists(newMainDirectory))
                Directory.CreateDirectory(newMainDirectory);

            if (!File.Exists(newMainFile))
                File.Create(newMainFile).Close();


            FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();
            FileDatabaseConnectionService.MainDataDirectory = newMainDirectory;
            FileDatabaseConnectionService.FileName = newMainFile;

            int numberOfElements = 10;
            int expectedResult = numberOfElements;

            for (int i = 0; i < numberOfElements; i++)
                FileDatabaseConnectionService.SaveBevreageToDatabase(new Beverage() { Name = "Test" + i.ToString(), TestIcon = null }, null);

            // Act
            ObservableCollection<Beverage> actuallResule = FileDatabaseConnectionService.GetDataFromDatabaseWithConditions(PollPurpose.None, 5, PollMixes.None, new List<Flavour>(), PollPricePoints.None);

            // Assert

            if (File.Exists(newMainFile))
                File.Delete(newMainFile);

            if (Directory.Exists(newMainDirectory))
                Directory.Delete(newMainDirectory);

            Assert.Equal(expectedResult, actuallResule.Count());
        }

        [Theory]
        [InlineData(5, 2)]
        [InlineData(20, 8)]
        public void GetAllData_When_There_Are_All_Conditions(int NumberOfElements, int NumberOfMatchingElements)
        {
            // Arrange
            int expectedResult = NumberOfMatchingElements;
            string newMainDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RumikApp\\GetDataFromDatabaseWithConditions2";
            string newMainFile = newMainDirectory + "\\GetDataFromDatabaseWithConditions2.json";

            if (!Directory.Exists(newMainDirectory))
                Directory.CreateDirectory(newMainDirectory);

            if (!File.Exists(newMainFile))
                File.Create(newMainFile).Close();


            FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();
            FileDatabaseConnectionService.MainDataDirectory = newMainDirectory;
            FileDatabaseConnectionService.FileName = newMainFile;

            ObservableCollection<Beverage> ListOfBeverages = new ObservableCollection<Beverage>();

            Random random = new Random();

            for (int i = 0; i < NumberOfElements - NumberOfMatchingElements; i++)
                ListOfBeverages.Add(new Beverage() { Name = "NotMatching" + random.Next(), TestIcon = null });


            Beverage MatchingBeverage = new Beverage() { Name = "MatchingBeverage" };

            MatchingBeverage.Vanila.IsSet = true;
            MatchingBeverage.Nuts.IsSet = true;
            MatchingBeverage.Caramel.IsSet = true;
            MatchingBeverage.Smoke.IsSet = true;
            MatchingBeverage.Cinnamon.IsSet = true;
            MatchingBeverage.Spirit.IsSet = true;
            MatchingBeverage.Fruits.IsSet = true;
            MatchingBeverage.Honey.IsSet = true;
            MatchingBeverage.BeAPirate.IsSet = true;

            MatchingBeverage.Price = 1000;
            MatchingBeverage.Capacity = 50;
            MatchingBeverage.Grade = 12;

            for (int i = 0; i < NumberOfMatchingElements; i++)
                ListOfBeverages.Add(MatchingBeverage);

            foreach (Beverage beverage in ListOfBeverages)
                FileDatabaseConnectionService.SaveBevreageToDatabase(beverage, null);

            // Act
            List<Flavour> Flavours = new List<Flavour>();

            Flavours.Add(new Flavour("/IMGs/PollIMG/Vanila.png", "Vanila"));
            Flavours.Add(new Flavour("/IMGs/PollIMG/Nuts.png", "Nuts"));
            Flavours.Add(new Flavour("/IMGs/PollIMG/Carmel.png", "Caramel"));
            Flavours.Add(new Flavour("/IMGs/PollIMG/Smoked.png", "Smoke"));
            Flavours.Add(new Flavour("/IMGs/PollIMG/Cinamon.png", "Cinnamon"));
            Flavours.Add(new Flavour("/IMGs/PollIMG/Spirit.png", "Spirit"));
            Flavours.Add(new Flavour("/IMGs/PollIMG/Fruits.png", "Fruits"));
            Flavours.Add(new Flavour("/IMGs/PollIMG/Honey.png", "Honey"));
            Flavours.Add(new Flavour("/IMGs/PollIMG/BeAPirate.png", "BeAPirate"));

            ObservableCollection<Beverage> actuallResule = FileDatabaseConnectionService.GetDataFromDatabaseWithConditions(PollPurpose.Exclusive, 5, PollMixes.Solo, Flavours, PollPricePoints.PricePoint4);

            // Assert

            if (File.Exists(newMainFile))
                File.Delete(newMainFile);

            if (Directory.Exists(newMainDirectory))
                Directory.Delete(newMainDirectory);

            Assert.Equal(expectedResult, actuallResule.Count);
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

            Beverage beverage = new Beverage() { Name = "ForPartyBoolTest", AlcoholPercentage = 500, Price = 5, Capacity = 6 };

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

            Assert.Null(actuallResult);
        }


        [Fact]
        public void DoesBeverageFulfillSetPurposeRequirement_When_It_Does_Make_GoodButCheap_Purpose()
        {
            //((beverage.Grade + beverage.GradeWithCoke) / 2) / (100 * (beverage.Price / beverage.Capacity)) > pollPurposeWeight && (beverage.Grade + beverage.GradeWithCoke) / 2 > 5
            // Arrange
            FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();
            // Act
            PollPurpose pollPurpose = PollPurpose.GoodButCheap;

            Beverage beverage = new Beverage() { Name = "GoodButCheapTest", Price = 60, Capacity = 700, Grade = 9, GradeWithCoke = 9 };

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

            Assert.Null(actuallResult);
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

            Assert.Null(actuallResult);
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

            Assert.Null(actuallResult);
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

            Beverage beverage = new Beverage() { Name = "SoloTest", Grade = 6 };

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

            Beverage beverage = new Beverage() { Name = "SoloTest", Grade = 4 };

            // Act

            Beverage actuallResult = FileDatabaseConnectionService.DoesBeverageFulfillSetMixesRequirement(pollMixes, beverage);
            // Assert

            Assert.Null(actuallResult);
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

            Beverage beverage = new Beverage() { Name = "WithCokeTest", GradeWithCoke = 4 };
            // Act

            Beverage actuallResult = FileDatabaseConnectionService.DoesBeverageFulfillSetMixesRequirement(pollMixes, beverage);
            // Assert

            Assert.Null(actuallResult);
        }


        [Fact]
        public void DoesBeverageFulfillSetFlavoursRequirement_No_Flavours()
        {
            // Arrange
            FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();

            Beverage beverage = new Beverage() { Name = "NoFlavoursTest" };

            // Act

            Beverage actuallResult = FileDatabaseConnectionService.DoesBeverageFulfillSetFlavoursRequirement(new List<Flavour>(), beverage);
            // Assert

            Assert.Equal(beverage, actuallResult);
        }

        [Fact]
        public void DoesBeverageFulfillSetFlavoursRequirement_All_Flavours()
        {
            // Arrange
            FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();

            Beverage beverage = new Beverage() { Name = "AllFlavoursTest" };

            beverage.Vanila.IsSet = true;
            beverage.Nuts.IsSet = true;
            beverage.Caramel.IsSet = true;
            beverage.Smoke.IsSet = true;
            beverage.Cinnamon.IsSet = true;
            beverage.Spirit.IsSet = true;
            beverage.Fruits.IsSet = true;
            beverage.Honey.IsSet = true;
            beverage.BeAPirate.IsSet = true;


            List<Flavour> Flavours = new List<Flavour>();

            Flavours.Add(new Flavour("/IMGs/PollIMG/Vanila.png", "Vanila"));
            Flavours.Add(new Flavour("/IMGs/PollIMG/Nuts.png", "Nuts"));
            Flavours.Add(new Flavour("/IMGs/PollIMG/Carmel.png", "Caramel"));
            Flavours.Add(new Flavour("/IMGs/PollIMG/Smoked.png", "Smoke"));
            Flavours.Add(new Flavour("/IMGs/PollIMG/Cinamon.png", "Cinnamon"));
            Flavours.Add(new Flavour("/IMGs/PollIMG/Spirit.png", "Spirit"));
            Flavours.Add(new Flavour("/IMGs/PollIMG/Fruits.png", "Fruits"));
            Flavours.Add(new Flavour("/IMGs/PollIMG/Honey.png", "Honey"));
            Flavours.Add(new Flavour("/IMGs/PollIMG/BeAPirate.png", "BeAPirate"));

            // Act

            Beverage actuallResult = FileDatabaseConnectionService.DoesBeverageFulfillSetFlavoursRequirement(Flavours, beverage);

            // Assert

            Assert.Equal(beverage, actuallResult);
        }

        [Fact]
        public void DoesBeverageFulfillSetFlavoursRequirement_Beverage_Is_Null()
        {
            // Arrange
            FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();

            Beverage beverage = null;

            // Act

            Beverage actuallResult = FileDatabaseConnectionService.DoesBeverageFulfillSetFlavoursRequirement(new List<Flavour>(), beverage);
            // Assert

            Assert.Null(actuallResult);
        }

        [Fact]
        public void DoesBeverageFulfillSetPriceRequirement_When_It_Does_Make_None_Purpose()
        {
            // Arrange


            FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();

            PollPricePoints pollPricePoints = PollPricePoints.None;

            Beverage beverage = new Beverage() { Name = "NoneTest" };

            // Act

            Beverage actuallResult = FileDatabaseConnectionService.DoesBeverageFulfillSetPriceRequirement(pollPricePoints, beverage);
            // Assert

            Assert.Equal(beverage, actuallResult);
        }

        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(49)]
        public void DoesBeverageFulfillSetPriceRequirement_When_It_Does_Make_PricePoint1_Purpose(int price)
        {
            // Arrange

            FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();

            PollPricePoints pollPricePoints = PollPricePoints.PricePoint1;

            Beverage beverage = new Beverage() { Name = "PricePoint1Test", Price = price };

            // Act

            Beverage actuallResult = FileDatabaseConnectionService.DoesBeverageFulfillSetPriceRequirement(pollPricePoints, beverage);
            // Assert

            Assert.Equal(beverage, actuallResult);
        }

        [Theory]
        [InlineData(51)]
        [InlineData(int.MaxValue)]
        public void DoesBeverageFulfillSetPriceRequirement_When_It_Does_Not_Make_PricePoint1_Purpose(int price)
        {
            // Arrange
            FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();

            PollPricePoints pollPricePoints = PollPricePoints.PricePoint1;

            Beverage beverage = new Beverage() { Name = "PricePoint1Test", Price = price };

            // Act

            Beverage actuallResult = FileDatabaseConnectionService.DoesBeverageFulfillSetPriceRequirement(pollPricePoints, beverage);
            // Assert

            Assert.Null(actuallResult);
        }


        [Theory]
        [InlineData(50)]
        [InlineData(69)]
        public void DoesBeverageFulfillSetPriceRequirement_When_It_Does_Make_PricePoint2_Purpose(int price)
        {
            // Arrange

            FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();

            PollPricePoints pollPricePoints = PollPricePoints.PricePoint2;

            Beverage beverage = new Beverage() { Name = "PricePoint2Test", Price = price };

            // Act
            Beverage actuallResult = FileDatabaseConnectionService.DoesBeverageFulfillSetPriceRequirement(pollPricePoints, beverage);

            // Assert
            Assert.Equal(beverage, actuallResult);
        }


        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(49)]
        [InlineData(70)]
        [InlineData(int.MaxValue)]
        public void DoesBeverageFulfillSetPriceRequirement_When_It_Does_Not_Make_PricePoint2_Purpose(int price)
        {
            // Arrange
            FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();

            PollPricePoints pollPricePoints = PollPricePoints.PricePoint2;

            Beverage beverage = new Beverage() { Name = "PricePoint2Test", Price = price };

            // Act

            Beverage actuallResult = FileDatabaseConnectionService.DoesBeverageFulfillSetPriceRequirement(pollPricePoints, beverage);
            // Assert

            Assert.Null(actuallResult);
        }

        [Theory]
        [InlineData(70)]
        [InlineData(89)]
        public void DoesBeverageFulfillSetPriceRequirement_When_It_Does_Make_PricePoint3_Purpose(int price)
        {
            // Arrange

            FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();

            PollPricePoints pollPricePoints = PollPricePoints.PricePoint3;

            Beverage beverage = new Beverage() { Name = "PricePoint3Test", Price = price };

            // Act
            Beverage actuallResult = FileDatabaseConnectionService.DoesBeverageFulfillSetPriceRequirement(pollPricePoints, beverage);

            // Assert
            Assert.Equal(beverage, actuallResult);
        }


        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(69)]
        [InlineData(90)]
        [InlineData(int.MaxValue)]
        public void DoesBeverageFulfillSetPriceRequirement_When_It_Does_Not_Make_PricePoint3_Purpose(int price)
        {
            // Arrange
            FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();

            PollPricePoints pollPricePoints = PollPricePoints.PricePoint3;

            Beverage beverage = new Beverage() { Name = "PricePoint3Test", Price = price };

            // Act

            Beverage actuallResult = FileDatabaseConnectionService.DoesBeverageFulfillSetPriceRequirement(pollPricePoints, beverage);
            // Assert

            Assert.Null(actuallResult);
        }

        [Theory]
        [InlineData(90)]
        [InlineData(900)]
        [InlineData(int.MaxValue)]
        public void DoesBeverageFulfillSetPriceRequirement_When_It_Does_Make_PricePoint4_Purpose(int price)
        {
            // Arrange

            FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();

            PollPricePoints pollPricePoints = PollPricePoints.PricePoint4;

            Beverage beverage = new Beverage() { Name = "PricePoint4Test", Price = price };

            // Act
            Beverage actuallResult = FileDatabaseConnectionService.DoesBeverageFulfillSetPriceRequirement(pollPricePoints, beverage);

            // Assert
            Assert.Equal(beverage, actuallResult);
        }


        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(1)]
        [InlineData(89)]
        public void DoesBeverageFulfillSetPriceRequirement_When_It_Does_Not_Make_PricePoint4_Purpose(int price)
        {
            // Arrange
            FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();

            PollPricePoints pollPricePoints = PollPricePoints.PricePoint4;

            Beverage beverage = new Beverage() { Name = "PricePoint4Test", Price = price };

            // Act

            Beverage actuallResult = FileDatabaseConnectionService.DoesBeverageFulfillSetPriceRequirement(pollPricePoints, beverage);
            // Assert

            Assert.Null(actuallResult);
        }

        [Fact]
        public void GetRandomRow_When_There_Is_No_Data_To_Randomize()
        {
            // Arrange
            string newMainDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RumikApp\\GetRandomRowTest";
            string newMainFile = newMainDirectory + "\\GetRandomRowFileTest.json";

            if (File.Exists(newMainFile))
                File.Delete(newMainFile);

            if (Directory.Exists(newMainDirectory))
                Directory.Delete(newMainDirectory);


            FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();
            FileDatabaseConnectionService.MainDataDirectory = newMainDirectory;
            FileDatabaseConnectionService.FileName = newMainFile;

            // Act
            Beverage actuallResule = FileDatabaseConnectionService.GetRandomRow();

            // Assert
            Assert.Null(actuallResule);
        }

        [Fact]
        public void GetRandomRow_When_There_Is_Data_To_Randomize()
        {
            // Arrange
            Random random = new Random(1);

            string newMainDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RumikApp\\GetAllDataTest";
            string newMainFile = newMainDirectory + "\\GetAllDataFileTest.json";

            if (File.Exists(newMainFile))
                File.Delete(newMainFile);

            if (Directory.Exists(newMainDirectory))
                Directory.Delete(newMainDirectory);


            FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();
            FileDatabaseConnectionService.MainDataDirectory = newMainDirectory;
            FileDatabaseConnectionService.FileName = newMainFile;

            Beverage expectedResult = new Beverage() { Name = "ThatOne" };

            ObservableCollection<Beverage> ListOfBeverages = new ObservableCollection<Beverage>();
            ListOfBeverages.Add(new Beverage() { Name = "NotThisOne" });
            ListOfBeverages.Add(expectedResult);
            ListOfBeverages.Add(new Beverage() { Name = "NotThisOne" });
            ListOfBeverages.Add(new Beverage() { Name = "NotThisOne" });
            ListOfBeverages.Add(new Beverage() { Name = "NotThisOne" });
            ListOfBeverages.Add(new Beverage() { Name = "NotThisOne" });

            foreach (Beverage beverage in ListOfBeverages)
                FileDatabaseConnectionService.SaveBevreageToDatabase(beverage, null);

            // Act
            Beverage actuallResule = FileDatabaseConnectionService.GetRandomRow(random);

            // Assert
            Assert.Equal(expectedResult.Name, actuallResule.Name);
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
