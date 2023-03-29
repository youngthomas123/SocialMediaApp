using SocialMedia.BusinessLogic;
using SocialMedia.BusinessLogic.Interfaces;
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
    public class UserDB : IUserDataAcess
    {
        private string connection = "Server=mssqlstud.fhict.local;Database=dbi511464_i511464fh;User Id=dbi511464_i511464fh;Password=12345;";

        public void DeleteUser(string username)
        {
            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            string sql = $"delete from Users " +
                         $"where UserName = '{username}'; ";

            SqlCommand cmd = new SqlCommand(sql, conn);

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
                User user = new User();

                user.UserName = (string)dr[UserNameIndex];
                user.Password = (string)dr[PasswordIndex];
                user.Email = (string)dr[EmailIndex];
                user.DateCreated = (DateTime)dr[DateCreatedIndex];
                


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
                          $"set Password = '{user.Password}', Email = '{user.Email}', DateCreated = @UpdateDateCreated " +
                          $" where UserName = '{user.UserName}'";



            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.Add("@UpdateDateCreated", SqlDbType.DateTime);

            cmd.Parameters["@UpdateDateCreated"].Value = user.DateCreated;


            cmd.ExecuteNonQuery();

            conn.Close();


        }
    }
}
