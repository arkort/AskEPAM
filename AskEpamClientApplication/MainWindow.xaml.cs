using AskEpamClientApplication.ServiceReference1;
using AskEpamEntities;
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
        List<QuestionSection> sections;

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

            //fill combobox with questions
            clientInstance.obtainedListOfQuestions += ClientInstance_obtainedListOfQuestions;
            connectionToServer.ListQuestions();

            //read comments
            clientInstance.obtainedListOfComments += clientInstance_obtainedListOfComments;
            connectionToServer.ListComments();
        }

        void clientInstance_obtainedListOfComments(object sender, ClientInstance.CommentEventArgs e)
        {
            allMsg.Text = "";
            List<string> listQuestionText = new List<string>();
            foreach (UserComment comment in e.ListComments)
            {
                listQuestionText.Add(comment.Text);
                allMsg.Text += "\n" + comment.Text + "\n-------------------------------------------";
            }
            
            

        }

        /// <summary>
        /// fill combobox with questions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ClientInstance_obtainedListOfQuestions(object sender, ClientInstance.QuestionEventArgs e)
        {
            List<string> listQuestionText = new List<string>();
            foreach (Question question in e.ListQuestions)
            {
                listQuestionText.Add(question.QuestionText);
            }

            sections = e.Sections.ToList();

            ListBox.ItemsSource = listQuestionText;
            ListBox.SelectedIndex = 0;
        }



        /// <summary>
        /// double click on notify icon
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

            connectionToServer.AddComment(0, myMsg.Text);
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
            AskQuestionWindow aqw = new AskQuestionWindow(connectionToServer, sections);
            aqw.ShowDialog();
        }

    }
}
