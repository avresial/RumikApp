using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
//using MySql.Data.MySqlClient;
using RumikApp.Infrastructure.Enums;
using RumikApp.Infrastructure.Services;
using RumikApp.UserControls;
using RumikApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace RumikApp.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private DispatcherTimer dispatcherTimerInCaseDatabaseDoesNotWork = new DispatcherTimer();

        private WelcomePanelViewModel _WelcomePanelViewModel;
        public WelcomePanelViewModel WelcomePanelViewModel
        {
            get { return _WelcomePanelViewModel; }
            set
            {
                if (_WelcomePanelViewModel == value)
                    return;

                _WelcomePanelViewModel = value;
                RaisePropertyChanged(nameof(WelcomePanelViewModel));
            }
        }

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

        //private DataGridViewModel _DataGridViewModel;
        //public DataGridViewModel DataGridViewModel
        //{
        //    get { return _DataGridViewModel; }
        //    set
        //    {
        //        if (_DataGridViewModel == value)
        //            return;

        //        _DataGridViewModel = value;
        //        RaisePropertyChanged(nameof(DataGridViewModel));
        //    }
        //}

        //private DataGridViewModel _RandomDataGrid;
        //public DataGridViewModel RandomDataGrid
        //{
        //    get { return _RandomDataGrid; }
        //    set
        //    {
        //        if (_RandomDataGrid == value)
        //            return;

        //        _RandomDataGrid = value;
        //        RaisePropertyChanged(nameof(RandomDataGrid));
        //    }
        //}

        //private DataGridViewModel _ItemsControl;
        //public DataGridViewModel ItemsControl
        //{
        //    get { return _ItemsControl; }
        //    set
        //    {
        //        if (_ItemsControl == value)
        //            return;

        //        _ItemsControl = value;
        //        RaisePropertyChanged(nameof(ItemsControl));
        //    }
        //}

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

        private EditLocalDataViewModel _EditLocalDataViewModel;
        public EditLocalDataViewModel EditLocalDataViewModel
        {
            get { return _EditLocalDataViewModel; }
            set
            {
                if (_EditLocalDataViewModel == value)
                    return;

                _EditLocalDataViewModel = value;
                RaisePropertyChanged(nameof(EditLocalDataViewModel));
            }
        }


        //private IDatabaseConnectionService _DatabaseConnectionService;
        //public IDatabaseConnectionService DatabaseConnectionService
        //{
        //    get { return _DatabaseConnectionService; }
        //    set
        //    {
        //        if (_DatabaseConnectionService == value)
        //            return;

        //        _DatabaseConnectionService = value;
        //        RaisePropertyChanged(nameof(DatabaseConnectionService));
        //    }
        //}

        //private IPanelVisibilityService _PanelVisibilityService;
        //public IPanelVisibilityService PanelVisibilityService
        //{
        //    get { return _PanelVisibilityService; }
        //    set
        //    {
        //        if (_PanelVisibilityService == value)
        //            return;

        //        _PanelVisibilityService = value;
        //        RaisePropertyChanged(nameof(PanelVisibilityService));
        //    }
        //}

        public MainViewModel(WelcomePanelViewModel welcomePanelViewModel, MainControlPanelViewModel mainControlPanelViewModel, PollViewModel pollViewModel,
            //DataGridViewModel dataGridViewModel, DataGridViewModel dataGridViewModel2, DataGridViewModel itemsControl,
            InsertDataToDatabaseFormViewModel insertDataToDatabaseFormViewModel, EditLocalDataViewModel editLocalDataViewModel
            //,IDatabaseConnectionService databaseConnectionService, IPanelVisibilityService panelVisibilityService
            )
        {
            WelcomePanelViewModel = welcomePanelViewModel;
            InsertDataToDatabaseForm = insertDataToDatabaseFormViewModel;
            MainControlPanelViewModel = mainControlPanelViewModel;
            //DatabaseConnectionService = databaseConnectionService;
            //EditLocalDataViewModel = editLocalDataViewModel;
            //PanelVisibilityService = panelVisibilityService;
            //RandomDataGrid = dataGridViewModel2;
            //DataGridViewModel = dataGridViewModel;
            //PollViewModel = pollViewModel;
            //ItemsControl = itemsControl;

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

        private async Task CheckingDatabaseConnection(object sender, EventArgs e)
        {
            //if (await  DatabaseConnectionService.TestConnectionToDatabase() && DatabaseConnectionService.TestConnectionToTable(DatabaseConnectionService.MainDataTable))
            //{
            //    loadFunctionality();
            //    dispatcherTimerInCaseDatabaseDoesNotWork.Stop();
            //}

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