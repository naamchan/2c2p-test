#nullable enable

using System.Collections.Generic;
using System.Threading.Tasks;

namespace _2c2p_test.Common.FileFormat
{
    public interface IFileFormat
    {
        Task<IFileFormat?> TryParse(IEnumerable<string> contents);
    }
}