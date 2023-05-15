﻿using SocialMedia.BusinessLogic;
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
    public class UserDB : IUserDataAccess
    {
        private string connection = "Server=mssqlstud.fhict.local;Database=dbi511464_i511464fh;User Id=dbi511464_i511464fh;Password=12345;";

        public void DeleteUserByUserName(string username)
        {
            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            string sql = $"delete from Users " +
                         $"where UserName = @Username; ";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@Username", username);

            cmd.ExecuteNonQuery();

            conn.Close();


        }

        public void DeleteUserById(Guid Id)
        {
            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            string sql = $"delete from Users " +
                         $"where UserName = @Id; ";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@Id", Id);

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public List<User> LoadUser()
        {
            List<User> users = new List<User>();


            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            string sql = $"select * " +
                         $"from Users ";


            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            int UserNameIndex = dr.GetOrdinal("UserName");
            int PasswordIndex = dr.GetOrdinal("Password");
            int EmailIndex = dr.GetOrdinal("Email");
            int DateCreatedIndex = dr.GetOrdinal("DateCreated");
            int UserIdIndex = dr.GetOrdinal("UserId");
            int SaltIndex = dr.GetOrdinal("Salt");



            while (dr.Read())
            {


                var UserName = (string)dr[UserNameIndex];
                var Password = (string)dr[PasswordIndex];
                var Email = (string)dr[EmailIndex];
                var DateCreated = (DateTime)dr[DateCreatedIndex];
                var UserId = (Guid)dr[UserIdIndex];
                var Salt = (string)dr[SaltIndex];

                User user = new User(UserId, UserName, Password, Salt, Email, DateCreated);

                users.Add(user);
            }


            dr.Close();

            return users;


        }

        public void SaveUser(User user)
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            string sql = "insert into Users ([UserName], [Password], [Email], [DateCreated], [Salt], [UserId]) " +
                         "Values (@username, @password, @email, @datecreated, @salt, @UserId)";
            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@username", user.UserName);
            cmd.Parameters.AddWithValue("@password", user.Password);
            cmd.Parameters.AddWithValue("@email", user.Email ?? "null");
            cmd.Parameters.AddWithValue("@datecreated", user.DateCreated);
            cmd.Parameters.AddWithValue("@salt", user.Salt);
            cmd.Parameters.AddWithValue("@UserId", user.UserId);


            cmd.ExecuteNonQuery();

            conn.Close();


        }

        public void UpdateUserById(User user)
        {
            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            string sql = $"update Users " +
                          $"set Password = @UpdatePassword, Email = @UpdateEmail, DateCreated = @UpdateDateCreated, UserName = @UpdateUserName, Salt = @UpdateSalt " +
                          $" where UserId = @UserId ";



            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@UpdatePassword", user.Password);
            cmd.Parameters.AddWithValue("@UpdateEmail", user.Email);
            cmd.Parameters.AddWithValue("@UpdateUserName", user.UserName);
            cmd.Parameters.AddWithValue("@UserId", user.UserId);
            cmd.Parameters.AddWithValue("@UpdateSalt", user.Salt);

            cmd.Parameters.Add("@UpdateDateCreated", SqlDbType.DateTime);

            cmd.Parameters["@UpdateDateCreated"].Value = user.DateCreated;


            cmd.ExecuteNonQuery();

            conn.Close();


        }
        public List<string> GetUserNames()
        {
            List<string> userNames = new List<string>();

            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            string sql = $"Select UserName " +
                         $"from Users ";

            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            int UserNameIndex = dr.GetOrdinal("UserName");


            while (dr.Read())
            {


                var UserName = (string)dr[UserNameIndex];



                userNames.Add(UserName);
            }


            dr.Close();

            return userNames;


        }
        public string? GetSalt(string username)
        {
            string salt = null;

            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            string sql = $"Select Salt " +
                         $"from Users " +
                         $"where UserName = @username ";


            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@username", username);

            SqlDataReader dr = cmd.ExecuteReader();

            int SaltIndex = dr.GetOrdinal("Salt");

            while (dr.Read())
            {
                var Salt = (string)dr[SaltIndex];
                salt = Salt;
            }


            dr.Close();

            return salt;

        }
        public string? GetPassword(string username)
        {
            string password = null;

            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            string sql = $"Select Password " +
                         $"from Users " +
                         $"where UserName = @username ";


            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@username", username);

            SqlDataReader dr = cmd.ExecuteReader();

            int PasswordIndex = dr.GetOrdinal("Password");

            while (dr.Read())
            {
                var Password = (string)dr[PasswordIndex];
                password = Password;
            }


            dr.Close();

            return password;
        }
        public string? GetUserId(string username)
        {
            string userId = null;

            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            string sql = $"Select UserId " +
                         $"from Users " +
                         $"where UserName = @username ";


            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@username", username);

            SqlDataReader dr = cmd.ExecuteReader();

            int UserIdIndex = dr.GetOrdinal("UserId");

            while (dr.Read())
            {
                var UserId = (Guid)dr[UserIdIndex];
                userId = UserId.ToString();
            }


            dr.Close();

            conn.Close();

            return userId;
        }
        public string GetUserName(Guid UserId)
        {
            string userName = null;

            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            string sql = $"Select UserName " +
                         $"from Users " +
                         $"where UserId = @userId ";


            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@userId", UserId);

            SqlDataReader dr = cmd.ExecuteReader();

            int UserNameIndex = dr.GetOrdinal("UserName");

            while (dr.Read())
            {
                var UserName = (string)dr[UserNameIndex];
                userName = UserName;
            }


            dr.Close();

            conn.Close();
            return userName;
        }
        public List <string[]>? SearchUserNameAndId(string searchquery)
        {


            List <string[]> UserNamesAndIds = new List<string[]>();
            

			SqlConnection conn = new SqlConnection(connection);

			conn.Open();

			string sql = $"Select UserName, UserId " +
						 $"from Users " +
						 $"where UserName like @SearchQuery ";


			SqlCommand cmd = new SqlCommand(sql, conn);

			cmd.Parameters.AddWithValue("@SearchQuery", "%" + searchquery + "%");


			

			SqlDataReader dr = cmd.ExecuteReader();

			int UserNameIndex = dr.GetOrdinal("UserName");
			int UserIdIndex = dr.GetOrdinal("UserId");

			while (dr.Read())
			{
				var UserName = (string)dr[UserNameIndex];
                var UserId = (Guid)dr[UserIdIndex];
               


				string[] UserNameAndId = { UserName, UserId.ToString() };

				UserNamesAndIds.Add(UserNameAndId);
			}


			dr.Close();

			conn.Close();
			return UserNamesAndIds;

		}

    }
}
