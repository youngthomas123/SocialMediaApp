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
                         $"where PostId = '{id}'; ";

            SqlCommand cmd = new SqlCommand(sql, conn);

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
                Post post = new Post();

                post.DateCreated = (DateTime)dr[DateCreatedIndex];
                post.PostId = (Guid)dr[PostIdIndex];
                post.Creator = (string)dr[CreatorIndex];
                post.Title = (string)dr[TitleIndex];
                post.Body = (string)dr[BodyIndex];
                post.Upvotes = (int)dr[UpvotesIndex];
                post.Downvotes = (int)dr[DownvotesIndex];
                post.CommunityId = (Guid)dr[CommunityIdIndex];


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
                          $"set DateCreated = @UpdatePostDate, Creator = '{post.Creator}', Title = '{post.Title}', Body = '{post.Body}', Upvotes = '{post.Upvotes}', Downvotes = '{post.Downvotes}', CommunityId = '{post.CommunityId}' " +
                          $" where PostId = '{post.PostId}'";



            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.Add("@UpdatePostDate", SqlDbType.DateTime);

            cmd.Parameters["@UpdatePostDate"].Value = post.DateCreated;


            cmd.ExecuteNonQuery();

            conn.Close();
        }
    }
}
