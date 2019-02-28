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
        public event EventHandler<MessageEventArgsModel> SentStartMessage;
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
                var message = new MessageEventArgsModel {
                };
                //SentMessage(message);
                SentStartMessage(this, message);
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
                    var message = JsonConvert.DeserializeObject<MessageEventArgsModel>(sDataIncomming);
                    if (message == null)
                    {
                        continue;
                    }
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
                        OnMessage(this, message);
                    }
                }
            }).Start();

        }

        public void SentMessage(MessageEventArgsModel messageEventArgsModel)
        {
            string messageJson = JsonConvert.SerializeObject(messageEventArgsModel);
            _sWriter.WriteLine(messageJson);
            _sWriter.Flush();
        }

        public bool IsAuthenticated()
        {
            return _isAuthenticated;
        }
    }
}

