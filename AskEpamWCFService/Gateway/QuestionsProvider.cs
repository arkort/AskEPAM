

using AskEpamEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AskEpamWCFService.Gateway
{
    public class QuestionsProvider
    {
        private const string CONNECTION_STRING = "Data Source=EPRUSARW1092;Persist Security Info=True;database=AskEpamDB";

        
        public static void AddQuestion(int section,string question)
        {
            AskEpamDB_LINQDataContext context = new AskEpamDB_LINQDataContext();

            UserQuestion userQuestion = new UserQuestion()
            {
                idUser = 0,
                idSection = section,
                Question = question
            };

            context.UserQuestions.InsertOnSubmit(userQuestion);
            context.UserQuestions.Context.SubmitChanges();
        }

        public static List<Question> ListQuestions()
        {
            List<Question> questions = new List<Question>();

            AskEpamDB_LINQDataContext context = new AskEpamDB_LINQDataContext();
            List<UserQuestion> allUserQuestions = context.UserQuestions.ToList();

            foreach(UserQuestion userQuestion in allUserQuestions)
            {
                questions.Add(new Question(userQuestion.id, userQuestion.Question));
            }

            return questions;
        }
    }
}