using SocialMedia.BusinessLogic.Interfaces.IDataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.DataAccess
{
	public class ReportedPostsDB : IReportedPostsDataAccess
	{
		private string connection = "Server=mssqlstud.fhict.local;Database=dbi511464_i511464fh;User Id=dbi511464_i511464fh;Password=12345;";

		public void CreateRecord(Guid postId, Guid userId)
		{
			SqlConnection conn = new SqlConnection(connection);

			conn.Open();

			string sql = $"insert into ReportedPosts ([PostId], [UserId]) " +
						 $"Values (@postId, @userId) ";

			SqlCommand cmd = new SqlCommand(sql, conn);

			cmd.Parameters.AddWithValue("@postId", postId);
			cmd.Parameters.AddWithValue("@userId", userId);


			cmd.ExecuteNonQuery();

			conn.Close();
		}

		public void DeleteRecord(Guid postId, Guid userId)
		{
			SqlConnection conn = new SqlConnection(connection);

			conn.Open();

			string sql = $"Delete from ReportedPosts " +
						 $"where PostId = @postId and UserId = @userId ";

			SqlCommand cmd = new SqlCommand(sql, conn);

			cmd.Parameters.AddWithValue("@postId", postId);
			cmd.Parameters.AddWithValue("@userId", userId);


			cmd.ExecuteNonQuery();

			conn.Close();
		}

		public bool CheckRecordExists(Guid postId, Guid userId)
		{
			bool doesRecordExists = false;

			SqlConnection conn = new SqlConnection(connection);

			conn.Open();

			string sql = $"select count(*) as record " +
						 $"from ReportedPosts " +
						 $"where PostId = @postId and UserId = @userId ";


			SqlCommand cmd = new SqlCommand(sql, conn);

			cmd.Parameters.AddWithValue("@postId", postId);


			cmd.Parameters.AddWithValue("@userId", userId);

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
	}
}
