using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic
{
	public class Comment
	{
		public Comment(string commentId, string creator, string body)
		{
			DateCreated = DateTime.Now;
			CommentId = commentId;
			Creator = creator;
			Body = body;
			Upvotes = 0;
			Downvotes = 0;
		}

		public DateTime DateCreated { get; set; }

		public string CommentId { get; set; }

		public string Creator { get; set; }

		public string Body { get; set; }

		public int Upvotes { get; private set; }

		public int Downvotes { get; private set; } 
	}
}
