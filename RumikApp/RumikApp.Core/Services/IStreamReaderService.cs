using System;
using System.IO;

namespace RumikApp.Core.Services
{
    public interface IStreamReaderService : IDisposable
    {
        IStreamReaderService Create(string filePath);
        string ReadToEnd();
    }
}