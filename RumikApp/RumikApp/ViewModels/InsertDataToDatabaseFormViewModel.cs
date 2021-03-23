using GalaSoft.MvvmLight;
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

        private BitmapImage _TestIcon;
        public BitmapImage TestIcon
        {
            get { return _TestIcon; }
            set
            {
                if (_TestIcon == value)
                    return;

                _TestIcon = value;
                RaisePropertyChanged("TestIcon");
            }
        }

        public InsertDataToDatabaseFormViewModel(MainViewModel mainViewModel)
        {
            ;
            byte[] XD = loadImage(null);
            //

            TestIcon = ImageProcessingService.ConvertToBitMapImage(XD);
        }

        void saveToDatabase()
        {
            byte[] Image = new byte[250000];
            mainViewModel.DatabaseConnectionService.SaveImageToDatabase(Image);
        }

        byte[] loadImage(string imagePath)
        {
            if (imagePath == null || imagePath == "")
                imagePath = Environment.CurrentDirectory + "\\IMG\\Unknown.png";

            if (File.Exists(imagePath))
            {

                byte[] array = ImageProcessingService.FileToByteArray(imagePath);
                TestIcon = ImageProcessingService.ConvertToBitMapImage(array);

                return array;

            }
            return null;

        }
    }
}
