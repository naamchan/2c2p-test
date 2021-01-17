#nullable enable


using System.Threading.Tasks;
using _2c2p_test.Model;
using _2c2p_test.Model.Factory;
using CsvHelper;

namespace _2c2p_test.Common.FileFormat
{
    public class CSVTransactionModelFormat : CSVFormat<TransactionModel>
    {
        protected override Task<TransactionModel?> TryCreateRecord(CsvReader csvReader)
        {
            if (
                csvReader.TryGetField<string>(0, out var transactionID) &&
                csvReader.TryGetField<string>(1, out var amount) &&
                csvReader.TryGetField<string>(2, out var currencyCode) &&
                csvReader.TryGetField<string>(3, out var transactionDate) &&
                csvReader.TryGetField<string>(4, out var status)
            )
            {
                var model = CSVTransactionModelFactory.Create(transactionID, amount, currencyCode, transactionDate, status);
                if (model != null)
                {
                    return Task.FromResult<TransactionModel?>(model);
                }
            }
            return Task.FromResult<TransactionModel?>(null);
        }
    }
}