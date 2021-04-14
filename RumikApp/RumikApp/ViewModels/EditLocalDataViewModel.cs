using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using RumikApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RumikApp.ViewModels
{
    public class EditLocalDataViewModel : ViewModelBase
    {
        private FileDatabaseConnectionService fileDatabaseConnectionService;
   
        private IInformationBusService _IInformationBusService;
        public IInformationBusService IInformationBusService
        {
            get { return _IInformationBusService; }
            set
            {
                if (_IInformationBusService == value)
                    return;

                _IInformationBusService = value;
                RaisePropertyChanged(nameof(IInformationBusService));
            }
        }

        private List<String> _ColorsList = new List<string>();
        public List<String> ColorsList
        {
            get { return _ColorsList; }
            set
            {
                if (_ColorsList == value)
                    return;

                _ColorsList = value;
                RaisePropertyChanged(nameof(ColorsList));
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

        private Beverage _SelectedBeverage;
        public Beverage SelectedBeverage
        {
            get { return _SelectedBeverage; }
            set
            {
                if (_SelectedBeverage == value)
                    return;

                _SelectedBeverage = value;
                RaisePropertyChanged(nameof(SelectedBeverage));
            }
        }


        public EditLocalDataViewModel(IPanelVisibilityService panelVisibilityService, IInformationBusService informationBusService, FileDatabaseConnectionService fileDatabaseConnectionService)
        {
            this.fileDatabaseConnectionService = fileDatabaseConnectionService;
            PanelVisibilityService = panelVisibilityService;
            IInformationBusService = informationBusService;


            ColorsList.Add("Złoty");
            ColorsList.Add("Miedziany");
            ColorsList.Add("Biały");
            ColorsList.Add("Bursztynowy");
            ColorsList.Add("Czarny");

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
                    },
                    () =>
                    {
                        return true;
                    });
                }

                return _GoToMainMenu;
            }
        }

    }
}
