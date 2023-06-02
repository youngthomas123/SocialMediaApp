using SocialMedia.BusinessLogic;
using SocialMedia.BusinessLogic.Dto;
using SocialMedia.BusinessLogic.Interfaces.IDataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.DataAccess
{
	public class UserFriendsDB : IUserFriendsDataAccess
	{
		private string connection = "Server=mssqlstud.fhict.local;Database=dbi511464_i511464fh;User Id=dbi511464_i511464fh;Password=12345;";

		public void CreateRecord(Guid userId, Guid friendId)
		{
			SqlConnection conn = new SqlConnection(connection);

			conn.Open();

			string sql = $"insert into UserFriends ([UserId], [FriendId]) " +
						 $"Values (@userId, @friendId) ";

			SqlCommand cmd = new SqlCommand(sql, conn);

			cmd.Parameters.AddWithValue("@userId", userId);
			cmd.Parameters.AddWithValue("@friendId", friendId);


			cmd.ExecuteNonQuery();

			conn.Close();
		}

		public void DeleteRecord(Guid userId, Guid friendId)
		{
			SqlConnection conn = new SqlConnection(connection);

			conn.Open();

			string sql = $"Delete from UserFriends " +
						 $"where UserId = @userId and FriendId = @friendId ";

			SqlCommand cmd = new SqlCommand(sql, conn);

			cmd.Parameters.AddWithValue("@userId", userId);
			cmd.Parameters.AddWithValue("@friendId", friendId);


			cmd.ExecuteNonQuery();

			conn.Close();
		}

		public List<Guid>GetUserFriends(Guid userId)
		{
            List <Guid> Friends = new List<Guid>();

            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            string sql = $"Select FriendId " +
                         $"from UserFriends " +
                         $"where UserId = @userId";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@userId", userId);

            SqlDataReader dr = cmd.ExecuteReader();

           
            int FriendIdIndex = dr.GetOrdinal("FriendId");
           


            while (dr.Read())
            {

                var FriendId = (Guid)dr[FriendIdIndex];
            
				Friends.Add(FriendId);

            }
			dr.Close();
			conn.Close();
            return Friends;
        }

		public bool CheckRecordExists(Guid userId, Guid friendId)
		{
			bool doesRecordExists = false;

			SqlConnection conn = new SqlConnection(connection);

			conn.Open();

			string sql = $"select count(*) as record " +
				         $"from UserFriends " +
						 $"where UserId = @userId and FriendId = @friendId ";


            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@userId", userId);


            cmd.Parameters.AddWithValue("@friendId", friendId);

            SqlDataReader dr = cmd.ExecuteReader();


            int RecordIndex = dr.GetOrdinal("record");



            while (dr.Read())
            {

                var Record = (int)dr[RecordIndex];

                if(Record == 0)
				{
					doesRecordExists = false;
				}
				else if (Record ==1)
				{
					doesRecordExists = true;
				}

            }
			dr.Close();
			conn.Close();
			return doesRecordExists;
        }


    }
}
