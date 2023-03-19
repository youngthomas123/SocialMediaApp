using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic
{
	public class Messsage
	{
		public Messsage(DateTime dateCreated, string messageId, string subject, string body, string senderName, string recipientName)
		{
			DateCreated = dateCreated;
			MessageId = messageId;
			Subject = subject;
			Body = body;
			SenderName = senderName;
			RecipientName = recipientName;
			Status = MessageStatus.None;
		}

		public DateTime DateCreated { get; set; }

		public string MessageId { get; set; }

		public string Subject { get; set; }

		public string Body { get; set; }

		public string SenderName { get; set; }

		public string RecipientName { get; set;}

		public MessageStatus Status { get; set; }	
	}
}
