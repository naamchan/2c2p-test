#nullable enable

using System;
using System.Globalization;
using _2c2p_test.Common;

namespace _2c2p_test.Model
{
    public record CSVTransactionModel
    {
        private CSVTransactionModel(string transactionID, decimal amount, string currencyCode, DateTime transactionDate, TransactionStatus transactionStatus) =>
            (TransactionID, Amount, CurrencyCode, TransactionDate, Status) = (transactionID, amount, currencyCode, transactionDate, transactionStatus);

        public string TransactionID { get; }
        public decimal Amount { get; }
        public string CurrencyCode { get; }
        public DateTime TransactionDate { get; }
        public TransactionStatus Status { get; }

        public enum TransactionStatus
        {
            Approved = 0,
            Failed = 1,
            Finished = 2
        }

        public static CSVTransactionModel? Create(string transactionID, decimal amount, string currencyCode, string transactionDateString, string transactionStatus)
        {
            var status = ConvertTransactionStatus(transactionStatus);
            var transactionDate = ConvertTransactionDate(transactionDateString);

            if (ValidateTransactionID(transactionID) && ValidateCurrencyCode(currencyCode) && transactionDate.HasValue && status.HasValue)
            {
                return new CSVTransactionModel(transactionID, amount, currencyCode, transactionDate.Value, status.Value);
            }

            return null;
        }

        private static bool ValidateTransactionID(string transactionID) => !string.IsNullOrEmpty(transactionID) && transactionID.Length <= 50;
        private static bool ValidateCurrencyCode(string currencyCode) => ISO4217CurrencySymbol.Validate(currencyCode);

        private static bool ValidateTransactionDate(string transactionDate) => ConvertTransactionDate(transactionDate) != null;

        private static DateTime? ConvertTransactionDate(string transactionDateString)
        {
            var format = "dd/MM/yyyy hh:mm:ss";
            if (DateTime.TryParseExact(transactionDateString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var transactionDate))
            {
                return transactionDate;
            }
            return null;
        }

        private static TransactionStatus? ConvertTransactionStatus(string transactionStatus)
        {
            switch (transactionStatus)
            {
                case "Approved":
                    return TransactionStatus.Approved;
                case "Failed":
                    return TransactionStatus.Failed;
                case "Finished":
                    return TransactionStatus.Finished;
                default:
                    return null;
            }
        }
    }
}