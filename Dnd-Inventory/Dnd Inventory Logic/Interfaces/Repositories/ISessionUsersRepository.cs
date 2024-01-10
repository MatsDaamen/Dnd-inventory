using Dnd_Inventory_Logic.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnd_Inventory_Logic.Interfaces.Repositories
{
    public interface ISessionUsersRepository
    {
        public List<SessionUserModels> GetAllBySessionId(int sessionId);
        public void DeleteSessionUser(int sessionId, string userId);
    }
}
