using AskEpamClientApplication.ServiceReference1;
using AskEpamEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AskEpamClientApplication
{
    public class ClientInstance : IAskServiceCallback
    {
        public string[] Sections { get; set; }

        public class QuestionEventArgs : EventArgs
        {
            public Question[] ListQuestions { get; set; }
            public QuestionSection[] Sections { get; set; }
        }
        public event EventHandler<QuestionEventArgs> obtainedListOfQuestions;

        public class CommentEventArgs : EventArgs
        {
            public UserComment[] ListComments { get; set; }
        }
        public event EventHandler<CommentEventArgs> obtainedListOfComments;

        public class UserEventArgs : EventArgs
        {
            public EpamUser User { get; set; }
        }
        public event EventHandler<UserEventArgs> obtainedUser;

        public void SendAnswerToClient(string answer, string answerer)
        {

        }
                 
        public void AskClient(int id, string question)
        {

        }

        public void SendListQuestionsToClient(Question[] list,QuestionSection[] sections)
        {
            obtainedListOfQuestions(null, new QuestionEventArgs() { ListQuestions = list, Sections = sections });
        }

        public void SendListCommentsToClient(UserComment[] list)
        {
            obtainedListOfComments(null, new CommentEventArgs() { ListComments = list });
        }

        public void SendUserToClient(EpamUser user)
        {
            obtainedUser(null, new UserEventArgs() { User = user });
        }
    }
}
