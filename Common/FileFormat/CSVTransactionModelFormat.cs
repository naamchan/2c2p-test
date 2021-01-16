#nullable enable


using System.Threading.Tasks;
using _2c2p_test.Model;
using CsvHelper;

namespace _2c2p_test.Common.FileFormat
{
    public class CSVTransactionModelFormat : CSVFormat<CSVTransactionModel>
    {
        protected override Task<CSVTransactionModel?> TryCreateRecord(CsvReader csvReader)
        {
            if (
                csvReader.TryGetField<string>(0, out var transactionID) &&
                csvReader.TryGetField<decimal>(1, out var amount) &&
                csvReader.TryGetField<string>(2, out var currencyCode) &&
                csvReader.TryGetField<string>(3, out var transactionDate) &&
                csvReader.TryGetField<string>(4, out var status)
            )
            {
                var model = CSVTransactionModel.Create(transactionID, amount, currencyCode, transactionDate, status);
                if (model != null)
                {
                    return Task.FromResult<CSVTransactionModel?>(model);
                }
            }
            return Task.FromResult<CSVTransactionModel?>(null);
        }
    }
}