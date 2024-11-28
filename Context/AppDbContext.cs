using Microsoft.EntityFrameworkCore;
using Simple_Online_Survey_Application.Entity.Entities;

namespace Simple_Online_Survey_Application.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Poll> Polls { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Vote> Votes { get; set; }
    }
}
