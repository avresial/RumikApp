using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumikApp.Core.Services
{
    public class StreamReaderService : IStreamReaderService
    {
        private StreamReader streamReader;

        public StreamReaderService()
        {
        }

        public IStreamReaderService Create(string filePath)
        {
            streamReader = new StreamReader(filePath);
            return this;
        }

        public void Dispose()
        {
            streamReader.Dispose();
        }

        public string ReadToEnd()
        {
            if (streamReader is null) throw new ArgumentNullException(nameof(streamReader));

            return streamReader.ReadToEnd();
        }
    }
}
