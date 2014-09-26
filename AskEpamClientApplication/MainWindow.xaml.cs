using AskEpamClientApplication.ServiceReference1;
using AskEpamWCFService.Entities;
using Hardcodet.Wpf.TaskbarNotification;
using Samples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AskEpamClientApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        InstanceContext instContext;
        AskServiceClient connectionToServer;
        ClientInstance clientInstance;

        public MainWindow()
        {
            InitializeComponent();

            // for notify icon
            CommandBinding bind = new CommandBinding(ApplicationCommands.Open);
            bind.Executed += OpenFormCommand_Executed;
            this.CommandBindings.Add(bind);

            clientInstance = new ClientInstance();

            instContext = new InstanceContext(clientInstance);
            connectionToServer = new AskServiceClient(instContext);

            clientInstance.obtainedListOfQuestions += clientInstance_obtainedListOfQuestions;

            //fill combobox with questions
            connectionToServer.ListQuestions();
        }

        /// <summary>
        /// fill combobox with questions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void clientInstance_obtainedListOfQuestions(object sender, ClientInstance.MyEventArgs e)
        {
            List<string> listQuestionText = new List<string>();
            foreach (Question question in e.listQuestions)
            {
                listQuestionText.Add(question.QuestionText);
            }
            ListBox.ItemsSource = listQuestionText;
        }



        /// <summary>
        /// double click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenFormCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.WindowState = WindowState.Normal;
            this.ShowInTaskbar = true;

            myNotifyIcon.Visibility = Visibility.Hidden;
        }


        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                myNotifyIcon.Visibility = Visibility.Visible;
            }
        }

        private void MsgButton_Click(object sender, RoutedEventArgs e)
        {

            //temporary code for check work of icon in tray
            myNotifyIcon.ShowCustomBalloon(new FancyBalloon(),PopupAnimation.Slide, 4000);
        
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult resQuestion = MessageBox.Show("Do you really want to close the program?", "Confirm", MessageBoxButton.OKCancel);

            if (resQuestion == MessageBoxResult.OK)
            {
                this.Close();
            }
        }

        private void AddNewQuestion_Click(object sender, RoutedEventArgs e)
        {
            AskQuestionWindow aqw = new AskQuestionWindow(connectionToServer);
            aqw.ShowDialog();
        }

    }
}
