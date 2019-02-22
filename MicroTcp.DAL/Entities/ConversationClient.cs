﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroTcp.DAL.Entities
{
    public class ConversationClient
    {
        public int Id { get; set; }
        public DateTime AddToConversation { get; set; }
        public DateTime DeleteToConversation { get; set; }
        public Client Client { get; set; }
    }
}