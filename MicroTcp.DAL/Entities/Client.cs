using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MicroTcp.DAL.Entities
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        public string NickName { get; set; }
        public string Password { get; set; }
    }
}
