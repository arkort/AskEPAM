

using AskEpamEntities;
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

		public static EpamUser GetUser(string userName)
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

						var user = new EpamUser()
						{
							Id = reader.GetInt32(reader.GetOrdinal("Id")),
							Login = reader.GetString(reader.GetOrdinal("DomainName")),
							SkillValue = reader.GetInt32(reader.GetOrdinal("Skill"))
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

		public static EpamUser AddUser(EpamUser user)
		{
            //using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            //{
            //    SqlCommand command = new SqlCommand("ADD_USER", connection);
            //    command.CommandType = System.Data.CommandType.StoredProcedure;

            //    command.Parameters.AddWithValue("@login", user.DomainName);
            //    command.Parameters.AddWithValue("@pwd", user.Pwd);

            //    connection.Open();

            //    var id = command.ExecuteScalar();

            //    return new EpamUser() { Id = (int)(decimal)id, DomainName = user.DomainName, SkillValue = 0 };
            //}
            AskEpamDB_LINQDataContext context = new AskEpamDB_LINQDataContext();
            var result = context.ADD_USER(user.Login, user.Pwd);

            //this code needs improvement
            int id = -1;
            foreach (ADD_USERResult res in result)
            {
                id = res.id;
            }

            return new EpamUser(id, user.Login, 0);

		}

        public static EpamUser Autorization(EpamUser user)
        {
            AskEpamDB_LINQDataContext context = new AskEpamDB_LINQDataContext();
            var result = context.AUTHORIZATION(user.Login, user.Pwd);

            EpamUser epamUser=new EpamUser();
            //this code needs improvement

            foreach (AUTHORIZATIONResult res in result)
            {
                epamUser.Id = res.id;
                epamUser.Login = res.login;
                epamUser.SkillValue = Convert.ToInt32(res.skill);
            }

            if (epamUser.Id > 0)
            {
                return epamUser;
            }

            return null;
        }

        
        public static string GetUserNameByID(int id)
        {
            //Need to create storage procedure!!!!!!!!!!!!

            AskEpamDB_LINQDataContext context = new AskEpamDB_LINQDataContext();
            User user = context.Users.Where((u) => u.id ==  id ).FirstOrDefault();

            if (user != null) { return user.login; }
            return "";
        }
	}
}