using SocialMedia.BusinessLogic.Interfaces.IDataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.DataAccess
{
	public class UpvotedCommentsDB : IUpvotedCommentsDataAccess
	{
		private string connection = "Server=mssqlstud.fhict.local;Database=dbi511464_i511464fh;User Id=dbi511464_i511464fh;Password=12345;";

		public void CreateRecord(Guid userId, Guid commentId)
		{
			SqlConnection conn = new SqlConnection(connection);
			conn.Open();

			string sql =  $"insert into UpvotedComments " +
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

			string sql = $"delete from UpvotedComments " +
						 $"where UserId = @UserId and CommentId = @CommentId ";

			SqlCommand cmd = new SqlCommand(sql, conn);

			cmd.Parameters.AddWithValue("@UserId", userId);
			cmd.Parameters.AddWithValue("@CommentId", commentId);

			cmd.ExecuteNonQuery();

			conn.Close();
		}

		public bool HasUserUpvoted(Guid userId, Guid commentId)
		{
			int record = -1;

			bool hasUserUpvotedComment = false;

			SqlConnection conn = new SqlConnection(connection);

			conn.Open();

			string sql = $"select COUNT(*) as 'Record' " +
						 $"from UpvotedComments " +
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
				hasUserUpvotedComment = true;
			}
			else if (record == 0)
			{
				hasUserUpvotedComment = false;
			}

			dr.Close();
			conn.Close();

			return hasUserUpvotedComment;


		}

	}
}

