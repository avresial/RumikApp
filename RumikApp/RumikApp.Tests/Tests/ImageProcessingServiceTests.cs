using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RumikApp.Services;
using Xunit;
using System.Windows.Media.Imaging;

namespace RumikApp.Tests
{
    public class ImageProcessingServiceTests
    {
        [Fact]
        void FileToByteArray_When_It_Loads_Data()
        {
            // Arrange

            byte[] actualResult = null;
            string FileLocation = "../../IMGS/Test.png";


            // Act
            actualResult = ImageProcessingService.FileToByteArray(FileLocation);


            // Assert
            Assert.NotNull(actualResult);
            Assert.Equal(4661, actualResult.Count());

        }

        [Fact]
        void FileToByteArray_When_There_Is_No_File()
        {
            // Arrange
            byte[] actualResult = null;
            string FileLocation = "../../IMGS/404ThereIsNotSuchAFile.png";

            // Act
            actualResult = ImageProcessingService.FileToByteArray(FileLocation);

            // Assert
            Assert.Null(actualResult);
        }


        [Fact]
        void ConvertToBitMapImage_When_It_Loads_Data()
        {
            // Arrange

            BitmapImage actualResult = null;
            string FileLocation = "../../IMGS/Test.png";


            // Act
            byte[] byteArray = ImageProcessingService.FileToByteArray(FileLocation);

            actualResult = ImageProcessingService.ConvertToBitMapImage(byteArray);
            // Assert
            Assert.NotNull(actualResult);
            Assert.True(actualResult.PixelWidth <= 500);

        }

        [Fact]
        void ConvertToBitMapImage_When_ByteArray_Is_Null()
        {
            // Arrange
            BitmapImage actualResult = null;

            // Act
            actualResult = ImageProcessingService.ConvertToBitMapImage(null);

            // Assert
            Assert.Null(actualResult);

        }


        [Fact]
        void ConvertBitMapImageToByteArray_When_It_Loads_Data()
        {
            // Arrange

            byte[] actualResult = null;
            string FileLocation = "../../IMGS/Test.png";


            // Act
            byte[] byteArray = ImageProcessingService.FileToByteArray(FileLocation);

            BitmapImage bitmapImage = ImageProcessingService.ConvertToBitMapImage(byteArray);

            actualResult = ImageProcessingService.ConvertBitMapImageToByteArray(bitmapImage);

            // Assert
            Assert.NotNull(actualResult);
            Assert.True(actualResult.Count() > byteArray.Count() / 2); // it makes sure some data is being read
            //Assert.True(actualResult.Count() == byteArray.Count());

        }

        [Fact]
        void ConvertBitMapImageToByteArray_When_There_Is_No_File()
        {
            // Arrange

            BitmapImage actualResult = null;
            string FileLocation = "../../IMGS/404ThereIsNotSuchAFile.png";


            // Act
            byte[] byteArray = ImageProcessingService.FileToByteArray(FileLocation);
            actualResult = ImageProcessingService.ConvertToBitMapImage(byteArray);

            // Assert
            Assert.Null(actualResult);

        }
    }
}
