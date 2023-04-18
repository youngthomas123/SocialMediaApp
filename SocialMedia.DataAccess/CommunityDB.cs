using SocialMedia.BusinessLogic;
using SocialMedia.BusinessLogic.Interfaces.IDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SocialMedia.DataAccess
{
    public class CommunityDB : ICommunityDataAccess
    {
        private string connection = "Server=mssqlstud.fhict.local;Database=dbi511464_i511464fh;User Id=dbi511464_i511464fh;Password=12345;";

        public void DeleteCommunity(Guid id)
        {
            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

     
            string sql = $"delete from Communities " +
                         $"where CommunityId = @Id; ";


            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@Id", id);

            cmd.ExecuteNonQuery();

            conn.Close();


        }


        public List<Community> LoadCommunitys()
        {
            List<Community> communities = new List<Community>();


            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            string sql = $"select * " +
                         $"from Communities";

    
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            int DateCreatedIndex = dr.GetOrdinal("DateCreated");
            int CommunityNameIndex = dr.GetOrdinal("CommunityName");
            int DescriptionIndex = dr.GetOrdinal("Description");
            int CommunityIdIndex = dr.GetOrdinal("CommunityId");
            int CreatorIndex = dr.GetOrdinal("Creator");


            while (dr.Read())
            {
                
                var DateCreated = (DateTime)dr[DateCreatedIndex];
                var Name = (string)dr[CommunityNameIndex];
                var Description = (string)dr[DescriptionIndex];
                var CommunityId = (Guid)dr[CommunityIdIndex];
                var Creator= (string)dr[CreatorIndex];
                Community community = new Community(DateCreated, Name, Description, CommunityId, Creator);
                communities.Add(community);
            }

            dr.Close();

            return communities;
            

        }

        public void SaveCommunity(Community community)
        {

            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            

            string sql = "insert into Communities ([DateCreated], [CommunityName], [Description], [CommunityId], [Creator]) " +
               "Values (@date, @name, @description, @Id, @creator)";



            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@date", community.DateCreated);
            cmd.Parameters.AddWithValue("@name", community.Name);
            cmd.Parameters.AddWithValue("@description", community.Description);
            cmd.Parameters.AddWithValue("@Id", community.CommunityId);
            cmd.Parameters.AddWithValue("@creator", community.Creator);
            

            cmd.ExecuteNonQuery();


           conn.Close();

        }

        public void UpdateCommunity(Community community)
        {
            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            string sql =  $"update Communities " +
                          $"set DateCreated = @Updatedate, CommunityName = @UpdateName, Description = @UpdateDescription, Creator = @UpdateCreator " +
                          $"where CommunityId = @UpdateCommunityId";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.Add("@Updatedate", SqlDbType.DateTime);
            cmd.Parameters.Add("@UpdateName", SqlDbType.VarChar);
            cmd.Parameters.Add("@UpdateDescription", SqlDbType.NVarChar);
            cmd.Parameters.Add("@UpdateCreator", SqlDbType.VarChar);
            cmd.Parameters.Add("@UpdateCommunityId", SqlDbType.UniqueIdentifier);


            cmd.Parameters["@Updatedate"].Value = community.DateCreated;
            cmd.Parameters["@UpdateName"].Value = community.Name;
            cmd.Parameters["@UpdateDescription"].Value = community.Description;
            cmd.Parameters["@UpdateCreator"].Value = community.Creator;
            cmd.Parameters["@UpdateCommunityId"].Value = community.CommunityId;


            cmd.ExecuteNonQuery();
            conn.Close ();  

        }
    }
}
