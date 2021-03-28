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

namespace RumikApp.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        private MainControlPanelViewModel _MainControlPanelViewModel;
        public MainControlPanelViewModel MainControlPanelViewModel
        {
            get { return _MainControlPanelViewModel; }
            set
            {
                if (_MainControlPanelViewModel == value)
                    return;

                _MainControlPanelViewModel = value;
                RaisePropertyChanged("MainControlPanelViewModel");
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
                RaisePropertyChanged("PollViewModel");
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
                RaisePropertyChanged("DataGridViewModel");
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
                RaisePropertyChanged("DataGridViewModel2");
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
                RaisePropertyChanged("ItemsControl");
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
                RaisePropertyChanged("InsertDataToDatabaseForm");
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
                RaisePropertyChanged("DatabaseConnectionService");
            }
        }

        public MainViewModel()
        {
            DatabaseConnectionService = new DatabaseConnectionService();
            PollViewModel = new PollViewModel(this);
            MainControlPanelViewModel = new MainControlPanelViewModel(this);
            DataGridViewModel = new DataGridViewModel(this);
            DataGridViewModel2 = new DataGridViewModel(this);
            InsertDataToDatabaseForm = new InsertDataToDatabaseFormViewModel(this);

            ItemsControl = new DataGridViewModel(this);
            
            DatabaseConnectionService.TestConnectionToDatabase();
            DatabaseConnectionService.TestConnectionToTable(AvailableTables.RumsBaseTEST);
            Random rand = new Random();
            for (int i = 0; i < 15; i++)
                ItemsControl.Beverages.Add(new Beverage().GetRandomBevrage(rand));
        }
    }
}