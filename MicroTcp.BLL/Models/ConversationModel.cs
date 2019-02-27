using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroTcp.BLL.Models
{
    public class ConversationModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? StartDateTime { get; set; }
    }
}
