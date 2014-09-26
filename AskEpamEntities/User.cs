using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AskEpamWCFService.Entities
{
	[DataContract]
	public class User
	{
		[DataMember]
		public int Id { get; set; }

		[DataMember]
		public string DomainName { get; set; }

		[DataMember]
		public int SkillId { get; set; }
	}
}