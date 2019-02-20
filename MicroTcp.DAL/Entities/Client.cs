using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MicroTcp.DAL.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string NickName { get; set; }
        public string Password { get; set; }
    }
}
