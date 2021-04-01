using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
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
        private MainViewModel mainViewModel;
        public MainControlPanelViewModel(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
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
                        Visibility = Visibility.Collapsed;
                        mainViewModel.DataGridViewModel2.Beverages = new ObservableCollection<Beverage>() { mainViewModel.DatabaseConnectionService.GetRandomRow() };
                        mainViewModel.DataGridViewModel2.Visibility = Visibility.Visible;

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
                        Visibility = Visibility.Collapsed;
                        mainViewModel.PollViewModel.Visibility = Visibility.Visible;
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
                        Visibility = Visibility.Collapsed;
                        mainViewModel.DataGridViewModel.Reload();
                        mainViewModel.DataGridViewModel.Visibility = Visibility.Visible;
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
