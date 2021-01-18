#nullable enable

using System;
using System.Globalization;

namespace _2c2p_test.Model.Factory
{
    public static class CSVTransactionModelFactory
    {
        public static TransactionModel? Create(string? transactionID, string? amountString, string? currencyCode, string? transactionDateString, string? transactionStatus)
        {
            if (string.IsNullOrEmpty(transactionID))
            {
                throw new ArgumentException($"'{nameof(transactionID)}' cannot be null or empty.", nameof(transactionID));
            }

            if (string.IsNullOrEmpty(currencyCode))
            {
                throw new ArgumentException($"'{nameof(currencyCode)}' cannot be null or empty.", nameof(currencyCode));
            }

            var status = ConvertTransactionStatus(transactionStatus);
            var transactionDate = ConvertTransactionDate(transactionDateString);
            var isAmountParseSuccess = Decimal.TryParse(amountString, out var amount);

            if (TransactionModel.ValidateTransactionID(transactionID)
                && TransactionModel.ValidateCurrencyCode(currencyCode)
                && transactionDate is not null
                && transactionDate.HasValue
                && status.HasValue
            )
            {
                return new TransactionModel(transactionID, amount, currencyCode, transactionDate.Value, status.Value);
            }

            return null;
        }

        private static DateTime? ConvertTransactionDate(string? transactionDateString)
        {
            if (transactionDateString is null)
            {
                return null;
            }

            var format = "dd/MM/yyyy hh:mm:ss";
            if (DateTime.TryParseExact(
                transactionDateString,
                format,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var transactionDate))
            {
                return transactionDate;
            }
            return null;
        }

        private static TransactionModel.TransactionStatus? ConvertTransactionStatus(string? transactionStatus)
        {
            switch (transactionStatus)
            {
                case "Approved":
                    return TransactionModel.TransactionStatus.Approved;
                case "Failed":
                    return TransactionModel.TransactionStatus.Failed;
                case "Finished":
                    return TransactionModel.TransactionStatus.Finished;
                default:
                    return null;
            }
        }
    }
}