using Dnd_Inventory_DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dnd_Inventory_DAL
{
    public class SessionDbContext : DbContext
    {
        public SessionDbContext(DbContextOptions<SessionDbContext> options) : base(options)
        {
            
        }

        public DbSet<Session> Sessions { get; set; }

        public DbSet<SessionJoinKey> JoinKeys { get; set; }

        public DbSet<SessionUsers> SessionUsers { get; set; }

        // we override the OnModelCreating method here.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SessionUsers>().HasKey(su => new { su.SessionId, su.UserId });
        }
    }
}
