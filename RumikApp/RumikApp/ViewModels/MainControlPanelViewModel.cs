using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using RumikApp.Services;
using RumikApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RumikApp.ViewModels
{
    public class MainControlPanelViewModel : ViewModelBase
    {

        private IDatabaseConnectionService databaseConnectionService;

        private IInformationBusService informationBusService;

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


        public MainControlPanelViewModel(IDatabaseConnectionService databaseConnectionService, IPanelVisibilityService panelVisibilityService, IInformationBusService informationBusService)
        {
            this.informationBusService = informationBusService;
            this.databaseConnectionService = databaseConnectionService;
            PanelVisibilityService = panelVisibilityService;
        }

        private Visibility _Visibility = Visibility.Visible;
        public Visibility Visibility
        {
            get { return _Visibility; }
            set
            {
                if (_Visibility == value)
                    return;

                _Visibility = value;
                RaisePropertyChanged(nameof(Visibility));

            }
        }

        private RelayCommand _ImFeelingLucky;
        public RelayCommand ImFeelingLucky
        {
            get
            {
                if (_ImFeelingLucky == null)
                {
                    _ImFeelingLucky = new RelayCommand(
                    () =>
                    {
                        PanelVisibilityService.MainPanelVisibility = Visibility.Collapsed;
                        PanelVisibilityService.DataGridViewModel2Visibility = Visibility.Visible;

                        informationBusService.Beverages = new ObservableCollection<Beverage>() { databaseConnectionService.GetRandomRow() };
                    },
                    () =>
                    {
                        return true;
                    });
                }

                return _ImFeelingLucky;
            }
        }

        private RelayCommand _LetMeChoose;
        public RelayCommand LetMeChoose
        {
            get
            {
                if (_LetMeChoose == null)
                {
                    _LetMeChoose = new RelayCommand(
                    () =>
                    {
                        PanelVisibilityService.MainPanelVisibility = Visibility.Collapsed;
                        PanelVisibilityService.PollVisibility = Visibility.Visible;
                    },
                    () =>
                    {
                        return true;
                    });
                }

                return _LetMeChoose;
            }
        }

        private RelayCommand _GoStraightToDatabase;
        public RelayCommand GoStraightToDatabase
        {
            get
            {
                if (_GoStraightToDatabase == null)
                {
                    _GoStraightToDatabase = new RelayCommand(
                    () =>
                    {
                        PanelVisibilityService.MainPanelVisibility = Visibility.Collapsed;
                        PanelVisibilityService.DataGridViewModelVisibility = Visibility.Visible;
                    },
                    () =>
                    {
                        return true;
                    });
                }

                return _GoStraightToDatabase;
            }
        }

    }
}
