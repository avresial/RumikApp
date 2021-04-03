using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RumikApp.Services
{
    public class PanelVisibilityService : ViewModelBase, IPanelVisibilityService
    {
        private Visibility _MainPanelVisibility = Visibility.Visible;
        public Visibility MainPanelVisibility
        {
            get { return _MainPanelVisibility; }
            set
            {
                if (_MainPanelVisibility == value)
                    return;

                _MainPanelVisibility = value;
                RaisePropertyChanged(nameof(MainPanelVisibility));
            }
        }

        private Visibility _PollVisibility = Visibility.Collapsed;
        public Visibility PollVisibility
        {
            get { return _PollVisibility; }
            set
            {
                if (_PollVisibility == value)
                    return;

                _PollVisibility = value;
                RaisePropertyChanged(nameof(PollVisibility));
            }
        }

        private Visibility _InsertDataToDatabaseFormVisibility = Visibility.Collapsed;
        public Visibility InsertDataToDatabaseFormVisibility
        {
            get { return _InsertDataToDatabaseFormVisibility; }
            set
            {
                if (_InsertDataToDatabaseFormVisibility == value)
                    return;

                _InsertDataToDatabaseFormVisibility = value;
                RaisePropertyChanged(nameof(InsertDataToDatabaseFormVisibility));
            }
        }

        private Visibility _DataGridViewModelVisibility = Visibility.Collapsed;
        public Visibility DataGridViewModelVisibility
        {
            get { return _DataGridViewModelVisibility; }
            set
            {
                if (_DataGridViewModelVisibility == value)
                    return;

                _DataGridViewModelVisibility = value;
                RaisePropertyChanged(nameof(DataGridViewModelVisibility));
            }
        }

        private Visibility _DataGridViewModel2Visibility = Visibility.Collapsed;
        public Visibility DataGridViewModel2Visibility
        {
            get { return _DataGridViewModel2Visibility; }
            set
            {
                if (_DataGridViewModel2Visibility == value)
                    return;

                _DataGridViewModel2Visibility = value;
                RaisePropertyChanged(nameof(DataGridViewModel2Visibility));
            }
        }

        private Visibility _ItemsControlVisibility = Visibility.Collapsed;
        public Visibility ItemsControlVisibility
        {
            get { return _ItemsControlVisibility; }
            set
            {
                if (_ItemsControlVisibility == value)
                    return;

                _ItemsControlVisibility = value;
                RaisePropertyChanged(nameof(ItemsControlVisibility));
            }
        }
    }
}
