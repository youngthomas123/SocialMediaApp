using SocialMedia.BusinessLogic;
using SocialMedia.BusinessLogic.Interfaces.IDataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
		public Community LoadCommunity(string communityName)
        {
            Community Community = null;
			SqlConnection conn = new SqlConnection(connection);

			conn.Open();

			string sql = $"select * " +
						 $"from Communities " +
                         $"where CommunityName = @communityName ";


			SqlCommand cmd = new SqlCommand(sql, conn);
			cmd.Parameters.AddWithValue("@communityName", communityName);
			SqlDataReader dr = cmd.ExecuteReader();

			int DateCreatedIndex = dr.GetOrdinal("DateCreated");
			int CommunityNameIndex = dr.GetOrdinal("CommunityName");
			int DescriptionIndex = dr.GetOrdinal("Description");
			int CommunityIdIndex = dr.GetOrdinal("CommunityId");
			int UserIdIndex = dr.GetOrdinal("UserId");


			while (dr.Read())
			{

				var DateCreated = (DateTime)dr[DateCreatedIndex];
				var Name = (string)dr[CommunityNameIndex];
				var Description = (string)dr[DescriptionIndex];
				var CommunityId = (Guid)dr[CommunityIdIndex];
				var UserId = (Guid)dr[UserIdIndex];
				Community community = new Community(DateCreated, Name, Description, CommunityId, UserId);
				Community = community;
			}

			dr.Close();
            conn.Close();

			return Community;


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
            int UserIdIndex = dr.GetOrdinal("UserId");


            while (dr.Read())
            {
                
                var DateCreated = (DateTime)dr[DateCreatedIndex];
                var Name = (string)dr[CommunityNameIndex];
                var Description = (string)dr[DescriptionIndex];
                var CommunityId = (Guid)dr[CommunityIdIndex];
                var UserId= (Guid)dr[UserIdIndex];
                Community community = new Community(DateCreated, Name, Description, CommunityId, UserId);
                communities.Add(community);
            }

            dr.Close();
            conn.Close();

            return communities;
            

        }

        public void SaveCommunity(Community community)
        {

            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            

            string sql = "insert into Communities ([DateCreated], [CommunityName], [Description], [CommunityId], [UserId]) " +
               "Values (@date, @name, @description, @Id, @userId)";



            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@date", community.DateCreated);
            cmd.Parameters.AddWithValue("@name", community.CommunityName);
            cmd.Parameters.AddWithValue("@description", community.Description);
            cmd.Parameters.AddWithValue("@Id", community.CommunityId);
            cmd.Parameters.AddWithValue("@userId", community.UserId);
            

            cmd.ExecuteNonQuery();


           conn.Close();

        }

        public void UpdateCommunity(Community community)
        {
            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            string sql =  $"update Communities " +
                          $"set DateCreated = @Updatedate, CommunityName = @UpdateName, Description = @UpdateDescription, UserId = @UpdateUserId " +
                          $"where CommunityId = @UpdateCommunityId";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.Add("@Updatedate", SqlDbType.DateTime);
            cmd.Parameters.Add("@UpdateName", SqlDbType.VarChar);
            cmd.Parameters.Add("@UpdateDescription", SqlDbType.NVarChar);
            cmd.Parameters.Add("@UpdateUserId", SqlDbType.UniqueIdentifier);
            cmd.Parameters.Add("@UpdateCommunityId", SqlDbType.UniqueIdentifier);


            cmd.Parameters["@Updatedate"].Value = community.DateCreated;
            cmd.Parameters["@UpdateName"].Value = community.CommunityName;
            cmd.Parameters["@UpdateDescription"].Value = community.Description;
            cmd.Parameters["@UpdateUserId"].Value = community.UserId;
            cmd.Parameters["@UpdateCommunityId"].Value = community.CommunityId;


            cmd.ExecuteNonQuery();
            conn.Close ();  

        }
        public List<string> GetCommunityNames()
        {
            List<string> Communitynames = new List<string>();


            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            string sql = $"select CommunityName " +
                         $"from Communities";

            SqlCommand cmd = new SqlCommand(sql, conn);
            

            

            SqlDataReader dr = cmd.ExecuteReader();

            int CommunityNameIndex = dr.GetOrdinal("CommunityName");

            while (dr.Read())
            {

               
                var CommunityName = (string)dr[CommunityNameIndex];

                Communitynames.Add(CommunityName);
            }

            dr.Close();

            conn.Close();
            return Communitynames;


        }
        public string GetCommunityId(string communityname)
        {

            string communityId = null;

            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            string sql = $"select CommunityId " +
                         $"from Communities " +
                         $"where CommunityName = @communityName ";


            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@communityName", communityname);

            SqlDataReader dr = cmd.ExecuteReader();


            int CommunityIdIndex = dr.GetOrdinal("CommunityId");

            while (dr.Read())
            {

                var CommunityId = (Guid)dr[CommunityIdIndex];
                communityId = CommunityId.ToString();
            }

            dr.Close();

            conn.Close();   

            return communityId;
        }
        public List<Array> GetCommunityNameAndId()
        {
            List <Array> arrays = new List<Array>();

            

            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            string sql = $"select CommunityName, CommunityId " +
                         $"from Communities ";


            SqlCommand cmd = new SqlCommand(sql, conn);

            

            SqlDataReader dr = cmd.ExecuteReader();

            int CommunityNameIndex = dr.GetOrdinal("CommunityName");
            int CommunityIdIndex = dr.GetOrdinal("CommunityId");

            string communityId;
            while (dr.Read())
            {
                var CommunityName = (string)dr[CommunityNameIndex];
                var CommunityId = (Guid)dr[CommunityIdIndex];

                communityId = CommunityId.ToString();   

                string[] arr = new string[] { CommunityName, communityId };

                arrays.Add(arr);    
            }

            dr.Close();

            conn.Close();

            return arrays;
        }
        public string GetCommunityName(Guid CommunityId)
        {
            string communityName = null;

            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            string sql = $"select CommunityName " +
                         $"from Communities " +
                         $"where CommunityId = @communityId ";


            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@communityId", CommunityId);

            SqlDataReader dr = cmd.ExecuteReader();


            int CommunityNameIndex = dr.GetOrdinal("CommunityName");

            while (dr.Read())
            {

                var CommunityName = (string)dr[CommunityNameIndex];
                communityName = CommunityName;
            }

            dr.Close();

            conn.Close();

            return communityName;
        }
		public List<string[]>? SearchCommunityAndId(string searchquery)
		{


			List<string[]> CommunityNamesAndIds = new List<string[]>();


			SqlConnection conn = new SqlConnection(connection);

			conn.Open();

			string sql = $"Select CommunityName, CommunityId " +
						 $"from Communities " +
						 $"where CommunityName like @SearchQuery ";


			SqlCommand cmd = new SqlCommand(sql, conn);

			cmd.Parameters.AddWithValue("@SearchQuery", "%" + searchquery + "%");




			SqlDataReader dr = cmd.ExecuteReader();

			int CommunityNameIndex = dr.GetOrdinal("CommunityName");
			int CommunityIdIndex = dr.GetOrdinal("CommunityId");

			while (dr.Read())
			{
				var CommunityName = (string)dr[CommunityNameIndex];
				var CommunityId = (Guid)dr[CommunityIdIndex];



				string[] CommunityNameAndId = { CommunityName, CommunityId.ToString() };

				CommunityNamesAndIds.Add(CommunityNameAndId);
			}


			dr.Close();

			conn.Close();
			return CommunityNamesAndIds;

		}
        public bool DoesCommunityIdExist(Guid communityId)
        {
            bool doesRecordExists = false;
            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            string sql = $"select count(CommunityId) as record " +
                         $"from Communities " +
                         $"where CommunityId = @communityId ";


            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@communityId", communityId);




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

        public bool DoesCommunityNameExist(string communityName)
        {
            bool doesRecordExists = false;
            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            string sql = $"select count(CommunityName) as record " +
                         $"from Communities " +
                         $"where CommunityName = @communityName ";


            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@communityName", communityName);




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

        public List<string>GetCommunityNamesCreatedByUser(Guid userId)
        {
            List<string> Communitynames = new List<string>();


            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            string sql = $"select CommunityName " +
                         $"from Communities " +
                         $"where UserId = @userId";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@userId", userId);


            SqlDataReader dr = cmd.ExecuteReader();

            int CommunityNameIndex = dr.GetOrdinal("CommunityName");

            while (dr.Read())
            {


                var CommunityName = (string)dr[CommunityNameIndex];

                Communitynames.Add(CommunityName);
            }

            dr.Close();

            conn.Close();
            return Communitynames;
        }

	}

    
}
