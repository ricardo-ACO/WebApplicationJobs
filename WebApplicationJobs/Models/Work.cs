namespace WebApplicationJobs.Models
{
    public class Work
    {
        public int Id { get; set; }
        public string Title { get; set; }

        // Propriedade de navegação para o relacionamento muitos-para-muitos
        public ICollection<User_Work> UserWorks { get; set; }

    }
}
