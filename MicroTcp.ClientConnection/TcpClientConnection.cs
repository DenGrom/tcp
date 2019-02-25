using MicroTcp.BLL.Models;
using MicroTcp.DAL.Entities;
using MicroTcp.DAL.Entities.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MicroTcp.ClientConnection
{
    public class TcpClientConnection
    {
        private TcpClient _client;
        private StreamReader _sReader;
        private StreamWriter _sWriter;
        private int _portNumber;
        private bool _isAuthenticated;
        private Boolean _isConnected;
        public event EventHandler<MessageEventArgsModel> OnMessage;
        public void StartTcpClient(int portNumber)
        {
            _portNumber = portNumber;
            var ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), portNumber);

            _client = new TcpClient();
            _client.Connect(ipPoint);

            _sReader = new StreamReader(_client.GetStream(), Encoding.ASCII);
            _sWriter = new StreamWriter(_client.GetStream(), Encoding.ASCII);
            _isConnected = true;
            HandleCommunication();
            if (!_isAuthenticated)
            {
                SentMessage(0, string.Empty, MessageType.Authenticate);
            }

        }

        public void HandleCommunication()
        {
            new Thread(() =>
            {
                while (_isConnected)
                {

                    String sDataIncomming = _sReader.ReadLine();
                    if (string.IsNullOrWhiteSpace(sDataIncomming))
                    {
                        continue;
                    }
                    var message = JsonConvert.DeserializeObject<Message>(sDataIncomming);
                    if (message == null)
                    {
                        continue;
                    }
                    //this.Dispatcher.Invoke(() =>
                    //{
                    if (message.MessageType == MessageType.Authenticate)
                    {
                        //textBox.Text = $"You are conected to port{message.Text}";
                        int toPort = 0;
                        if (Int32.TryParse(message.Text, out toPort))
                        {
                            _isConnected = false;
                            _client.Close();
                            _portNumber = toPort;
                            _isAuthenticated = true;
                            StartTcpClient(_portNumber);
                        }

                    }
                    if (message.MessageType == MessageType.ToAnotherClient)
                    {
                        OnMessage(this, new MessageEventArgsModel(message.Text, 
                            message.Conversation?.Id ?? 0,
                            message.Client?.Id ?? 0,
                            message.Id
                            ) );
                    }
                    //});
                }
            }).Start();

        }

        public void SentMessage(int toPort, string text, MessageType messageType)
        {
            var message = new Message
            {
                Text = text,
                FromPort = _portNumber,
                ToPort = toPort,
                MessageType = messageType
            };

            string messageJson = JsonConvert.SerializeObject(message);


            _sWriter.WriteLine(messageJson);
            _sWriter.Flush();
        }

        public bool IsAuthenticated()
        {
            return _isAuthenticated;
        }
    }
}

