using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AskEpamEntities
{
    [DataContract]
	public class Question
	{
        [DataMember]
		public long Id { get; set; }

        [DataMember]
		public string QuestionText { get; set; }

        public Question(long Id, string QuestionText)
        {
            this.Id = Id;
            this.QuestionText = QuestionText;
        }
	}
}