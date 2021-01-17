#nullable enable

using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using _2c2p_test.Common;
using _2c2p_test.Common.FileFormat;
using _2c2p_test.Model;

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

            var models = await (new FileFormatConverter<TransactionModel>(new FileReader(file.OpenReadStream()), new IFileFormatToModel<TransactionModel>[] {
                new CSVTransactionModelFormat(),
                new XMLTransactionModelFormat()
            })).TryConvert();

            if (models == null)
            {
                return BadRequest("Unknown format");
            }



            return Ok();
        }
    }
}