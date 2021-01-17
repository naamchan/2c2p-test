#nullable enable

using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;

namespace _2c2p_test.Common.FileFormat
{
    public abstract class CSVFormat<TModel> : IFileFormatToModel<TModel>
    {
        public List<TModel> Contents = new();
        public List<(int, string)> parseFailed = new();
        IEnumerable<TModel> IFileFormatToModel<TModel>.GetContents() => Contents;

        async Task<IFileFormatToModel<TModel>?> IFileFormatToModel<TModel>.Parse(Stream readStream)
        {
            using var textReader = new StreamReader(readStream);
            var csvReader = new CsvReader(textReader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
                BadDataFound = BadDataCallback,
            });

            int line = 0;
            while (await csvReader.ReadAsync())
            {
                line++;

                var model = await TryCreateRecord(csvReader);
                if (model is not null)
                {
                    Contents.Add(model);
                }
                else
                {
                    parseFailed.Add((line, csvReader.Context.RawRecord));
                }
            }

            return parseFailed.Any() ? null : this;
        }

        abstract protected Task<TModel?> TryCreateRecord(CsvReader csvReader);

        private void BadDataCallback(ReadingContext context)
        {
            parseFailed.Add((context.RawRow, context.RawRecord));
        }
    }
}