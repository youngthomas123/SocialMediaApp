using SocialMedia.BusinessLogic;
using SocialMedia.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SocialMedia.DataAccess
{
    public class CommentDB : ICommentDataAcess
    {

       private string connection = "Server=mssqlstud.fhict.local;Database=dbi511464_i511464fh;User Id=dbi511464_i511464fh;Password=12345;";

      

        public void DeleteComment(Guid id)
        {
            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            string sql = $"delete from Comment" +
                         $" where CommentId = '{id}';";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.ExecuteNonQuery();

            conn.Close();   
        }



        public List<Comment> LoadComment()
        {
            List<Comment> list = new List<Comment>();


            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            string sql = $"select * " +
                         $"from Comments ";
                         

            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Comment comment = new Comment();
                comment.DateCreated = (DateTime)dr[0];
                comment.CommentId = (Guid)dr[1];
                comment.Creator = (string)dr[2];
                comment.Body = (string)dr[3];
                comment.Upvotes = (int)dr[4];
                comment.Downvotes = (int)dr[5];
                list.Add(comment);
            }


            dr.Close();

            return list;
        }

        public void SaveComment(Comment comment)
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            string sql = "insert into Comments ([DateCreated], [CommentId], [Creator], [Body], [Upvotes], [Downvotes])" +
                "Values (@date, @Id, @creator, @body, @upvotes, @downvotes)";



            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@date", comment.DateCreated);
            cmd.Parameters.AddWithValue("@Id", comment.CommentId);
            cmd.Parameters.AddWithValue("@creator", comment.Creator);
            cmd.Parameters.AddWithValue("@body", comment.Body);
            cmd.Parameters.AddWithValue("@upvotes", comment.Upvotes);
            cmd.Parameters.AddWithValue("@downvotes", comment.Downvotes);

            cmd.ExecuteNonQuery();



            conn.Close();
        }


        public void UpdateComment(Comment comment)
        {
            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            string sql =  $"update Comments" +
                          $"set DateCreated = '{comment.DateCreated}, Creator = '{comment.Creator}', Body = '{comment.Body}', Upvotes = '{comment.Upvotes}', Downvotes = '{comment.Downvotes}' " +
                          $" where CommentId = '{comment.CommentId}'";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.ExecuteNonQuery();

            conn.Close();

        }
    }
}
