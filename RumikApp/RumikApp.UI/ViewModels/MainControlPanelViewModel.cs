using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using RumikApp.Core.Models;
using RumikApp.Core.Services;
using RumikApp.Infrastructure.Dto;
using RumikApp.Infrastructure.Extensions;
using RumikApp.Infrastructure.Repositories;
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
        private Random random = new Random();
        private IBeverageRepository beverageRepository;
        private BeverageContainer beverageContainer;

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

        public MainControlPanelViewModel(IPanelVisibilityService panelVisibilityService, IBeverageRepository beverageRepository, BeverageContainer beverages)
        {
            //this.informationBusService = informationBusService;
            //this.databaseConnectionService = databaseConnectionService;
            //this.fileDatabaseConnectionService = fileDatabaseConnectionService;
            this.PanelVisibilityService = panelVisibilityService;
            this.beverageRepository = beverageRepository;
            this.beverageContainer = beverages;
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

                        beverageContainer.Clear();

                        var newBeverages = await beverageRepository.BrowseAll();
                        beverageContainer.Add(newBeverages.ElementAt(random.Next(0, newBeverages.Count())).BeverageDtoToBeverage());
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

                        beverageContainer.Clear();
                        foreach (BeverageDto beverageDto in await beverageRepository.BrowseAll())
                            beverageContainer.Add(beverageDto.BeverageDtoToBeverage());

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
