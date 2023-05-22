using SocialMedia.BusinessLogic.Interfaces.IDataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.DataAccess
{
    public class DownvotedPostsDB : IDownvotedPostsDataAccess
    {
        private string connection = "Server=mssqlstud.fhict.local;Database=dbi511464_i511464fh;User Id=dbi511464_i511464fh;Password=12345;";

        public void CreateRecord(Guid userId, Guid postId)
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            string sql = $"insert into DownvotedPosts " +
                          $"Values (@UserId, @PostId)";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@PostId", postId);

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void DeleteRecord(Guid userId, Guid postId)
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            string sql = $"delete from DownvotedPosts " +
                         $"where UserId = @UserId and PostId = @PostId ";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@PostId", postId);

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public void DeleteRecord(Guid postId)
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            string sql = $"delete from DownvotedPosts " +
                         $"where PostId = @PostId ";

            SqlCommand cmd = new SqlCommand(sql, conn);

          
            cmd.Parameters.AddWithValue("@PostId", postId);

            cmd.ExecuteNonQuery();

            conn.Close();
        }


        public bool HasUserDownvoted(Guid userId, Guid postId)
        {
            int record = -1;

            bool hasUserDownvotedPost = false;

            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            string sql = $"select COUNT(*) as 'Record' " +
                         $"from DownvotedPosts " +
                         $"where UserId = @UserId and PostId = @postId ";


            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@PostId", postId);

            SqlDataReader dr = cmd.ExecuteReader();

            int RecordIndex = dr.GetOrdinal("Record");



            while (dr.Read())
            {
                var number = (int)dr[RecordIndex];
                record = number;
            }

            if (record > 0)
            {
                hasUserDownvotedPost = true;
            }
            else if (record == 0)
            {
                hasUserDownvotedPost = false;
            }

            dr.Close();
            conn.Close();

            return hasUserDownvotedPost;


        }
        public List <Guid>GetDownvotedUserIdsByPost(Guid postId)
        {
            List<Guid> userIds = new List<Guid>();


            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            string sql = $"select UserId " +
                         $"from DownvotedPosts " +
                         $"where PostId = @postId";


            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@postId", postId);

            SqlDataReader dr = cmd.ExecuteReader();


            int UserIdIndex = dr.GetOrdinal("UserId");


            while (dr.Read())
            {


                var UserId = (Guid)dr[UserIdIndex];

                userIds.Add(UserId);


            }


            dr.Close();

            return userIds;
        }



    }
}
