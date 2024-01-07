using Dnd_Inventory_DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

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

        public DbSet<Item> items { get; set; }

        public DbSet<Inventory> inventories { get; set; }

        // we override the OnModelCreating method here.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SessionUsers>().HasKey(x => new { x.SessionId, x.UserId });

            modelBuilder.Entity<SessionUsers>()
                .HasMany(x => x.Items)
                .WithMany(x => x.users)
                .UsingEntity<Inventory>(
                l => l.HasOne<Item>().WithMany().HasForeignKey(e => e.ItemId),
                r => r.HasOne<SessionUsers>().WithMany().HasForeignKey(e => new { e.SessionId, e.UserId })
                );
        }
    }
}
