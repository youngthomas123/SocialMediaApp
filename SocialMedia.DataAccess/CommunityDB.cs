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
                         $"where CommunityId = @Id; " +
                         
                         $"delete from CommunityRules " +
                         $"where CommunityId = @Id; " +
                        
                         $"delete from Communities " +
                         $"where CommunityId = @Id; ";



            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@Id", id);

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
                
                var DateCreated = (DateTime)dr[DateCreatedIndex];
                var Name = (string)dr[CommunityNameIndex];
                var Description = (string)dr[DescriptionIndex];
                var CommunityId = (Guid)dr[CommunityIdIndex];
                var Creator= (string)dr[CreatorIndex];
                Community community = new Community(DateCreated, Name, Description, CommunityId, Creator);
                communities.Add(community);
            }

            dr.Close();


            foreach (Community community in communities )
            {
                sql = $"select Members " +
                      $"from CommunityMembers " +
                      $"where CommunityId = @CommunityIdMLoad;";

                cmd.Parameters.AddWithValue("@CommunityIdMLoad", community.CommunityId);
                //cmd.Parameters["@CommunityIdMLoad"].Value = community.CommunityId;

                cmd.CommandText = sql;

                
                

                dr = cmd.ExecuteReader();


               
                int MembersIndex = dr.GetOrdinal("Members");

                List <string>members = new List<string>();  

                while (dr.Read())
                {
                    members.Add((string)dr[MembersIndex]);
                }
                community.Members = members;
                cmd.Parameters.Clear();
                dr.Close();

            }



            foreach ( Community community in communities )
            {
                sql = $"select Rules " +
                      $"from CommunityRules " +
                      $"where CommunityId = @CommunityIdR ;";

                cmd.CommandText = sql;

                cmd.Parameters.AddWithValue("@CommunityIdR", community.CommunityId);

                dr = cmd.ExecuteReader();

                int RulesIndex = dr.GetOrdinal("Rules");

                List<string> rules = new List<string>();
                while (dr.Read())
                {
                    rules.Add((string)dr[RulesIndex]);
                }
                community.Rules = rules;
                cmd.Parameters.Clear();
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

            // deleting members
            sql = $"delete from CommunityMembers " +
                  $"where CommunityId = @CommunityIdDM ";

            cmd.CommandText= sql;

            cmd.Parameters.AddWithValue("@CommunityIdDM", community.CommunityId);

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
                  $"where CommunityId = @CommunityIdDR ";

            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue("@CommunityIdDR", community.CommunityId);

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
