using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic
{
	public class Post
	{
		public Post(string id, string creator, string title, string body)
		{
			DateCreated = DateTime.Now;
			PostId = id;
			Creator = creator;
			Title = title;
			Body = body;
			Upvotes = 0;
			Downvotes = 0;
		}

		public DateTime DateCreated { get; set; }

		public string PostId { get; set; }

		public string Creator { get; set; }

		public string Title { get; set; }

		public string Body { get; set; }

		public int Upvotes { get; private set; }

		public int Downvotes { get; private set; }
	}
}
