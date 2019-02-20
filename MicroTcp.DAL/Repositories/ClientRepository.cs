using MicroTcp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroTcp.DAL.Repositories
{
    public class ClientRepository
    {
        private ApplicationContext _context;
        public ClientRepository()
        {
            _context = new ApplicationContext();

        }

        public bool CheckIn()
        {
            try
            {
                var newClient = new Client {
                    NickName = "First",
                    Password = "First"
                };
                _context.Clients.Add(newClient);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }


        }




    }
}
