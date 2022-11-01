using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using RumikApp.Core.Extensions;
using RumikApp.Core.Services;
using RumikApp.Infrastructure.Dto;
using RumikApp.Infrastructure.Extensions;
using RumikApp.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace RumikApp.ViewModels
{
    public class EditLocalDataViewModel : ViewModelBase
    {

        public IBeverageRepository LocalBeverageRepository { get; set; }

        private ObservableCollection<Beverage> _BeveragesCollection;
        public ObservableCollection<Beverage> BeveragesCollection
        {
            get { return _BeveragesCollection; }
            set
            {
                if (_BeveragesCollection == value)
                    return;

                _BeveragesCollection = value;
                RaisePropertyChanged();
            }
        }

        private List<String> _ColorsList = new List<string>();
        public List<String> ColorsList
        {
            get { return _ColorsList; }
            set
            {
                if (_ColorsList == value)
                    return;

                _ColorsList = value;
                RaisePropertyChanged(nameof(ColorsList));
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



        private Beverage _SelectedBeverage = new Beverage() { ID = -1 };
        public Beverage SelectedBeverage
        {
            get { return _SelectedBeverage; }
            set
            {
                if (_SelectedBeverage == value)
                    return;

                _SelectedBeverage = value;
                RaisePropertyChanged(nameof(SelectedBeverage));
            }
        }


        public EditLocalDataViewModel(IPanelVisibilityService panelVisibilityService, IBeverageRepository localBeverageRepository)
        {
            BeveragesCollection = new ObservableCollection<Beverage>();
            PanelVisibilityService = panelVisibilityService;
            LocalBeverageRepository = localBeverageRepository;

            UpdateList();

            ColorsList.Add("Złoty");
            ColorsList.Add("Miedziany");
            ColorsList.Add("Biały");
            ColorsList.Add("Bursztynowy");
            ColorsList.Add("Czarny");

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
                        UpdateList();
                    },
                    () =>
                    {
                        return true;
                    });
                }

                return _GoToMainMenu;
            }
        }

        private RelayCommand _LoadNewImage;
        public RelayCommand LoadNewImage
        {
            get
            {
                if (_LoadNewImage == null)
                {
                    _LoadNewImage = new RelayCommand(
                    () =>
                    {
                        var fileContent = string.Empty;
                        var filePath = string.Empty;

                        using (OpenFileDialog openFileDialog = new OpenFileDialog())
                        {
                            openFileDialog.InitialDirectory = "c:\\";
                            openFileDialog.Filter = "png files (*.png)|*.png";
                            openFileDialog.FilterIndex = 2;
                            openFileDialog.RestoreDirectory = true;

                            if (openFileDialog.ShowDialog() == DialogResult.OK)
                            {

                                filePath = openFileDialog.FileName;

                                byte[] loadedIMG = loadImage(filePath);

                                BitmapImage CheckSize = loadedIMG.ConvertToBitMapImage();

                                if (CheckSize.PixelWidth <= 500 && CheckSize.PixelHeight <= 500)
                                {
                                    SelectedBeverage.TestIcon = CheckSize;
                                }
                                else
                                {
                                    string message = "Zdięcie wydaje się być za duże.\nPrzyjmujemy zdjęcia o 500x500 px";
                                    string title = "Task failed succesfully";
                                    System.Windows.MessageBox.Show(message, title);
                                }
                            }
                        }
                    },
                    () =>
                    {
                        return true;
                    });
                }

                return _LoadNewImage;
            }
        }

        private RelayCommand _SaveCurrentBeverage;
        public RelayCommand SaveCurrentBeverage
        {
            get
            {
                if (_SaveCurrentBeverage == null)
                {
                    _SaveCurrentBeverage = new RelayCommand(
                    async () =>
                    {
                        BeverageDto BeverageDto = SelectedBeverage.BeverageoToBeverageDto();
                        IEnumerable<BeverageDto> databaseBeverages = await LocalBeverageRepository.BrowseAll();

                        if (!databaseBeverages.Where(x => x.ID == BeverageDto.ID).Any())
                        {
                            int maxId = databaseBeverages.Max(x => x.ID);
                            BeverageDto.ID = ++maxId;
                        }


                        await LocalBeverageRepository.SaveToRepository(BeverageDto);
                        UpdateList();
                    },
                    () =>
                    {
                        return SelectedBeverage != null;
                    });
                }

                return _SaveCurrentBeverage;
            }
        }

        private RelayCommand _DeleteCurrentBeverage;
        public RelayCommand DeleteCurrentBeverage
        {
            get
            {
                if (_DeleteCurrentBeverage == null)
                {
                    _DeleteCurrentBeverage = new RelayCommand(
                    async () =>
                    {
                        BeverageDto BeverageDto = SelectedBeverage.BeverageoToBeverageDto();
                        await LocalBeverageRepository.RemoveFromRepository(BeverageDto);
                        UpdateList();
                    },
                    () =>
                    {

                        return true;
                        //return (SelectedBeverage == null) ? false : true;
                    });
                }

                return _DeleteCurrentBeverage;
            }
        }

        private byte[] loadImage(string imagePath)
        {
            if (imagePath == null || imagePath == "")
                imagePath = "../../IMGs/Bottles/UnknownBottle.png";

            return imagePath.FileToByteArray();
        }

        private void UpdateList()
        {
            BeveragesCollection.Clear();
            IEnumerable<BeverageDto> databaseBeverages = LocalBeverageRepository.BrowseAll().Result.OrderByDescending(x => x.ID);

            foreach (BeverageDto databaseBeverage in databaseBeverages)
                BeveragesCollection.Add(databaseBeverage.BeverageDtoToBeverage());

            BeveragesCollection.Insert(0, new Beverage() { Name = "Add new", ID = -1 });

        }

    }
}
