#nullable enable

using System.Text.Json.Serialization;

namespace _2c2p_test.Model
{
    public record ResultTransactionModel
    {
        [JsonPropertyName("id")]
        public string ID { get; init; }

        [JsonPropertyName("payment")]
        public string Payment { get; init; }

        [JsonPropertyName("status")]
        public string Status { get; init; }

        public ResultTransactionModel(string id, string payment, string status)
        {
            ID = id;
            Payment = payment;
            Status = status;
        }

        public ResultTransactionModel(TransactionModel transactionModel)
        {
            ID = transactionModel.TransactionID;
            Payment = $"{transactionModel.Amount} {transactionModel.CurrencyCode}";
            Status = ConvertStatusToUnifiedStatus(transactionModel.Status);
        }

        public static string ConvertStatusToUnifiedStatus(TransactionModel.TransactionStatus status)
        {
            switch (status)
            {
                case TransactionModel.TransactionStatus.Approved:
                    return "A";
                case TransactionModel.TransactionStatus.Finished:
                    return "R";
                case TransactionModel.TransactionStatus.Failed:
                    return "D";
                default:
                    return "N/A";
            }
        }

        public static TransactionModel.TransactionStatus? ConvertUnifiedStatusToStatus(string statusCode)
        {
            switch (statusCode)
            {
                case "A":
                    return TransactionModel.TransactionStatus.Approved;
                case "R":
                    return TransactionModel.TransactionStatus.Finished;
                case "D":
                    return TransactionModel.TransactionStatus.Failed;
                default:
                    return null;
            }
        }
    }
}