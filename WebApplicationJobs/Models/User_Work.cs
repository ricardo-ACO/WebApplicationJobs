namespace WebApplicationJobs.Models
{
    public class User_Work
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int WorkId { get; set; }
        public Work Work { get; set; }

    }
}
