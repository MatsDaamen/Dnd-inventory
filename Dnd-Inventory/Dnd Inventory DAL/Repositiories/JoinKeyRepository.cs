using Dnd_Inventory_DAL.Entities;
using Dnd_Inventory_Logic.DomainModels;
using Dnd_Inventory_Logic.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnd_Inventory_DAL.Repositiories
{
    public class JoinKeyRepository : IJoinKeyRepository
    {
        private readonly SessionDbContext _db;

        public JoinKeyRepository(SessionDbContext db)
        {
            _db = db;
        }

        public List<SessionJoinKeyModel> GetAllJoinKeys(int sessionId)
        {
            List<SessionJoinKey> joinKeys = _db.JoinKeys.Where(joinkey => joinkey.SessionId == sessionId).ToList();

            List<SessionJoinKeyModel> joinKeyModels = joinKeys.Select(joinKey => new SessionJoinKeyModel
            {
                Id = joinKey.Id,
                SessionId = joinKey.SessionId,
                UsesLeft = joinKey.UsesLeft,
                JoinKey = joinKey.JoinKey
            }).ToList();

            return joinKeyModels;
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
        public void UpdateJoinKey(SessionJoinKeyModel sessionJoinKey)
        {
            SessionJoinKey joinKey = _db.JoinKeys.First(joinKey => joinKey.Id == sessionJoinKey.Id);

            joinKey.UsesLeft--;

            _db.SaveChanges();
        }

        public SessionJoinKeyModel ValidateJoinKey(Guid sessionJoinKey)
        {
            SessionJoinKey? joinKey = _db.JoinKeys.FirstOrDefault(joinkey => joinkey.JoinKey == sessionJoinKey);

            if (joinKey is null || joinKey.UsesLeft <= 0)
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
        public void DeleteSessionJoinKey(Guid sessionJoinKey)
        {
            SessionJoinKey _sessionJoinKey = _db.JoinKeys.First(joinKey => joinKey.JoinKey == sessionJoinKey);

            if (_sessionJoinKey != null)
            {
                _db.Remove(_sessionJoinKey);
                _db.SaveChanges();
            }
        }
    }
}
