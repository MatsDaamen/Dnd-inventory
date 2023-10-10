using Dnd_Inventory_DAL.Entities;
using Dnd_Inventory_Logic.Interfaces.Repositories;
using Dnd_Inventory_Logic.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnd_Inventory_Logic.Services
{
    public class SessionService : ISessionService
    {
        private ISessionRepository _sessionRepository;
        public SessionService(ISessionRepository sessionRepository) 
        {
            _sessionRepository = sessionRepository;
        }

        public void CreateSession(string name, int createdBy)
        {
            Session session = new Session
            {
                Name = name,
                CreatedBy = createdBy,
            };

            _sessionRepository.CreateSession(session);
        }

        public Guid CreateSessionJoinKey(int sessionId, int AmountOfUses, int createdBy)
        {
            Session session = _sessionRepository.Get(sessionId);

            if (session.CreatedBy != createdBy)
                //TODO: THROW ERROR
                throw new Exception("key created by none-owner");

            SessionJoinKey sessionJoinKey = new SessionJoinKey
            {
                JoinKey = Guid.NewGuid(),
                UsesLeft = AmountOfUses,
                Session = session
            };

            _sessionRepository.CreateSessionJoinKey(sessionJoinKey);

            return sessionJoinKey.JoinKey;
        }

        public void DeleteSession(int sessionId)
        {
            _sessionRepository.DeleteSession(sessionId);
        }

        public void DeleteSessionJoinKey(Guid sessionJoinKey)
        {
            _sessionRepository.DeleteSessionJoinKey(sessionJoinKey);
        }

        public void JoinSession(int sessionId, Guid sessionJoinKey, int userId)
        {
            if (!_sessionRepository.ValidateJoinKey(sessionId, sessionJoinKey))
                throw new Exception("join code not valide");
        }
    }
}
