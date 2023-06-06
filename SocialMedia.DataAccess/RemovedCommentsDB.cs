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
	public class RemovedCommentsDB :IRemovedCommentsDataAccess
	{
		private string connection = "Server=mssqlstud.fhict.local;Database=dbi511464_i511464fh;User Id=dbi511464_i511464fh;Password=12345;";


		public void CreateRecord(Guid commentId)
		{
			SqlConnection conn = new SqlConnection(connection);

			conn.Open();

			string sql = $"insert into RemovedComments ([CommentId]) " +
						 $"Values (@commentId) ";

			SqlCommand cmd = new SqlCommand(sql, conn);

			cmd.Parameters.AddWithValue("@commentId", commentId);


			cmd.ExecuteNonQuery();

			conn.Close();
		}

		public void DeleteRecord(Guid commentId)
		{
			SqlConnection conn = new SqlConnection(connection);

			conn.Open();

			string sql = $"Delete from RemovedComments " +
						 $"where CommentId = @commentId ";

			SqlCommand cmd = new SqlCommand(sql, conn);

			cmd.Parameters.AddWithValue("@commentId", commentId);


			cmd.ExecuteNonQuery();

			conn.Close();
		}

		public bool CheckRecordExists(Guid commentId)
		{
			bool doesRecordExists = false;

			SqlConnection conn = new SqlConnection(connection);

			conn.Open();

			string sql = $"select count(*) as record " +
						 $"from RemovedComments " +
						 $"where CommentId = @commentId ";


			SqlCommand cmd = new SqlCommand(sql, conn);

			cmd.Parameters.AddWithValue("@commentId", commentId);




			SqlDataReader dr = cmd.ExecuteReader();


			int RecordIndex = dr.GetOrdinal("record");



			while (dr.Read())
			{

				var Record = (int)dr[RecordIndex];

				if (Record == 0)
				{
					doesRecordExists = false;
				}
				else if (Record == 1)
				{
					doesRecordExists = true;
				}

			}

			dr.Close();
			conn.Close();
			return doesRecordExists;
		}

		public List<Guid> GetRemovedCommentIds()
		{
			List<Guid> commentIds = new List<Guid>();

			SqlConnection conn = new SqlConnection(connection);

			conn.Open();

			string sql = $"Select * " +
						 $"from RemovedComments ";

			SqlCommand cmd = new SqlCommand(sql, conn);

			SqlDataReader dr = cmd.ExecuteReader();


			int CommentIdIndex = dr.GetOrdinal("CommentId");
			while (dr.Read())
			{
				var CommentId = (Guid)dr[CommentIdIndex];

				commentIds.Add(CommentId);
			}

			dr.Close();
			conn.Close();
			return commentIds;


		}
	}
}
