using Dnd_Inventory_DAL.Entities;
using Dnd_Inventory_Logic.DomainModels;
using Dnd_Inventory_Logic.Interfaces.Repositories;

namespace Dnd_Inventory_DAL.Repositiories
{
    public class SessionUsersRepository : ISessionUsersRepository
    {
        private SessionDbContext _db;

        public SessionUsersRepository(SessionDbContext db)
        {
            _db = db;
        }

        public List<SessionUserModels> GetAllBySessionId(int sessionId)
        {
            List<SessionUsers> users = _db.SessionUsers.Where(sessionUser => sessionUser.SessionId == sessionId).ToList();

            List<SessionUserModels> userModels = users.Select(user => new SessionUserModels
            {
                SessionId = sessionId,
                UserId = user.UserId,
            }).ToList();

            return userModels;
        }

        public void DeleteSessionUser(int sessionId, string userId)
        {
            SessionUsers user = _db.SessionUsers.First(sessionuser => sessionuser.SessionId == sessionId && sessionuser.UserId == userId);
        
            _db.SessionUsers.Remove(user);
            _db.SaveChanges();
        }
    }
}
