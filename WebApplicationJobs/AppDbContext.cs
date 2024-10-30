using Microsoft.EntityFrameworkCore;
using WebApplicationJobs.Models;

//Entity Framework - first approach
namespace WebApplicationJobs
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Work> Works { get; set; }
        public DbSet<User_Work> User_Works { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<User_Work>()
                .ToTable("user_work");

            modelBuilder.Entity<User_Work>()
                .HasKey(uw => new { uw.UserId, uw.WorkId });



            modelBuilder.Entity<User>()
                .HasMany(u => u.UserWorks)
                .WithOne(uw => uw.User)
                .HasForeignKey(uw => uw.UserId);

            modelBuilder.Entity<Work>()
                .HasMany(w => w.UserWorks)
                .WithOne(uw => uw.Work)
                .HasForeignKey(uw => uw.WorkId);
        }
    }
}
