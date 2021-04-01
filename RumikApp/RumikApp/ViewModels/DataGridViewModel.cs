using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using RumikApp.Services;
using RumikApp.ViewModel;
using System.Collections.ObjectModel;
using System.Windows;

namespace RumikApp.ViewModels
{
    public class DataGridViewModel : ViewModelBase
    {
        private IDatabaseConnectionService databaseConnectionService;
       

        private IInformationBusService _informationBusService;
        public IInformationBusService informationBusService
        {
            get{return _informationBusService;}
            set
            {
                if (_informationBusService == value)
                    return;

                _informationBusService = value;
                RaisePropertyChanged(nameof(informationBusService));
            }
        }

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

        private Visibility _ScrollViewerVisibility;
        public Visibility ScrollViewerVisibility
        {
            get { return _ScrollViewerVisibility; }
            set
            {
                if (_ScrollViewerVisibility == value)
                    return;

                _ScrollViewerVisibility = value;
                RaisePropertyChanged(nameof(ScrollViewerVisibility));
            }
        }

        private Visibility _UnknownGuyVisibility = Visibility.Collapsed;
        public Visibility UnknownGuyVisibility
        {
            get { return _UnknownGuyVisibility; }
            set
            {
                if (_UnknownGuyVisibility == value)
                    return;

                _UnknownGuyVisibility = value;
                RaisePropertyChanged(nameof(UnknownGuyVisibility));
            }
        }

        private RelayCommand _GoToMainMenu;
        public RelayCommand GoToMainMenu
        {
            get
            {
                if (_GoToMainMenu == null)
                {
                    _GoToMainMenu = new RelayCommand(
                    () =>
                    {
                        PanelVisibilityService.MainPanelVisibility = Visibility.Visible;
                        PanelVisibilityService.DataGridViewModel2Visibility = Visibility.Collapsed;
                        PanelVisibilityService.DataGridViewModelVisibility = Visibility.Collapsed;

                    },
                    () =>
                    {
                        return true;
                    });
                }

                return _GoToMainMenu;
            }
        }

        private RelayCommand _AddNewToDatabase;
        public RelayCommand AddNewToDatabase
        {
            get
            {
                if (_AddNewToDatabase == null)
                {
                    _AddNewToDatabase = new RelayCommand(
                    () =>
                    {
                        PanelVisibilityService.InsertDataToDatabaseFormVisibility = Visibility.Visible;
                        PanelVisibilityService.DataGridViewModel2Visibility = Visibility.Collapsed;
                        PanelVisibilityService.DataGridViewModelVisibility = Visibility.Collapsed;
                    },
                    () =>
                    {
                        return true;
                    });
                }

                return _AddNewToDatabase;
            }
        }


        public DataGridViewModel(IDatabaseConnectionService databaseConnectionService, IPanelVisibilityService panelVisibilityService, IInformationBusService informationBusService)
        {
            PanelVisibilityService = panelVisibilityService;
            this.databaseConnectionService = databaseConnectionService;
            this.informationBusService = informationBusService;
        }
    }
}
