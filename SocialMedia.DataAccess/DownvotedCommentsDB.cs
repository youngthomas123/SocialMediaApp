using SocialMedia.BusinessLogic;
using SocialMedia.BusinessLogic.Interfaces.IDataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.DataAccess
{
	public class DownvotedCommentsDB : IDownvotedCommentsDataAccess
	{
		private string connection = "Server=mssqlstud.fhict.local;Database=dbi511464_i511464fh;User Id=dbi511464_i511464fh;Password=12345;";

		public void CreateRecord(Guid userId, Guid commentId)
		{
			SqlConnection conn = new SqlConnection(connection);
			conn.Open();

			string sql =  $"insert into DownvotedComments " +
						  $"Values (@UserId, @CommentId)";

			SqlCommand cmd = new SqlCommand(sql, conn);

			cmd.Parameters.AddWithValue("@UserId", userId);
			cmd.Parameters.AddWithValue("@CommentId", commentId);

			cmd.ExecuteNonQuery();
			conn.Close();
		}

		public void DeleteRecord(Guid userId, Guid commentId)
		{
			SqlConnection conn = new SqlConnection(connection);
			conn.Open();

			string sql = $"delete from DownvotedComments " +
						 $"where UserId = @UserId and CommentId = @CommentId ";

			SqlCommand cmd = new SqlCommand(sql, conn);

			cmd.Parameters.AddWithValue("@UserId", userId);
			cmd.Parameters.AddWithValue("@CommentId", commentId);

			cmd.ExecuteNonQuery();

			conn.Close();
		}

        public void DeleteRecord(Guid commentId)
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            string sql = $"delete from DownvotedComments " +
                         $"where CommentId = @CommentId ";

            SqlCommand cmd = new SqlCommand(sql, conn);

           
            cmd.Parameters.AddWithValue("@CommentId", commentId);

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public bool HasUserDownvoted(Guid userId, Guid commentId)
		{
			int record = -1;

			bool hasUserDownvotedComment = false;

			SqlConnection conn = new SqlConnection(connection);

			conn.Open();

			string sql = $"select COUNT(*) as 'Record' " +
						 $"from DownvotedComments " +
						 $"where UserId = @UserId and CommentId = @CommentId ";


			SqlCommand cmd = new SqlCommand(sql, conn);

			cmd.Parameters.AddWithValue("@UserId", userId);
			cmd.Parameters.AddWithValue("@CommentId", commentId);

			SqlDataReader dr = cmd.ExecuteReader();

			int RecordIndex = dr.GetOrdinal("Record");



			while (dr.Read())
			{
				var number = (int)dr[RecordIndex];
				record = number;
			}

			if (record > 0)
			{
				hasUserDownvotedComment = true;
			}
			else if (record == 0)
			{
				hasUserDownvotedComment = false;
			}

			dr.Close();
			conn.Close();

			return hasUserDownvotedComment;


		}

		public List <Guid>GetDownvotedUserIdsByComment(Guid commentId)
		{
            List<Guid> userIds = new List<Guid>();


            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            string sql = $"select UserId " +
                         $"from DownvotedComments " +
                         $"where CommentId = @commentId";


            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@commentId", commentId);

            SqlDataReader dr = cmd.ExecuteReader();


            int UserIdIndex = dr.GetOrdinal("UserId");


            while (dr.Read())
            {


                var UserId = (Guid)dr[UserIdIndex];

                userIds.Add(UserId);


            }


            dr.Close();
			conn.Close();

            return userIds;
        }

	}
}

