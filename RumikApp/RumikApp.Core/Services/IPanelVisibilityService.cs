using System.Windows;

namespace RumikApp.Core.Services
{
    public interface IPanelVisibilityService
    {
        Visibility WelcomePanelVisibility { get; set; }
        Visibility MainPanelVisibility { get; set; }
        Visibility PollVisibility { get; set; }
        Visibility InsertDataToDatabaseFormVisibility { get; set; }
        Visibility DataGridViewModelVisibility { get; set; }
        Visibility RandomDataGridVisibility { get; set; }
        Visibility ItemsControlVisibility { get; set; }
        Visibility EditLocalDataVisibility { get; set; }
    }
}