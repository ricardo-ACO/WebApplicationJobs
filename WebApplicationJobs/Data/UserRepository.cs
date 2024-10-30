using System.Data.SqlClient;
using WebApplicationJobs.Models;

namespace WebApplicationJobs.Data
{
    public class UserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            var users = new List<User>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = new SqlCommand("SELECT u.Id, u.Name, u.Email, w.Id as WorkId, w.Title FROM Users u INNER JOIN User_Work uw ON u.Id = uw.UserId INNER JOIN Works w ON w.Id = uw.WorkId", connection);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var userId = reader.GetInt32(0);
                        var user = users.FirstOrDefault(u => u.Id == userId);

                        if (user == null)
                        {
                            user = new User
                            {
                                Id = userId,
                                Name = reader.GetString(1),
                                Email = reader.GetString(2),
                                UserWorks = new List<User_Work>()
                            };
                            users.Add(user);
                        }

                        if (!reader.IsDBNull(3))
                        {
                            user.UserWorks.Add(new User_Work
                            {
                                Work = new Work
                                {
                                    Id = reader.GetInt32(3),
                                    Title = reader.GetString(4)
                                }
                            });
                        }
                    }
                }
            }

            return users;
        }
        public async Task AddUserAsync(User user)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new SqlCommand("INSERT INTO Users (Name, Email) VALUES (@Name, @Email); SELECT CAST(scope_identity() AS int);", connection);
            command.Parameters.AddWithValue("@Name", user.Name);
            command.Parameters.AddWithValue("@Email", user.Email);

            user.Id = (int)await command.ExecuteScalarAsync(); // Pega o Id do usuário recém-inserido
        }

        public async Task AddUserWorkAsync(int userId, int workId)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new SqlCommand("INSERT INTO User_Work (UserId, WorkId) VALUES (@UserId, @WorkId)", connection);
            command.Parameters.AddWithValue("@UserId", userId);
            command.Parameters.AddWithValue("@WorkId", workId);

            await command.ExecuteNonQueryAsync();
        }
    }
}
