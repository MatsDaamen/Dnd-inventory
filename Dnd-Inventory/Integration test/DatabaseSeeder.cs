using Dnd_Inventory_DAL;
using Dnd_Inventory_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration_test
{
    public static class DatabaseSeeder
    {
        private static SessionDbContext _db;

        public static void Init(SessionDbContext db) 
        {
            _db = db;
        }

        public static void Seed()
        {
            _db.Database.EnsureDeleted();
            _db.Database.EnsureCreated();

            _db.Sessions.AddRange(
                    new Session { Id = 1, Name="Arcus's session", CreatedBy= "6565e4921336ad0c552c2ebb" },
                    new Session { Id = 2, Name="Kalrez's session", CreatedBy= "6565e4921336ad0c552c2ebb" }
                );

            _db.SessionUsers.AddRange(
                    new SessionUsers { SessionId = 1, UserId = "6565e4921336ad0c552c2ebb" },
                    new SessionUsers { SessionId = 2, UserId = "6565e4921336ad0c552c2ebb" }
                );

            _db.SaveChanges();
        }
    }
}
