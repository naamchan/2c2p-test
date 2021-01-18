#nullable enable

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace _2c2p_test.Common.FileFormat
{
    public class XMLFormat : IFileFormat
    {
        Task<IFileFormat?> IFileFormat.Parse(Stream readStream)
        {
            //return Task.FromResult<IFileFormat?>(new System.Random().Next() % 2 == 0 ? this : null);
            return Task.FromResult<IFileFormat?>(null);
        }
    }
}