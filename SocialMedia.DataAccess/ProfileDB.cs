using SocialMedia.BusinessLogic;
using SocialMedia.BusinessLogic.Dto;
using SocialMedia.BusinessLogic.Interfaces.IDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.DataAccess
{
    public class ProfileDB : IProfileDataAccess
    {
        private string connection = "Server=mssqlstud.fhict.local;Database=dbi511464_i511464fh;User Id=dbi511464_i511464fh;Password=12345;";


        
        
       

        public void CreateRecord(Guid userId, string username)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                string sql = "INSERT INTO Profile ([UserId], [UserName]) " +
                             "VALUES (@userId, @userName)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@userName", username);
              
                cmd.ExecuteNonQuery();

                conn.Close();
            }
        }




        public void DeleteRecord(string userId)
        {
            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            string sql =$"delete into Profle  " +
                        $"where UserId = @userId ";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@userId", userId);
          


            cmd.ExecuteNonQuery();

            conn.Close();
        }
        public void UpdateRecord(Guid userId, string username, string? bio, string? gender, byte[] picture, string? location)
        {
            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            string sql = $"Update profile " +
                         $"set UserName = @UpdateUserName, Bio = @UpdateBio, Gender = @UpdateGender, ProfilePic = @UpdateProfilePic, Location = @UpdateLocation " +
                         $"where UserId = @userId";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@UpdateUserName", username);
            cmd.Parameters.AddWithValue("@UpdateBio", (object)bio ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UpdateGender", (object)gender ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UpdateProfilePic", picture );
            cmd.Parameters.AddWithValue("@UpdateLocation", (object)location ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@userId", userId);

            cmd.ExecuteNonQuery();

            conn.Close();

        }

        public void UpdateRecord(Guid userId, string username, string? bio, string? gender, string? location)
        {
            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            string sql = $"Update profile " +
                         $"set UserName = @UpdateUserName, Bio = @UpdateBio, Gender = @UpdateGender, Location = @UpdateLocation " +
                         $"where UserId = @userId";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@UpdateUserName", username);
            cmd.Parameters.AddWithValue("@UpdateBio", (object)bio ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UpdateGender", (object)gender ?? DBNull.Value);
            
            cmd.Parameters.AddWithValue("@UpdateLocation", (object)location ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@userId", userId);

            cmd.ExecuteNonQuery();

            conn.Close();

        }

        public void UpdateProfilePicture(Guid UserId, byte[] picture)
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            string sql = $"Update Profile " +
                         $"set ProfilePic = @pic " +
                         $"where UserId = @userId";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@pic", picture);
            cmd.Parameters.AddWithValue("@userId", UserId);

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public byte[] GetProfilePicture(Guid UserId)
        {

            byte[] picture = null;  

            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            string sql = $"select ProfilePic " +
                         $"From Profile " +
                         $"where UserId = @userId";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@userId", UserId);

            SqlDataReader dr = cmd.ExecuteReader();

            int ProfilePicIndex = dr.GetOrdinal("ProfilePic");

            while (dr.Read())
            {


                
                var ProfilePic = (byte[])dr[ProfilePicIndex];

                picture = ProfilePic;

             
            }
            dr.Close();
            conn.Close();

            return picture;
        }

        public ProfileDto LoadProfileRecord(Guid userId)
        {
            ProfileDto profile = new ProfileDto();

            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            string sql = $"Select * " +
                         $"from Profile " +
                         $"where UserId = @userId";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@userId", userId);

            SqlDataReader dr = cmd.ExecuteReader();

            int UserIdIndex = dr.GetOrdinal("UserId");
            int UserNameIndex = dr.GetOrdinal("UserName");
            int BioIndex = dr.GetOrdinal("Bio");
            int GenderIndex = dr.GetOrdinal("Gender");
            int ProfilePicIndex = dr.GetOrdinal("ProfilePic");
            int LocationIndex = dr.GetOrdinal("Location");




            while (dr.Read())
            {


                var UserId = (Guid)dr[UserIdIndex];
                var UserName = (string)dr[UserNameIndex];
                var Bio = dr.IsDBNull(BioIndex) ? null : (string)dr[BioIndex];
                var Gender = dr.IsDBNull(GenderIndex) ? null : (string)dr[GenderIndex];
                var  ProfilePic = dr.IsDBNull(ProfilePicIndex) ? null : (byte[])dr[ProfilePicIndex];
                var Location = dr.IsDBNull(LocationIndex) ? null : (string)dr[LocationIndex];






                profile.UserId = userId;
                profile.UserName = UserName;
                profile.Bio = Bio;
                profile.Gender = Gender;
                profile.ProfilePic = ProfilePic;
                profile.Location = Location;



            }

            dr.Close();
            conn.Close();
            return profile;

        }
    }
}
