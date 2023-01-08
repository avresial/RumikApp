using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using RumikApp.Services;
using System.Linq;
using RumikApp.ViewModel;
using System.Windows;
using System.Collections.ObjectModel;
using RumikApp.Infrastructure.Services;

namespace RumikApp.ViewModels
{
    public class DataGridViewModel : ViewModelBase
    {
        private IDatabaseConnectionService databaseConnectionService;
        private readonly IBeverageService beverageService;
        private IInformationBusService _informationBusService;
        public IInformationBusService informationBusService
        {
            get { return _informationBusService; }
            set
            {
                if (_informationBusService == value)
                    return;

                _informationBusService = value;
                RaisePropertyChanged(nameof(informationBusService));
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

        private Visibility _ScrollViewerVisibility;
        public Visibility ScrollViewerVisibility
        {
            get { return _ScrollViewerVisibility; }
            set
            {
                if (_ScrollViewerVisibility == value)
                    return;

                _ScrollViewerVisibility = value;
                RaisePropertyChanged(nameof(ScrollViewerVisibility));
            }
        }

        private Visibility _UnknownGuyVisibility = Visibility.Collapsed;
        public Visibility UnknownGuyVisibility
        {
            get { return _UnknownGuyVisibility; }
            set
            {
                if (_UnknownGuyVisibility == value)
                    return;

                _UnknownGuyVisibility = value;
                RaisePropertyChanged(nameof(UnknownGuyVisibility));
            }
        }

        private string _SortByNameSource = "/IMGs/Icons/None.png";
        public string SortByNameSource
        {
            get { return _SortByNameSource; }
            set
            {
                if (_SortByNameSource == value)
                    return;

                _SortByNameSource = value;
                RaisePropertyChanged(nameof(SortByNameSource));
            }
        }

        private string _SortByPriceSource = "/IMGs/Icons/None.png";
        public string SortByPriceSource
        {
            get { return _SortByPriceSource; }
            set
            {
                if (_SortByPriceSource == value)
                    return;

                _SortByPriceSource = value;
                RaisePropertyChanged(nameof(SortByPriceSource));
            }
        }

        private string _SortByGradeSource = "/IMGs/Icons/None.png";
        public string SortByGradeSource
        {
            get { return _SortByGradeSource; }
            set
            {
                if (_SortByGradeSource == value)
                    return;

                _SortByGradeSource = value;
                RaisePropertyChanged(nameof(SortByGradeSource));
            }
        }

        private string _SortByGradeWithCokeSource = "/IMGs/Icons/None.png";
        public string SortByGradeWithCokeSource
        {
            get { return _SortByGradeWithCokeSource; }
            set
            {
                if (_SortByGradeWithCokeSource == value)
                    return;

                _SortByGradeWithCokeSource = value;
                RaisePropertyChanged(nameof(SortByGradeWithCokeSource));
            }
        }

        private RelayCommand _SortByName;
        public RelayCommand SortByName
        {
            get
            {
                if (_SortByName == null)
                {
                    _SortByName = new RelayCommand(
                    () =>
                    {

                        SortByNameSource = SwitchArrow(SortByNameSource);
                        switch (SortByNameSource)
                        {
                            case "/IMGs/Icons/None.png":
                                informationBusService.ShowDefault();
                                break;

                            case "/IMGs/Icons/ArrowDown.png":
                                informationBusService.SortByNameDescending();
                                break;

                            case "/IMGs/Icons/ArrowUp.png":
                                informationBusService.SortByNameAscending();
                                break;
                        }


                    },
                    () =>
                    {
                        return true;
                    });
                }

                return _SortByName;
            }
        }

        private RelayCommand _SortByPrice;
        public RelayCommand SortByPrice
        {
            get
            {
                if (_SortByPrice == null)
                {
                    _SortByPrice = new RelayCommand(
                    () =>
                    {
                        SortByPriceSource = SwitchArrow(SortByPriceSource);
                        switch (SortByPriceSource)
                        {
                            case "/IMGs/Icons/None.png":
                                informationBusService.ShowDefault();
                                break;

                            case "/IMGs/Icons/ArrowDown.png":
                                informationBusService.SortByPriceDescending();
                                break;

                            case "/IMGs/Icons/ArrowUp.png":
                                informationBusService.SortByPriceAscending();
                                break;
                        }
                    },
                    () =>
                    {
                        return true;
                    });
                }

                return _SortByPrice;
            }
        }

        private RelayCommand _SortByGrade;
        public RelayCommand SortByGrade
        {
            get
            {
                if (_SortByGrade == null)
                {
                    _SortByGrade = new RelayCommand(
                    () =>
                    {
                        SortByGradeSource = SwitchArrow(SortByGradeSource);
                        switch (SortByGradeSource)
                        {
                            case "/IMGs/Icons/None.png":
                                informationBusService.ShowDefault();
                                break;

                            case "/IMGs/Icons/ArrowDown.png":
                                informationBusService.SortByGradeDescending();
                                break;

                            case "/IMGs/Icons/ArrowUp.png":
                                informationBusService.SortByGradeAscending();
                                break;
                        }
                    },
                    () =>
                    {
                        return true;
                    });
                }

                return _SortByGrade;
            }
        }

        private RelayCommand _SortByGradeWithCoke;
        public RelayCommand SortByGradeWithCoke
        {
            get
            {
                if (_SortByGradeWithCoke == null)
                {
                    _SortByGradeWithCoke = new RelayCommand(
                    () =>
                    {
                        SortByGradeWithCokeSource = SwitchArrow(SortByGradeWithCokeSource);
                        switch (SortByGradeWithCokeSource)
                        {
                            case "/IMGs/Icons/None.png":
                                informationBusService.ShowDefault();
                                break;

                            case "/IMGs/Icons/ArrowDown.png":
                                informationBusService.SortByGradeWithCokeDescending();
                                break;

                            case "/IMGs/Icons/ArrowUp.png":
                                informationBusService.SortByGradeWithCokeAscending();
                                break;
                        }
                    },
                    () =>
                    {
                        return true;
                    });
                }

                return _SortByGradeWithCoke;
            }
        }

        void ClearAllSortingIcons()
        {
            SortByNameSource = "/IMGs/Icons/None.png";
            SortByPriceSource = "/IMGs/Icons/None.png";
            SortByGradeSource = "/IMGs/Icons/None.png";
            SortByGradeWithCokeSource = "/IMGs/Icons/None.png";
        }
        string SwitchArrow(string sortingName)
        {
            switch (sortingName)
            {
                case "/IMGs/Icons/None.png":
                    ClearAllSortingIcons();
                    return "/IMGs/Icons/ArrowDown.png";
                    break;
                case "/IMGs/Icons/ArrowDown.png":
                    ClearAllSortingIcons();
                    return "/IMGs/Icons/ArrowUp.png";
                    break;
                case "/IMGs/Icons/ArrowUp.png":
                    ClearAllSortingIcons();
                    return "/IMGs/Icons/None.png";
                    break;
            }
            return "";
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
                        PanelVisibilityService.MainPanelVisibility = Visibility.Visible;
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
                        PanelVisibilityService.InsertDataToDatabaseFormVisibility = Visibility.Visible;
                    },
                    () =>
                    {
                        return true;
                    });
                }

                return _AddNewToDatabase;
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
                     async () =>
                     {

                         PanelVisibilityService.RandomDataGridVisibility = Visibility.Visible;

                         var randomBeverage = await this.beverageService.PickRandomBeverage();

                         Beverage randomOne = await databaseConnectionService.GetRandomRow();

                         if (randomOne == null)
                             informationBusService.OriginalBeverages = new ObservableCollection<Beverage>();
                         else
                             informationBusService.OriginalBeverages = new ObservableCollection<Beverage>() { randomOne };
                     },
                    () =>
                    {
                        return true;
                    });
                }

                return _ImFeelingLucky;
            }
        }


        public DataGridViewModel(IDatabaseConnectionService databaseConnectionService, IPanelVisibilityService panelVisibilityService, IInformationBusService informationBusService, IBeverageService beverageService)
        {
            this.PanelVisibilityService = panelVisibilityService;
            this.informationBusService = informationBusService;
            this.beverageService = beverageService;
            this.databaseConnectionService = databaseConnectionService;
        }
    }
}
