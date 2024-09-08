using Events.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Events.DataAccess.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<LiveEvent> LiveEvents { get; set; }
        public DbSet<User> User { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
