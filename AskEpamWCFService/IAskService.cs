
using AskEpamEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace AskEpamWCFService
{
	[ServiceContract(CallbackContract = typeof(IAskCallback))]
	public interface IAskService
	{
		[OperationContract]
		void Handshake(string user);

		[OperationContract(IsOneWay = true)]
		void AskQuestion(string asker, int section, string question);

        [OperationContract(IsOneWay = true)]
        void ListQuestions();

        [OperationContract(IsOneWay = true)]
        void AddComment(int idQuestion, string text,int idUser);

        [OperationContract(IsOneWay = true)]
        void ListComments(int idQuestion);

        //[OperationContract(IsOneWay = true)]
        //List<Skill> GetAreas();

        [OperationContract(IsOneWay = true)]
		void UpdateSkill(string user, int skill);

		[OperationContract(IsOneWay = true)]
		void GetAnswerFromClient(int id, string answer, string answerer);

        [OperationContract(IsOneWay = true)]
        void AddUser(EpamUser user);

        [OperationContract(IsOneWay = true)]
        void Autorization(EpamUser user);
	}

	public interface IAskCallback
	{
		[OperationContract(IsOneWay = true)]
		void SendAnswerToClient(string answer, string answerer);

        [OperationContract(IsOneWay = true)]
        void AskClient(int id, string question);

        [OperationContract(IsOneWay = true)]
        void SendListQuestionsToClient(List<Question> list, List<QuestionSection> sections);

        [OperationContract(IsOneWay = true)]
        void SendListCommentsToClient(List<UserComment> list);

        [OperationContract(IsOneWay = true)]
        void SendUserToClient(EpamUser user);
	}
}
