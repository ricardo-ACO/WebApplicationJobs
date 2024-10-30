using System.Data.SqlClient;
using WebApplicationJobs.Models;

namespace WebApplicationJobs.Data
{
    public class WorkRepository
    {
        private readonly string _connectionString;

        public WorkRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Work>> GetWorksAsync()
        {
            var works = new List<Work>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT Id, Title FROM Works", connection);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        works.Add(new Work
                        {
                            Id = reader.GetInt32(0),
                            Title = reader.GetString(1)
                        });
                    }
                }
            }
            return works;
        }
    }
}
