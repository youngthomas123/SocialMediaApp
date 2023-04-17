using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic
{
	public class Message
	{

	
		public Message( string subject, string body, string senderName, string recipientName)
		{
			Guid guid = Guid.NewGuid();

			DateCreated = DateTime.Now;
			MessageId = guid;
			Subject = subject;
			Body = body;
			SenderName = senderName;
			RecipientName = recipientName;
			Status = MessageStatus.None;
		}

		public Message(DateTime dateCreated, Guid messageId, string subject, string body, string senderName, string recipientName, MessageStatus status)
		{
            DateCreated = dateCreated;
			MessageId = messageId;
			Subject = subject;
			Body = body;
			SenderName = senderName;
			RecipientName = recipientName;
			Status = status;

        }


		public DateTime DateCreated { get; private set; }

		public Guid MessageId { get; private set; }

		public string Subject { get; private set; }

		public string Body { get; private set; }

		public string SenderName { get; private set; }

		public string RecipientName { get; private set;}

		public MessageStatus Status { get; private set; }	
	}
}
