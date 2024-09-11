using Microsoft.Data.Sqlite;

using System.Collections.Generic;
using System.Linq;

using umbraco.Models;

namespace umbraco.Services
{
    public class SQLiteService
    {
        private readonly string _connectionString;

        public SQLiteService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<EmailLog> GetData()
        {
            var data = new List<EmailLog>();
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM YourTable";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        data.Add(new EmailLog
                        {
                            EmailId = reader.GetInt32(0),
                            SenderEmail = reader.GetString(1),
                            SentDate = reader.GetDateTime(2),
                         });
                    }
                }
            }
            return data;
        }
    }
}
