
using AskEpamWCFService.Entities;
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
		void AskQuestion(string asker, int area, string question);

        [OperationContract(IsOneWay = true)]
        void ListQuestions();

		[OperationContract]
		List<Skill> GetAreas();

		[OperationContract]
		void UpdateSkill(string user, int skill);

		[OperationContract(IsOneWay = true)]
		void GetAnswerFromClient(int id, string answer, string answerer);
	}

	public interface IAskCallback
	{
		[OperationContract(IsOneWay = true)]
		void SendAnswerToClient(string answer, string answerer);

		[OperationContract(IsOneWay = true)]
		void AskClient(int id, string question);

        [OperationContract(IsOneWay = true)]
        void SendListQuestionsToClient(List<Question> list);
	}
}
