using Microsoft.AspNetCore.Mvc;
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

        [BindProperty]
        public Work Work { get; set; } = new Work();

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
           

            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var query = "INSERT INTO Works (Title) VALUES (@Title)";
                using var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Title", Work.Title);

                try
                {
                    await command.ExecuteNonQueryAsync();
                    return RedirectToPage("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Erro ao gravar: {ex.Message}");
                    return Page();
                }
            }
        }
    }
}
