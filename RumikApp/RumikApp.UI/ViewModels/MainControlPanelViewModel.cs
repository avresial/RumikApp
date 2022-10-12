using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using RumikApp.Core.Services;
using RumikApp.Infrastructure.Services;
using RumikApp.UI.ViewModel;
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
        //private FileDatabaseConnectionService fileDatabaseConnectionService;

        //private IDatabaseConnectionService databaseConnectionService;

        //private IInformationBusService informationBusService;

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

        public MainControlPanelViewModel(IPanelVisibilityService panelVisibilityService)//, IDatabaseConnectionService databaseConnectionService,  IInformationBusService informationBusService, FileDatabaseConnectionService fileDatabaseConnectionService)
        {
            //this.informationBusService = informationBusService;
            //this.databaseConnectionService = databaseConnectionService;
            //this.fileDatabaseConnectionService = fileDatabaseConnectionService;
            this.PanelVisibilityService = panelVisibilityService;
        }



        private RelayCommand _ImFeelingLucky;
        public RelayCommand ImFeelingLucky
        {
            get
            {
                if (_ImFeelingLucky == null)
                {
                    _ImFeelingLucky = new RelayCommand(
                     async () =>
                    {

                        PanelVisibilityService.RandomDataGridVisibility = Visibility.Visible;

                        //Beverage randomOne = await databaseConnectionService.GetRandomRow();

                        //if (randomOne == null)
                        //    informationBusService.OriginalBeverages = new ObservableCollection<Beverage>();
                        //else
                        //    informationBusService.OriginalBeverages = new ObservableCollection<Beverage>() { randomOne };
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
                    async () =>
                    {
                        PanelVisibilityService.DataGridViewModelVisibility = Visibility.Visible;

                        //ObservableCollection<Beverage> allBeverages = await databaseConnectionService.GetAllData();

                        //informationBusService.OriginalBeverages = allBeverages;

                        ;
                    },
                    () =>
                    {
                        return true;
                    });
                }

                return _GoStraightToDatabase;
            }
        }



        private RelayCommand _EditLocalData;
        public RelayCommand EditLocalData
        {
            get
            {
                if (_EditLocalData == null)
                {
                    _EditLocalData = new RelayCommand(
                    async () =>
                    {
                        PanelVisibilityService.EditLocalDataVisibility = Visibility.Visible;
                        //informationBusService.OriginalBeverages.Clear();
                        //informationBusService.OriginalBeverages = await fileDatabaseConnectionService.GetAllData();
                    },
                    () =>
                    {
                        return true;
                    });
                }

                return _EditLocalData;
            }
        }


    }
}
