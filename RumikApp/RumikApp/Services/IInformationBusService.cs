using System.Collections.ObjectModel;
using System.Windows;

namespace RumikApp.Services
{
    public interface IInformationBusService
    {
        Visibility IsBeverageEmpty { get; set; }
        Visibility IsBeverageNotEmpty { get; set; }
        ISortingService SortableBeverages { get; set; }
        ObservableCollection<Beverage> Beverages { get; set; }
    }
}