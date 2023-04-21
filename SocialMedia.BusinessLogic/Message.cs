using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic
{
	public class Message
	{

	
		public Message( string subject, string body, Guid senderId, Guid recipientId)
		{
			Guid guid = Guid.NewGuid();

			DateCreated = DateTime.Now;
			MessageId = guid;
			Subject = subject;
			Body = body;
			SenderId = senderId;
			RecipientId = recipientId;
			Status = MessageStatus.None;
		}

		public Message(DateTime dateCreated, Guid messageId, string subject, string body, Guid senderId, Guid recipientId, MessageStatus status)
		{
            DateCreated = dateCreated;
			MessageId = messageId;
			Subject = subject;
			Body = body;
			SenderId = senderId;
			RecipientId = recipientId;
			Status = status;

        }


		public DateTime DateCreated { get; private set; }

		public Guid MessageId { get; private set; }

		public string Subject { get; private set; }

		public string Body { get; private set; }

		public Guid SenderId { get; private set; }

		public Guid RecipientId { get; private set;}

		public MessageStatus Status { get; private set; }	
	}
}
