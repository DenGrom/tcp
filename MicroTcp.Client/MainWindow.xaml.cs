﻿using MicroTcp.BLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using MicroTcp.DAL.Entities;
using MicroTcp.DAL.Entities.Enums;
using System.Collections.ObjectModel;
using MicroTcp.ClientConnection;

namespace MicroTcp.Client
{
    public partial class MainWindow : Window
    {
        
        public static DAL.Entities.Client _currentСlient;
        private int _portNumber = 5555;
        private TcpClientConnection _clientTcp;
        private BLL.Common _common;
        ObservableCollection<DAL.Entities.Client>  Clients;

        
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
                Clients = new ObservableCollection<DAL.Entities.Client>();
                _clientTcp = new TcpClientConnection();
                //_clientTcp.OnMessage += OnMessage;
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
            var userConnections = _common.GetUserConnections(_currentСlient.Id).ToList();
            foreach (var userConnection in userConnections)
            {
                Clients.Add(userConnection.Participant);
            }
            
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

        

        //private void OnMessage(object sender, string e)
        //{
        //    textSent.Text = e;
        //}

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {

        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
