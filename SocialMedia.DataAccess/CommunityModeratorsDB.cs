using SocialMedia.BusinessLogic.Interfaces.IDataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.DataAccess
{
	public class CommunityModeratorsDB : ICommunityModeratorsDataAccess
	{
		private string connection = "Server=mssqlstud.fhict.local;Database=dbi511464_i511464fh;User Id=dbi511464_i511464fh;Password=12345;";

		public void CreateRecord(Guid communityId, Guid moderatorId)
		{
			SqlConnection conn = new SqlConnection(connection);

			conn.Open();

			string sql = $"insert into CommunityModerators ([CommunityId], [ModeratorId]) " +
						 $"Values (@communityId, @moderatorId) ";

			SqlCommand cmd = new SqlCommand(sql, conn);

			cmd.Parameters.AddWithValue("@communityId", communityId);
			cmd.Parameters.AddWithValue("@moderatorId", moderatorId);


			cmd.ExecuteNonQuery();

			conn.Close();
		}

		public void DeleteRecord(Guid communityId, Guid moderatorId)
		{
			SqlConnection conn = new SqlConnection(connection);

			conn.Open();

			string sql = $"Delete from CommunityModerators " +
						 $"where CommunityId = @communityId and ModeratorId = @moderatorId ";

			SqlCommand cmd = new SqlCommand(sql, conn);

			cmd.Parameters.AddWithValue("@communityId", communityId);
			cmd.Parameters.AddWithValue("@moderatorId", moderatorId);


			cmd.ExecuteNonQuery();

			conn.Close();
		}

		public bool CheckRecordExists(Guid communityId, Guid moderatorId)
		{
			bool doesRecordExists = false;

			SqlConnection conn = new SqlConnection(connection);

			conn.Open();

			string sql = $"select count(*) as record " +
						 $"from CommunityModerators " +
						 $"where CommunityId = @communityId and ModeratorId = @moderatorId ";


			SqlCommand cmd = new SqlCommand(sql, conn);

			cmd.Parameters.AddWithValue("@communityId", communityId);


			cmd.Parameters.AddWithValue("@moderatorId", moderatorId);

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
			return doesRecordExists;
		}
	}
}
