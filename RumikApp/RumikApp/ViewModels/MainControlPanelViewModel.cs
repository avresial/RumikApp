﻿using GalaSoft.MvvmLight;
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
        private FileDatabaseConnectionService fileDatabaseConnectionService;

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


        public MainControlPanelViewModel(IDatabaseConnectionService databaseConnectionService, IPanelVisibilityService panelVisibilityService, IInformationBusService informationBusService, FileDatabaseConnectionService fileDatabaseConnectionService)
        {
            this.informationBusService = informationBusService;
            this.databaseConnectionService = databaseConnectionService;
            this.fileDatabaseConnectionService = fileDatabaseConnectionService;
            PanelVisibilityService = panelVisibilityService;
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
                        PanelVisibilityService.DataGridViewModel2Visibility = Visibility.Visible;

                        Beverage randomOne = databaseConnectionService.GetRandomRow();

                        if (randomOne == null)
                            informationBusService.Beverages.Clear();
                        else
                            informationBusService.Beverages = new ObservableCollection<Beverage>() { randomOne };
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
                    () =>
                    {
                        PanelVisibilityService.DataGridViewModelVisibility = Visibility.Visible;
                        informationBusService.Beverages = databaseConnectionService.GetAllData();
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
                    () =>
                    {
                        informationBusService.Beverages = fileDatabaseConnectionService.GetAllData();
                        PanelVisibilityService.EditLocalDataVisibility = Visibility.Visible;
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
