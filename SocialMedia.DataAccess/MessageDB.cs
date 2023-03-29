﻿using SocialMedia.BusinessLogic;
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
    public class MessageDB : IMessageDataAcess
    {
        private string connection = "Server=mssqlstud.fhict.local;Database=dbi511464_i511464fh;User Id=dbi511464_i511464fh;Password=12345;";

        public void DeleteMessage(Guid id)
        {
            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            string sql = $"delete from Messages " +
                         $"where MessageId = '{id}'; ";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public List<Message> LoadMessage()
        {
            List<Message> messages = new List<Message>();


            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            string sql = $"select * " +
                         $"from Messages ";


            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            int DateCreatedIndex = dr.GetOrdinal("DateCreated");
            int MessageIdIndex = dr.GetOrdinal("MessageId");
            int SubjectIndex = dr.GetOrdinal("Subject");
            int BodyIndex = dr.GetOrdinal("Body");
            int SenderNameIndex = dr.GetOrdinal("SenderName");
            int RecipientNameIndex = dr.GetOrdinal("RecipientName");
            int StatusIndex = dr.GetOrdinal("Status");

            while (dr.Read())
            {
                Message message = new Message();
                message.DateCreated = (DateTime)dr[DateCreatedIndex];
                message.MessageId = (Guid)dr[MessageIdIndex];
                message.Subject = (string)dr[SubjectIndex];
                message.Body = (string)dr[BodyIndex];
                message.SenderName = (string)dr[SenderNameIndex];
                message.RecipientName = (string)dr[RecipientNameIndex];
                message.Status  = Enum.Parse<MessageStatus>((string)dr[StatusIndex]);


                messages.Add(message);
            }


            dr.Close();

            return messages;

        }

        public void SaveMessage(Message message)
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            string sql = "insert into Messages ([DateCreated], [MessageId], [Subject], [Body], [SenderName], [RecipientName], [Status]) " +
                "Values (@date, @Id, @subject, @body, @senderName, @RecipientName, @status)";

            

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.Add("@status", SqlDbType.VarChar);

            cmd.Parameters.AddWithValue("@date", message.DateCreated);
            cmd.Parameters.AddWithValue("@Id", message.MessageId);
            cmd.Parameters.AddWithValue("@subject", message.Subject);
            cmd.Parameters.AddWithValue("@body", message.Body);
            cmd.Parameters.AddWithValue("@senderName", message.SenderName);
            cmd.Parameters.AddWithValue("@RecipientName", message.RecipientName);
            cmd.Parameters["@status"].Value = message.Status.ToString();


            cmd.ExecuteNonQuery();



            conn.Close();


        }

        public void UpdateMessage(Message message)
        {
            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            string sql = $"update Messages " +
                          $"set DateCreated = @UpdateMessageDate, Subject = '{message.Subject}', Body = '{message.Body}', SenderName = '{message.SenderName}', RecipientName = '{message.RecipientName}', Status = @updateStatus " +
                          $" where MessageId = '{message.MessageId}'";



            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.Add("@UpdateMessageDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@updateStatus", SqlDbType.VarChar);

            cmd.Parameters["@UpdateMessageDate"].Value = message.DateCreated;
            cmd.Parameters["@updateStatus"].Value = message.Status.ToString();


            cmd.ExecuteNonQuery();

            conn.Close();


        }
    }
}
