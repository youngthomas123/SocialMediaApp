using SocialMedia.BusinessLogic;
using SocialMedia.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
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

            string sql = $"delete from Comments " +
                         $"where CommentId = '{id}'; ";

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

            int DateCreatedIndex = dr.GetOrdinal("DateCreated");
            int CommentIdIndex = dr.GetOrdinal("CommentId");
            int CreatorIndex = dr.GetOrdinal("Creator");          
            int BodyIndex = dr.GetOrdinal("Body");
            int UpvotesIndex = dr.GetOrdinal("Upvotes");
            int DownvotesIndex = dr.GetOrdinal("Downvotes");
            int PostIdIndex = dr.GetOrdinal("PostId");


            while (dr.Read())
            {
                Comment comment = new Comment();
                comment.DateCreated = (DateTime)dr[DateCreatedIndex];
                comment.CommentId = (Guid)dr[CommentIdIndex];
                comment.Creator = (string)dr[CreatorIndex];
                comment.Body = (string)dr[BodyIndex];
                comment.Upvotes = (int)dr[UpvotesIndex];
                comment.Downvotes = (int)dr[DownvotesIndex];
                comment.PostId = (Guid)dr[PostIdIndex];
                list.Add(comment);
            }


            dr.Close();

            return list;
        }

        public void SaveComment(Comment comment)
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            string sql = "insert into Comments ([DateCreated], [CommentId], [Creator], [Body], [Upvotes], [Downvotes], [PostId]) " +
                "Values (@date, @Id, @creator, @body, @upvotes, @downvotes, @postid)";



            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@date", comment.DateCreated);
            cmd.Parameters.AddWithValue("@Id", comment.CommentId);
            cmd.Parameters.AddWithValue("@creator", comment.Creator);
            cmd.Parameters.AddWithValue("@body", comment.Body);
            cmd.Parameters.AddWithValue("@upvotes", comment.Upvotes);
            cmd.Parameters.AddWithValue("@downvotes", comment.Downvotes);
            cmd.Parameters.AddWithValue("@postid", comment.PostId);

            cmd.ExecuteNonQuery();



            conn.Close();
        }


        public void UpdateComment(Comment comment)
        {
            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            string sql =  $"update Comments " +
                          $"set DateCreated = @UpdateCommentDate, Creator = '{comment.Creator}', Body = '{comment.Body}', Upvotes = '{comment.Upvotes}', Downvotes = '{comment.Downvotes}', PostId = '{comment.PostId}' " +
                          $" where CommentId = '{comment.CommentId}'";



            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.Add("@UpdateCommentDate", SqlDbType.DateTime);

            cmd.Parameters["@UpdateCommentDate"].Value = comment.DateCreated;


            cmd.ExecuteNonQuery();

            conn.Close();

        }
    }
}
