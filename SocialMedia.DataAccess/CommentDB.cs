using SocialMedia.BusinessLogic;
using SocialMedia.BusinessLogic.Interfaces.IDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SocialMedia.DataAccess
{
    public class CommentDB : ICommentDataAccess
    {

       private string connection = "Server=mssqlstud.fhict.local;Database=dbi511464_i511464fh;User Id=dbi511464_i511464fh;Password=12345;";

      

        public void DeleteComment(Guid id)
        {
            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            string sql = $"delete from Comments " +
                         $"where CommentId = @Id; ";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@Id", id);

            cmd.ExecuteNonQuery();

            conn.Close();   
        }



        public List<Comment> LoadComments()
        {
            List<Comment> comments = new List<Comment>();


            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            string sql = $"select * " +
                         $"from Comments ";
                         

            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            int DateCreatedIndex = dr.GetOrdinal("DateCreated");
            int CommentIdIndex = dr.GetOrdinal("CommentId");
            int UserIdIndex = dr.GetOrdinal("UserId");          
            int BodyIndex = dr.GetOrdinal("Body");
            int UpvotesIndex = dr.GetOrdinal("Upvotes");
            int DownvotesIndex = dr.GetOrdinal("Downvotes");
            int PostIdIndex = dr.GetOrdinal("PostId");


            while (dr.Read())
            {
                var UserId = (Guid)dr[UserIdIndex];
                var PostId = (Guid)dr[PostIdIndex];
                var Body = (string)dr[BodyIndex];
                
                var CommentId = (Guid)dr[CommentIdIndex];
                var DateCreated = (DateTime)dr[DateCreatedIndex];
                var Upvotes = (int)dr[UpvotesIndex];
                var Downvotes = (int)dr[DownvotesIndex];
                Comment comment = new Comment(DateCreated, CommentId, UserId, Body, PostId, Upvotes, Downvotes);

                comments.Add(comment);
            }


            dr.Close();

            conn.Close();

            return comments;
        }

        public void SaveComment(Comment comment)
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            string sql = "insert into Comments ([DateCreated], [CommentId], [UserId], [Body], [Upvotes], [Downvotes], [PostId]) " +
                "Values (@date, @Id, @userId, @body, @upvotes, @downvotes, @postid) ";



            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@date", comment.DateCreated);
            cmd.Parameters.AddWithValue("@Id", comment.CommentId);
            cmd.Parameters.AddWithValue("@userId", comment.UserId);
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
                          $"set DateCreated = @UpdateCommentDate, UserId = @UpdateUserId, Body = @UpdateBody, Upvotes = @UpdateUpvotes, Downvotes = @UpdateDownvotes, PostId = @UpdatePostID " +
                          $" where CommentId = @CommentID ";



            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@UpdateUserId", comment.UserId);
            cmd.Parameters.AddWithValue("@UpdateBody", comment.Body);
            cmd.Parameters.AddWithValue("@UpdateUpvotes", comment.Upvotes);
            cmd.Parameters.AddWithValue("@UpdateDownvotes", comment.Downvotes);
            cmd.Parameters.AddWithValue("@UpdatePostID", comment.PostId);
            cmd.Parameters.AddWithValue("@CommentID", comment.CommentId);
           

            cmd.Parameters.Add("@UpdateCommentDate", SqlDbType.DateTime);

            cmd.Parameters["@UpdateCommentDate"].Value = comment.DateCreated;


            cmd.ExecuteNonQuery();

            conn.Close();

        }
        public List<Comment>LoadCommentsInPost(Guid postId)
        {
            List<Comment> comments = new List<Comment>();


            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            string sql = $"select * " +
                         $"from Comments " +
                         $"where PostId = @PostId ";


            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@PostId", postId);
            SqlDataReader dr = cmd.ExecuteReader();

            int DateCreatedIndex = dr.GetOrdinal("DateCreated");
            int CommentIdIndex = dr.GetOrdinal("CommentId");
            int UserIdIndex = dr.GetOrdinal("UserId");
            int BodyIndex = dr.GetOrdinal("Body");
            int UpvotesIndex = dr.GetOrdinal("Upvotes");
            int DownvotesIndex = dr.GetOrdinal("Downvotes");
            int PostIdIndex = dr.GetOrdinal("PostId");


            while (dr.Read())
            {
                var UserId = (Guid)dr[UserIdIndex];
                var PostId = (Guid)dr[PostIdIndex];
                var Body = (string)dr[BodyIndex];

                var CommentId = (Guid)dr[CommentIdIndex];
                var DateCreated = (DateTime)dr[DateCreatedIndex];
                var Upvotes = (int)dr[UpvotesIndex];
                var Downvotes = (int)dr[DownvotesIndex];
                Comment comment = new Comment(DateCreated, CommentId, UserId, Body, PostId, Upvotes, Downvotes);

                comments.Add(comment);
            }


            dr.Close();

            conn.Close();

            return comments;
        }
        public Comment LoadCommentById(Guid commentId)
        {
            Comment Comment = null;

            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            string sql = $"select * " +
                        $"from Comments " +
                        $"where CommentId = @commentId ";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@commentId", commentId);
            SqlDataReader dr = cmd.ExecuteReader();

            int DateCreatedIndex = dr.GetOrdinal("DateCreated");
            int CommentIdIndex = dr.GetOrdinal("CommentId");
            int UserIdIndex = dr.GetOrdinal("UserId");
            int BodyIndex = dr.GetOrdinal("Body");
            int UpvotesIndex = dr.GetOrdinal("Upvotes");
            int DownvotesIndex = dr.GetOrdinal("Downvotes");
            int PostIdIndex = dr.GetOrdinal("PostId");




            while (dr.Read())
            {

                var UserId = (Guid)dr[UserIdIndex];
                var PostId = (Guid)dr[PostIdIndex];
                var Body = (string)dr[BodyIndex];

                var CommentId = (Guid)dr[CommentIdIndex];
                var DateCreated = (DateTime)dr[DateCreatedIndex];
                var Upvotes = (int)dr[UpvotesIndex];
                var Downvotes = (int)dr[DownvotesIndex];
                Comment comment = new Comment(DateCreated, CommentId, UserId, Body, PostId, Upvotes, Downvotes);



                Comment = comment;
            }


            dr.Close();

            conn.Close();
            return Comment;
        }
    }
}
