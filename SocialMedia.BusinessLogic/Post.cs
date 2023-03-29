using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic
{
	public class Post
	{

		public Post() { }
		public Post(string creator, string title, string body, Guid communityid)
		{
			Guid guid = Guid.NewGuid();

			DateCreated = DateTime.Now;
			PostId = guid;
			Creator = creator;
			Title = title;
			Body = body;
			Upvotes = 0;
			Downvotes = 0;
			CommunityId = communityid;	
		}

		public DateTime DateCreated { get; set; }

		public Guid PostId { get; set; }

		public string Creator { get; set; }

		public string Title { get; set; }

		public string Body { get; set; }

		public int Upvotes { get;  set; }

		public int Downvotes { get;  set; }

		public Guid CommunityId { get;  set; }
	}
}
