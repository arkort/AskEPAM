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
    /// Interaction logic for AskQuestionWindow.xaml
    /// </summary>
    public partial class AskQuestionWindow : Window
    {
        AskServiceClient connectionToServer;
            
        public AskQuestionWindow(AskServiceClient connectionToServer)
        {
            InitializeComponent();

            this.connectionToServer = connectionToServer;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            connectionToServer.AskQuestion("user", 0, myMsg.Text);
            connectionToServer.ListQuestions();

            this.Close();
        }
    }
}
