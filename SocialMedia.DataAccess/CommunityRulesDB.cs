using SocialMedia.BusinessLogic;
using SocialMedia.BusinessLogic.Interfaces.IDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SocialMedia.DataAccess
{
    public class CommunityRulesDB : ICommunityRulesDataAccess
    {
        

        private string connection = "Server=mssqlstud.fhict.local;Database=dbi511464_i511464fh;User Id=dbi511464_i511464fh;Password=12345;";
        public void CreateRule(Guid communityId, string rule)
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            string sql = $"insert into CommunityRules " +
                          $"Values (@Id, @Rule)";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@Id", communityId);
            cmd.Parameters.AddWithValue("@Rule", rule);

            cmd.ExecuteNonQuery();
            conn.Close();

        }

        public void DeleteRule(Guid communityId, string rule)
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            string sql = $"delete from CommunityRules " +
                         $"where CommunityId = '@Id' and Rules = '@Rule' ";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@Id", communityId);
            cmd.Parameters.AddWithValue("@Rule", rule);

            cmd.ExecuteNonQuery();

            conn.Close();

        }

        public List<string> LoadRules(Guid CommunityId)
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            List <string>rules = new List<string>();

            string sql = $"Select Rules " +
                         $"from CommunityRules " +
                         $"where CommunityId = @CommunityId ";


            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@CommunityId", CommunityId);

            SqlDataReader dr = cmd.ExecuteReader();

            
            int RulesIndex = dr.GetOrdinal("Rules");

            while (dr.Read())
            {
                
                var rule = (string)dr[RulesIndex];
              
                rules.Add(rule);
            }
            dr.Close(); 
            conn.Close();
            return rules;

        }

        public void UpdateRule(Guid communityId, string rule)
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            string sql =  $"Update CommunityRules " +
                          $"set Rules = '@rule' " +
                          $"where CommunityId = '@Id' ";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@rule", rule);
            cmd.Parameters.AddWithValue("@Id", communityId);

            cmd.ExecuteNonQuery();

            conn.Close();
        }
    }
}
