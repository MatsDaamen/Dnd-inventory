using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnd_Inventory_Logic.Exceptions
{
    public class SessionJoinException : Exception
    {
        public SessionJoinException(string message) : base(message) 
        {
        
        }
    }
}
