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
    public class PostDB : IPostDataAcess
    {
        private string connection = "Server=mssqlstud.fhict.local;Database=dbi511464_i511464fh;User Id=dbi511464_i511464fh;Password=12345;";

        public void DeletePost(Guid id)
        {
            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            string sql = $"delete from Posts " +
                         $"where PostId = @Id; ";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@Id", id);

            cmd.ExecuteNonQuery();

            conn.Close();

        }

        public List<Post> LoadPost()
        {
            List<Post> posts = new List<Post>();


            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            string sql = $"select * " +
                         $"from Posts ";


            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            int DateCreatedIndex = dr.GetOrdinal("DateCreated");
            int PostIdIndex = dr.GetOrdinal("PostId");
            int CreatorIndex = dr.GetOrdinal("Creator");
            int TitleIndex = dr.GetOrdinal("Title");
            int BodyIndex = dr.GetOrdinal("Body");
            int UpvotesIndex = dr.GetOrdinal("Upvotes");
            int DownvotesIndex = dr.GetOrdinal("Downvotes");
            int CommunityIdIndex = dr.GetOrdinal("CommunityId");



            while (dr.Read())
            {
                

                var DateCreated = (DateTime)dr[DateCreatedIndex];
                var PostId = (Guid)dr[PostIdIndex];
                var Creator = (string)dr[CreatorIndex];
                var Title = (string)dr[TitleIndex];
                var Body = (string)dr[BodyIndex];
                var Upvotes = (int)dr[UpvotesIndex];
                var Downvotes = (int)dr[DownvotesIndex];
                var CommunityId = (Guid)dr[CommunityIdIndex];

                Post post = new Post(DateCreated, PostId, Creator, Title, Body, Upvotes, Downvotes, CommunityId);
                posts.Add(post);
            }


            dr.Close();

            return posts;


        }

        public void SavePost(Post post)
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            string sql = "insert into Posts ([DateCreated], [PostId], [Creator], [Title], [Body], [Upvotes], [Downvotes], [CommunityId]) " +
                "Values (@date, @postId, @creator, @title, @body, @upvotes, @downvotes, @communityId)";



            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@date", post.DateCreated);
            cmd.Parameters.AddWithValue("@postId", post.PostId);
            cmd.Parameters.AddWithValue("@creator", post.Creator);
            cmd.Parameters.AddWithValue("@title", post.Title);
            cmd.Parameters.AddWithValue("@body", post.Body);
            cmd.Parameters.AddWithValue("@upvotes", post.Upvotes);
            cmd.Parameters.AddWithValue("@downvotes", post.Downvotes);
            cmd.Parameters.AddWithValue("@communityId", post.CommunityId);


            cmd.ExecuteNonQuery();



            conn.Close();


        }

        public void UpdatePost(Post post)
        {
            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            string sql = $"update Posts " +
                          $"set DateCreated = @UpdatePostDate, Creator = @UpdateCreator, Title = @UpdateTitle, Body = @UpdateBody, Upvotes = @UpdateUpvotes, Downvotes = @UpdateDownvotes, CommunityId = @UpdateCommunityId " +
                          $" where PostId = @UpdatePostId";



            SqlCommand cmd = new SqlCommand(sql, conn);


            cmd.Parameters.AddWithValue("@UpdateCreator",post.Creator);
            cmd.Parameters.AddWithValue("@UpdateTitle",post.Title);
            cmd.Parameters.AddWithValue("@UpdateBody",post.Body);
            cmd.Parameters.AddWithValue("@UpdateUpvotes",post.Upvotes);
            cmd.Parameters.AddWithValue("@UpdateDownvotes",post.Downvotes);
            cmd.Parameters.AddWithValue("@UpdateCommunityId", post.CommunityId);
            cmd.Parameters.AddWithValue("@UpdatePostId", post.PostId);

            cmd.Parameters.Add("@UpdatePostDate", SqlDbType.DateTime);

            cmd.Parameters["@UpdatePostDate"].Value = post.DateCreated;


            cmd.ExecuteNonQuery();

            conn.Close();
        }
    }
}
