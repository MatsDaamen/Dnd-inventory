using Dnd_Inventory_Logic.DomainModels;
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

        public SessionModel Get(int sessionId);

        public void CreateSession(SessionModel session);

        public Guid CreateSessionJoinKey(SessionJoinKeyModel sessionJoinKey);

        public bool ValidateJoinKey(int sessionId, Guid sessionJoinKey);

        public void JoinSession(int sessionId, int userId);

        public void DeleteSession(int sessionId);

        public void DeleteSessionJoinKey(Guid sessionJoinKey);
    }
}
