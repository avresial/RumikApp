using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using RumikApp.Enums;
using RumikApp.Services;
using RumikApp.ViewModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Windows;

namespace RumikApp.UserControls
{
    public class PollViewModel : ViewModelBase
    {
        //private MainViewModel mainViewModel;

        const string imagesLocalization = "/IMGs/PollIMG/";

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

        private ObservableCollection<Beverage> _Beverages = new ObservableCollection<Beverage>();
        public ObservableCollection<Beverage> Beverages
        {
            get { return _Beverages; }
            set
            {
                if (_Beverages == value)
                    return;

                _Beverages = value;
                RaisePropertyChanged(nameof(Beverages));
            }
        }

        private PollPurpose _PollPurpose;
        public PollPurpose PollPurpose
        {
            get { return _PollPurpose; }
            set
            {
                if (_PollPurpose == value)
                    return;

                _PollPurpose = value;
                RaisePropertyChanged(nameof(PollPurpose));
            }
        }

        private PollMixes _PollMixes;
        public PollMixes PollMixes
        {
            get { return _PollMixes; }
            set
            {
                if (_PollMixes == value)
                    return;

                _PollMixes = value;
                RaisePropertyChanged(nameof(PollMixes));
            }
        }

        private PollPricePoints _PollPricePoints;
        public PollPricePoints PollPricePoints
        {
            get { return _PollPricePoints; }
            set
            {
                if (_PollPricePoints == value)
                    return;

                _PollPricePoints = value;
                RaisePropertyChanged(nameof(PollPricePoints));
            }
        }

        // first section - potrzebuję w celu

        private bool _ForPartyBool;
        public bool ForPartyBool
        {
            get { return _ForPartyBool; }
            set
            {
                if (_ForPartyBool == value)
                    return;

                if (value)
                {
                    GoodButCheap = false;
                    Exclusive = false;
                    ForPiratesFromCarabien = false;
                    PollPurpose = PollPurpose.ForPartyBool;
                }
                else
                {
                    if (PollPurpose == PollPurpose.ForPartyBool)
                        PollPurpose = PollPurpose.None;
                }

                _ForPartyBool = value;
                RaisePropertyChanged(nameof(ForPartyBool));
            }
        }

        private bool _GoodButCheap;
        public bool GoodButCheap
        {
            get { return _GoodButCheap; }
            set
            {
                if (_GoodButCheap == value)
                    return;

                if (value)
                {
                    ForPartyBool = false;
                    Exclusive = false;
                    ForPiratesFromCarabien = false;
                    PollPurpose = PollPurpose.GoodButCheap;
                }
                else
                {
                    if (PollPurpose == PollPurpose.GoodButCheap)
                        PollPurpose = PollPurpose.None;
                }

                _GoodButCheap = value;
                RaisePropertyChanged(nameof(GoodButCheap));
            }
        }

        private bool _Exclusive;
        public bool Exclusive
        {
            get { return _Exclusive; }
            set
            {
                if (_Exclusive == value)
                    return;

                if (value)
                {
                    ForPartyBool = false;
                    GoodButCheap = false;
                    ForPiratesFromCarabien = false;
                    PollPurpose = PollPurpose.Exclusive;
                }
                else
                {
                    if (PollPurpose == PollPurpose.Exclusive)
                        PollPurpose = PollPurpose.None;
                }

                _Exclusive = value;
                RaisePropertyChanged(nameof(Exclusive));
            }
        }

        private bool _ForPiratesFromCarabien;
        public bool ForPiratesFromCarabien
        {
            get { return _ForPiratesFromCarabien; }
            set
            {
                if (_ForPiratesFromCarabien == value)
                    return;

                if (value)
                {
                    ForPartyBool = false;
                    GoodButCheap = false;
                    Exclusive = false;
                    PollPurpose = PollPurpose.ForPiratesFromCarabien;
                }
                else
                {
                    if (PollPurpose == PollPurpose.ForPiratesFromCarabien)
                        PollPurpose = PollPurpose.None;
                }

                _ForPiratesFromCarabien = value;
                RaisePropertyChanged(nameof(ForPiratesFromCarabien));

                if (value)
                    GoForPiratesFromCarabien();
            }
        }

        // second section - do picia
        private bool _solo;
        public bool solo
        {
            get { return _solo; }
            set
            {
                if (_solo == value)
                    return;

                if (value)
                {
                    PollMixes = PollMixes.Solo;
                    WithCoke = false;
                }
                else
                {
                    if (PollMixes == PollMixes.Solo)
                        PollMixes = PollMixes.None;
                }

                _solo = value;
                RaisePropertyChanged(nameof(solo));
            }
        }

        private bool _WithCoke;
        public bool WithCoke
        {
            get { return _WithCoke; }
            set
            {
                if (_WithCoke == value)
                    return;
                if (value)
                {
                    PollMixes = PollMixes.WithCoke;
                    solo = false;
                }
                else
                {
                    if (PollMixes == PollMixes.WithCoke)
                        PollMixes = PollMixes.None;
                }

                _WithCoke = value;
                RaisePropertyChanged(nameof(WithCoke));
            }
        }

        // third section - chce poczuc smak

        private Flavour _Vanila = new Flavour($"{imagesLocalization + nameof(Vanila)}.png", nameof(Vanila));
        public Flavour Vanila
        {
            get { return _Vanila; }
            set
            {
                if (_Vanila == value)
                    return;

                _Vanila = value;
                RaisePropertyChanged(nameof(Vanila));
            }
        }

        private Flavour _Nuts = new Flavour($"{imagesLocalization + nameof(Nuts)}.png", nameof(Nuts));
        public Flavour Nuts
        {
            get { return _Nuts; }
            set
            {
                if (_Nuts == value)
                    return;

                _Nuts = value;
                RaisePropertyChanged(nameof(Nuts));
            }
        }

        private Flavour _Caramel = new Flavour($"{imagesLocalization + nameof(Caramel)}.png", nameof(Caramel));
        public Flavour Caramel
        {
            get { return _Caramel; }
            set
            {
                if (_Caramel == value)
                    return;

                _Caramel = value;
                RaisePropertyChanged(nameof(Caramel));
            }
        }

        private Flavour _Smoke = new Flavour($"{imagesLocalization + nameof(Smoke)}.png", nameof(Smoke));
        public Flavour Smoke
        {
            get { return _Smoke; }
            set
            {
                if (_Smoke == value)
                    return;

                _Smoke = value;
                RaisePropertyChanged(nameof(Smoke));
            }
        }

        private Flavour _Cinnamon = new Flavour($"{imagesLocalization + nameof(Cinnamon)}.png", nameof(Cinnamon));
        public Flavour Cinnamon
        {
            get { return _Cinnamon; }
            set
            {
                if (_Cinnamon == value)
                    return;

                _Cinnamon = value;
                RaisePropertyChanged(nameof(Cinnamon));
            }
        }

        private Flavour _Spirit = new Flavour($"{imagesLocalization + nameof(Spirit)}.png", nameof(Spirit));
        public Flavour Spirit
        {
            get { return _Spirit; }
            set
            {
                if (_Spirit == value)
                    return;

                _Spirit = value;
                RaisePropertyChanged(nameof(Spirit));
            }
        }

        private Flavour _Fruits = new Flavour($"{imagesLocalization + nameof(Fruits)}.png", nameof(Fruits));
        public Flavour Fruits
        {
            get { return _Fruits; }
            set
            {
                if (_Fruits == value)
                    return;

                _Fruits = value;
                RaisePropertyChanged(nameof(Fruits));
            }
        }

        private Flavour _Honey = new Flavour($"{imagesLocalization + nameof(Honey)}.png", nameof(Honey));
        public Flavour Honey
        {
            get { return _Honey; }
            set
            {
                if (_Honey == value)
                    return;

                _Honey = value;

                RaisePropertyChanged(nameof(Honey));
            }
        }

        // fourth section - price
        private bool _PricePoint1;
        public bool PricePoint1
        {
            get { return _PricePoint1; }
            set
            {
                if (_PricePoint1 == value)
                    return;
                if (value)
                {
                    PricePoint2 = false;
                    PricePoint3 = false;
                    PricePoint4 = false;
                    PollPricePoints = PollPricePoints.PricePoint1;
                }
                else
                {
                    if (PollPricePoints == PollPricePoints.PricePoint1)
                        PollPricePoints = PollPricePoints.None;
                }

                _PricePoint1 = value;
                RaisePropertyChanged(nameof(PricePoint1));
            }
        }

        private bool _PricePoint2;
        public bool PricePoint2
        {
            get { return _PricePoint2; }
            set
            {
                if (_PricePoint2 == value)
                    return;

                if (value)
                {
                    PricePoint1 = false;
                    PricePoint3 = false;
                    PricePoint4 = false;
                    PollPricePoints = PollPricePoints.PricePoint2;
                }
                else
                {
                    if (PollPricePoints == PollPricePoints.PricePoint2)
                        PollPricePoints = PollPricePoints.None;
                }

                _PricePoint2 = value;
                RaisePropertyChanged(nameof(PricePoint2));
            }
        }

        private bool __PricePoint3;
        public bool PricePoint3
        {
            get { return __PricePoint3; }
            set
            {
                if (__PricePoint3 == value)
                    return;

                if (value)
                {
                    PricePoint1 = false;
                    PricePoint2 = false;
                    PricePoint4 = false;
                    PollPricePoints = PollPricePoints.PricePoint3;
                }
                else
                {
                    if (PollPricePoints == PollPricePoints.PricePoint3)
                        PollPricePoints = PollPricePoints.None;
                }

                __PricePoint3 = value;
                RaisePropertyChanged(nameof(PricePoint3));
            }
        }

        private bool __PricePoint4;
        public bool PricePoint4
        {
            get { return __PricePoint4; }
            set
            {
                if (__PricePoint4 == value)
                    return;

                if (value)
                {
                    PricePoint1 = false;
                    PricePoint2 = false;
                    PricePoint3 = false;
                    PollPricePoints = PollPricePoints.PricePoint4;
                }
                else
                {
                    if (PollPricePoints == PollPricePoints.PricePoint4)
                        PollPricePoints = PollPricePoints.None;
                }

                __PricePoint4 = value;
                RaisePropertyChanged(nameof(PricePoint4));
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
                        PanelVisibilityService.MainPanelVisibility = Visibility.Visible;
                        PanelVisibilityService.PollVisibility = Visibility.Collapsed;
                    },
                    () =>
                    {
                        return true;
                    });
                }

                return _GoToMainMenu;
            }
        }

        private RelayCommand _GetMeThatRum;
        public RelayCommand GetMeThatRum
        {
            get
            {
                if (_GetMeThatRum == null)
                {
                    _GetMeThatRum = new RelayCommand(
                    () =>
                    {
                        PanelVisibilityService.PollVisibility = Visibility.Collapsed;
                        PanelVisibilityService.DataGridViewModel2Visibility = Visibility.Visible;

                        informationBusService.Beverages = databaseConnectionService.GetDataFromDatabaseWithConditions(PollPurpose, 5, PollMixes, getListWithSetFlavours(), PollPricePoints);

                        ClearSellection();
                    },
                    () =>
                    {
                        return true;
                    });
                }

                return _GetMeThatRum;
            }
        }

        public PollViewModel(IInformationBusService informationBusService, IDatabaseConnectionService databaseConnectionService, IPanelVisibilityService panelVisibilityService)
        {
            PanelVisibilityService = panelVisibilityService;
            this.databaseConnectionService = databaseConnectionService;
            this.informationBusService = informationBusService;
        }

        public void ClearSellection()
        {
            ForPartyBool = false;
            GoodButCheap = false;
            Exclusive = false;
            ForPiratesFromCarabien = false;

            solo = false;
            WithCoke = false;

            Vanila.IsSet = false;
            Nuts.IsSet = false;
            Caramel.IsSet = false;
            Smoke.IsSet = false;
            Cinnamon.IsSet = false;
            Spirit.IsSet = false;
            Fruits.IsSet = false;
            Honey.IsSet = false;

            PricePoint1 = false;
            PricePoint2 = false;
            PricePoint3 = false;
            PricePoint4 = false;
        }

        List<Flavour> getListWithSetFlavours()
        {
            List<Flavour> Flavours = new List<Flavour>();

            if (Vanila.IsSet)
                Flavours.Add(Vanila);
            if (Nuts.IsSet)
                Flavours.Add(Nuts);
            if (Caramel.IsSet)
                Flavours.Add(Caramel);
            if (Smoke.IsSet)
                Flavours.Add(Smoke);
            if (Cinnamon.IsSet)
                Flavours.Add(Cinnamon);
            if (Spirit.IsSet)
                Flavours.Add(Spirit);
            if (Fruits.IsSet)
                Flavours.Add(Fruits);
            if (Honey.IsSet)
                Flavours.Add(Honey);
            return Flavours;
        }

        void GoForPiratesFromCarabien()
        {

            informationBusService.Beverages = databaseConnectionService.GetDataFromDatabaseWithConditions(PollPurpose.ForPiratesFromCarabien, 5, PollMixes.None, new List<Flavour>(), PollPricePoints.None);

            PanelVisibilityService.PollVisibility = Visibility.Collapsed;
            PanelVisibilityService.DataGridViewModel2Visibility = Visibility.Visible;

            ForPiratesFromCarabien = false;
            ClearSellection();
        }

    }
}
