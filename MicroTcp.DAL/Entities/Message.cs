using MicroTcp.DAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroTcp.DAL.Entities
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public int FromPort { get; set; }
        public int ToPort { get; set; }
        public virtual Client Client { get; set; }
        public virtual Conversation Conversation { get; set; }
        public int MessageType { get; set; }
        public DateTime? PostingDateTime { get; set; }
        
    }
}
