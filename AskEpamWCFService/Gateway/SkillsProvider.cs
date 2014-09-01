using AskEpamWCFService.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AskEpamWCFService.Gateway
{
	public static class SkillsProvider
	{
		private const string CONNECTION_STRING = "Data Source=EPRUSARW0875;Persist Security Info=True;User ID=dev;Password=Welcome1;database=AskEpamDB";

		public static List<Skill> GetAllSkills()
		{
			List<Skill> skills = new List<Skill>();

			using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
			{
				SqlCommand command = new SqlCommand("GET_SKILLS", connection);
				command.CommandType = System.Data.CommandType.StoredProcedure;

				connection.Open();

				using (var reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						skills.Add(new Skill()
						{
							Id = reader.GetInt32(reader.GetOrdinal("Id")),
							SkillName = reader.GetString(reader.GetOrdinal("Skill"))
						});
					}
				}
			}

			return skills;
		}

		public static void UpdateSkillOfUser(string username, int skill)
		{
			using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
			{
				SqlCommand command = new SqlCommand("UPDATE_SKILL_OF_USER", connection);
				command.CommandType = System.Data.CommandType.StoredProcedure;

				command.Parameters.AddWithValue("@username", username);
				command.Parameters.AddWithValue("@skill", skill);

				connection.Open();

				command.ExecuteNonQuery();
			}
		}
	}
}