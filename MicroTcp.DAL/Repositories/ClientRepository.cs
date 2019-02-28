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
                var newClient = new Client
                {
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

        public Client SignIn(string nickName, string password)
        {
            try
            {
                var client = _context.Clients
                     .Where(x => x.NickName == nickName && x.Password == password).FirstOrDefault();
                if (client != null)
                {
                    return client;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //public IQueryable<UserConnection> GetUserConnections(int id)
        //{
        //    var client = _context.UserConnections
        //             .Where(x => x.Owner.Id == id);
        //    return client;
        //}

        public IQueryable<Conversation> GetConversationsByClientId(int id)
        {
            var conversation = _context.ConversationClients
                     .Where(x => x.Client.Id == id).Select(x => x.Conversation);
            return conversation;
        }

        public IQueryable<Message> GetMassagesByConversationId(int id)
        {
            var messages = _context.Messages
                     .Where(x => x.Client.Id == id);
            return messages;
        }

        public Client GetClientById(int id)
        {
            var client = _context.Clients
                     .Where(x => x.Id == id).FirstOrDefault();
            return client;
        }


        public Conversation GetConversationById(int id)
        {
            var conversation = _context.Conversations
                     .Where(x => x.Id == id).FirstOrDefault();
            return conversation;
        }

        public int SaveMessage(Message entityMessage)
        {
            var client = GetClientById(entityMessage?.Client?.Id ?? 0);
            entityMessage.Client = client;
            var conversation = GetConversationById(entityMessage?.Client?.Id ?? 0);
            entityMessage.Conversation = conversation;
            _context.Messages.Add(entityMessage); 
            _context.SaveChanges();
            return entityMessage.Id;
        }

        public IQueryable<Client> GetClientRecipientsByConversationId(int conversationId, int clientId)
        {
            var clients = GetClientsByConversationId(conversationId).
                    Where(x => x.Id != clientId);
            return clients;
        }

        public IQueryable<Client> GetClientsByConversationId(int conversationId)
        {
            var clients = _context.ConversationClients
         .Where(x => x.Conversation.Id == conversationId).Select(x => x.Client);
            return clients;
        }
    }
}
