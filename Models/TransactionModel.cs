#nullable enable

using System;
using _2c2p_test.Common;

namespace _2c2p_test.Model
{
    public record TransactionModel
    {
        public TransactionModel(
            string transactionID,
            decimal amount,
            string currencyCode,
            DateTime transactionDate,
            TransactionStatus transactionStatus) =>
            (TransactionID, Amount, CurrencyCode, TransactionDate, Status)
            = (transactionID, amount, currencyCode, transactionDate, transactionStatus);

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

        public static bool ValidateTransactionID(string? transactionID) =>
            !string.IsNullOrEmpty(transactionID) && transactionID.Length <= 50;

        public static bool ValidateCurrencyCode(string? currencyCode) =>
            currencyCode is not null && ISO4217CurrencySymbol.Validate(currencyCode);

        public override string? ToString()
        {
            return System.Text.Json.JsonSerializer.Serialize(this);
        }
    }
}