using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AskEpamEntities
{
    [Serializable]
	[DataContract]
	public class EpamUser
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Login { get; set; }

        [DataMember]
        public string Pwd { get; set; }

		[DataMember]
		public int SkillValue { get; set; }


        public EpamUser()
        {

        }

        public EpamUser(string DomainName, string Pwd)
        {
            this.Pwd = Pwd;
            this.Login = DomainName;
        }

        public EpamUser(int id, string DomainName, int SkillValue)
        {
            this.Id = id;
            this.Login = DomainName;
            this.SkillValue = SkillValue;
        }
	}
}