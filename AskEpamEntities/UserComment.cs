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

        [DataMember]
        public DateTime dateTimeCreation { get; set; }

        [DataMember]
        public string loginUser { get; set; }

        public UserComment(int IdQuestion, string Text, DateTime dateTimeCreation, string loginUser)
        {
            this.IdQuestion = IdQuestion;
            this.Text = Text;
            this.dateTimeCreation = dateTimeCreation;
            this.loginUser = loginUser;
        }
    }
}
