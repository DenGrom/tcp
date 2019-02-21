using MicroTcp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroTcp.DAL
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("DefaultConnection")
        {
        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<ConversationClient> ConversationClients { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<UserConnection> UserConnections { get; set; }
    }
}
