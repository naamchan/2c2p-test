using MySql.Data.MySqlClient;

namespace _2c2p_test.Services
{
    public class MySQLService
    {
        private readonly string connectionString;

        public MySQLService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}