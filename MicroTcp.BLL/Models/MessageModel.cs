using MicroTcp.DAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroTcp.BLL.Models
{
    public class MessageModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int FromPort { get; set; }
        public int ToPort { get; set; }
        public ClientModel Client { get; set; }
        public ConversationModel Conversation { get; set; }
        public MessageType MessageType { get; set; }
        public DateTime PostingDateTime { get; set; }
    }
}
