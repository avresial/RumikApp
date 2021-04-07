using RumikApp.Services;
using System.Windows;
using System.Collections.ObjectModel;
using Xunit;

namespace RumikApp.Tests
{
    public class InformationBusServiceTests
    {
        [Fact]
        public void Beverages_IsEmpty() 
        {
            // Arrange
            Visibility expectedVisibilityIsBeverageEmpty = Visibility.Visible;
            Visibility expectedVisibilityIsBeverageNotEmpty = Visibility.Collapsed;

            // Act
            InformationBusService informationBusService = new InformationBusService();
            informationBusService.Beverages = new ObservableCollection<Beverage>();
            informationBusService.Beverages.Clear();

            Visibility actualVisibilityIsBeverageEmpty = informationBusService.IsBeverageEmpty;
            Visibility actualVisibilityIsBeverageNotEmpty = informationBusService.IsBeverageNotEmpty;


            // Assert
            Assert.Equal(expectedVisibilityIsBeverageEmpty, actualVisibilityIsBeverageEmpty);
            Assert.Equal(expectedVisibilityIsBeverageNotEmpty, actualVisibilityIsBeverageNotEmpty);
        }

        [Fact]
        public void Beverages_IsNotEmpty()
        {
            // Arrange
            Visibility expectedVisibilityIsBeverageEmpty = Visibility.Collapsed;
            Visibility expectedVisibilityIsBeverageNotEmpty = Visibility.Visible;

            // Act
            InformationBusService informationBusService = new InformationBusService();
            informationBusService.Beverages = new ObservableCollection<Beverage>() { new Beverage()};
            

            Visibility actualVisibilityIsBeverageEmpty = informationBusService.IsBeverageEmpty;
            Visibility actualVisibilityIsBeverageNotEmpty = informationBusService.IsBeverageNotEmpty;


            // Assert
            Assert.Equal(expectedVisibilityIsBeverageEmpty, actualVisibilityIsBeverageEmpty);
            Assert.Equal(expectedVisibilityIsBeverageNotEmpty, actualVisibilityIsBeverageNotEmpty);
        }
    }
}
