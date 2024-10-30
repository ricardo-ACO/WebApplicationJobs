using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplicationJobs.Data;
using WebApplicationJobs.Models;

namespace WebApplicationJobs.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly UserRepository _userRepository;
        private readonly WorkRepository _workRepository;

        public CreateModel(UserRepository userRepository, WorkRepository workRepository)
        {
            _userRepository = userRepository;
            _workRepository = workRepository;
        }

        [BindProperty]
        public User User { get; set; }

        [BindProperty]
        public List<int> SelectedWorkIds { get; set; } = new List<int>();

        public List<Work> Works { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            
            Works = await _workRepository.GetWorksAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                
                await _userRepository.AddUserAsync(User);

                
                foreach (var workId in SelectedWorkIds)
                {
                    await _userRepository.AddUserWorkAsync(User.Id, workId);
                }

                return RedirectToPage("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Erro ao gravar: {ex.Message}");
                Works = await _workRepository.GetWorksAsync();
                return Page();
            }
        }
    }
}
