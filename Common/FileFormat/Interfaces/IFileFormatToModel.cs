#nullable enable

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace _2c2p_test.Common.FileFormat
{
    public interface IFileFormatToModel<TModel>
    {
        Task<IFileFormatToModel<TModel>?> Parse(Stream readStream);
        IEnumerable<TModel> GetContents();
    }
}