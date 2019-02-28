using MicroTcp.BLL.Models;
using MicroTcp.ClientConnection;
using MicroTcp.DAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MicroTcp.Client.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static DAL.Entities.Client _currentСlient;
        private int _portNumber = 5555;
        private TcpClientConnection _clientTcp;
        private BLL.Common _common;
        ObservableCollection<ConversationModel> Conversations;
        //public event EventHandler OnMessagesChange;
        //List<MessagesModels> MessagesModels;
        ObservableCollection<MessageEventArgsModel> Messages;


        public MainWindow()
        {
            ModalWindow modalWindow = new ModalWindow();
            modalWindow.ShowDialog();
            _currentСlient = ModalWindow._currentСlient;
            if (_currentСlient == null)
            {
                System.Windows.Application.Current.Shutdown();
            }
            else
            {
                InitializeComponent();
                _common = new BLL.Common();
                Conversations = new ObservableCollection<ConversationModel>();
                Messages = new ObservableCollection<MessageEventArgsModel>();
                _clientTcp = new TcpClientConnection();
                _clientTcp.OnMessage += SetMessage;
                _clientTcp.SentStartMessage += SentStartMessage;
                UpdateСlientData();
                _clientTcp.StartTcpClient(_portNumber);

            }

        }

        private void UpdateСlientData()
        {
            var conversations = _common.GetConversationsByClientId(_currentСlient.Id).ToList();
            foreach (var conversation in conversations)
            {
                if (conversation == null)
                {
                    continue;
                }
                Conversations.Add(new ConversationModel
                {
                    Id = conversation.Id,
                    Name = conversation.Name,
                    StartDateTime = conversation.StartDateTime
                });
            }
            listBox.ItemsSource = Conversations;
        }

        private void btn_Sent_Click(object sender, RoutedEventArgs e)
        {
            var isAuthenticated = _clientTcp.IsAuthenticated();
            if (!isAuthenticated || string.IsNullOrWhiteSpace(textSent.Text))
            {
                return;
            }

            var selectedConversation = (ConversationModel)listBox.SelectedItem ;
            if(selectedConversation != null)
            {
                var message = new MessageEventArgsModel
                {
                    Text = textSent.Text,
                    MessageType = MessageType.ToAnotherClient,
                    ConversationId = selectedConversation.Id,
                    ClientId = _currentСlient.Id,
                    PostingDateTime = DateTime.Now
                };
                var messageId = _common.SaveMessage(message);
                message.Id = messageId;
                _clientTcp.SentMessage(message);
                textSent.Text = string.Empty;
            }
        }

        private void SetMessage(object sender, MessageEventArgsModel e)
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                Messages.Add(e);
                textBox.ItemsSource = Messages;
            }));
        }

        private void SentStartMessage(object sender, MessageEventArgsModel e)
        {
            e.ClientId = _currentСlient.Id;
            e.MessageType = MessageType.Authenticate;
            e.PostingDateTime = DateTime.Now;
            _clientTcp.SentMessage(e);
        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {

        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Messages.Clear();
            var conversationModel = (ConversationModel)e.AddedItems[0];
            var massages = _common.GetMassagesByConversationId(conversationModel?.Id ?? 0).ToList();
            foreach (var message in massages)
            {
                if (message == null)
                {
                    continue;
                }
                Messages.Add(new MessageEventArgsModel
                {
                    Id = message.Id,
                    Text = message.Text
                });
            }
            textBox.ItemsSource = Messages;
        }
        private void textBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        
    }
}
