namespace MicroTcp.DAL.Migrations
{
    using Entities;
    using Entities.Enums;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MicroTcp.DAL.ApplicationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(MicroTcp.DAL.ApplicationContext context)
        {
            var isClients = context.Clients.Any();
            if (!isClients)
            {
                for(int i = 1; i < 5; i++)
                {
                    var newClient = new Client { Id = i, NickName = i.ToString(), Password = i.ToString() };
                    context.Clients.Add(newClient);
                }
                context.SaveChanges();
            }
            var isConversations = context.Conversations.Any();
            if (!isConversations)
            {
                for (int i = 1; i < 5; i++)
                {
                    var newConversation = new Conversation { Id = i, Name = i.ToString(), StartDateTime = DateTime.Now };
                    context.Conversations.Add(newConversation);
                }
                context.SaveChanges();
            }
            var isConversationClients = context.ConversationClients.Any();
            if (!isConversationClients)
            {
                var id = 1;
                for (int i = 1; i < 5; i++)
                {
                    var client = context.Clients.FirstOrDefault(x => x.Id == i);
                    for (int k = 1; k < 5; k++)
                    {
                        var conversation = context.Conversations.FirstOrDefault(x => x.Id == i);
                        var newConversationClient = new ConversationClient
                        {
                            Id = id,
                            AddToConversation = DateTime.Now,
                            Client = client,
                            Conversation = conversation,
                            DeleteToConversation = DateTime.Now
                        };
                        context.ConversationClients.Add(newConversationClient);
                        ++id;
                    }
                }
                context.SaveChanges();
            }
            var isMessages = context.Messages.Any();
            if (!isMessages)
            {
                var id = 1;
                for (int i = 1; i < 5; i++)
                {
                    var client = context.Clients.FirstOrDefault(x => x.Id == i);
                    for (int k = 1; k < 5; k++)
                    {
                        var conversation = context.Conversations.FirstOrDefault(x => x.Id == i);
                        var newMessage = new Message
                        {
                            Id = id,
                            Client = client,
                            Conversation = conversation,
                            MessageType = (int)MessageType.ToAnotherClient,
                            PostingDateTime = DateTime.Now,
                            Text = $"ClientId: {i} ConversationId {k}"
                        };
                        context.Messages.Add(newMessage);
                        ++id;
                    }
                }
                context.SaveChanges();
            }
        }
    }
}
