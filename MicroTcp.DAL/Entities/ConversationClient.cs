using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroTcp.DAL.Entities
{
    public class ConversationClient
    {
        [Key]
        public int Id { get; set; }
        public DateTime? AddToConversation { get; set; }
        public DateTime? DeleteToConversation { get; set; }
        public virtual Client Client { get; set; }
        public virtual Conversation Conversation { get; set; }
    }
}
