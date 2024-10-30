namespace WebApplicationJobs.Models
{
    public class Work
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public ICollection<User_Work> UserWorks { get; set; }

    }
}
