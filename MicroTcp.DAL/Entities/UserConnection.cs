using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroTcp.DAL.Entities
{
    public class UserConnection
    {
        public int Id { get; set; }
        public virtual  Client Owner { get; set; }
        public virtual Client Participant { get; set; }

    }
}
