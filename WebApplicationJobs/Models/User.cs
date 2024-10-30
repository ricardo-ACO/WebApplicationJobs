namespace WebApplicationJobs.Models
{
    public class User
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        // Propriedade de navegação para o relacionamento muitos-para-muitos
        //public ICollection<User_Work> UserWorks { get; set; } = new List<User_Work>();

        public ICollection<User_Work> UserWorks { get; set; }

    }
}
