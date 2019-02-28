using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MicroTcp.BLL.Models
{
    public class ClientModel
    {
        public int ClientId { get; set; }
        public int Port { get; set; }
        public TcpClient TcpClient { get; set; }
    }
}
