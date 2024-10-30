using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplicationJobs.Data;
using WebApplicationJobs.Models;

namespace WebApplicationJobs.Pages.Works
{
    public class IndexWorkModel : PageModel
    {
        private readonly WorkRepository _workRepository;

        public IndexWorkModel(WorkRepository workRepository)
        {
            _workRepository = workRepository;
        }

        public List<Work> Works { get; set; }

        public async Task OnGetAsync()
        {
            Works = await _workRepository.GetWorksAsync();
        }
    }
}
