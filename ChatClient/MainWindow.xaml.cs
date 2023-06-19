using ChatClient.ServiceChat;
using System;
using System.Windows;
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
                ID = client.Connect(textbox_UserName.Text);
                textbox_UserName.IsEnabled = false;
                button_ConnDicon.Content = "Отсоединится";
                isConnected = true;
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
            string text = "Здравствуй пользователь. Раз ты нажал на эту кнопку, значит ты хочешь узнать, как пользоваться приложением. " +
                "\n1. Чтобы подключиться к серверу, надо сначала ввести имя пользователя. " +
                "\n2. Чтобы вы смогли подключиться к серверу, вы должны нажать на кнопку \"Подключиться\". " +
                "\n3. Чтобы подключение прошло успешно, должен быть включён сервер. " +
                "\n4. Общаться можно с помощью пустого поля, где написано слово \"Сообщение\". " +
                "\n5. Отправлять сообщения можно при помощи кнопки \"Enter\".";
            MessageBox.Show(text);
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
