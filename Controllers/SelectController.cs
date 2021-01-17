#nullable enable

using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using _2c2p_test.Model;
using _2c2p_test.Repository;

namespace _2c2p_test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SelectController : ControllerBase
    {
        [HttpGet("currency/{currencyCode}")]
        public async Task<IActionResult> Currency(string currencyCode)
        {
            var result = await new TransactionRepository(HttpContext.RequestServices).FetchByCurrencyCode(currencyCode);
            return Ok(result.Select((x) => new ResultTransactionModel(x)));
        }
    }
}