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
			dr.Close();
			conn.Close();
			return doesRecordExists;
		}
		
		public List<Guid>LoadCommunityIdsByUser(Guid userId)
		{
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            List<Guid> CommunityIds = new List<Guid>();

            string sql = $"Select CommunityId " +
                         $"from CommunityModerators " +
                         $"where ModeratorId = @userId ";


            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@userId", userId);

            SqlDataReader dr = cmd.ExecuteReader();


            int CommunityIdIndex = dr.GetOrdinal("CommunityId");

            while (dr.Read())
            {

                var communityId = (Guid)dr[CommunityIdIndex];

                CommunityIds.Add(communityId);
            }
            dr.Close();
            conn.Close();
            return CommunityIds;
        }

		public List<Guid>GetModsInCommunity(Guid communityId)
		{
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            List<Guid> ModeratorIds = new List<Guid>();

            string sql = $"Select ModeratorId " +
                         $"from CommunityModerators " +
                         $"where CommunityId = @communityId ";


            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@communityId", communityId);

            SqlDataReader dr = cmd.ExecuteReader();


            int ModeratorIdIndex = dr.GetOrdinal("ModeratorId");

            while (dr.Read())
            {

                var ModeratorId = (Guid)dr[ModeratorIdIndex];

                ModeratorIds.Add(ModeratorId);
            }
            dr.Close();
            conn.Close();
            return ModeratorIds;
        }
	}
}
