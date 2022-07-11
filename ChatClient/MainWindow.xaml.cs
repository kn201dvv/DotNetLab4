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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatClient
{
    public partial class MainWindow : Window, ServiceReference1.IServiceChatCallback
    {
        bool isconnected = false;
        ServiceReference1.ServiceChatClient client;
        int id;
        int idSendTo = -1;
        public MainWindow()
        {
            InitializeComponent();
        }
        void conectuser()
        {
            if (!isconnected)
            {
                client = new ServiceReference1.ServiceChatClient(new System.ServiceModel.InstanceContext(this));
                id = client.Connect(tb_username.Text);


                tb_username.IsEnabled = false;
                button_connect.Content = "Disconect";
                isconnected = true;
            }

        }
        void disconect()
        {
            if (isconnected)
            {
                client.Disconnect(id);
                client = null;
                tb_username.IsEnabled = true;
                button_connect.Content = "Conect";
                isconnected = false;
            }

        }
        private void button_connect_Click(object sender, RoutedEventArgs e)
        {
            if (!isconnected)
            {
                conectuser();
                // if(id == 0) client.getUsers2(id); 
                client.getUsers();
            }
            else
            {
                disconect();
                //client.getUsers();
            }
        }

        public void MsgCallBack(string msg)
        {
            lbmes.Items.Add(msg);
            lbmes.ScrollIntoView(lbmes.Items[lbmes.Items.Count - 1]);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            disconect();
        }

        private void tb_message_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (client != null && idSendTo == -1)
                    client.SendMsg(tb_message.Text, id);
                else if (client != null && idSendTo != -1)
                {
                    client.SendMsgToParticular(tb_message.Text, id, idSendTo);
                    idSendTo = -1;
                }
                tb_message.Text = "";
            }
        }


        public void UsrCallBack(string[] user)
        {
            lbusers.Items.Clear();
            for (int i = 0; i < user.Length; i++)
            {
                lbusers.Items.Add(user[i]);
            }

        }

        public void UsrCallBack2(string user)
        {
            lbusers.Items.Add(user);
        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            if (isconnected)
            {
                conectuser();
                // if(id == 0) client.getUsers2(id); 
                client.getUsers();
            }
        }

        private void lbusers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tb_message.Clear();
            idSendTo = lbusers.SelectedIndex;
            tb_message.Text = "Private messag (" + lbusers.SelectedItem.ToString() + "): ";
        }

        public void MsgToParticularCallBack(string msg)
        {
            lbmes.Items.Add(msg);
            lbmes.ScrollIntoView(lbmes.Items[lbmes.Items.Count - 1]);
        }
    }
}
