using System.IO;

namespace RumikApp.Services
{
    public interface IFileService
    {
        bool FileExists(string filePath);
        void FileCreate(string filePath);

        bool DirectoryExists(string directoryath);
        DirectoryInfo CreateDirectory(string directoryath);
    }
}