using MicroTcp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroTcp.BLL.Models
{
    public class ValidatePortModel
    {
        public bool IsValid { get; set; }
        public Client Client { get; set; }
    }
}
