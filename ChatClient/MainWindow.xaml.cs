using ChatClient.ServiceChat;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ChatClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IServiceChatCallback
    {
        bool isConnected = false;
        ServiceChatClient client;
        int ID;
        public MainWindow()
        {
            InitializeComponent();
        }


        void ConnectUser()
        {
            if (!isConnected && (!string.IsNullOrEmpty(textbox_UserName.Text)))
            {
                client = new ServiceChatClient(new System.ServiceModel.InstanceContext(this));
                try
                {
                    ID = client.Connect(textbox_UserName.Text);
                    textbox_UserName.IsEnabled = false;
                    button_ConnDicon.Content = "Отсоединится";
                    isConnected = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Запустите сервер, чтобы пользоваться чатом" , "Сервер не запущен", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
            }
        }

        void DisconnectUser()
        {
            if (isConnected)
            {
                client.Disconnect(ID);
                client = null;
                textbox_UserName.IsEnabled = true;
                button_ConnDicon.Content = "Подключиться";
                isConnected = false;
            }
        }


        public void MsgCallback(string msg)
        {
            listbox_Chat.Items.Add(msg);
            listbox_Chat.ScrollIntoView(listbox_Chat.Items[listbox_Chat.Items.Count - 1]);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DisconnectUser();
        }

        private void tbMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (client != null)
                {
                    client.SendMsg(textbox_Message.Text, ID);
                    textbox_Message.Text = string.Empty;
                }
            }
        }

        private void bConnDicon_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textbox_UserName.Text))
            {
                if (isConnected)
                {
                    DisconnectUser();
                }
                else
                {
                    ConnectUser();
                }
            }
            else
            {
                MessageBox.Show("Требуется ввести имя", "Нет имени", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonMenu_Click(object sender, RoutedEventArgs e)
        {
            if (grid_HidenMenu.Visibility == Visibility.Visible)
            {
                grid_HidenMenu.Visibility = Visibility.Hidden;
            }
            else if (grid_HidenMenu.Visibility == Visibility.Hidden)
            {
                grid_HidenMenu.Visibility = Visibility.Visible;
            }
        }

        private void buttonGuid_Click(object sender, RoutedEventArgs e)
        {
            GuideWindow guideWindow= new GuideWindow();
            guideWindow.Show();
        }

        private void DarkTheme_Click(object sender, RoutedEventArgs e)
        {
            AppTheme.ChangeTheme(new Uri("Themes/Dark.xaml", UriKind.Relative));
        }

        private void LightTheme_Click(object sender, RoutedEventArgs e)
        {
            AppTheme.ChangeTheme(new Uri("Themes/Light.xaml", UriKind.Relative));
        }

        private void ColorTheme_Click(object sender, RoutedEventArgs e)
        {
            AppTheme.ChangeTheme(new Uri("Themes/Color.xaml", UriKind.Relative));
        }
    }
}
