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
    /// Interaction logic for SignUpWindow.xaml
    /// </summary>
    public partial class SignUpWindow : Window
    {
        AskServiceClient connectionToServer;

        public SignUpWindow(AskServiceClient connectionToServer)
        {
            InitializeComponent();
            this.connectionToServer = connectionToServer;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string login = TextBoxLogin.Text;
            string pwd = TextBoxPwd.Password; ;
            string confirmPwd = TextBoxConfirmPwd.Password;

            if (checkInsertValue(login, pwd, confirmPwd))
            {
                AskEpamEntities.EpamUser epamUser = new AskEpamEntities.EpamUser(login, pwd);
                connectionToServer.AddUser(epamUser);
                this.Close();
            }            
        }


        private bool checkInsertValue(string login, string pwd, string confirmPwd)
        {
            try
            {
                if (!(CheckValue.ContainsOnlyDigitOrLetter(login) &&
                CheckValue.ContainsOnlyDigitOrLetter(pwd) &&
                CheckValue.ContainsOnlyDigitOrLetter(confirmPwd)))
                {
                    throw new Exception("Not valid value");
                }

                if (pwd != confirmPwd)
                {
                    throw new Exception("Not the same value of password");
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }

            return true;
        }

    }
}
