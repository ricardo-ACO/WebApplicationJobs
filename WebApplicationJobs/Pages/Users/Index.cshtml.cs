using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplicationJobs.Data;
using WebApplicationJobs.Models;

namespace WebApplicationJobs.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly UserRepository _userRepository;

        public IndexModel(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<User> Users { get; set; }

        public async Task OnGetAsync()
        {
            Users = await _userRepository.GetUsersAsync();
        }
    }
}
