using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnd_Inventory_Logic.Interfaces.Services
{
    public interface ISessionService
    {
        public void CreateSession(string name, int createdBy);

        public Guid CreateSessionJoinKey(int sessionId, int AmountOfUses, int createdBy);

        public void JoinSession(int sessionId, Guid sessionJoinKey, int userId);

        public void DeleteSession(int sessionId);

        public void DeleteSessionJoinKey(Guid sessionJoinKey);
    }
}
