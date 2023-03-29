using SocialMedia.BusinessLogic;
using SocialMedia.BusinessLogic.Interfaces;
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
    public class CommunityDB : ICommunityDataAcess
    {
        private string connection = "Server=mssqlstud.fhict.local;Database=dbi511464_i511464fh;User Id=dbi511464_i511464fh;Password=12345;";

        public void DeleteCommunity(Guid id)
        {
            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            
            string sql = $"delete from CommunityMembers " +
                         $"where CommunityId = '{id}'; " +
                         
                         $"delete from CommunityRules " +
                         $"where CommunityId = '{id}'; " +
                        
                         $"delete from Communities " +
                         $"where CommunityId = '{id}'; ";



            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.ExecuteNonQuery();

            conn.Close();



        }

        public List<Community> LoadCommunity()
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
                Community community = new Community();
                community.DateCreated = (DateTime)dr[DateCreatedIndex];
                community.Name = (string)dr[CommunityNameIndex];
                community.Description = (string)dr[DescriptionIndex];
                community.CommunityId = (Guid)dr[CommunityIdIndex];
                community.Creator= (string)dr[CreatorIndex];

                communities.Add(community);
            }

            dr.Close();


            foreach ( Community community in communities )
            {
                sql = $"select Members " +
                      $"from CommunityMembers " +
                      $"where CommunityId = '{community.CommunityId}'";

                cmd.CommandText = sql;

                dr = cmd.ExecuteReader();


               
                int MembersIndex = dr.GetOrdinal("Members");

                List <string>members = new List<string>();  

                while (dr.Read())
                {
                    members.Add((string)dr[MembersIndex]);
                }
                community.Members = members;
                dr.Close();

            }



            foreach ( Community community in communities )
            {
                sql = $"select Rules " +
                      $"from CommunityRules " +
                      $"where CommunityId = '{community.CommunityId}';";

                cmd.CommandText = sql;

                dr = cmd.ExecuteReader();

                int RulesIndex = dr.GetOrdinal("Rules");

                List<string> rules = new List<string>();
                while (dr.Read())
                {
                    rules.Add((string)dr[RulesIndex]);
                }
                community.Rules = rules;
                dr.Close();
            }
            
           conn.Close();


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

            // next sql table



            sql = $"insert into CommunityMembers ([CommunityId], [Members])" +
                      $"Values (@CID, @memberName)";

            cmd.CommandText = sql;

            cmd.Parameters.Add("@CID", SqlDbType.UniqueIdentifier);
            cmd.Parameters.Add("@memberName", SqlDbType.VarChar);

            foreach (string member in community.Members)
            {

                cmd.Parameters["@CID"].Value = community.CommunityId;
                cmd.Parameters["@memberName"].Value = member;


                cmd.ExecuteNonQuery();

            }

            // next sql table

            sql = $"insert into CommunityRules ([CommunityId], [Rules])" +
                     $"Values (@RID, @rule) ";

            cmd.CommandText = sql;

            cmd.Parameters.Add("@RID", SqlDbType.UniqueIdentifier);
            cmd.Parameters.Add("@rule", SqlDbType.VarChar);


            foreach (string rule in community.Rules)
            {

                cmd.Parameters["@RID"].Value = community.CommunityId;
                cmd.Parameters["@rule"].Value = rule;

                cmd.ExecuteNonQuery();
            }

           conn.Close();

        }

        public void UpdateCommunity(Community community)
        {
            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            string sql =  $"update Communities " +
                          $"set DateCreated = @Updatedate, CommunityName = @UpdateName, Description = @UpdateDescription, Creator = @UpdateCreator " +
                          $"where CommunityId = '{community.CommunityId}'";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.Add("@Updatedate", SqlDbType.DateTime);
            cmd.Parameters.Add("@UpdateName", SqlDbType.VarChar);
            cmd.Parameters.Add("@UpdateDescription", SqlDbType.NVarChar);
            cmd.Parameters.Add("@UpdateCreator", SqlDbType.VarChar);


            cmd.Parameters["@Updatedate"].Value = community.DateCreated;
            cmd.Parameters["@UpdateName"].Value = community.Name;
            cmd.Parameters["@UpdateDescription"].Value = community.Description;
            cmd.Parameters["@UpdateCreator"].Value = community.Creator;

            
            cmd.ExecuteNonQuery();

            // deleting members
            sql = $"delete from CommunityMembers " +
                  $"where CommunityId = '{community.CommunityId}' ";

            cmd.CommandText= sql;

            cmd.ExecuteNonQuery ();


            // next sql table 

            sql =     $"insert into CommunityMembers([CommunityId], [Members]) " +
                      $"Values (@SMId, @Smember)";

            cmd.CommandText = sql;

            cmd.Parameters.Add("@SMId", SqlDbType.UniqueIdentifier);
            cmd.Parameters.Add("@Smember", SqlDbType.VarChar);


            foreach (string member in community.Members)
            {

                cmd.Parameters["@SMId"].Value = community.CommunityId;
                cmd.Parameters["@Smember"].Value = member;

                cmd.ExecuteNonQuery();
            }

            // deleting rules
            sql = $"delete from CommunityRules " +
                  $"where CommunityId = '{community.CommunityId}' ";

            cmd.CommandText = sql;

            cmd.ExecuteNonQuery();


            // next sql table 
            sql = $"insert into CommunityRules ([CommunityId], [Rules]) " +
                      $"Values (@SRID, @Srule) ";

            cmd.CommandText = sql;

            cmd.Parameters.Add("@SRID", SqlDbType.UniqueIdentifier);
            cmd.Parameters.Add("@Srule", SqlDbType.VarChar);

            foreach (string rule in community.Rules)
            {

                cmd.Parameters["@SRID"].Value = community.CommunityId;
                cmd.Parameters["@Srule"].Value = rule;


                cmd.ExecuteNonQuery();
            }

            conn.Close ();  


        }
    }
}
