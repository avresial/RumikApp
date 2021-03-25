﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using RumikApp.ViewModel;
using System.Collections.ObjectModel;
using System.Windows;

namespace RumikApp.ViewModels
{
    public class DataGridViewModel : ViewModelBase
    {
        private MainViewModel mainViewModel;

        private Visibility _Visibility = Visibility.Collapsed;
        public Visibility Visibility
        {
            get { return _Visibility; }
            set
            {
                if (_Visibility == value)
                    return;

                _Visibility = value;
                RaisePropertyChanged("Visibility");

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
                RaisePropertyChanged("ScrollViewerVisibility");
            }
        }

        private Visibility _UnknownGuyVisibility;
        public Visibility UnknownGuyVisibility
        {
            get { return _UnknownGuyVisibility; }
            set
            {
                if (_UnknownGuyVisibility == value)
                    return;

                _UnknownGuyVisibility = value;
                RaisePropertyChanged("UnknownGuyVisibility");
            }
        }

        private ObservableCollection<Beverage> _Beverages = new ObservableCollection<Beverage>();
        public ObservableCollection<Beverage> Beverages
        {
            get { return _Beverages; }
            set
            {
                if (_Beverages == value)
                    return;

                if (value.Count > 0)
                {
                    ScrollViewerVisibility = Visibility.Visible;
                    UnknownGuyVisibility = Visibility.Collapsed;
                }
                else
                {
                    ScrollViewerVisibility = Visibility.Collapsed;
                    UnknownGuyVisibility = Visibility.Visible;
                }



                _Beverages = value;
                RaisePropertyChanged("Beverages");
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
                        mainViewModel.MainControlPanelViewModel.Visibility = Visibility.Visible;
                        Visibility = Visibility.Collapsed;
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
                        mainViewModel.InsertDataToDatabaseForm.Visibility = Visibility.Visible;
                        Visibility = Visibility.Collapsed;
                    },
                    () =>
                    {
                        return true;
                    });
                }

                return _AddNewToDatabase;
            }
        }


        public DataGridViewModel(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;

        }

        public void Reload()
        {
            Beverages = mainViewModel.DatabaseConnectionService.GetAllData();
        }

    }
}
