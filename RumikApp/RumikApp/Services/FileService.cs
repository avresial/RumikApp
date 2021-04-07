using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumikApp.Services
{
    class FileService : IFileService
    {
        public bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }
    }
}
