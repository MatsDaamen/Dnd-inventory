﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Mock
{
    public class SessionRepositoryMock : ISessionRepository
    {
        public int CreateSession(SessionModel session)
        {
            return 1;
        }

        public Guid CreateSessionJoinKey(SessionJoinKeyModel sessionJoinKey)
        {
            return Guid.NewGuid();
        }

        public void DeleteSession(int sessionId)
        {
            
        }

        public void DeleteSessionJoinKey(Guid sessionJoinKey)
        {

        }

        public SessionModel Get(int sessionId)
        {
            return new SessionModel(1, "test", 1);
        }

        public List<SessionModel> GetAll()
        {
            Random rnd = new Random();

            List<SessionModel> sessions = new List<SessionModel>();

            for (int i = 0; i < 10; i++)
            {
                sessions.Add(new SessionModel(i, "test" + i.ToString(), rnd.Next(1, 10)));
            }

            return sessions;
        }

        public List<SessionModel> GetAll(int userId)
        {
            List<SessionModel> sessions = new List<SessionModel>();

            for (int i = 0; i < 10; i++)
            {
                sessions.Add(new SessionModel(i, "test" + i.ToString(), 1));
            }

            return sessions;
        }

        public void JoinSession(int sessionId, int userId)
        {

        }

        public int ValidateJoinKey(Guid sessionJoinKey)
        {
            return 1;
        }
    }
}