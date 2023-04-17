using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace SocialMedia.BusinessLogic
{
	public class Post
	{

		
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
			_score = 0;
		}

		public Post (DateTime dateCreated, Guid postId, string creator, string title, string body, int upvotes, int downvotes, Guid communityid)
		{
            DateCreated = dateCreated;
			PostId = postId;
			Creator = creator;
			Title = title;
			Body = body;
			Upvotes = upvotes;
			Downvotes = downvotes;
			CommunityId = communityid;
			
        }

		public DateTime DateCreated { get; private set; }

		public Guid PostId { get; private set; }

		public string Creator { get; private set; }
		private string _title;
		public string Title
		{
			get
			{
				return _title;
			}
			 private  set
			 {
                if (value.Length <= 250)
                {
                    _title = value;
                }
                else
                {
                    throw new Exception("The post title is too big");
                }
             }
		}
		private string _body;
		public string Body
		{
			get
			{
				return _body;
			}
			  private set
			  {
                if (value.Length <= 750)
                {
                    _body = value;
                }
                else
                {
                    throw new Exception("The post body is too big");
                }

              }
        }

		public int Upvotes { get; private set; }

		public int Downvotes { get; private set; }

		public Guid CommunityId { get; private set; }
		private int _score;
		public int Score
		{
			get
			{
                CalculateScore();
                return _score;
			}
			

		}

        public void upvote()
        {
            Upvotes = Upvotes + 1;
			CalculateScore();
        }
        public void downvote()
        {
            Downvotes = Downvotes + 1;
            CalculateScore();
        }

        public void CalculateScore()
        {
            _score = Upvotes - Downvotes;
			
        }
    }
	
}
