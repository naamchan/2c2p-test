#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2c2p_test.Exceptions;
using _2c2p_test.Model;
using _2c2p_test.Services;
using MySql.Data.MySqlClient;

namespace _2c2p_test.Repository
{
    public class TransactionRepository : RepositoryBase<TransactionModel>
    {
        private readonly IEnumerable<TransactionModel>? models;

        public TransactionRepository(IServiceProvider serviceProvider, IEnumerable<TransactionModel>? models = null) : base(serviceProvider)
        {
            this.models = models;
        }

        public override async Task<bool> Save()
        {
            if (models is null || !models.Any())
            {
                return false;
            }

            var mysqlService = serviceProvider.GetService(typeof(MySQLService)) as MySQLService;
            if (mysqlService is null)
            {
                throw new CannotFindServiceException("MySQL");
            }
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
            var c = await command.ExecuteNonQueryAsync();
            System.Console.WriteLine(c);

            return true;
        }

        private void Append(StringBuilder queryText, TransactionModel model)
        {
            queryText.Append("(")
                .Append("'").Append(model.TransactionID).Append("',")
                .Append("'").Append(model.Amount.ToString()).Append("',")
                .Append("'").Append(model.CurrencyCode).Append("',")
                .Append("'").Append(model.TransactionDate.ToString("o")).Append("',")
                .Append("'").Append(((int)model.Status)).Append("'")
                .Append(")");
        }
    }
}