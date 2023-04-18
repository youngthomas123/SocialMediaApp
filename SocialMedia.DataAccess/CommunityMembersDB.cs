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

        public void CreateMember(Guid communityId, string member)
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            string sql = $"insert into CommunityMembers " +
                          $"Values (@Id, @Member)";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@Id", communityId);
            cmd.Parameters.AddWithValue("@Member", member);

            cmd.ExecuteNonQuery();
            conn.Close();

        }
        public void DeleteMember(Guid communityId, string member)
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            string sql = $"delete from CommunityMembers " +
                         $"where CommunityId = '@Id' and Members = '@Member' ";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@Id", communityId);
            cmd.Parameters.AddWithValue("@Members", member);

            cmd.ExecuteNonQuery();

            conn.Close();

        }
        public List<string> LoadMembers(Guid CommunityId)
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            List<string> members = new List<string>();

            string sql = $"Select Members " +
                         $"from CommunityMembers " +
                         $"where CommunityId = @CommunityId ";


            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@CommunityId", CommunityId);

            SqlDataReader dr = cmd.ExecuteReader();


            int MemberIndex = dr.GetOrdinal("Members");

            while (dr.Read())
            {

                var member = (string)dr[MemberIndex];

                members.Add(member);
            }
            dr.Close();
            conn.Close();
            return members;

        }

        public void UpdateMember(Guid communityId, string member)
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            string sql = $"Update CommunityMembers " +
                          $"set Members = '@member' " +
                          $"where CommunityId = '@Id' ";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@member", member);
            cmd.Parameters.AddWithValue("@Id", communityId);

            cmd.ExecuteNonQuery();

            conn.Close();
        }
    }
}
