﻿using Dnd_Inventory_Logic.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnd_Inventory_Logic.Interfaces.Repositories
{
    public interface ISessionRepository
    {
        public List<SessionModel> GetAll();
        public List<SessionModel> GetAll(string userId);

        public SessionModel Get(int sessionId);

        public int CreateSession(SessionModel session);

        public Guid CreateSessionJoinKey(SessionJoinKeyModel sessionJoinKey);

        public SessionJoinKeyModel ValidateJoinKey(Guid sessionJoinKey);

        public void JoinSession(int sessionId, string userId);

        public void DeleteSession(int sessionId);

        public void DeleteSessionJoinKey(Guid sessionJoinKey);

        public void UpdateJoinKey(SessionJoinKeyModel sessionJoinKey);

        public List<SessionJoinKeyModel> GetAllJoinKeys(int sessionId);
    }
}
