
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Mock
{
    public class SessionRepositoryMock : ISessionRepository
    {
        public List<SessionModel> Sessions { get; set; }

        public List<SessionJoinKeyModel> JoinKeys { get; set; }

        public int CreateSession(SessionModel session)
        {
            Sessions.Add(session);

            return session.Id;
        }

        public Guid CreateSessionJoinKey(SessionJoinKeyModel sessionJoinKey)
        {
            JoinKeys.Add(sessionJoinKey);

            return sessionJoinKey.JoinKey;
        }

        public void DeleteSession(int sessionId)
        {
            Sessions.Remove(Sessions.First(session => session.Id == sessionId));
        }

        public void DeleteSessionJoinKey(Guid sessionJoinKey)
        {
            JoinKeys.Remove(JoinKeys.First(joinkey => joinkey.JoinKey == sessionJoinKey));
        }

        public SessionModel Get(int sessionId)
        {
            return Sessions.First(session => session.Id == sessionId);
        }

        public List<SessionModel> GetAll()
        {
            return Sessions;
        }

        public List<SessionModel> GetAll(int userId)
        {
            return Sessions;
        }

        public List<SessionJoinKeyModel> GetAllJoinKeys(int sessionId)
        {
            return JoinKeys;
        }

        public void JoinSession(int sessionId, int userId)
        {
            
        }

        public void UpdateJoinKey(SessionJoinKeyModel sessionJoinKey)
        {

        }

        public SessionJoinKeyModel ValidateJoinKey(Guid sessionJoinKey)
        {
            return JoinKeys.First(joinKey => joinKey.JoinKey == sessionJoinKey);
        }
    }
}
