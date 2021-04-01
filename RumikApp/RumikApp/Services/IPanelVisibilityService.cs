using System.Windows;

namespace RumikApp.Services
{
    public interface IPanelVisibilityService
    {

        Visibility MainPanelVisibility { get; set; }
        Visibility PollVisibility { get; set; }
        Visibility InsertDataToDatabaseFormVisibility { get; set; }
        Visibility DataGridViewModelVisibility { get; set; }
        Visibility DataGridViewModel2Visibility { get; set; }
        Visibility ItemsControlVisibility { get; set; }
    }
}