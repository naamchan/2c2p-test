#nullable enable

using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using _2c2p_test.Model;
using _2c2p_test.Repository;
using System.Collections.Generic;
using System;

namespace _2c2p_test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SelectController : ControllerBase
    {
        [HttpGet("currency/{currencyCode}")]
        public async Task<IActionResult> Currency(string currencyCode)
        {
            return Ok(await Fetch((repository) => repository.FetchByCurrencyCode(currencyCode)));
        }

        [HttpGet("status/{unifiedStatusCode}")]
        public async Task<IActionResult> Status(string unifiedStatusCode)
        {
            var status = ResultTransactionModel.ConvertUnifiedStatusToStatus(unifiedStatusCode);
            if (!status.HasValue)
            {
                return BadRequest("Invalid status code");
            }

            return Ok(await Fetch((repository) => repository.FetchByStatus(status.Value)));
        }

        [HttpGet("transaction_date")]
        public async Task<IActionResult> TransactionDate(DateTime start, DateTime end)
        {
            return Ok(await Fetch((repository) => repository.FetchByTransactionDate(start, end)));
        }

        private async Task<List<ResultTransactionModel>> Fetch(
            Func<TransactionRepository, IAsyncEnumerable<TransactionModel>> fetcher
        )
        {
            if (fetcher is null)
            {
                throw new ArgumentNullException(nameof(fetcher));
            }

            List<ResultTransactionModel> result = new();
            var repository = new TransactionRepository(HttpContext.RequestServices);
            await foreach (var model in fetcher.Invoke(repository))
            {
                result.Add(new ResultTransactionModel(model));
            }
            return result;
        }
    }
}