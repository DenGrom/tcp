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
                _clientTcp = new TcpClientConnection();
                _clientTcp.OnMessage += SetMessage;
                UpdateСlientData();
                _clientTcp.StartTcpClient(_portNumber);

            }

        }

        private void UpdateСlientData()
        {
            //var userConnections = _common.GetUserConnections(_currentСlient.Id).ToList();
            // foreach (var userConnection in userConnections)
            // {
            //     listBox.Items.Add(userConnection);
            // }
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
            var toPort = 0;
            if (textSent.Text.Contains("to ports:"))
            {
                var toPortsString = textSent.Text.Replace("to ports:", string.Empty);
                Int32.TryParse(toPortsString, out toPort);
            }
            _clientTcp.SentMessage(toPort, textSent.Text, MessageType.ToAnotherClient);
            textSent.Text = string.Empty;
        }

        private void SetMessage(object sender, MessageEventArgsModel e)
        {
            this.Dispatcher.Invoke(() =>
            {
                textBox.Text = e.Text;
            });
        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {

        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
