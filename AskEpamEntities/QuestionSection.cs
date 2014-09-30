using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AskEpamEntities
{
    [DataContract]
    public class QuestionSection
    {
        public QuestionSection(int Id,string SectionName)
        {
            this.Id = Id;
            this.SectionName = SectionName;
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string SectionName { get; set; }
    }
}
