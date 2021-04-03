using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MySql.Data.MySqlClient;
using RumikApp.Enums;
using RumikApp.Services;
using RumikApp.UserControls;
using RumikApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;
using System.Windows.Threading;

namespace RumikApp.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private DispatcherTimer dispatcherTimerInCaseDatabaseDoesNotWork = new DispatcherTimer();

        private MainControlPanelViewModel _MainControlPanelViewModel;
        public MainControlPanelViewModel MainControlPanelViewModel
        {
            get { return _MainControlPanelViewModel; }
            set
            {
                if (_MainControlPanelViewModel == value)
                    return;

                _MainControlPanelViewModel = value;
                RaisePropertyChanged(nameof(MainControlPanelViewModel));
            }
        }

        private PollViewModel _PollViewModel;
        public PollViewModel PollViewModel
        {
            get { return _PollViewModel; }
            set
            {
                if (_PollViewModel == value)
                    return;

                _PollViewModel = value;
                RaisePropertyChanged(nameof(PollViewModel));
            }
        }

        private DataGridViewModel _DataGridViewModel;
        public DataGridViewModel DataGridViewModel
        {
            get { return _DataGridViewModel; }
            set
            {
                if (_DataGridViewModel == value)
                    return;

                _DataGridViewModel = value;
                RaisePropertyChanged(nameof(DataGridViewModel));
            }
        }

        private DataGridViewModel _DataGridViewModel2;
        public DataGridViewModel DataGridViewModel2
        {
            get { return _DataGridViewModel2; }
            set
            {
                if (_DataGridViewModel2 == value)
                    return;

                _DataGridViewModel2 = value;
                RaisePropertyChanged(nameof(DataGridViewModel2));
            }
        }

        private DataGridViewModel _ItemsControl;
        public DataGridViewModel ItemsControl
        {
            get { return _ItemsControl; }
            set
            {
                if (_ItemsControl == value)
                    return;

                _ItemsControl = value;
                RaisePropertyChanged(nameof(ItemsControl));
            }
        }

        private InsertDataToDatabaseFormViewModel _InsertDataToDatabaseForm;
        public InsertDataToDatabaseFormViewModel InsertDataToDatabaseForm
        {
            get { return _InsertDataToDatabaseForm; }
            set
            {
                if (_InsertDataToDatabaseForm == value)
                    return;

                _InsertDataToDatabaseForm = value;
                RaisePropertyChanged(nameof(InsertDataToDatabaseForm));
            }
        }

        private IDatabaseConnectionService _DatabaseConnectionService;
        public IDatabaseConnectionService DatabaseConnectionService
        {
            get { return _DatabaseConnectionService; }
            set
            {
                if (_DatabaseConnectionService == value)
                    return;

                _DatabaseConnectionService = value;
                RaisePropertyChanged(nameof(DatabaseConnectionService));
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

        public MainViewModel(MainControlPanelViewModel mainControlPanelViewModel, PollViewModel pollViewModel,
            DataGridViewModel dataGridViewModel, DataGridViewModel dataGridViewModel2, DataGridViewModel itemsControl,
            InsertDataToDatabaseFormViewModel insertDataToDatabaseFormViewModel, IDatabaseConnectionService databaseConnectionService, IPanelVisibilityService panelVisibilityService)
        {
            PanelVisibilityService = panelVisibilityService;

            MainControlPanelViewModel = mainControlPanelViewModel;
            PollViewModel = pollViewModel;
            DatabaseConnectionService = databaseConnectionService;
            DataGridViewModel = dataGridViewModel;
            DataGridViewModel2 = dataGridViewModel2;
            ItemsControl = itemsControl;
            InsertDataToDatabaseForm = insertDataToDatabaseFormViewModel;

            //if (DatabaseConnectionService.TestConnectionToDatabase() && DatabaseConnectionService.TestConnectionToTable(DatabaseConnectionService.MainDataTable))
            //{
            //    loadFunctionality();
            //}
            //else
            //{

            //    dispatcherTimerInCaseDatabaseDoesNotWork.Tick += new EventHandler(CheckingDatabaseConnection);
            //    dispatcherTimerInCaseDatabaseDoesNotWork.Interval = new TimeSpan(0, 0, 1);
            //    dispatcherTimerInCaseDatabaseDoesNotWork.Start();

            //}
        }

        private void CheckingDatabaseConnection(object sender, EventArgs e)
        {
            if (DatabaseConnectionService.TestConnectionToDatabase() && DatabaseConnectionService.TestConnectionToTable(DatabaseConnectionService.MainDataTable))
            {
                loadFunctionality();
                dispatcherTimerInCaseDatabaseDoesNotWork.Stop();
            }

        }

        void loadFunctionality()
        {
            //PollViewModel = new PollViewModel(this);
            //MainControlPanelViewModel = new MainControlPanelViewModel(this);
            //DataGridViewModel = new DataGridViewModel(this);
            //DataGridViewModel2 = new DataGridViewModel(this);
            //InsertDataToDatabaseForm = new InsertDataToDatabaseFormViewModel(this);

            //ItemsControl = new DataGridViewModel(this);

            //Random rand = new Random();
            //for (int i = 0; i < 15; i++)
            //    ItemsControl.Beverages.Add(new Beverage().GetRandomBevrage(rand));

        }

    }
}