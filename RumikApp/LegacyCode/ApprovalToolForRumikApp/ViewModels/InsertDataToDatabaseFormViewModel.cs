using ApprovalToolForRumikApp.Enums;
using ApprovalToolForRumikApp.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace ApprovalToolForRumikApp.ViewModels
{
    public class InsertDataToDatabaseFormViewModel : ViewModelBase
    {
        private IDatabaseConnectionService databaseConnectionService;

        private Beverage _Beverage = new Beverage();
        public Beverage Beverage
        {
            get { return _Beverage; }
            set
            {
                if (_Beverage == value)
                    return;
                AmountOfRows = databaseConnectionService.GetNumberOfRows();
                _Beverage = value;
                RaisePropertyChanged(nameof(Beverage));
            }
        }

        private int _AmountOfRows;
        public int AmountOfRows
        {
            get { return _AmountOfRows; }
            set
            {
                if (_AmountOfRows == value)
                    return;

                _AmountOfRows = value;
                RaisePropertyChanged(nameof(AmountOfRows));
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

                                byte[] TMPArray = loadImage(filePath);

                                BitmapImage CheckSize = ImageProcessingService.ConvertToBitMapImage(TMPArray);

                                if (CheckSize.PixelWidth <= 500 && CheckSize.PixelHeight <= 500)
                                {
                                    img = TMPArray;
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
                        saveToDatabase();

                        Beverage = databaseConnectionService.GetRandomRow();

                    },
                    () =>
                    {
                        return doesFormContainsNewData();
                    });
                }

                return _SaveToDatabase;
            }
        }

        private RelayCommand _DeleteRecord;
        public RelayCommand DeleteRecord
        {
            get
            {
                if (_DeleteRecord == null)
                {
                    _DeleteRecord = new RelayCommand(
                    () =>
                    {
                        databaseConnectionService.DeleteBevreageFromDatabase(Beverage, databaseConnectionService.MainDataTable);
                        Beverage = databaseConnectionService.GetRandomRow();
                    },
                    () =>
                    {
                        return doesFormContainsNewData();
                    });
                }

                return _DeleteRecord;
            }
        }



        private RelayCommand _GetAnother;
        public RelayCommand GetAnother
        {
            get
            {
                if (_GetAnother == null)
                {
                    _GetAnother = new RelayCommand(
                    () =>
                    {
                        Beverage = databaseConnectionService.GetRandomRow();
                    },
                    () =>
                    {
                        return true;
                    });
                }

                return _GetAnother;
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

        public InsertDataToDatabaseFormViewModel(IDatabaseConnectionService databaseConnectionService)
        {
            this.databaseConnectionService = databaseConnectionService;

            byte[] TMPArray = loadImage(null);

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
            Beverage = databaseConnectionService.GetRandomRow();
        }

        void saveToDatabase()
        {
            byte[] Image = new byte[250000];
            databaseConnectionService.DeleteBevreageFromDatabase(Beverage, databaseConnectionService.MainDataTable);
            Output = databaseConnectionService.SaveBevreageToDatabase(Beverage, img);

        }

        byte[] loadImage(string imagePath)
        {
            if (imagePath == null || imagePath == "")
                imagePath = "../../IMGs/UnknownBottle.png";

            return ImageProcessingService.FileToByteArray(imagePath);
        }
        bool doesFormContainsNewData()
        {
            if (Beverage == null)
                return false;

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
