﻿using MicroTcp.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroTcp.BLL.Models
{
    public class MessagesModels
    {
        public int Id { get; set; }
        public MessagesModels MessageModel { get; set; }
    }
}
