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
    public class MessageDB : IMessageDataAccess
    {
        private string connection = "Server=mssqlstud.fhict.local;Database=dbi511464_i511464fh;User Id=dbi511464_i511464fh;Password=12345;";

        public void DeleteMessage(Guid id)
        {
            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            string sql = $"delete from Messages " +
                         $"where MessageId = @Id; ";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@Id", id);

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
            int SenderIdIndex = dr.GetOrdinal("SenderId");
            int RecipientIdIndex = dr.GetOrdinal("RecipientId");
            


            while (dr.Read())
            {
                
                var DateCreated = (DateTime)dr[DateCreatedIndex];
                var MessageId = (Guid)dr[MessageIdIndex];
                var Subject = (string)dr[SubjectIndex];
                var Body = (string)dr[BodyIndex];
                var SenderId = (Guid)dr[SenderIdIndex];
                var RecipientId = (Guid)dr[RecipientIdIndex];
                
                Message message = new Message(DateCreated, MessageId, Subject, Body, SenderId, RecipientId);


                messages.Add(message);
            }


            dr.Close();
            conn.Close();

            return messages;

        }

        public void SaveMessage(Message message)
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            string sql = "insert into Messages ([DateCreated], [MessageId], [Subject], [Body], [SenderId], [RecipientId] " +
                "Values (@date, @messageId, @subject, @body, @senderId, @RecipientId ";

            

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.Add("@status", SqlDbType.VarChar);

            cmd.Parameters.AddWithValue("@date", message.DateCreated);
            cmd.Parameters.AddWithValue("@messageId", message.MessageId);
            cmd.Parameters.AddWithValue("@subject", message.Subject);
            cmd.Parameters.AddWithValue("@body", message.Body);
            cmd.Parameters.AddWithValue("@senderId", message.SenderId);
            cmd.Parameters.AddWithValue("@RecipientId", message.RecipientId);
           


            cmd.ExecuteNonQuery();



            conn.Close();


        }

        public void UpdateMessage(Message message)
        {
            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            string sql = $"update Messages " +
                          $"set DateCreated = @UpdateMessageDate, Subject = @UpdateSubject, Body = @UpdateBody, SenderId = @UpdateSenderId, RecipientId = @UpdateRecipientId " +
                          $" where MessageId = @UpdateMessageId ";



            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@UpdateSubject",message.Subject);
            cmd.Parameters.AddWithValue("@UpdateBody",message.Body);
            cmd.Parameters.AddWithValue("@UpdateSenderId",message.SenderId);
            cmd.Parameters.AddWithValue("@UpdateRecipientId",message.RecipientId);
            cmd.Parameters.AddWithValue("@UpdateMessageId",message.MessageId);

            cmd.Parameters.Add("@UpdateMessageDate", SqlDbType.DateTime);
            

            cmd.Parameters["@UpdateMessageDate"].Value = message.DateCreated;
          


            cmd.ExecuteNonQuery();

            conn.Close();


        }

        public List<Message>UserReceivedMessages(Guid recipientId)
        {
            List<Message> messages = new List<Message>();


            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            string sql = $"select * " +
                         $"from Messages " +
                         $"where RecipientId = @recipientId";


            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@recipientId", recipientId);
            SqlDataReader dr = cmd.ExecuteReader();

            int DateCreatedIndex = dr.GetOrdinal("DateCreated");
            int MessageIdIndex = dr.GetOrdinal("MessageId");
            int SubjectIndex = dr.GetOrdinal("Subject");
            int BodyIndex = dr.GetOrdinal("Body");
            int SenderIdIndex = dr.GetOrdinal("SenderId");
            int RecipientIdIndex = dr.GetOrdinal("RecipientId");



            while (dr.Read())
            {

                var DateCreated = (DateTime)dr[DateCreatedIndex];
                var MessageId = (Guid)dr[MessageIdIndex];
                var Subject = (string)dr[SubjectIndex];
                var Body = (string)dr[BodyIndex];
                var SenderId = (Guid)dr[SenderIdIndex];
                var RecipientId = (Guid)dr[RecipientIdIndex];

                Message message = new Message(DateCreated, MessageId, Subject, Body, SenderId, RecipientId);


                messages.Add(message);
            }


            dr.Close();
            conn.Close();

            return messages;
        }
    }
}
