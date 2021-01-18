#nullable enable

using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using _2c2p_test.Common;
using _2c2p_test.Common.FileFormat;

namespace _2c2p_test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UploadController : ControllerBase
    {
        public UploadController()
        {
        }

        [HttpPost]
        [RequestSizeLimit(1048576)]
        public async Task<IActionResult> Post(IFormFile? file)
        {
            System.Console.WriteLine("Posted");

            if (file == null)
            {
                return BadRequest("Unknown format");
            }

            var format = await (new FileFormatConverter(new FileReader(file.OpenReadStream()), new IFileFormat[] {
                new CSVFormat(),
                new XMLFormat()
            })).TryConvert();

            if (format != null)
            {
                System.Console.WriteLine($"{format.GetType().Name}");
                return Ok();
            }

            System.Console.WriteLine($"Failed");
            return BadRequest("Unknown format");
        }
    }
}