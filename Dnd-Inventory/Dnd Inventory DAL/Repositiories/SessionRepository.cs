using Dnd_Inventory_DAL.Entities;
using Dnd_Inventory_Logic.DomainModels;
using Dnd_Inventory_Logic.Exceptions;
using Dnd_Inventory_Logic.Interfaces.Repositories;

namespace Dnd_Inventory_DAL.Repositiories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly SessionDbContext _db;

        public SessionRepository(SessionDbContext db)
        {
            _db = db;
        }

        public SessionModel Get(int sessionId)
        {
            Session dbSession = _db.Sessions.First(session => session.Id == sessionId);

            SessionModel sessionModel = new SessionModel( dbSession.Id, dbSession.Name, dbSession.CreatedBy);

            return sessionModel;
        }

        public List<SessionModel> GetAll()
        {
            List<Session> sessions = _db.Sessions.ToList();

            List<SessionModel> sessionModels = sessions.Select(dbSession => new SessionModel(dbSession.Id, dbSession.Name, dbSession.CreatedBy)).ToList();

            return sessionModels;
        }

        public List<SessionModel> GetAll(string userId)
        {
            List<int> sessionIds = _db.SessionUsers.Where(SessionUser => SessionUser.UserId == userId)
                .Select(sessionUser => sessionUser.SessionId)
                .ToList();

            List<Session> sessions = _db.Sessions.Where(session => sessionIds.Contains(session.Id)).ToList();


            List<SessionModel> sessionModels = sessions.Select(dbSession => new SessionModel(dbSession.Id, dbSession.Name, dbSession.CreatedBy)).ToList();

            return sessionModels;
        }

        public int CreateSession(SessionModel sessionModel)
        {
            Session session = new Session
            {
                Name = sessionModel.Name,
                CreatedBy = sessionModel.CreatedBy,
            };

            _db.Add(session);
            _db.SaveChanges();

            return session.Id;
        }

        public void DeleteSession(int sessionId)
        {
            Session _session = _db.Sessions.First(session => session.Id == sessionId);

            if (_session != null)
            {
                _db.Remove(_session);
                _db.SaveChanges();
            }
        }

        public void JoinSession(int sessionId, string userId)
        {
            Session? session = _db.Sessions.FirstOrDefault(item => item.Id == sessionId);

            if (session == null)
                throw new SessionJoinException("session doesn't exists");

            SessionUsers sessionUsers = new SessionUsers
            {
                SessionId = session.Id,
                UserId = userId
            };

            _db.Add(sessionUsers);
            _db.SaveChanges();
        }
    }
}
