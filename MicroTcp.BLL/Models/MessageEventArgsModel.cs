using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroTcp.BLL.Models
{
    public class MessageEventArgsModel : EventArgs
    {
        public MessageEventArgsModel(string text)
        {
            Text = text;
        }
        public string Text { get; set; }

    }
}
