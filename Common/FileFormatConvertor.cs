#nullable enable

using System.Threading.Tasks;
using System.Collections.Generic;
using _2c2p_test.Common.FileFormat;
using System.Linq;

namespace _2c2p_test.Common
{
    public class FileFormatConverter<TModel>
    {
        private readonly FileReader reader;
        private readonly IEnumerable<IFileFormatToModel<TModel>> supportedFormats;

        public FileFormatConverter(
            FileReader reader,
            IEnumerable<IFileFormatToModel<TModel>> supportedFormats)
        {
            this.reader = reader;
            this.supportedFormats = supportedFormats;
        }

        public async Task<IEnumerable<TModel>?> TryConvert()
        {
            if (!(await reader.Read()))
            {
                return null;
            }

            var remainingTasks = new HashSet<Task<IFileFormatToModel<TModel>?>>(
                supportedFormats.Select((x) => x.Parse(reader.GetReadStream())
            ));

            IFileFormatToModel<TModel>? supportedFormat = null;
            while (remainingTasks.Any())
            {
                var next = await Task.WhenAny(remainingTasks);

                if (next.Result != null)
                {
                    supportedFormat = next.Result;
                    break;
                }
                remainingTasks.Remove(next);
            }

            if (supportedFormat is null)
            {
                return null;
            }

            return supportedFormat.GetContents();
        }
    }
}