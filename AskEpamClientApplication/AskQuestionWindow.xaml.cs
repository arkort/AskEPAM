using AskEpamClientApplication.ServiceReference1;
using AskEpamEntities;
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
        List<QuestionSection> sections;
            
        public AskQuestionWindow(AskServiceClient connectionToServer,List<QuestionSection> sections)
        {
            InitializeComponent();

            this.connectionToServer = connectionToServer;

            List<string> nameSections = new List<string>();

            foreach(QuestionSection questionSection in sections)
            {
                nameSections.Add(questionSection.SectionName);
            }

            SectionsComboBox.ItemsSource = nameSections;
            SectionsComboBox.SelectedIndex = 0;

            this.sections = sections;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            QuestionSection sect = sections.Where((sec) => { return sec.SectionName == SectionsComboBox.Text; }).FirstOrDefault();
            if(sect!=null)
            {
                connectionToServer.AskQuestion("user", sect.Id, myMsg.Text);
                connectionToServer.ListQuestions();
            }

            this.Close();
        }
    }
}
