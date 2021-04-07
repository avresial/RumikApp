using Moq;
using Newtonsoft.Json;
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
        private int JSONListOfBeveragesCountInTotal;
        private int JSONListOfBeveragesCountOfPirateBeverages;

        private string JSONListOfBeverages;

        private Mock<IFileService> fileService;
        private Mock<IStreamReaderService> streamReaderService;

        private FileDatabaseConnectionService sut;

        //setup
        #region Setup 

        public FileDatabaseConnectionServiceTests()
        {
            fileService = new Mock<IFileService>();
            streamReaderService = new Mock<IStreamReaderService>();
            sut = new FileDatabaseConnectionService(fileService.Object, streamReaderService.Object);

            List<JsonBeverage> jsonBeverage = new List<JsonBeverage>();

            JSONListOfBeveragesCountOfPirateBeverages = 1;

            for (int i = 0; i < JSONListOfBeveragesCountOfPirateBeverages; i++)
            {
                JsonBeverage JsonBeverage = new JsonBeverage();
                JsonBeverage.Name = "Pirate Beverage";
                JsonBeverage.BeAPirate.IsSet = true;
                jsonBeverage.Add(JsonBeverage);
            }

            JSONListOfBeveragesCountInTotal = jsonBeverage.Count();
            JSONListOfBeverages = JsonConvert.SerializeObject(jsonBeverage);

        }

        private JsonBeverage getFullFlavourJsonBeverage()
        {
            JsonBeverage FullFlavourBeverage = new JsonBeverage() { Name = "Full Flavour Beverage" };

            FullFlavourBeverage.Vanila.IsSet = true;
            FullFlavourBeverage.Nuts.IsSet = true;
            FullFlavourBeverage.Caramel.IsSet = true;
            FullFlavourBeverage.Smoke.IsSet = true;
            FullFlavourBeverage.Cinnamon.IsSet = true;
            FullFlavourBeverage.Spirit.IsSet = true;
            FullFlavourBeverage.Fruits.IsSet = true;
            FullFlavourBeverage.Honey.IsSet = true;
            FullFlavourBeverage.BeAPirate.IsSet = true;

            FullFlavourBeverage.Price = 1000;
            FullFlavourBeverage.Capacity = 50;
            FullFlavourBeverage.Grade = 12;

            return FullFlavourBeverage;
        }

        #endregion

        [Fact]
        public void GetAllData_When_There_Are_No_File_To_Load()
        {
            // Arrange
            fileService.Setup(x => x.FileExists(It.IsAny<string>())).Returns(false);
            streamReaderService.Setup(x => x.ReadToEnd()).Returns(JSONListOfBeverages);

            // Act
            ObservableCollection<Beverage> actuallResule = sut.GetAllData();

            // Assert
            Assert.Empty(actuallResule);
        }

        [Fact]
        public void GetAllData_When_There_Are_No_Data_To_Load()
        {
            // Arrange

            fileService.Setup(x => x.FileExists(It.IsAny<string>())).Returns(true);
            streamReaderService.Setup(x => x.ReadToEnd()).Returns("");

            // Act
            ObservableCollection<Beverage> actuallResule = sut.GetAllData();

            // Assert

            Assert.True(0 == actuallResule.Count);
        }

        [Fact]
        public void GetAllData_When_There_Is_Some_Data_To_Load()
        {
            // Arrange
            fileService.Setup(x => x.FileExists(It.IsAny<string>())).Returns(true);
            streamReaderService.Setup(x => x.ReadToEnd()).Returns(JSONListOfBeverages);

            // Act
            ObservableCollection<Beverage> actuallResule = sut.GetAllData();

            // Assert
            Assert.True(JSONListOfBeveragesCountInTotal == actuallResule.Count);

        }

        [Fact]
        public void GetAllPiratesBeverages_When_There_Is_Data_To_Load()
        {
            // Arrange
            fileService.Setup(x => x.FileExists(It.IsAny<string>())).Returns(true);
            streamReaderService.Setup(x => x.ReadToEnd()).Returns(JSONListOfBeverages);

            // Act
            ObservableCollection<Beverage> actuallResule = sut.GetAllPiratesBeverages();

            // Assert
            Assert.Equal(JSONListOfBeveragesCountOfPirateBeverages, actuallResule.Count);

        }

        [Fact]
        public void GetDataFromDatabaseWithConditions_When_There_Are_No_Conditions()
        {
            // Arrange
            fileService.Setup(x => x.FileExists(It.IsAny<string>())).Returns(true);
            streamReaderService.Setup(x => x.ReadToEnd()).Returns(JSONListOfBeverages);

            // Act
            ObservableCollection<Beverage> actuallResule = sut.GetDataFromDatabaseWithConditions(PollPurpose.None, 5, PollMixes.None, new List<Flavour>(), PollPricePoints.None);

            // Assert

            Assert.True(JSONListOfBeveragesCountOfPirateBeverages == actuallResule.Count());
        }

        [Theory]
        [InlineData(2, 1)]
        [InlineData(2, 2)]
        public void GetAllData_When_There_Are_All_Conditions(int NumberOfElements, int NumberOfMatchingElements)
        {
            // Arrange
            int expectedResult = NumberOfMatchingElements;

            ObservableCollection<JsonBeverage> ListOfBeverages = new ObservableCollection<JsonBeverage>();

            for (int i = 0; i < NumberOfElements - NumberOfMatchingElements; i++)
                ListOfBeverages.Add(new JsonBeverage() { Name = "NotMatching" + i, TestIcon = null });

            for (int i = 0; i < NumberOfMatchingElements; i++)
                ListOfBeverages.Add(getFullFlavourJsonBeverage());

            fileService.Setup(x => x.FileExists(It.IsAny<string>())).Returns(true);
            streamReaderService.Setup(x => x.ReadToEnd()).Returns(JsonConvert.SerializeObject(ListOfBeverages));

            // Creating list of all flavours
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
            ObservableCollection<Beverage> actuallResule = sut.GetDataFromDatabaseWithConditions(PollPurpose.Exclusive, 5, PollMixes.Solo, Flavours, PollPricePoints.PricePoint4);

            // Assert
            Assert.Equal(expectedResult, actuallResule.Count);
        }


        [Fact]
        public void DoesBeverageFulfillSetPurposeRequirement_When_It_Does_Make_None_Purpose()
        {
            // Arrange
            PollPurpose pollPurpose = PollPurpose.None;
            Beverage beverage = new Beverage() { Name = "NoneTest" };

            // Act
            Beverage actuallResult = sut.DoesBeverageFulfillSetPurposeRequirement(pollPurpose, 0, beverage);

            // Assert
            Assert.Equal(beverage, actuallResult);
        }


        [Fact]
        public void DoesBeverageFulfillSetPurposeRequirement_When_It_Does_Make_ForPartyBool_Purpose()
        {
            // Arrange
            PollPurpose pollPurpose = PollPurpose.ForPartyBool;
            Beverage beverage = new Beverage() { Name = "ForPartyBoolTest", AlcoholPercentage = 500, Price = 5, Capacity = 6 };

            // Act
            Beverage actuallResult = sut.DoesBeverageFulfillSetPurposeRequirement(pollPurpose, 5, beverage);

            // Assert
            Assert.Equal(beverage, actuallResult);
        }

        [Fact]
        public void DoesBeverageFulfillSetPurposeRequirement_When_It_Does_Not_Make_ForPartyBool_Purpose()
        {
            // Arrange
            PollPurpose pollPurpose = PollPurpose.ForPartyBool;
            Beverage beverage = new Beverage() { Name = "ForPartyBoolTest", AlcoholPercentage = 400, Price = 5, Capacity = 6 };

            // Act
            Beverage actuallResult = sut.DoesBeverageFulfillSetPurposeRequirement(pollPurpose, 5, beverage);

            // Assert
            Assert.Null(actuallResult);
        }


        [Theory]
        [InlineData(60, 700, 9, 9, 5)]
        public void DoesBeverageFulfillSetPurposeRequirement_When_It_Does_Make_GoodButCheap_Purpose(int price, int capacity, int grade, int gradeWithCoke, int pollPurposeWeight)
        {
            // Arrange
            PollPurpose pollPurpose = PollPurpose.GoodButCheap;
            Beverage beverage = new Beverage() { Name = "GoodButCheapTest", Price = price, Capacity = capacity, Grade = grade, GradeWithCoke = gradeWithCoke };

            // Act
            Beverage actuallResult = sut.DoesBeverageFulfillSetPurposeRequirement(pollPurpose, pollPurposeWeight, beverage);

            // Assert
            Assert.Equal(beverage, actuallResult);
        }

        [Theory]
        [InlineData(60, 700, 6, 6, 5)]
        public void DoesBeverageFulfillSetPurposeRequirement_When_It_Does_Not_Make_GoodButCheap_Purpose(int price, int capacity, int grade, int gradeWithCoke, int pollPurposeWeight)
        {
            // Arrange
            PollPurpose pollPurpose = PollPurpose.ForPartyBool;
            Beverage beverage = new Beverage() { Name = "GoodButCheapTest", Price = price, Capacity = capacity, Grade = grade, GradeWithCoke = gradeWithCoke };

            // Act
            Beverage actuallResult = sut.DoesBeverageFulfillSetPurposeRequirement(pollPurpose, pollPurposeWeight, beverage);

            // Assert
            Assert.Null(actuallResult);
        }


        //    [Fact]
        //    public void DoesBeverageFulfillSetPurposeRequirement_When_It_Does_Make_Exclusive_Purpose()
        //    {
        //        // Arrange
        //        FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();
        //        // Act
        //        PollPurpose pollPurpose = PollPurpose.Exclusive;

        //        Beverage beverage = new Beverage() { Name = "GoodButCheapTest", Price = 120, Capacity = 700, Grade = 9, GradeWithCoke = 9, AlcoholPercentage = 40 };

        //        Beverage actuallResult = FileDatabaseConnectionService.DoesBeverageFulfillSetPurposeRequirement(pollPurpose, 5, beverage);
        //        // Assert

        //        Assert.Equal(beverage, actuallResult);
        //    }

        //    [Fact]
        //    public void DoesBeverageFulfillSetPurposeRequirement_When_It_Does_Not_Make_Exclusive_Purpose()
        //    {
        //        // Arrange
        //        FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();
        //        // Act
        //        PollPurpose pollPurpose = PollPurpose.Exclusive;

        //        Beverage beverage = new Beverage() { Name = "GoodButCheapTest", Price = 60, Capacity = 700, Grade = 6, GradeWithCoke = 6, AlcoholPercentage = 40 };

        //        Beverage actuallResult = FileDatabaseConnectionService.DoesBeverageFulfillSetPurposeRequirement(pollPurpose, 5, beverage);
        //        // Assert

        //        Assert.Null(actuallResult);
        //    }


        //    [Fact]
        //    public void DoesBeverageFulfillSetPurposeRequirement_When_It_Does_Make_ForPiratesFromCarabien_Purpose()
        //    {
        //        // Arrange
        //        FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();

        //        PollPurpose pollPurpose = PollPurpose.ForPiratesFromCarabien;

        //        Flavour BeAPirate = new Flavour("/IMGs/PollIMG/BeAPirate.png", "BeAPirate");
        //        BeAPirate.IsSet = true;

        //        Beverage beverage = new Beverage() { Name = "ForPiratesFromCarabienTest", Price = 120, Capacity = 700, Grade = 9, GradeWithCoke = 9, AlcoholPercentage = 40, BeAPirate = BeAPirate };


        //        // Act

        //        Beverage actuallResult = FileDatabaseConnectionService.DoesBeverageFulfillSetPurposeRequirement(pollPurpose, 5, beverage);
        //        // Assert

        //        Assert.Equal(beverage, actuallResult);
        //    }

        //    [Fact]
        //    public void DoesBeverageFulfillSetPurposeRequirement_When_It_Does_Not_Make_ForPiratesFromCarabien_Purpose()
        //    {
        //        // Arrange
        //        FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();
        //        // Act
        //        PollPurpose pollPurpose = PollPurpose.ForPiratesFromCarabien;

        //        Beverage beverage = new Beverage() { Name = "ForPiratesFromCarabienTest", Price = 60, Capacity = 700, Grade = 6, GradeWithCoke = 6, AlcoholPercentage = 40 };

        //        Beverage actuallResult = FileDatabaseConnectionService.DoesBeverageFulfillSetPurposeRequirement(pollPurpose, 5, beverage);
        //        // Assert

        //        Assert.Null(actuallResult);
        //    }


        //    [Fact]
        //    public void DoesBeverageFulfillSetMixesRequirement_When_It_Does_Make_None_Purpose()
        //    {
        //        // Arrange
        //        FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();

        //        PollMixes pollMixes = PollMixes.None;

        //        Beverage beverage = new Beverage() { Name = "NoneTest" };

        //        // Act

        //        Beverage actuallResult = FileDatabaseConnectionService.DoesBeverageFulfillSetMixesRequirement(pollMixes, beverage);
        //        // Assert

        //        Assert.Equal(beverage, actuallResult);
        //    }


        //    [Fact]
        //    public void DoesBeverageFulfillSetMixesRequirement_When_It_Does_Make_Solo_Purpose()
        //    {
        //        // Arrange
        //        FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();

        //        PollMixes pollMixes = PollMixes.Solo;

        //        Beverage beverage = new Beverage() { Name = "SoloTest", Grade = 6 };

        //        // Act

        //        Beverage actuallResult = FileDatabaseConnectionService.DoesBeverageFulfillSetMixesRequirement(pollMixes, beverage);
        //        // Assert

        //        Assert.Equal(beverage, actuallResult);
        //    }

        //    [Fact]
        //    public void DoesBeverageFulfillSetMixesRequirement_When_It_Does_Not_Make_Solo_Purpose()
        //    {
        //        // Arrange
        //        FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();

        //        PollMixes pollMixes = PollMixes.Solo;

        //        Beverage beverage = new Beverage() { Name = "SoloTest", Grade = 4 };

        //        // Act

        //        Beverage actuallResult = FileDatabaseConnectionService.DoesBeverageFulfillSetMixesRequirement(pollMixes, beverage);
        //        // Assert

        //        Assert.Null(actuallResult);
        //    }


        //    [Fact]
        //    public void DoesBeverageFulfillSetMixesRequirement_When_It_Does_Make_WithCoke_Purpose()
        //    {
        //        // Arrange
        //        FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();

        //        PollMixes pollMixes = PollMixes.WithCoke;

        //        Beverage beverage = new Beverage() { Name = "WithCokeTest", GradeWithCoke = 6, };

        //        // Act

        //        Beverage actuallResult = FileDatabaseConnectionService.DoesBeverageFulfillSetMixesRequirement(pollMixes, beverage);
        //        // Assert

        //        Assert.Equal(beverage, actuallResult);
        //    }

        //    [Fact]
        //    public void DoesBeverageFulfillSetMixesRequirement_When_It_Does_Not_Make_WithCoke_Purpose()
        //    {
        //        // Arrange
        //        FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();

        //        PollMixes pollMixes = PollMixes.WithCoke;

        //        Beverage beverage = new Beverage() { Name = "WithCokeTest", GradeWithCoke = 4 };
        //        // Act

        //        Beverage actuallResult = FileDatabaseConnectionService.DoesBeverageFulfillSetMixesRequirement(pollMixes, beverage);
        //        // Assert

        //        Assert.Null(actuallResult);
        //    }


        //    [Fact]
        //    public void DoesBeverageFulfillSetFlavoursRequirement_No_Flavours()
        //    {
        //        // Arrange
        //        FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();

        //        Beverage beverage = new Beverage() { Name = "NoFlavoursTest" };

        //        // Act

        //        Beverage actuallResult = FileDatabaseConnectionService.DoesBeverageFulfillSetFlavoursRequirement(new List<Flavour>(), beverage);
        //        // Assert

        //        Assert.Equal(beverage, actuallResult);
        //    }

        //    [Fact]
        //    public void DoesBeverageFulfillSetFlavoursRequirement_All_Flavours()
        //    {
        //        // Arrange
        //        FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();

        //        Beverage beverage = new Beverage() { Name = "AllFlavoursTest" };

        //        beverage.Vanila.IsSet = true;
        //        beverage.Nuts.IsSet = true;
        //        beverage.Caramel.IsSet = true;
        //        beverage.Smoke.IsSet = true;
        //        beverage.Cinnamon.IsSet = true;
        //        beverage.Spirit.IsSet = true;
        //        beverage.Fruits.IsSet = true;
        //        beverage.Honey.IsSet = true;
        //        beverage.BeAPirate.IsSet = true;


        //        List<Flavour> Flavours = new List<Flavour>();

        //        Flavours.Add(new Flavour("/IMGs/PollIMG/Vanila.png", "Vanila"));
        //        Flavours.Add(new Flavour("/IMGs/PollIMG/Nuts.png", "Nuts"));
        //        Flavours.Add(new Flavour("/IMGs/PollIMG/Carmel.png", "Caramel"));
        //        Flavours.Add(new Flavour("/IMGs/PollIMG/Smoked.png", "Smoke"));
        //        Flavours.Add(new Flavour("/IMGs/PollIMG/Cinamon.png", "Cinnamon"));
        //        Flavours.Add(new Flavour("/IMGs/PollIMG/Spirit.png", "Spirit"));
        //        Flavours.Add(new Flavour("/IMGs/PollIMG/Fruits.png", "Fruits"));
        //        Flavours.Add(new Flavour("/IMGs/PollIMG/Honey.png", "Honey"));
        //        Flavours.Add(new Flavour("/IMGs/PollIMG/BeAPirate.png", "BeAPirate"));

        //        // Act

        //        Beverage actuallResult = FileDatabaseConnectionService.DoesBeverageFulfillSetFlavoursRequirement(Flavours, beverage);

        //        // Assert

        //        Assert.Equal(beverage, actuallResult);
        //    }

        //    [Fact]
        //    public void DoesBeverageFulfillSetFlavoursRequirement_Beverage_Is_Null()
        //    {
        //        // Arrange
        //        FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();

        //        Beverage beverage = null;

        //        // Act

        //        Beverage actuallResult = FileDatabaseConnectionService.DoesBeverageFulfillSetFlavoursRequirement(new List<Flavour>(), beverage);
        //        // Assert

        //        Assert.Null(actuallResult);
        //    }

        //    [Fact]
        //    public void DoesBeverageFulfillSetPriceRequirement_When_It_Does_Make_None_Purpose()
        //    {
        //        // Arrange


        //        FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();

        //        PollPricePoints pollPricePoints = PollPricePoints.None;

        //        Beverage beverage = new Beverage() { Name = "NoneTest" };

        //        // Act

        //        Beverage actuallResult = FileDatabaseConnectionService.DoesBeverageFulfillSetPriceRequirement(pollPricePoints, beverage);
        //        // Assert

        //        Assert.Equal(beverage, actuallResult);
        //    }

        //    [Theory]
        //    [InlineData(int.MinValue)]
        //    [InlineData(49)]
        //    public void DoesBeverageFulfillSetPriceRequirement_When_It_Does_Make_PricePoint1_Purpose(int price)
        //    {
        //        // Arrange

        //        FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();

        //        PollPricePoints pollPricePoints = PollPricePoints.PricePoint1;

        //        Beverage beverage = new Beverage() { Name = "PricePoint1Test", Price = price };

        //        // Act

        //        Beverage actuallResult = FileDatabaseConnectionService.DoesBeverageFulfillSetPriceRequirement(pollPricePoints, beverage);
        //        // Assert

        //        Assert.Equal(beverage, actuallResult);
        //    }

        //    [Theory]
        //    [InlineData(51)]
        //    [InlineData(int.MaxValue)]
        //    public void DoesBeverageFulfillSetPriceRequirement_When_It_Does_Not_Make_PricePoint1_Purpose(int price)
        //    {
        //        // Arrange
        //        FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();

        //        PollPricePoints pollPricePoints = PollPricePoints.PricePoint1;

        //        Beverage beverage = new Beverage() { Name = "PricePoint1Test", Price = price };

        //        // Act

        //        Beverage actuallResult = FileDatabaseConnectionService.DoesBeverageFulfillSetPriceRequirement(pollPricePoints, beverage);
        //        // Assert

        //        Assert.Null(actuallResult);
        //    }


        //    [Theory]
        //    [InlineData(50)]
        //    [InlineData(69)]
        //    public void DoesBeverageFulfillSetPriceRequirement_When_It_Does_Make_PricePoint2_Purpose(int price)
        //    {
        //        // Arrange

        //        FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();

        //        PollPricePoints pollPricePoints = PollPricePoints.PricePoint2;

        //        Beverage beverage = new Beverage() { Name = "PricePoint2Test", Price = price };

        //        // Act
        //        Beverage actuallResult = FileDatabaseConnectionService.DoesBeverageFulfillSetPriceRequirement(pollPricePoints, beverage);

        //        // Assert
        //        Assert.Equal(beverage, actuallResult);
        //    }


        //    [Theory]
        //    [InlineData(int.MinValue)]
        //    [InlineData(49)]
        //    [InlineData(70)]
        //    [InlineData(int.MaxValue)]
        //    public void DoesBeverageFulfillSetPriceRequirement_When_It_Does_Not_Make_PricePoint2_Purpose(int price)
        //    {
        //        // Arrange
        //        FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();

        //        PollPricePoints pollPricePoints = PollPricePoints.PricePoint2;

        //        Beverage beverage = new Beverage() { Name = "PricePoint2Test", Price = price };

        //        // Act

        //        Beverage actuallResult = FileDatabaseConnectionService.DoesBeverageFulfillSetPriceRequirement(pollPricePoints, beverage);
        //        // Assert

        //        Assert.Null(actuallResult);
        //    }

        //    [Theory]
        //    [InlineData(70)]
        //    [InlineData(89)]
        //    public void DoesBeverageFulfillSetPriceRequirement_When_It_Does_Make_PricePoint3_Purpose(int price)
        //    {
        //        // Arrange

        //        FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();

        //        PollPricePoints pollPricePoints = PollPricePoints.PricePoint3;

        //        Beverage beverage = new Beverage() { Name = "PricePoint3Test", Price = price };

        //        // Act
        //        Beverage actuallResult = FileDatabaseConnectionService.DoesBeverageFulfillSetPriceRequirement(pollPricePoints, beverage);

        //        // Assert
        //        Assert.Equal(beverage, actuallResult);
        //    }


        //    [Theory]
        //    [InlineData(int.MinValue)]
        //    [InlineData(69)]
        //    [InlineData(90)]
        //    [InlineData(int.MaxValue)]
        //    public void DoesBeverageFulfillSetPriceRequirement_When_It_Does_Not_Make_PricePoint3_Purpose(int price)
        //    {
        //        // Arrange
        //        FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();

        //        PollPricePoints pollPricePoints = PollPricePoints.PricePoint3;

        //        Beverage beverage = new Beverage() { Name = "PricePoint3Test", Price = price };

        //        // Act

        //        Beverage actuallResult = FileDatabaseConnectionService.DoesBeverageFulfillSetPriceRequirement(pollPricePoints, beverage);
        //        // Assert

        //        Assert.Null(actuallResult);
        //    }

        //    [Theory]
        //    [InlineData(90)]
        //    [InlineData(900)]
        //    [InlineData(int.MaxValue)]
        //    public void DoesBeverageFulfillSetPriceRequirement_When_It_Does_Make_PricePoint4_Purpose(int price)
        //    {
        //        // Arrange

        //        FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();

        //        PollPricePoints pollPricePoints = PollPricePoints.PricePoint4;

        //        Beverage beverage = new Beverage() { Name = "PricePoint4Test", Price = price };

        //        // Act
        //        Beverage actuallResult = FileDatabaseConnectionService.DoesBeverageFulfillSetPriceRequirement(pollPricePoints, beverage);

        //        // Assert
        //        Assert.Equal(beverage, actuallResult);
        //    }


        //    [Theory]
        //    [InlineData(int.MinValue)]
        //    [InlineData(1)]
        //    [InlineData(89)]
        //    public void DoesBeverageFulfillSetPriceRequirement_When_It_Does_Not_Make_PricePoint4_Purpose(int price)
        //    {
        //        // Arrange
        //        FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();

        //        PollPricePoints pollPricePoints = PollPricePoints.PricePoint4;

        //        Beverage beverage = new Beverage() { Name = "PricePoint4Test", Price = price };

        //        // Act

        //        Beverage actuallResult = FileDatabaseConnectionService.DoesBeverageFulfillSetPriceRequirement(pollPricePoints, beverage);
        //        // Assert

        //        Assert.Null(actuallResult);
        //    }

        //    [Fact]
        //    public void GetRandomRow_When_There_Is_No_Data_To_Randomize()
        //    {
        //        // Arrange
        //        string newMainDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RumikApp\\GetRandomRowTest";
        //        string newMainFile = newMainDirectory + "\\GetRandomRowFileTest.json";

        //        if (File.Exists(newMainFile))
        //            File.Delete(newMainFile);

        //        if (Directory.Exists(newMainDirectory))
        //            Directory.Delete(newMainDirectory);


        //        FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();
        //        FileDatabaseConnectionService.MainDataDirectory = newMainDirectory;
        //        FileDatabaseConnectionService.FileName = newMainFile;

        //        // Act
        //        Beverage actuallResule = FileDatabaseConnectionService.GetRandomRow();

        //        // Assert
        //        Assert.Null(actuallResule);
        //    }

        //    [Fact]
        //    public void GetRandomRow_When_There_Is_Data_To_Randomize()
        //    {
        //        // Arrange
        //        Random random = new Random(1);

        //        string newMainDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RumikApp\\GetAllDataTest";
        //        string newMainFile = newMainDirectory + "\\GetAllDataFileTest.json";

        //        if (File.Exists(newMainFile))
        //            File.Delete(newMainFile);

        //        if (Directory.Exists(newMainDirectory))
        //            Directory.Delete(newMainDirectory);


        //        FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();
        //        FileDatabaseConnectionService.MainDataDirectory = newMainDirectory;
        //        FileDatabaseConnectionService.FileName = newMainFile;

        //        Beverage expectedResult = new Beverage() { Name = "ThatOne" };

        //        ObservableCollection<Beverage> ListOfBeverages = new ObservableCollection<Beverage>();
        //        ListOfBeverages.Add(new Beverage() { Name = "NotThisOne" });
        //        ListOfBeverages.Add(expectedResult);
        //        ListOfBeverages.Add(new Beverage() { Name = "NotThisOne" });
        //        ListOfBeverages.Add(new Beverage() { Name = "NotThisOne" });
        //        ListOfBeverages.Add(new Beverage() { Name = "NotThisOne" });
        //        ListOfBeverages.Add(new Beverage() { Name = "NotThisOne" });

        //        foreach (Beverage beverage in ListOfBeverages)
        //            FileDatabaseConnectionService.SaveBevreageToDatabase(beverage, null);

        //        // Act
        //        Beverage actuallResule = FileDatabaseConnectionService.GetRandomRow(random);

        //        // Assert
        //        Assert.Equal(expectedResult.Name, actuallResule.Name);
        //    }

        //    [Fact]
        //    public void TestConnectionToDatabase_When_There_Is_No_Directory()
        //    {
        //        // Arrange

        //        string newMainDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RumikApp\\DirectoryTest1";

        //        if (Directory.Exists(newMainDirectory))
        //            Directory.Delete(newMainDirectory);

        //        FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();
        //        FileDatabaseConnectionService.MainDataDirectory = newMainDirectory;

        //        bool expectedResult = false;

        //        // Act

        //        bool actuallResule = FileDatabaseConnectionService.TestConnectionToDatabase();

        //        // Assert
        //        Assert.Equal(expectedResult, actuallResule);
        //    }

        //    [Fact]
        //    public void TestConnectionToDatabase_When_There_Is_A_Directory()
        //    {
        //        // Arrange

        //        string newMainDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RumikApp\\DirectoryTest2";

        //        if (!Directory.Exists(newMainDirectory))
        //            Directory.CreateDirectory(newMainDirectory);

        //        FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();
        //        FileDatabaseConnectionService.MainDataDirectory = newMainDirectory;

        //        bool expectedResult = true;

        //        // Act

        //        bool actuallResule = FileDatabaseConnectionService.TestConnectionToDatabase();

        //        // Assert
        //        Assert.Equal(expectedResult, actuallResule);

        //        if (Directory.Exists(newMainDirectory))
        //            Directory.Delete(newMainDirectory);
        //    }

        //    [Fact]
        //    public void TestConnectionToTable_There_Is_No_File()
        //    {
        //        // Arrange

        //        string newMainDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RumikApp\\FileTest1";
        //        string newMainFile = newMainDirectory + "\\FileTest1.json";

        //        if (!Directory.Exists(newMainDirectory))
        //            Directory.CreateDirectory(newMainDirectory);

        //        if (!File.Exists(newMainFile))
        //            File.Delete(newMainFile);

        //        FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();

        //        FileDatabaseConnectionService.MainDataDirectory = newMainDirectory;
        //        FileDatabaseConnectionService.FileName = newMainFile;

        //        bool expectedResult = false;

        //        // Act

        //        bool actuallResule = FileDatabaseConnectionService.TestConnectionToTable(Enums.AvailableTables.RumsBase);

        //        // Assert
        //        Assert.Equal(expectedResult, actuallResule);

        //        if (Directory.Exists(newMainDirectory))
        //            Directory.Delete(newMainDirectory);
        //    }

        //    [Fact]
        //    public void TestConnectionToTable_There_Is_A_File()
        //    {
        //        // Arrange

        //        string newMainDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RumikApp\\FileTest2";
        //        string newMainFile = newMainDirectory + "\\FileTest2.json";

        //        if (!Directory.Exists(newMainDirectory))
        //            Directory.CreateDirectory(newMainDirectory);

        //        if (!File.Exists(newMainFile))
        //            File.Create(newMainFile).Close();

        //        FileDatabaseConnectionService FileDatabaseConnectionService = new FileDatabaseConnectionService();

        //        FileDatabaseConnectionService.MainDataDirectory = newMainDirectory;
        //        FileDatabaseConnectionService.FileName = newMainFile;

        //        bool expectedResult = true;

        //        // Act

        //        bool actuallResule = FileDatabaseConnectionService.TestConnectionToTable(Enums.AvailableTables.RumsBase);

        //        // Assert

        //        if (File.Exists(newMainFile))
        //            File.Delete(newMainFile);

        //        if (Directory.Exists(newMainDirectory))
        //            Directory.Delete(newMainDirectory);

        //        Assert.Equal(expectedResult, actuallResule);
        //    }

        //}
    }
}
