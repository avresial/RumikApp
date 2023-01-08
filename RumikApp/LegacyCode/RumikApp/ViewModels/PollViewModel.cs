using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using RumikApp.Enums;
using RumikApp.Models;
using RumikApp.Services;
using RumikApp.ViewModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows;

namespace RumikApp.UserControls
{
    public class PollViewModel : PollData
    {
        const string imagesLocalization = "/IMGs/PollIMG/";
        const string fileExtension = ".png";

        private IDatabaseConnectionService databaseConnectionService;
        private ISQLDatabaseConnectionService sQLDatabaseConnectionService;
        private IInformationBusService informationBusService;
        private ISettingsService settingsService;

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

        private RelayCommand _GetMeThatRum;
        public RelayCommand GetMeThatRum
        {
            get
            {
                if (_GetMeThatRum == null)
                {
                    _GetMeThatRum = new RelayCommand(
                    async () =>
                    {
                        PanelVisibilityService.DataGridViewModelVisibility = Visibility.Visible;

                        informationBusService.OriginalBeverages = await databaseConnectionService.GetDataFromDatabaseWithConditions(PollPurpose, 5, PollMixes, getListWithSetFlavours(), PollPricePoints);
                        Settings settings = settingsService.ReadSettings();
                        sQLDatabaseConnectionService.SendSearchingStatistics(settings.UserID, (PollData)this);
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


        public PollViewModel(IInformationBusService informationBusService, IDatabaseConnectionService databaseConnectionService, ISQLDatabaseConnectionService sQLDatabaseConnectionService, IPanelVisibilityService panelVisibilityService, ISettingsService settingsService)
        {
            this.PanelVisibilityService = panelVisibilityService;
            this.databaseConnectionService = databaseConnectionService;
            this.sQLDatabaseConnectionService = sQLDatabaseConnectionService;
            this.informationBusService = informationBusService;
            this.settingsService = settingsService;
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
    }
}
