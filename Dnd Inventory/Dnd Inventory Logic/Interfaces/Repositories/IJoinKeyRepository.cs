using Dnd_Inventory_Logic.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnd_Inventory_Logic.Interfaces.Repositories
{
    public interface IJoinKeyRepository
    {
        public List<SessionJoinKeyModel> GetAllJoinKeys(int sessionId);

        public Guid CreateSessionJoinKey(SessionJoinKeyModel sessionJoinKey);

        public void UpdateJoinKey(SessionJoinKeyModel sessionJoinKey);

        public SessionJoinKeyModel ValidateJoinKey(Guid sessionJoinKey);

        public void DeleteSessionJoinKey(Guid sessionJoinKey);

    }
}
