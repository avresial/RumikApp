using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using RumikApp.Enums;
using RumikApp.Services;
using RumikApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace RumikApp.ViewModels
{
    public class InsertDataToDatabaseFormViewModel : ViewModelBase
    {
        private FileDatabaseConnectionService fileDatabaseConnectionService;
        private IDatabaseConnectionService databaseConnectionService;

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

        private Beverage _Beverage = new Beverage();
        public Beverage Beverage
        {
            get { return _Beverage; }
            set
            {
                if (_Beverage == value)
                    return;

                _Beverage = value;
                RaisePropertyChanged(nameof(Beverage));
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
                                    img = loadedIMG;
                                    Beverage.TestIcon = CheckSize;
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

        private RelayCommand _SaveToDatabase;
        public RelayCommand SaveToDatabase
        {
            get
            {
                if (_SaveToDatabase == null)
                {
                    _SaveToDatabase = new RelayCommand(
                    () =>
                    {
                        if (fileDatabaseConnectionService.TestConnectionToTable(AvailableTables.NotYetApprovedTEST))
                        {
                            ObservableCollection<Beverage> allBeverage = fileDatabaseConnectionService.GetAllData();
                            if (allBeverage != null && allBeverage.Count > 0)
                                Beverage.ID = (int)allBeverage.Max(x => x.ID) + 1;
                        }

                        saveToDatabase();

                        Beverage = new Beverage();
                        byte[] TMPArray = loadImage(null);

                        BitmapImage CheckSize = ImageProcessingService.ConvertToBitMapImage(TMPArray);

                        if (CheckSize.PixelWidth <= 500 && CheckSize.PixelHeight <= 500)
                        {
                            img = TMPArray;
                            Beverage.TestIcon = CheckSize;
                        }
                        Beverage.Color = ColorsList[0];
                    },
                    () =>
                    {
                        return doesFormContainsNewData();
                    });
                }

                return _SaveToDatabase;
            }
        }

        private RelayCommand _CloseForm;
        public RelayCommand CloseForm
        {
            get
            {
                if (_CloseForm == null)
                {
                    _CloseForm = new RelayCommand(
                    () =>
                    {
                        PanelVisibilityService.MainPanelVisibility = Visibility.Visible;
                    },
                    () =>
                    {
                        return true;
                    });
                }

                return _CloseForm;
            }
        }

        private String _Output;
        public String Output
        {
            get { return _Output; }
            set
            {
                if (_Output == value)
                    return;

                _Output = value;
                RaisePropertyChanged(nameof(Output));
            }
        }

        private Random rad = new Random();

        private byte[] img;

        public InsertDataToDatabaseFormViewModel(IDatabaseConnectionService databaseConnectionService, IPanelVisibilityService panelVisibilityService, FileDatabaseConnectionService fileDatabaseConnectionService)
        {
            this.fileDatabaseConnectionService = fileDatabaseConnectionService;
            PanelVisibilityService = panelVisibilityService;
            this.databaseConnectionService = databaseConnectionService;

            byte[] TMPArray = loadImage(null);
            if (TMPArray == null)
                return;

            BitmapImage CheckSize = ImageProcessingService.ConvertToBitMapImage(TMPArray);

            if (CheckSize.PixelWidth <= 500 && CheckSize.PixelHeight <= 500)
            {
                img = TMPArray;
                Beverage.TestIcon = CheckSize;
            }

            ColorsList.Add("Złoty");
            ColorsList.Add("Miedziany");
            ColorsList.Add("Biały");
            ColorsList.Add("Bursztynowy");
            ColorsList.Add("Czarny");

            Beverage.Color = ColorsList[0];
        }

        void saveToDatabase()
        {
            byte[] Image = new byte[250000];

            Output = databaseConnectionService.SaveBevreageToDatabase(Beverage, img);
        }

        byte[] loadImage(string imagePath)
        {
            if (imagePath == null || imagePath == "")
                imagePath = "IMGs/Bottles/UnknownBottleCopy.png";

            return ImageProcessingService.FileToByteArray(imagePath);

        }
        bool doesFormContainsNewData()
        {
            bool formContainsNewData = false;
            int controlSum = 0;

            if (Beverage.Name != null)
                if (Beverage.Name.Length > 1)
                    controlSum++;

            if (Beverage.Capacity != 0)
                controlSum++;

            if (Beverage.AlcoholPercentage != 0)
                controlSum++;

            if (Beverage.Price != 0)
                controlSum++;

            if (controlSum >= 4)
                formContainsNewData = true;

            return formContainsNewData;
        }

    }
}
