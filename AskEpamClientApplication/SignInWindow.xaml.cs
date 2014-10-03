using AskEpamClientApplication.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AskEpamClientApplication
{
    /// <summary>
    /// Interaction logic for LogInWindow.xaml
    /// </summary>
    public partial class SignInWindow : Window
    {
        AskServiceClient connectionToServer;

        public SignInWindow(AskServiceClient connectionToServer)
        {
            InitializeComponent();
            this.connectionToServer = connectionToServer;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string login = TextBoxLogin.Text;
            string pwd = TextBoxPwd.Password; ;

            if (checkInsertValue(login, pwd))
            {
                AskEpamEntities.EpamUser epamUser = new AskEpamEntities.EpamUser(login, pwd);

                connectionToServer.Autorization(epamUser);
                this.Close();
            }     
        }

        private bool checkInsertValue(string login, string pwd)
        {
            try
            {
                if (!(CheckValue.ContainsOnlyDigitOrLetter(login) &&
                CheckValue.ContainsOnlyDigitOrLetter(pwd) ))
                {
                    throw new Exception("Not valid value");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }

            return true;
        }
    }
}
