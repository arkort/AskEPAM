using AskEpamWCFService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AskEpamWCFService
{
	public class Client
	{
		public string Username { get; set; }

		public Skill Area { get; set; }

		public IAskCallback Callback { get; set; }

		public Client(string username, Skill area, IAskCallback callback)
		{
			Username = username;
			Area = area;
			Callback = callback;
		}
	}
}