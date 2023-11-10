using Dnd_Inventory_Logic.DomainModels;
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

        public SessionModel Get(int id)
        {
            return _sessionRepository.Get(id);
        }

        public List<SessionModel> Get()
        {
            return _sessionRepository.GetAll();
        }

        public void Create(SessionModel session)
        {
            int createdSessionId = _sessionRepository.CreateSession(session);

            _sessionRepository.JoinSession(createdSessionId, session.CreatedBy);
        }

        public Guid CreateJoinKey(int sessionId, int AmountOfUses, int createdBy)
        {
            SessionJoinKeyModel sessionJoinKey = new SessionJoinKeyModel
            {
                JoinKey = Guid.NewGuid(),
                UsesLeft = AmountOfUses,
                SessionId = sessionId
            };

            _sessionRepository.CreateSessionJoinKey(sessionJoinKey);

            return sessionJoinKey.JoinKey;
        }

        public void Delete(int sessionId)
        {
            _sessionRepository.DeleteSession(sessionId);
        }

        public void DeleteJoinKey(Guid sessionJoinKey)
        {
            _sessionRepository.DeleteSessionJoinKey(sessionJoinKey);
        }

        public void Join(JoinRequestModel joinRequest)
        {
            int sessionId = _sessionRepository.ValidateJoinKey(joinRequest.sessionJoinKey);

            if (sessionId == 0)
                throw new Exception("session not found");

            _sessionRepository.JoinSession(sessionId, joinRequest.userId);
        }
    }
}
