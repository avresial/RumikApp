using GalaSoft.MvvmLight.CommandWpf;
using RumikApp.Infrastructure.Dto;
using System.Collections.Generic;
using System.Windows;
using RumikApp.Core.Domain;
using RumikApp.Core.Services;
using RumikApp.Core.Models;
using RumikApp.Infrastructure.Repositories;
using RumikApp.Infrastructure.Extensions;

namespace RumikApp.UI.ViewModels
{
    public class PollViewModel : PollData
    {
        private BeverageContainer beverages;
        private IBeverageRepository beverageRepository;

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
                        beverages.Clear();

                        IEnumerable<BeverageDto> BeverageDtos = await beverageRepository.Browse(x => BeverageDtoIsValid(x));

                        foreach (BeverageDto beverageDto in BeverageDtos)
                            beverages.Add(beverageDto.BeverageDtoToBeverage());

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


        public PollViewModel(IPanelVisibilityService panelVisibilityService, BeverageContainer beverages, IBeverageRepository beverageRepository = null)
        {
            this.PanelVisibilityService = panelVisibilityService;
            this.beverages = beverages;
            this.beverageRepository = beverageRepository;
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

            if (Vanila.IsSet) Flavours.Add(Vanila);
            if (Nuts.IsSet) Flavours.Add(Nuts);
            if (Caramel.IsSet) Flavours.Add(Caramel);
            if (Smoke.IsSet) Flavours.Add(Smoke);
            if (Cinnamon.IsSet) Flavours.Add(Cinnamon);
            if (Spirit.IsSet) Flavours.Add(Spirit);
            if (Fruits.IsSet) Flavours.Add(Fruits);
            if (Honey.IsSet) Flavours.Add(Honey);

            return Flavours;
        }

        private bool BeverageDtoIsValid(BeverageDto beverageDto)
        {
            if (WithCoke && beverageDto.GradeWithCoke < 5) return false;
            if (solo && beverageDto.Grade < 5) return false;
            if (!IsBeverageInAnyActivePricePoint(beverageDto)) return false;

            if (beverageDto.Vanila != Vanila.IsSet && Vanila.IsSet) return false;
            if (beverageDto.Nuts != Nuts.IsSet && Nuts.IsSet) return false;
            if (beverageDto.Caramel != Caramel.IsSet && Caramel.IsSet) return false;
            if (beverageDto.Smoke != Smoke.IsSet && Smoke.IsSet) return false;
            if (beverageDto.Cinnamon != Cinnamon.IsSet && Cinnamon.IsSet) return false;
            if (beverageDto.Spirit != Spirit.IsSet && Spirit.IsSet) return false;
            if (beverageDto.Fruits != Fruits.IsSet && Fruits.IsSet) return false;
            if (beverageDto.Honey != Honey.IsSet && Honey.IsSet) return false;

            return true;
        }

        private bool IsBeverageInAnyActivePricePoint(BeverageDto beverageDto)
        {
            bool result = false;

            if (!PricePoint1 && !PricePoint2 && !PricePoint3 && !PricePoint4) return true;

            if (PricePoint1 && beverageDto.Price < 50) result = true;
            if (PricePoint2 && beverageDto.Price >= 50 && beverageDto.Price < 70) result = true;
            if (PricePoint3 && beverageDto.Price >= 70 && beverageDto.Price < 90) result = true;
            if (PricePoint4 && beverageDto.Price >= 90) result = true;

            return result;
        }
    }
}
