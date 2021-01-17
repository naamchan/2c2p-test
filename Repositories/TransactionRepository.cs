#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2c2p_test.Model;
using MySql.Data.MySqlClient;

namespace _2c2p_test.Repository
{
    public class TransactionRepository : RepositoryBase<TransactionModel>
    {
        public TransactionRepository(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public override async Task<bool> Save(IEnumerable<TransactionModel>? models)
        {
            if (models is null || !models.Any())
            {
                return false;
            }

            var mysqlService = GetMySQLService();
            using var connection = mysqlService.GetConnection();

            StringBuilder queryText = new("INSERT INTO `transactions` (`id`, `amount`, `currency_code`, `transaction_date`, `status`) VALUES ");

            bool wasPrepended = false;
            foreach (var model in models)
            {
                if (!wasPrepended)
                {
                    wasPrepended = true;
                }
                else
                {
                    queryText.Append(",");
                }
                Append(queryText, model);
            }
            queryText.Append(";");
            System.Console.WriteLine(queryText.ToString());

            await connection.OpenAsync();
            using var command = new MySqlCommand(queryText.ToString(), connection);
            await command.ExecuteNonQueryAsync();

            return true;
        }

        public async Task<IEnumerable<TransactionModel>> FetchByCurrencyCode(string currencyCode)
        {
            var mysqlService = GetMySQLService();
            using var connection = mysqlService.GetConnection();

            var queryText = "SELECT `id`, `amount`, `currency_code`, `transaction_date`, `status` FROM `transactions` where `currency_code`=?currencyCode";

            await connection.OpenAsync();
            using var command = new MySqlCommand(queryText.ToString(), connection);
            command.Parameters.Add(new MySqlParameter("?currencyCode", currencyCode));

            using var cursor = await command.ExecuteReaderAsync();
            List<TransactionModel> models = new();

            while (await cursor.ReadAsync())
            {
                models.Add(new TransactionModel(
                    transactionID: Convert.ToString(cursor["id"]) ?? "N/A",
                    amount: Convert.ToDecimal(cursor["amount"]),
                    currencyCode: Convert.ToString(cursor["currency_code"]) ?? "N/A",
                    transactionDate: Convert.ToDateTime(cursor["transaction_date"]),
                    transactionStatus: (TransactionModel.TransactionStatus)Convert.ToInt32(cursor["status"])
                ));
            }

            return models;
        }

        private void Append(StringBuilder queryText, TransactionModel model)
        {
            queryText.Append("(")
                .Append("'").Append(MySqlHelper.EscapeString(model.TransactionID)).Append("',")
                .Append("'").Append(MySqlHelper.EscapeString(model.Amount.ToString())).Append("',")
                .Append("'").Append(MySqlHelper.EscapeString(model.CurrencyCode)).Append("',")
                .Append("'").Append(model.TransactionDate.ToString("o")).Append("',")
                .Append("'").Append(((int)model.Status)).Append("'")
                .Append(")");
        }


    }
}