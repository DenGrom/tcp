using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroTcp.BLL.Models
{
    public class MessageEventArgsModel : EventArgs
    {
        public MessageEventArgsModel(string text, int conversationId, int clientId, int id)
        {
            Id = id;
            Text = text;
            ConversationId = conversationId;
            ClientId = clientId;
        }
        public int Id { get; set; }
        public string Text { get; set; }
        public int ConversationId { get; set; }
        public int ClientId { get; set; }
    }
}
