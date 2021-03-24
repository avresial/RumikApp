using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using RumikApp.Services;
using RumikApp.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace RumikApp.ViewModels
{
    public class InsertDataToDatabaseFormViewModel : ViewModelBase
    {
        private MainViewModel mainViewModel;

        private Beverage _Beverage = new Beverage();
        public Beverage Beverage
        {
            get { return _Beverage; }
            set
            {
                if (_Beverage == value)
                    return;

                _Beverage = value;
                RaisePropertyChanged("Beverage");
            }
        }

        private List<String> _StringList = new List<string>();
        public List<String> StringList
        {
            get { return _StringList; }
            set
            {
                if (_StringList == value)
                    return;

                _StringList = value;
                RaisePropertyChanged("StringList");
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
                                    MessageBox.Show(message, title);
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
                        Beverage = new Beverage();
                        byte[] TMPArray = loadImage(null);

                        BitmapImage CheckSize = ImageProcessingService.ConvertToBitMapImage(TMPArray);

                        if (CheckSize.PixelWidth <= 500 && CheckSize.PixelHeight <= 500)
                        {
                            img = TMPArray;
                            Beverage.TestIcon = CheckSize;
                        }
                        Beverage.Color = StringList[0];
                    },
                    () =>
                    {
                        return doesFormContainsNewData();
                    });
                }

                return _SaveToDatabase;
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

        public InsertDataToDatabaseFormViewModel(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
            
            byte[] TMPArray = loadImage(null);

            BitmapImage CheckSize = ImageProcessingService.ConvertToBitMapImage(TMPArray);

            if (CheckSize.PixelWidth <= 500 && CheckSize.PixelHeight <= 500)
            {
                img = TMPArray;
                Beverage.TestIcon = CheckSize;
            }

            StringList.Add("Złoty");
            StringList.Add("Miedziany");
            StringList.Add("Biały");
            StringList.Add("Bursztynowy");

            Beverage.Color = StringList[0];
        }

        void saveToDatabase()
        {
            //RumsBaseTEST
            byte[] Image = new byte[250000];

            Output = mainViewModel.DatabaseConnectionService.SaveBevreageToDatabase(Beverage, img);
        }

        byte[] loadImage(string imagePath)
        {
            if (imagePath == null || imagePath == "")
                imagePath = "../../IMGs/Bottles/UnknownBottle.png";

            if (File.Exists(imagePath))
                return ImageProcessingService.FileToByteArray(imagePath);

            return null;
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

            //if (Beverage.Grade != 0)
            //    controlSum++;

            //if (Beverage.GradeWithCoke != 0)
            //    controlSum++;

            if (Beverage.Price != 0)
                controlSum++;


            if (controlSum >= 4)
                formContainsNewData = true;

            return formContainsNewData;
        }

    }
}
