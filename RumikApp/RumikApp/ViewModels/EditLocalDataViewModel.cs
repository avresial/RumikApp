using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using RumikApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace RumikApp.ViewModels
{
    public class EditLocalDataViewModel : ViewModelBase
    {
        private FileDatabaseConnectionService fileDatabaseConnectionService;

        private IInformationBusService _IInformationBusService;
        public IInformationBusService IInformationBusService
        {
            get { return _IInformationBusService; }
            set
            {
                if (_IInformationBusService == value)
                    return;

                _IInformationBusService = value;
                RaisePropertyChanged(nameof(IInformationBusService));
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

        private Beverage _SelectedBeverage;
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


        public EditLocalDataViewModel(IPanelVisibilityService panelVisibilityService, IInformationBusService informationBusService, FileDatabaseConnectionService fileDatabaseConnectionService)
        {
            this.fileDatabaseConnectionService = fileDatabaseConnectionService;
            PanelVisibilityService = panelVisibilityService;
            IInformationBusService = informationBusService;


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

                                BitmapImage CheckSize = ImageProcessingService.ConvertToBitMapImage(loadedIMG);

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
                    () =>
                    {
                        ObservableCollection<Beverage> updatedBeverages = new ObservableCollection<Beverage>();
                        foreach (Beverage beverage in IInformationBusService.OriginalBeverages)
                            updatedBeverages.Add(beverage);

                        fileDatabaseConnectionService.UpdateBeveragesDatabase(updatedBeverages);
                    },
                    () =>
                    {
                        return (SelectedBeverage == null) ? false : true;
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
                        fileDatabaseConnectionService.DeleteBeverageDatabase(SelectedBeverage);
                        IInformationBusService.OriginalBeverages = await fileDatabaseConnectionService.GetAllData();
                    },
                    () =>
                    {
                        return (SelectedBeverage == null) ? false : true;
                    });
                }

                return _DeleteCurrentBeverage;
            }
        }

        private byte[] loadImage(string imagePath)
        {
            if (imagePath == null || imagePath == "")
                imagePath = "../../IMGs/Bottles/UnknownBottle.png";

            return ImageProcessingService.FileToByteArray(imagePath);

        }

    }
}
