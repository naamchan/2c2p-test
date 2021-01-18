#nullable enable

using System.Collections.Generic;
using System.Threading.Tasks;

namespace _2c2p_test.Common.FileFormat
{
    public class XMLFormat : IFileFormat
    {
        async Task<IFileFormat?> IFileFormat.TryParse(IEnumerable<string> contents)
        {
            return new System.Random().Next() % 2 == 0 ? this : null;
        }
    }
}