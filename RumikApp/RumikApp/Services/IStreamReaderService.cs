using System;
using System.IO;

namespace RumikApp.Services
{
    public interface IStreamReaderService : IDisposable
    {
        IStreamReaderService Create(string filePath);
        string ReadToEnd();
    }
}