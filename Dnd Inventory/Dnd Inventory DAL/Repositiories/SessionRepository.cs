using System;
using System.Collections.Generic;
using System.Linq;
using Dnd_Inventory_DAL.Entities;
using Dnd_Inventory_Logic.DomainModels;
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

        public List<SessionModel> GetAll(int userId)
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

        public Guid CreateSessionJoinKey(SessionJoinKeyModel sessionJoinKeyModel)
        {
            SessionJoinKey sessionJoinKey = new SessionJoinKey
            {
                JoinKey = sessionJoinKeyModel.JoinKey,
                UsesLeft = sessionJoinKeyModel.UsesLeft,
                SessionId = sessionJoinKeyModel.SessionId
            };

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

        public SessionJoinKeyModel ValidateJoinKey(Guid sessionJoinKey)
        {
            SessionJoinKey? joinKey = _db.JoinKeys.FirstOrDefault(joinkey => joinkey.JoinKey == sessionJoinKey);

            if (joinKey.UsesLeft <= 0)
                throw new Exception("joinKey not valid");

            SessionJoinKeyModel joinKeyModel = new SessionJoinKeyModel()
            {
                Id = joinKey.Id,
                SessionId = joinKey.SessionId,
                UsesLeft = joinKey.UsesLeft,
                JoinKey = joinKey.JoinKey
            };

            return joinKeyModel;
        }

        public void JoinSession(int sessionId, int userId)
        {
            Session? session = _db.Sessions.FirstOrDefault(item => item.Id == sessionId);

            if (session == null)
                throw new Exception("session doesn't exists");

            SessionUsers sessionUsers = new SessionUsers
            {
                SessionId = session.Id,
                UserId = userId
            };

            _db.Add(sessionUsers);
            _db.SaveChanges();
        }

        public void UpdateJoinKey(SessionJoinKeyModel sessionJoinKey)
        {
            SessionJoinKey joinKey = _db.JoinKeys.First(joinKey => joinKey.Id == sessionJoinKey.Id);

            joinKey.UsesLeft--;

            _db.SaveChanges();
        }
    }
}
