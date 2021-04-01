using System.Collections.ObjectModel;

namespace RumikApp.Services
{
    public interface IInformationBusService
    {
        ObservableCollection<Beverage> Beverages { get; set; }
    }
}