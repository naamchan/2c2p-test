#nullable enable

using System.Threading.Tasks;
using System.Collections.Generic;
using _2c2p_test.Common.FileFormat;
using System.Linq;

namespace _2c2p_test.Common
{
    public class FileFormatConverter
    {
        private readonly FileReader reader;
        private readonly IEnumerable<IFileFormat> supportedFormats;

        public FileFormatConverter(FileReader reader, IEnumerable<IFileFormat> supportedFormats)
        {
            this.reader = reader;
            this.supportedFormats = supportedFormats;
        }

        public async Task<IFileFormat?> TryConvert()
        {
            if (!(await reader.Read()))
            {
                return null;
            }

            var remainingTasks = new HashSet<Task<IFileFormat?>>(
                supportedFormats.Select((x) => x.Parse(reader.GetReadStream())
            ));

            while (remainingTasks.Any())
            {
                var next = await Task.WhenAny(remainingTasks);

                if (next.Result != null)
                {
                    return next.Result;
                }
                remainingTasks.Remove(next);
            }
            return null;
        }
    }
}