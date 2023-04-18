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
    public class UserDB : IUserDataAccess
    {
        private string connection = "Server=mssqlstud.fhict.local;Database=dbi511464_i511464fh;User Id=dbi511464_i511464fh;Password=12345;";

        public void DeleteUser(string username)
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
            



            while (dr.Read())
            {
               

                var UserName = (string)dr[UserNameIndex];
                var Password = (string)dr[PasswordIndex];
                var Email = (string)dr[EmailIndex];
                var DateCreated = (DateTime)dr[DateCreatedIndex];

                User user = new User(UserName, Password, Email, DateCreated);

                users.Add(user);
            }


            dr.Close();

            return users;


        }

        public void SaveUser(User user)
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            string sql = "insert into Users ([UserName], [Password], [Email], [DateCreated]) " +
                         "Values (@username, @password, @email, @datecreated)";
            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@username", user.UserName);
            cmd.Parameters.AddWithValue("@password", user.Password);
            cmd.Parameters.AddWithValue("@email", user.Email ?? "null");
            cmd.Parameters.AddWithValue("@datecreated", user.DateCreated);
           

            cmd.ExecuteNonQuery();

            conn.Close();


        }

        public void UpdateUser(User user)
        {
            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            string sql = $"update Users " +
                          $"set Password = @UpdatePassword, Email = @UpdateEmail, DateCreated = @UpdateDateCreated " +
                          $" where UserName = @UpdateUserName ";



            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@UpdatePassword", user.Password);
            cmd.Parameters.AddWithValue("@UpdateEmail",user.Email);
            cmd.Parameters.AddWithValue("@UpdateUserName",user.UserName);


            cmd.Parameters.Add("@UpdateDateCreated", SqlDbType.DateTime);

            cmd.Parameters["@UpdateDateCreated"].Value = user.DateCreated;


            cmd.ExecuteNonQuery();

            conn.Close();


        }
    }
}
