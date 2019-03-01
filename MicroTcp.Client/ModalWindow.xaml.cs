using MicroTcp.BLL.Models;
using MicroTcp.DAL.Repositories;
using System;
using System.Collections.Generic;
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

namespace MicroTcp.Client
{
    /// <summary>
    /// Interaction logic for ModalWindow.xaml
    /// </summary>
    public partial class ModalWindow : Window
    {
        private BLL.Common _common;
        public static DAL.Entities.Client _currentСlient;

        
        public ModalWindow()
        {
            InitializeComponent();
            _common = new BLL.Common();

        }

        private void btn_Sign_In_Click(object sender, RoutedEventArgs e)
        {
            _currentСlient = _common.SignIn(txt_NickName.Text,
                txt_Password.Text);
            if (_currentСlient != null)
            {
                this.Close();
            }
            txt_NickName.Text = String.Empty;
            txt_Password.Text = String.Empty;
        }

        private void Registration_Click(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
