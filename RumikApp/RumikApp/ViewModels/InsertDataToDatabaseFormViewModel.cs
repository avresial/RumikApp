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
                        ;
                    },
                    () =>
                    {
                        return true;
                    });
                }

                return _LoadNewImage;
            }
        }



        public InsertDataToDatabaseFormViewModel(MainViewModel mainViewModel)
        {
            Beverage.TestIcon = ImageProcessingService.ConvertToBitMapImage(loadImage(null));
        }

        void saveToDatabase()
        {
            byte[] Image = new byte[250000];
            mainViewModel.DatabaseConnectionService.SaveImageToDatabase(Image);
        }

        byte[] loadImage(string imagePath)
        {
            if (imagePath == null || imagePath == "")
                imagePath = Environment.CurrentDirectory + "\\IMG\\UnknownBottle.png";

            if (File.Exists(imagePath))
            {
                byte[] array = ImageProcessingService.FileToByteArray(imagePath);

                return array;
            }

            return null;
        }
    }
}
