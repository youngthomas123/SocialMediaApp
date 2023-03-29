using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic
{
	public class Comment
	{

		public Comment() { }
		public Comment(string creator, string body, Guid postId)
		{
            Guid guid = Guid.NewGuid();

            DateCreated = DateTime.Now;
			CommentId = guid;
            Creator = creator;
			Body = body;
			Upvotes = 0;
			Downvotes = 0;
			PostId = postId;
		}

		public DateTime DateCreated { get; set; }

		public Guid CommentId { get; set; }

		public string Creator { get; set; }

		public string Body { get; set; }

		public int Upvotes { get;  set; }

		public int Downvotes { get;  set; } 

		public Guid PostId { get; set; }	

	}
}
