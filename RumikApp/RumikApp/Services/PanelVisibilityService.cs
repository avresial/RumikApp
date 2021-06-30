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

                if (value == Visibility.Visible)
                    collapseAllViews();

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

                if (value == Visibility.Visible)
                    collapseAllViews();

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

                if (value == Visibility.Visible)
                    collapseAllViews();

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

                if (value == Visibility.Visible)
                    collapseAllViews();

                _DataGridViewModelVisibility = value;
                RaisePropertyChanged(nameof(DataGridViewModelVisibility));
            }
        }

        private Visibility _DataGridViewModel2Visibility = Visibility.Collapsed;
        public Visibility RandomDataGridVisibility
        {
            get { return _DataGridViewModel2Visibility; }
            set
            {
                if (_DataGridViewModel2Visibility == value)
                    return;

                if (value == Visibility.Visible)
                    collapseAllViews();

                _DataGridViewModel2Visibility = value;
                RaisePropertyChanged(nameof(RandomDataGridVisibility));
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

                if (value == Visibility.Visible)
                    collapseAllViews();

                _ItemsControlVisibility = value;
                RaisePropertyChanged(nameof(ItemsControlVisibility));
            }
        }

        private Visibility _EditLocalDataVisibility = Visibility.Collapsed;
        public Visibility EditLocalDataVisibility
        {
            get { return _EditLocalDataVisibility; }
            set
            {
                if (_EditLocalDataVisibility == value)
                    return;

                if (value == Visibility.Visible)
                    collapseAllViews();

                _EditLocalDataVisibility = value;
                RaisePropertyChanged(nameof(EditLocalDataVisibility));
            }
        }

        private void collapseAllViews()
        {
            MainPanelVisibility = Visibility.Collapsed;
            PollVisibility = Visibility.Collapsed;
            InsertDataToDatabaseFormVisibility = Visibility.Collapsed;
            DataGridViewModelVisibility = Visibility.Collapsed;
            RandomDataGridVisibility = Visibility.Collapsed;
            ItemsControlVisibility = Visibility.Collapsed;
            EditLocalDataVisibility = Visibility.Collapsed;
        }
    }
}
