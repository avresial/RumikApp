using GalaSoft.MvvmLight;
using RumikApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RumikApp.ViewModels
{
    public class WelcomePanelViewModel : ViewModelBase
    {

        private IFileDatabaseConnectionService FileDatabaseConnectionService;

        private IPanelVisibilityService _PanelVisibilityService;
        public IPanelVisibilityService PanelVisibilityService
        {
            get { return _PanelVisibilityService; }
            set
            {
                if (_PanelVisibilityService == value)
                    return;

                _PanelVisibilityService = value;
                RaisePropertyChanged(nameof(PanelVisibilityService));
            }
        }

        public WelcomePanelViewModel(IPanelVisibilityService panelVisibilityService, IFileDatabaseConnectionService fileDatabaseConnectionService)
        {
            this.FileDatabaseConnectionService = fileDatabaseConnectionService;
            this.PanelVisibilityService = panelVisibilityService;

            //FileDatabaseConnectionService.ChangeIsUserAbove18State(true);

            if (fileDatabaseConnectionService.CheckIsUserAbove18())
                PanelVisibilityService.MainPanelVisibility = Visibility.Visible;

        }
    }
}
