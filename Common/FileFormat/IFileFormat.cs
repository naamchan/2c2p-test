#nullable enable

using System.IO;
using System.Threading.Tasks;

namespace _2c2p_test.Common.FileFormat
{
    public interface IFileFormat
    {
        Task<IFileFormat?> Parse(Stream readStream);
    }
}