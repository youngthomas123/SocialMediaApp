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
    public class CommunityMembersDB : ICommunityMembersDataAccess
    {
        private string connection = "Server=mssqlstud.fhict.local;Database=dbi511464_i511464fh;User Id=dbi511464_i511464fh;Password=12345;";

        public void CreateMember(Guid communityId, Guid UserId)
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            string sql = $"insert into CommunityMembers " +
                          $"Values (@Id, @UserId)";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@Id", communityId);
            cmd.Parameters.AddWithValue("@UserId", UserId);

            cmd.ExecuteNonQuery();
            conn.Close();

        }
        public void DeleteMember(Guid communityId, Guid UserId)
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            string sql = $"delete from CommunityMembers " +
                         $"where CommunityId = @Id and UserId = @UserId ";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@Id", communityId);
            cmd.Parameters.AddWithValue("@UserId", UserId);

            cmd.ExecuteNonQuery();

            conn.Close();

        }

        public bool CheckRecordExists(Guid communityId, Guid UserId)
        {
            bool doesRecordExists = false;

            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            string sql = $"select count(*) as record " +
                         $"from CommunityMembers " +
                         $"where CommunityId = @communityId and UserId = @userId ";


            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@communityId", communityId);


            cmd.Parameters.AddWithValue("@userId", UserId);

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
        public List<Guid> LoadUserIds(Guid CommunityId)
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            List<Guid> UserIds = new List<Guid>();

            string sql = $"Select UserId " +
                         $"from CommunityMembers " +
                         $"where CommunityId = @CommunityId ";


            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@CommunityId", CommunityId);

            SqlDataReader dr = cmd.ExecuteReader();


            int UserIdIndex = dr.GetOrdinal("UserId");

            while (dr.Read())
            {

                var userId = (Guid)dr[UserIdIndex];

                UserIds.Add(userId);
            }
            dr.Close();
            conn.Close();
            return UserIds;

        }

        public void UpdateUserId(Guid communityId, Guid UserId)
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            string sql = $"Update CommunityMembers " +
                          $"set Members = @userId " +
                          $"where CommunityId = '@Id' ";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@userId", UserId);
            cmd.Parameters.AddWithValue("@Id", communityId);

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public List<Guid>LoadCommunityIdsByMember(Guid userId)
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            List<Guid> CommunityIds = new List<Guid>();

            string sql = $"Select CommunityId " +
                         $"from CommunityMembers " +
                         $"where UserId = @userId ";


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
    }
}
