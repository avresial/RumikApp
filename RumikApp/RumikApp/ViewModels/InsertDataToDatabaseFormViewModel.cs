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

                                Beverage.TestIcon = ImageProcessingService.ConvertToBitMapImage(loadImage(filePath));

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
                    },
                    () =>
                    {
                        return true;
                    });
                }

                return _SaveToDatabase;
            }
        }


        private Random rad = new Random();
        private byte[] img;

        public InsertDataToDatabaseFormViewModel(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
            Beverage.TestIcon = ImageProcessingService.ConvertToBitMapImage(loadImage(null));
           
        }

        void saveToDatabase()
        {
            //RumsBaseTEST
            byte[] Image = new byte[250000];

            mainViewModel.DatabaseConnectionService.SaveBevreageToDatabase(Beverage, img);
            //mainViewModel.DatabaseConnectionService.SaveImageToDatabase(Image);
        }

        byte[] loadImage(string imagePath)
        {
            if (imagePath == null || imagePath == "")
                imagePath = Environment.CurrentDirectory + "\\IMG\\UnknownBottle.png";

            if (File.Exists(imagePath))
            {
                byte[] array = ImageProcessingService.FileToByteArray(imagePath);

                if (array.Length < ImageProcessingService.MaxSupportedImageSize)
                {
                    img = array;
                    return array;
                }
                else
                {
                    string message = "Zdięcie wydaje się być za duże.\nPrzyjmujemy zdjęcia o rozmiarze 30 kB";
                    string title = "Task failed succesfully";
                    MessageBox.Show(message, title);
                }
            }

            return null;
        }
    }
}
