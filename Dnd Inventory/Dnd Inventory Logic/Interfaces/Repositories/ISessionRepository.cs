using Dnd_Inventory_Logic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnd_Inventory_Logic.Interfaces.Repositories
{
    public interface ISessionRepository
    {
        public List<Session> GetAll();

        public Session Get(int sessionId);

        public void CreateSession(Session session);

        public Guid CreateSessionJoinKey(SessionJoinKey sessionJoinKey);

        public bool ValidateJoinKey(int sessionId, Guid sessionJoinKey);

        public void JoinSession(int sessionId, int userId);

        public void DeleteSession(int sessionId);

        public void DeleteSessionJoinKey(Guid sessionJoinKey);
    }
}
