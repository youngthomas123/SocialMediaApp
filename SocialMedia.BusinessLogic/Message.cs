using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic
{
	public class Message
	{

		public Message() { }
		public Message(DateTime dateCreated, string subject, string body, string senderName, string recipientName)
		{
			Guid guid = Guid.NewGuid();

			DateCreated = dateCreated;
			MessageId = guid;
			Subject = subject;
			Body = body;
			SenderName = senderName;
			RecipientName = recipientName;
			Status = MessageStatus.None;
		}

		public DateTime DateCreated { get; set; }

		public Guid MessageId { get; set; }

		public string Subject { get; set; }

		public string Body { get; set; }

		public string SenderName { get; set; }

		public string RecipientName { get; set;}

		public MessageStatus Status { get; set; }	
	}
}
