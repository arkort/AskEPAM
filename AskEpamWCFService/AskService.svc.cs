using AskEpamWCFService.Entities;
using AskEpamWCFService.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace AskEpamWCFService
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
	// NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.

	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
	public class AskService : IAskService
	{
		private static object _sync = new object();

		private Dictionary<string, Client> _clients = new Dictionary<string, Client>();

		private int _lastId = 0;
		private List<Question> _questions = new List<Question>();

		public void Handshake(string user)
		{
			var currentUser = UserProvider.GetUser(user);

			if (currentUser == null)
			{
				currentUser = UserProvider.AddUser(user);
			}

			if (_clients.ContainsKey(user))
			{
				_clients[user].Callback = OperationContext.Current.GetCallbackChannel<IAskCallback>();
			}
			else
			{
				_clients.Add(user, new Client(user, new Skill() { Id = currentUser.SkillId }, OperationContext.Current.GetCallbackChannel<IAskCallback>()));
			}
		}

		public void AskQuestion(string asker, int area, string question)
		{
			Random rand = new Random();
			var possibleAnswerers = _clients.Values.Where(x => x.Area.Id == area).Where(x => x.Username != asker);

			if (possibleAnswerers.Count() > 0)
			{
				var answerer = possibleAnswerers.ToList()[rand.Next(possibleAnswerers.Count())];

				lock (_sync)
				{
					answerer.Callback.AskClient(_lastId, question);

					_questions.Add(new Question() { Id = _lastId, QuestionText = question, Asker = _clients[asker] });
					_lastId++;
				}
			}
		}

		public List<Skill> GetAreas()
		{
			return SkillsProvider.GetAllSkills();
		}

		public void UpdateSkill(string user, int skill)
		{
			SkillsProvider.UpdateSkillOfUser(user, skill);
		}

		public void GetAnswerFromClient(int id, string answer, string answerer)
		{
			_questions.FirstOrDefault(x => x.Id == id).Asker.Callback.SendAnswerToClient(answer, answerer);
			_questions.Remove(_questions.FirstOrDefault(x => x.Id == id));
		}
	}
}
