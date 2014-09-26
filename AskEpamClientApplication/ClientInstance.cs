using AskEpamClientApplication.ServiceReference1;
using AskEpamWCFService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AskEpamClientApplication
{
    public class ClientInstance : IAskServiceCallback
    {
        public Question[] ListQuestions { get; set; }

        public class MyEventArgs : EventArgs
        {
            public Question[] listQuestions { get; set; }
        }

        //public delegate void fillListQuestions(Question[] list);

        public event EventHandler<MyEventArgs> obtainedListOfQuestions;
       

        public void SendAnswerToClient(string answer, string answerer)
        {

        }

         
        public void AskClient(int id, string question)
        {

        }

        public void SendListQuestionsToClient(Question[] list)
        {
            ListQuestions = list;
            obtainedListOfQuestions(null,new MyEventArgs(){listQuestions = list});
        }
    }
}
