﻿using MicroTcp.BLL;
using MicroTcp.BLL.Models;
using MicroTcp.DAL.Entities;
using MicroTcp.DAL.Entities.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MicroTcp
{
    public class Server
    {
        public static List<TcpListener> _tcpServers;
        public static List<ClientModel> _clients;
        public static int _startsPortNumber = 5555;
        public static int _emptyPortNumber;
        public static int _newClientId;
        private static BLL.Common _common;
        static void Main()
        {
            _tcpServers = new List<TcpListener>();
            _clients = new List<ClientModel>();
            _common = new Common();
            StartNewTcpServerTread();
        }
        private static void StartNewTcpServerTread()
        {
            Thread t = new Thread(StartNewTcpServer);
            t.Start();
        }

        private static void StartNewTcpServer()
        {
            SetEmptyPortNumber();
            var ipPoint = new IPEndPoint(IPAddress.Parse($"127.0.0.1"), _emptyPortNumber);
            var server = new TcpListener(ipPoint);
            server.Start();
            _tcpServers.Add(server);
            LoopClients(server);

        }

        public static void LoopClients(TcpListener server)
        {
            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                Thread t = new Thread(new ParameterizedThreadStart(HandleClient));
                t.Start(client);
            }
        }

        public static void HandleClient(object obj)
        {
            TcpClient tcpClient = (TcpClient)obj;
            StreamWriter sWriter = new StreamWriter(tcpClient.GetStream(), Encoding.ASCII);
            StreamReader sReader = new StreamReader(tcpClient.GetStream(), Encoding.ASCII);
            Boolean bClientConnected = true;
            String messageJson = null;
            IPEndPoint endPoint = (IPEndPoint)tcpClient.Client.LocalEndPoint;
            var idsClients = _clients.Select(x => x.Port);
            if (_clients.Count == 0 || !idsClients.Contains(endPoint.Port))
            {
                SetEmptyPortNumber();
                var client = new ClientModel
                {
                    ClientId = _newClientId,
                    Port = _emptyPortNumber,
                    TcpClient = tcpClient
                };
                _clients.Add(client);
                _newClientId = 0;
            }

            while (bClientConnected)
            {

                messageJson = sReader.ReadLine();
                if (string.IsNullOrWhiteSpace(messageJson) )
                {
                    continue;
                }
                var message = JsonConvert.DeserializeObject<MessageEventArgsModel>(messageJson);
                if (message == null)
                {
                    continue;
                }
                Console.WriteLine("From Client; " + messageJson);
                if (message.MessageType == MessageType.Authenticate)
                {
                    _newClientId = message.ClientId;
                    StartNewTcpServerTread();
                    SetEmptyPortNumber();
                    message.Text = _emptyPortNumber.ToString();
                    SentToClient(sWriter, message);
                }
                if (message.MessageType == MessageType.ToAnotherClient)
                {
                    var allClients = _common.GetClientsByConversationId(message.ConversationId).ToList();
                    foreach(var client in allClients)
                    {
                        var messageRecipient = _clients.FirstOrDefault(x => x.ClientId == client.Id);
                        if (messageRecipient == null || message.ConversationId == default(int))
                        {
                            continue;
                        }
                        var sWriterRecipient = new StreamWriter(messageRecipient.TcpClient.GetStream(), Encoding.ASCII);
                        SentToClient(sWriterRecipient, message);
                    }
                }
            }
        }

        private static void SetClientId(ClientModel x, MessageEventArgsModel message)
        {
            if(x.Port == _startsPortNumber)
            {
                x.ClientId = message.ClientId;
            }
        }

        private static void SentToClient(StreamWriter sWriter, MessageEventArgsModel message)
        {
            string messageJson = JsonConvert.SerializeObject(message);
            sWriter.WriteLine(messageJson);
            sWriter.Flush();
        }

        private static void SetEmptyPortNumber()
        {
            if (_clients.Count > 0)
            {
                _emptyPortNumber = _clients.Select(x => x.Port).Max() + 1;
            }
            else
            {
                _emptyPortNumber = _startsPortNumber;
            }
        }
    }
}
