using Dnd_Inventory_Logic.DomainModels;
using Dnd_Inventory_Logic.Exceptions;
using Dnd_Inventory_Logic.Interfaces.Repositories;
using Dnd_Inventory_Logic.Interfaces.Services;

namespace Dnd_Inventory_Logic.Services
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IJoinKeyRepository _joinKeyRepository;
        private readonly ISessionUsersRepository _sessionUsersRepository;

        public SessionService(ISessionRepository sessionRepository, IJoinKeyRepository joinKeyRepository, ISessionUsersRepository usersRepository) 
        {
            _sessionRepository = sessionRepository;
            _joinKeyRepository = joinKeyRepository;
            _sessionUsersRepository = usersRepository;
        }

        public SessionModel Get(int id)
        {
            SessionModel sessionModel = _sessionRepository.Get(id);

            sessionModel.SessionJoinKeys = _joinKeyRepository.GetAllJoinKeys(sessionModel.Id);

            sessionModel.SessionUsers = _sessionUsersRepository.GetAllBySessionId(sessionModel.Id);

            return sessionModel;
        }

        public List<SessionModel> Get(string? userId)
        {
            List<SessionModel> sessions = new();

            if (!string.IsNullOrEmpty(userId))
                sessions = _sessionRepository.GetAll(userId);
            else
                sessions = _sessionRepository.GetAll();

            return sessions;
        }

        public void Create(SessionModel session)
        {
            int createdSessionId = _sessionRepository.CreateSession(session);

            _sessionRepository.JoinSession(createdSessionId, session.CreatedBy);
        }

        public Guid CreateJoinKey(SessionJoinKeyModel sessionJoinKey, string createdBy)
        {
            SessionModel session = Get(sessionJoinKey.SessionId);

            if (session.CreatedBy != createdBy)
                throw new JoinKeyCreationExecption("not owner of session");

            sessionJoinKey.JoinKey = Guid.NewGuid();

            _joinKeyRepository.CreateSessionJoinKey(sessionJoinKey);

            return sessionJoinKey.JoinKey;
        }

        public void Delete(int sessionId)
        {
            _sessionRepository.DeleteSession(sessionId);
        }

        public void DeleteJoinKey(Guid sessionJoinKey)
        {
            _joinKeyRepository.DeleteSessionJoinKey(sessionJoinKey);
        }

        public void Join(JoinRequestModel joinRequest)
        {
            SessionJoinKeyModel joinKeyModel = _joinKeyRepository.ValidateJoinKey(joinRequest.sessionJoinKey);

            _sessionRepository.JoinSession(joinKeyModel.SessionId, joinRequest.userId);

            if (joinKeyModel.UsesLeft <= 1)
                _joinKeyRepository.DeleteSessionJoinKey(joinKeyModel.JoinKey);
            else
                _joinKeyRepository.UpdateJoinKey(joinKeyModel);
        }

        public List<SessionUserModels> GetSessionUsers(int sessionId)
        {
            return _sessionUsersRepository.GetAllBySessionId(sessionId);
        }

        public void DeleteSessionUser(int sessionId, string userId)
        {
            _sessionUsersRepository.DeleteSessionUser(sessionId, userId);
        }
    }
}
