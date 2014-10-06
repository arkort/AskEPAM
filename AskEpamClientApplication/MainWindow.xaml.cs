using AskEpamClientApplication.ServiceReference1;
using AskEpamEntities;
using Hardcodet.Wpf.TaskbarNotification;
using Samples;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
        Question[] questions;

        InstanceContext instContext;
        AskServiceClient connectionToServer;
        ClientInstance clientInstance;

        EpamUser currentUser;

        public MainWindow()
        {
            InitializeComponent();

            // for notify icon
            CommandBinding bind = new CommandBinding(ApplicationCommands.Open);
            bind.Executed += OpenFormCommand_Executed;
            this.CommandBindings.Add(bind);

            //WCF duplex connection
            clientInstance = new ClientInstance();
            instContext = new InstanceContext(clientInstance);

            //WSDualHttpBinding binding = new WSDualHttpBinding();
            //EndpointAddress address = new EndpointAddress("http://localhost:62399/AskService.svc");

            connectionToServer = new AskServiceClient(instContext);

            //fill combobox with questions
            clientInstance.obtainedListOfQuestions += ClientInstance_obtainedListOfQuestions;
            connectionToServer.ListQuestions();

            //read comments
            clientInstance.obtainedListOfComments += clientInstance_obtainedListOfComments;
            clientInstance.obtainedUser += clientInstance_obtainedUser;

            SignInWindow siw = new SignInWindow(connectionToServer);
            siw.ShowDialog();

        }

        void clientInstance_obtainedUser(object sender, ClientInstance.UserEventArgs e)
        {
            currentUser = e.User;

            this.Title += "  User: " + currentUser.Login;

            SendMsgButton.IsEnabled = true;
            AddQuestionMenuItem.IsEnabled = true;
        }

        void clientInstance_obtainedListOfComments(object sender, ClientInstance.CommentEventArgs e)
        {
            allMsg.Text = "";
            
            foreach (UserComment comment in e.ListComments)
            {
                allMsg.Text += "\n" + comment.loginUser+" "+comment.dateTimeCreation;
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
            questions = e.ListQuestions;
            foreach (Question question in questions)
            {
                listQuestionText.Add(question.QuestionText);
            }

            sections = e.Sections.ToList();

            QuestionListBox.ItemsSource = listQuestionText;
            QuestionListBox.SelectedIndex = 0;

            //determine id of question
            if ((questions != null) && (questions.Length>0))
            {
                //QuestionSection questionSection = sections.Where((sec) => { return SectionListBox.Text == sec.SectionName; }).FirstOrDefault();
                connectionToServer.ListComments(questions[0].Id);
            }
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

        private void autoSignIn()
        {
            //string login = "testLogin";

            //EpamUser user = new EpamUser(login,Guid.NewGuid());

            //Stream TestFileStream = File.Create("testIni.askepam");
            //BinaryFormatter serializer = new BinaryFormatter();
            //serializer.Serialize(TestFileStream, user);
            //TestFileStream.Close();

            //FileStream TestFileStream222 = new FileStream("testIni.askepam", FileMode.Open);
            //BinaryFormatter serializer222 = new BinaryFormatter();
            //User resUser = (User)serializer222.Deserialize(TestFileStream222);
            //TestFileStream222.Close();
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

            Question currentQuestion = questions.Where((q) => { return q.QuestionText == QuestionListBox.Text; }).FirstOrDefault();

            if (currentQuestion != null)
            {
                int idAutor=0;
                if(currentUser!=null)
                {
                    idAutor=currentUser.Id;
                }
                connectionToServer.AddComment(currentQuestion.Id, myMsg.Text, idAutor);
            }

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

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            SignInWindow siw = new SignInWindow(connectionToServer);
            siw.ShowDialog();
        }

        private void QuestionListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string  selectedQuestion=QuestionListBox.Items[QuestionListBox.SelectedIndex].ToString();
            //determine id of question
            if ((questions != null) && (questions.Length > 0))
            {
                Question currentQuestion = questions.Where((q) => { return q.QuestionText == selectedQuestion; }).FirstOrDefault();

                if (currentQuestion != null)
                {
                    connectionToServer.ListComments(currentQuestion.Id);
                }
            }
        }

    }
}
