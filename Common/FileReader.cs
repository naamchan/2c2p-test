#nullable enable

using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace _2c2p_test.Common
{
    public class FileReader
    {
        private string Content = "";
        private readonly Stream stream;

        public FileReader(Stream stream)
        {
            this.stream = stream;
        }

        public async Task<bool> Read()
        {
            using var fileReader = new System.IO.StreamReader(this.stream);
            Content = await fileReader.ReadToEndAsync();
            return true;
        }

        public Stream GetReadStream()
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(Content));
        }
    }
}