using System.Collections.ObjectModel;
using System.Windows;

namespace RumikApp.Services
{
    public interface IInformationBusService: ISortingService
    {
        Visibility IsBeverageEmpty { get; set; }
        Visibility IsBeverageNotEmpty { get; set; }
    }
}