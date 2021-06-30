using System.Windows;

namespace RumikApp.Services
{
    public interface IPanelVisibilityService
    {

        Visibility MainPanelVisibility { get; set; }
        Visibility PollVisibility { get; set; }
        Visibility InsertDataToDatabaseFormVisibility { get; set; }
        Visibility DataGridViewModelVisibility { get; set; }
        Visibility RandomDataGridVisibility { get; set; }
        Visibility ItemsControlVisibility { get; set; }
        Visibility EditLocalDataVisibility { get; set; }
    }
}