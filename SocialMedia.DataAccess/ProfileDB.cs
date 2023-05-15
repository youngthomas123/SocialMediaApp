using SocialMedia.BusinessLogic;
using SocialMedia.BusinessLogic.Dto;
using SocialMedia.BusinessLogic.Interfaces.IDataAccess;
using System;
using System.Collections.Generic;
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


        public void CreateRecord(Guid userId, string username, string bio, string gender, byte[] picture, string location)
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            string sql = $"insert into Profle ([UserId], [UserName], [Bio], [Gender], [ProfilePic], [Location]) " +
                         $"Values (@userId, @userName, @bio, @gender, @profilePic, @location) ";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@userName", username);
            cmd.Parameters.AddWithValue("@bio", bio );
            cmd.Parameters.AddWithValue("@gender", gender);
            cmd.Parameters.AddWithValue("@profilePic", picture);
            cmd.Parameters.AddWithValue("@location", location);


            cmd.ExecuteNonQuery();

            conn.Close();
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
        public void UpdateRecord(Guid userId, string username, string bio, string gender, byte[] picture, string location)
        {
            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            string sql = $"Update profile " +
                         $"set UserName = @UpdateUserName, Bio = @UpdateBio, Gender = @UpdateGender, ProfilePic = @UpdateProfilePic, Location = @UpdateLocation " +
                         $"where UserId = @userId";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@UpdateUserName", username);
            cmd.Parameters.AddWithValue("@UpdateBio", bio);
            cmd.Parameters.AddWithValue("@UpdateGender", gender);
            cmd.Parameters.AddWithValue("@UpdateProfilePic", picture);
            cmd.Parameters.AddWithValue("@UpdateLocation", location);
            cmd.Parameters.AddWithValue("@userId", userId);

            cmd.ExecuteNonQuery();

            conn.Close();

        }

        public void UpdateRecord(Guid userId, string username, string bio, string gender, string location)
        {
            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            string sql = $"Update profile " +
                         $"set UserName = @UpdateUserName, Bio = @UpdateBio, Gender = @UpdateGender, Location = @UpdateLocation " +
                         $"where UserId = @userId";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@UpdateUserName", username);
            cmd.Parameters.AddWithValue("@UpdateBio", bio);
            cmd.Parameters.AddWithValue("@UpdateGender", gender);
            
            cmd.Parameters.AddWithValue("@UpdateLocation", location);
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
            return profile;

        }
    }
}
