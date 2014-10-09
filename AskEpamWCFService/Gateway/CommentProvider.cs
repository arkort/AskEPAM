using AskEpamEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AskEpamWCFService.Gateway
{
    public class CommentProvider
    {
        public static void AddComment(int idQuestion, string text,int idUser)
        {
            AskEpamDB_LINQDataContext context = new AskEpamDB_LINQDataContext();

            Comment userComment = new Comment()
            {
                idQuestion = idQuestion,
                text = text,
                dateTimeCreation = DateTime.Now,
                idUser = idUser
            };

            context.Comments.InsertOnSubmit(userComment);
            context.Comments.Context.SubmitChanges();
        }

        public static List<UserComment> ListComments(int idQuestion)
        {
            List<UserComment> userComments = new List<UserComment>();

            AskEpamDB_LINQDataContext context = new AskEpamDB_LINQDataContext();

            //List<Comment> allUserComments = context.Comments.ToList();

            //foreach (Comment comment in allUserComments)
            //{
            //    if (comment.idQuestion == idQuestion)
            //    {
            //        userComments.Add(new UserComment(
            //            Convert.ToInt32(comment.idQuestion),
            //            comment.text,
            //            Convert.ToDateTime(comment.dateTimeCreation),
            //            UserProvider.GetUserNameByID(Convert.ToInt32(comment.idUser))
            //            ));
            //    }
            //}

            //test with join
            var listComment = context.COMMENTS_LIST(idQuestion);
            foreach(COMMENTS_LISTResult comment in listComment)
            {
                userComments.Add(new UserComment(
                       idQuestion,
                       comment.text,
                       Convert.ToDateTime(comment.dateTimeCreation),
                       comment.login
                       ));
            }
            //-------------//-------------//---------//

            return userComments;
        }
    }
}