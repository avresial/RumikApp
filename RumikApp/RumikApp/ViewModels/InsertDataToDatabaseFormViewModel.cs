using GalaSoft.MvvmLight;
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
            //mainViewModel.DatabaseConnectionService.SaveImageToDatabase(XD);
            XD  = mainViewModel.DatabaseConnectionService.GetIMGData();
            TestIcon = ConvertToBitMapImage(XD);
        }
        
        byte[] loadImage(string imagePath)
        {
            if (imagePath == null || imagePath =="")
            
                imagePath = Environment.CurrentDirectory + "\\IMG\\Unknown.png";
            
            


            if (File.Exists(imagePath))
            {
                // Create image element to set as icon on the menu element

                BitmapImage bmImage = new BitmapImage();
                //bmImage.BeginInit();
                //bmImage.UriSource = new Uri(imagePath, UriKind.Absolute);
                //bmImage.EndInit();
                //TestIcon = bmImage;

                byte[] array = FileToByteArray(imagePath);
                TestIcon = ConvertToBitMapImage(array);

                //BitmapImage bImg = new BitmapImage();
                //bImg.Source = ConvertToBitMapImage(bytes);
                //byte[] bytes = ImageToByte(bImg);
                return array;

            }
            return null;

        }
        public static BitmapImage ConvertToBitMapImage(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(bytes))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            //image.Freeze();
            return image;
        }
        public static byte[] FileToByteArray(string fileName)
        {
            byte[] fileData = null;

            using (FileStream fs = File.OpenRead(fileName))
            {
                var binaryReader = new BinaryReader(fs);
                fileData = binaryReader.ReadBytes((int)fs.Length);
            }
            return fileData;
        }

    }
}
