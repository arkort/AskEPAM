using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AskEpamWCFService.Entities
{
	public class Question
	{
		public long Id { get; set; }
		public string QuestionText { get; set; }
		public Client Asker { get; set; }
	}
}