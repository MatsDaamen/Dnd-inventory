using System;
using System.Collections.Generic;
using System.Linq;
using Dnd_Inventory_API;
using Dnd_Inventory_Logic.Entities;
using Dnd_Inventory_Logic.Interfaces.Repositories;

namespace Dnd_Inventory_DAL.Repositiories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly InventoryDbContext _db;

        public SessionRepository(InventoryDbContext db)
        {
            _db = db;
        }

        public Session Get(int sessionId)
        {
            return _db.Sessions.First(session => session.Id == sessionId);
        }

        public List<Session> GetAll()
        {
            return _db.Sessions.ToList();
        }

        public void CreateSession(Session session)
        {
            _db.Add(session);
            _db.SaveChanges();
        }

        public Guid CreateSessionJoinKey(SessionJoinKey sessionJoinKey)
        {
            _db.Add(sessionJoinKey);
            _db.SaveChanges();

            return sessionJoinKey.JoinKey;
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

        public void DeleteSessionJoinKey(Guid sessionJoinKey)
        {
            SessionJoinKey _sessionJoinKey = _db.JoinKeys.First(joinKey => joinKey.JoinKey == sessionJoinKey);

            if (_sessionJoinKey != null)
            {
                _db.Remove(_sessionJoinKey);
                _db.SaveChanges();
            }
        }

        public bool ValidateJoinKey(int sessionId, Guid sessionJoinKey)
        {
            SessionJoinKey? joinKey = _db.JoinKeys.FirstOrDefault(joinkey => joinkey.JoinKey == sessionJoinKey && joinkey.Session.Id == sessionId);

            return joinKey != null && joinKey.UsesLeft > 0;
        }

        public void JoinSession(int sessionId, int userId)
        {
            Session? session = _db.Sessions.FirstOrDefault(item => item.Id == sessionId);

            if (session == null)
                throw new Exception("session doesn't exists");

            SessionUsers sessionUsers = new SessionUsers
            {
                Session = session,
                UserId = userId
            };

            _db.Add(sessionUsers);
            _db.SaveChanges();
        }


    }
}
