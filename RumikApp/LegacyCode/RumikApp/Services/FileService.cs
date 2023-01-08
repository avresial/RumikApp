using System.IO;

namespace RumikApp.Services
{
    class FileService : IFileService
    {
        public DirectoryInfo CreateDirectory(string directoryath)
        {
            return Directory.CreateDirectory(directoryath);
        }

        public void FileCreate(string filePath)
        {
            File.Create(filePath).Close();
        }

        public bool DirectoryExists(string directoryPath)
        {
            return Directory.Exists(directoryPath);
        }

        public bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }
    }
}
