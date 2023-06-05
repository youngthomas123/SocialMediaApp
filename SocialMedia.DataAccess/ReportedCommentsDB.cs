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
	public class ReportedCommentsDB : IReportedCommentsDataAccess
	{
		private string connection = "Server=mssqlstud.fhict.local;Database=dbi511464_i511464fh;User Id=dbi511464_i511464fh;Password=12345;";


		public void CreateRecord(Guid commentId, Guid userId, int reasonId)
		{
			SqlConnection conn = new SqlConnection(connection);

			conn.Open();

			string sql = $"insert into ReportedComments ([CommentId], [UserId], [ReasonId]) " +
						 $"Values (@commentId, @userId, @reasonId) ";

			SqlCommand cmd = new SqlCommand(sql, conn);

			cmd.Parameters.AddWithValue("@commentId", commentId);
			cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@reasonId", reasonId);


            cmd.ExecuteNonQuery();

			conn.Close();
		}

		public void DeleteRecord(Guid commentId, Guid userId)
		{
			SqlConnection conn = new SqlConnection(connection);

			conn.Open();

			string sql = $"Delete from ReportedComments " +
						 $"where CommentId = @commentId and UserId = @userId ";

			SqlCommand cmd = new SqlCommand(sql, conn);

			cmd.Parameters.AddWithValue("@commentId", commentId);
			cmd.Parameters.AddWithValue("@userId", userId);


			cmd.ExecuteNonQuery();

			conn.Close();
		}

		public void DeleteRecord(Guid commentId)
		{
			SqlConnection conn = new SqlConnection(connection);

			conn.Open();

			string sql = $"Delete from ReportedComments " +
						 $"where CommentId = @commentId ";

			SqlCommand cmd = new SqlCommand(sql, conn);

			cmd.Parameters.AddWithValue("@commentId", commentId);
		


			cmd.ExecuteNonQuery();

			conn.Close();
		}

		public bool CheckRecordExists(Guid commentId, Guid userId)
		{
			bool doesRecordExists = false;

			SqlConnection conn = new SqlConnection(connection);

			conn.Open();

			string sql = $"select count(*) as record " +
						 $"from ReportedComments " +
						 $"where CommentId = @commentId and UserId = @userId ";


			SqlCommand cmd = new SqlCommand(sql, conn);

			cmd.Parameters.AddWithValue("@commentId", commentId);


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

        public List<Guid> LoadAllReportedCommentIds()
        {
            List<Guid>commentIds = new List<Guid>();


            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            string sql = $"select Distinct(CommentId) " +
                         $"from ReportedComments ";

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

        public int GetReportCountInComment(Guid commentId)
        {
            int NumberOfReports = 0;


            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            string sql = $"select Count(CommentId) as Number " +
                         $"from ReportedComments " +
                         $"where CommentId = @commentId ";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@commentId", commentId);

            SqlDataReader dr = cmd.ExecuteReader();


            int NumberIndex = dr.GetOrdinal("Number");
            while (dr.Read())
            {
                var number = (int)dr[NumberIndex];

                NumberOfReports = number;
            }

            dr.Close();
            conn.Close();
            return NumberOfReports;
        }

    }
}
