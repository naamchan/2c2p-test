#nullable enable

using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using _2c2p_test.Model;
using _2c2p_test.Repository;
using System.Collections.Generic;

namespace _2c2p_test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SelectController : ControllerBase
    {
        [HttpGet("currency/{currencyCode}")]
        public async Task<IActionResult> Currency(string currencyCode)
        {
            List<ResultTransactionModel> result = new();
            await foreach (var model in new TransactionRepository(HttpContext.RequestServices).FetchByCurrencyCode(currencyCode))
            {
                result.Add(new ResultTransactionModel(model));
            }
            return Ok(result);
        }
    }
}