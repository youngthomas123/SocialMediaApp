﻿using SocialMedia.BusinessLogic;
using SocialMedia.BusinessLogic.Interfaces.IDataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.DataAccess
{
	public class RemovedPostsDB : IRemovedPostsDataAccess
	{
		private string connection = "Server=mssqlstud.fhict.local;Database=dbi511464_i511464fh;User Id=dbi511464_i511464fh;Password=12345;";

		public void CreateRecord(Guid postId)
		{
			SqlConnection conn = new SqlConnection(connection);

			conn.Open();

			string sql = $"insert into RemovedPosts ([PostId]) " +
						 $"Values (@postId) ";

			SqlCommand cmd = new SqlCommand(sql, conn);

			cmd.Parameters.AddWithValue("@postId", postId);
			

			cmd.ExecuteNonQuery();

			conn.Close();
		}

		public void DeleteRecord(Guid postId)
		{
			SqlConnection conn = new SqlConnection(connection);

			conn.Open();

			string sql = $"Delete from RemovedPosts " +
						 $"where PostId = @postId ";

			SqlCommand cmd = new SqlCommand(sql, conn);

			cmd.Parameters.AddWithValue("@postId", postId);
			

			cmd.ExecuteNonQuery();

			conn.Close();
		}

		public bool CheckRecordExists(Guid postId)
		{
			bool doesRecordExists = false;

			SqlConnection conn = new SqlConnection(connection);

			conn.Open();

			string sql = $"select count(*) as record " +
						 $"from RemovedPosts " +
						 $"where PostId = @postId ";


			SqlCommand cmd = new SqlCommand(sql, conn);

			cmd.Parameters.AddWithValue("@postId", postId);


			

			SqlDataReader dr = cmd.ExecuteReader();


			int RecordIndex = dr.GetOrdinal("record");



			while (dr.Read())
			{

				var Record = (int)dr[RecordIndex];

				if (Record == 0)
				{
					doesRecordExists = false;
				}
				else if (Record == 1)
				{
					doesRecordExists = true;
				}

			}

			dr.Close();
			conn.Close();
			return doesRecordExists;
		}

		public List<Guid>GetRemovedPostIds()
		{
			List<Guid> postIds = new List<Guid>();

			SqlConnection conn = new SqlConnection(connection);

			conn.Open();

			string sql = $"Select * " +
						 $"from RemovedPosts ";

			SqlCommand cmd = new SqlCommand(sql, conn);

			SqlDataReader dr = cmd.ExecuteReader();


			int PostIdIndex = dr.GetOrdinal("PostId");
			while (dr.Read())
			{
				var PostId = (Guid)dr[PostIdIndex];

				postIds.Add(PostId);
			}

			dr.Close();
			conn.Close();
			return postIds;


		}
	}
}
