#nullable enable

using System;
using System.Globalization;

namespace _2c2p_test.Model.Factory
{
    public static class XMLTransactionModelFactory
    {
        public static TransactionModel? Create(
            string? transactionID,
            string? amountString,
            string? currencyCode,
            string? transactionDateString,
            string? transactionStatus)
        {
            if (string.IsNullOrEmpty(transactionID))
            {
                throw new ArgumentException($"'{nameof(transactionID)}' cannot be null or empty.", nameof(transactionID));
            }

            if (string.IsNullOrEmpty(currencyCode))
            {
                throw new ArgumentException($"'{nameof(currencyCode)}' cannot be null or empty.", nameof(currencyCode));
            }

            if (string.IsNullOrEmpty(transactionDateString))
            {
                throw new ArgumentException($"'{nameof(transactionDateString)}' cannot be null or empty.", nameof(transactionDateString));
            }

            var isAmountParseSuccess = Decimal.TryParse(amountString, out var amount);
            var status = ConvertTransactionStatus(transactionStatus);
            var transactionDate = ConvertTransactionDate(transactionDateString);

            if (TransactionModel.ValidateTransactionID(transactionID) &&
                TransactionModel.ValidateCurrencyCode(currencyCode) &&
                transactionDate.HasValue &&
                status.HasValue &&
                isAmountParseSuccess
            )
            {
                return new TransactionModel(
                    transactionID,
                    amount,
                    currencyCode,
                    transactionDate.Value,
                    status.Value);
            }

            return null;
        }

        private static DateTime? ConvertTransactionDate(string transactionDateString)
        {
            var format = "yyyy-MM-ddTHH:mm:ss";

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
                case "Rejected":
                    return TransactionModel.TransactionStatus.Failed;
                case "Done":
                    return TransactionModel.TransactionStatus.Finished;
                default:
                    return null;
            }
        }
    }
}