


using AskEpamWCFService.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AskEpamWCFService.Gateway
{
	public static class UserProvider
	{
		private const string CONNECTION_STRING = "Data Source=EPRUSARW0875;Persist Security Info=True;User ID=dev;Password=Welcome1;database=AskEpamDB";

		public static User GetUser(string userName)
		{
			using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
			{
				SqlCommand command = new SqlCommand("GET_USER", connection);

				command.Parameters.AddWithValue("@username", userName);
				command.CommandType = System.Data.CommandType.StoredProcedure;

				connection.Open();

				using (var reader = command.ExecuteReader())
				{
					if (reader.HasRows)
					{
						reader.Read();

						var user = new User()
						{
							Id = reader.GetInt32(reader.GetOrdinal("Id")),
							DomainName = reader.GetString(reader.GetOrdinal("DomainName")),
							SkillId = reader.GetInt32(reader.GetOrdinal("Skill"))
						};

						return user;
					}
					else
					{
						return null;
					}
				}
			}
		}

		public static User AddUser(string userName)
		{
			using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
			{
				SqlCommand command = new SqlCommand("ADD_USER", connection);
				command.CommandType = System.Data.CommandType.StoredProcedure;

				command.Parameters.AddWithValue("@username", userName);

				connection.Open();

				var id = command.ExecuteScalar();

				return new User() { Id = (int)(decimal)id, DomainName = userName, SkillId = 1 };
			}
		}
	}
}