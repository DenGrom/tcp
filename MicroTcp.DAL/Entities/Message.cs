using MicroTcp.DAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroTcp.DAL.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int FromPort { get; set; }
        public int ToPort { get; set; }
        public virtual Client Client { get; set; }
        public virtual Conversation Conversation { get; set; }
        public MessageType MessageType { get; set; }
        public DateTime PostingDateTime { get; set; }
        
    }
}
