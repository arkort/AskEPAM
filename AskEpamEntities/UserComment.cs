using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AskEpamEntities
{
    [DataContract]
    public class UserComment
    {
        [DataMember]
		public int IdQuestion { get; set; }

        [DataMember]
		public string Text { get; set; }

        public UserComment(int IdQuestion, string Text)
        {
            this.IdQuestion = IdQuestion;
            this.Text = Text;
        }
    }
}
