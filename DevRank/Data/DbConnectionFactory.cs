using System.Configuration;
using MySql.Data.MySqlClient;

namespace DevRank.Data
{
    public static class DbConnectionFactory
    {
        public static MySqlConnection Create()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DevRankDb"].ConnectionString;
            return new MySqlConnection(connectionString);
        }
    }
}
