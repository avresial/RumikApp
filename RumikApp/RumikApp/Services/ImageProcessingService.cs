﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace RumikApp.Services
{
    static class ImageProcessingService
    {
        public static int MaxSupportedImageSize = 30000;
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

            return image;
        }

        /// <summary>
        /// Very experimental, but happens to be working :)
        /// </summary>
        /// <param name="bitmapImage"></param>
        /// <returns></returns>
        public static byte[] ConvertBitMapImageToByteArray(BitmapImage bitmapImage)
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

        public static byte[] FileToByteArray(string fileName)
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
    }
}
