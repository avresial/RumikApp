using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using RumikApp.Models;
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

            if ((FileDatabaseConnectionService.ReadSettings()).IsUserAbove18)
                PanelVisibilityService.MainPanelVisibility = Visibility.Visible;

        }

        private RelayCommand _IAmAbove18;
        public RelayCommand IAmAbove18
        {
            get
            {
                if (_IAmAbove18 == null)
                {
                    _IAmAbove18 = new RelayCommand(
                    () =>
                    {
                        Settings newSettings = FileDatabaseConnectionService.ReadSettings();
                        newSettings.IsUserAbove18 = true;
                        FileDatabaseConnectionService.SaveSettings(newSettings);
                        PanelVisibilityService.MainPanelVisibility = Visibility.Visible;
                    },
                    () =>
                    {
                        return true;
                    });
                }

                return _IAmAbove18;
            }
        }

        private RelayCommand _IAmNotAvove18;
        public RelayCommand IAmNotAvove18
        {
            get
            {
                if (_IAmNotAvove18 == null)
                {
                    _IAmNotAvove18 = new RelayCommand(
                    () =>
                    {
                        Application.Current.Shutdown();
                    },
                    () =>
                    {
                        return true;
                    });
                }

                return _IAmNotAvove18;
            }
        }


    }
}
