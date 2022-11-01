using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace RumikApp.Core.Extensions
{
    public static class ImageExtensions
    {
        public static int MaxSupportedImageSize = 30000;

        public static byte[] FileToByteArray(this string fileName)
        {
            if (!File.Exists(fileName))
                return null;

            byte[] fileData = null;

            using (FileStream fs = File.OpenRead(fileName))
            {
                var binaryReader = new BinaryReader(fs);
                fileData = binaryReader.ReadBytes((int)fs.Length);
            }
            return fileData;
        }

        public static BitmapImage ConvertToBitMapImage(this byte[] bytes)
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

            return image;
        }

        /// <summary>
        /// Very experimental, but happens to be working :)
        /// </summary>
        /// <param name="bitmapImage"></param>
        /// <returns></returns>
        public static byte[] ConvertBitMapImageToByteArray(this BitmapImage bitmapImage)
        {
            string fileName = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RumikApp" + "\\TMP.png";

            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));

            using (var fileStream = new System.IO.FileStream(fileName, System.IO.FileMode.Create))
            {
                encoder.Save(fileStream);
            }

            return FileToByteArray(fileName);
        }

    }
}
