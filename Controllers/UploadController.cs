#nullable enable
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

            using var fileReader = new System.IO.StreamReader(file.OpenReadStream());

            int lineNumber = 1;
            for (; ; )
            {
                var line = await fileReader.ReadLineAsync();
                if (line == null)
                {
                    break;
                }

                System.Console.WriteLine($"{lineNumber}: {line}");
                lineNumber++;
            }
            return Ok();
        }
    }
}