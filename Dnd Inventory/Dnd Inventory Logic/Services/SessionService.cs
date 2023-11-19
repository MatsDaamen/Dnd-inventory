﻿using Dnd_Inventory_Logic.DomainModels;
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

        public List<SessionModel> Get(int? userId)
        {
            List<SessionModel> sessions = new();

            if (userId != null)
                sessions = _sessionRepository.GetAll((int)userId);
            else
                sessions = _sessionRepository.GetAll();

            return sessions;
        }

        public void Create(SessionModel session)
        {
            int createdSessionId = _sessionRepository.CreateSession(session);

            _sessionRepository.JoinSession(createdSessionId, session.CreatedBy);
        }

        public Guid CreateJoinKey(SessionJoinKeyModel sessionJoinKey, int createdBy)
        {
            SessionModel session = Get(sessionJoinKey.SessionId);

            if (session.CreatedBy != createdBy)
                throw new Exception("not owner of session");

            sessionJoinKey.JoinKey = Guid.NewGuid();

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
            SessionJoinKeyModel joinKeyModel = _sessionRepository.ValidateJoinKey(joinRequest.sessionJoinKey);

            _sessionRepository.JoinSession(joinKeyModel.SessionId, joinRequest.userId);

            if (joinKeyModel.UsesLeft <= 1)
                _sessionRepository.DeleteSessionJoinKey(joinKeyModel.JoinKey);
            else
                _sessionRepository.UpdateJoinKey(joinKeyModel);
        }
    }
}
