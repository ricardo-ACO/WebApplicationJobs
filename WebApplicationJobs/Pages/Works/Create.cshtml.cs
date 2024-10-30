using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using WebApplicationJobs.Models;

namespace WebApplicationJobs.Pages.Works
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Work> Works { get; set; } = new List<Work>();

        public async Task OnGetAsync()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var command = new SqlCommand("SELECT Id, Title FROM Works", connection);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Works.Add(new Work
                        {
                            Id = reader.GetInt32(0),
                            Title = reader.GetString(1)
                        });
                    }
                }
            }
        }
    }
}
