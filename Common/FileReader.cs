#nullable enable

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace _2c2p_test.Common
{
    public class FileReader
    {
        public List<string> Contents { get; private set; } = new();
        private readonly Stream stream;

        public FileReader(Stream stream)
        {
            this.stream = stream;
        }

        public async Task<bool> Read()
        {
            using var fileReader = new System.IO.StreamReader(this.stream);
            for (; ; )
            {
                var line = await fileReader.ReadLineAsync();
                if (line == null)
                {
                    break;
                }
                Contents.Add(line);
            }
            return true;
        }
    }
}