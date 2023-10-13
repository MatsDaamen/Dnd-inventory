using Dnd_Inventory_Logic.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dnd_Inventory_API
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
        {
            
        }

        public DbSet<Session> Sessions { get; set; }

        public DbSet<SessionJoinKey> JoinKeys { get; set; }

        public DbSet<SessionUsers> SessionUsers { get; set; }
    }
}
