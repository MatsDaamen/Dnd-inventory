using Dnd_Inventory_Logic.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnd_Inventory_Logic.Interfaces.Services
{
    public interface ISessionService
    {
        public SessionModel Get(int id);
        public List<SessionModel> Get();

        public void Create(SessionModel session);

        public Guid CreateJoinKey(int sessionId, int AmountOfUses, int createdBy);

        public void Join(JoinRequestModel joinRequest);

        public void Delete(int sessionId);

        public void DeleteJoinKey(Guid sessionJoinKey);
    }
}
