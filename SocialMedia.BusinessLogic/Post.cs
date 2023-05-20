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

		
		//public Post(Guid userId, string title, string body, Guid communityid)     // create post with text 
		//{
		//	Guid guid = Guid.NewGuid();

		//	DateCreated = DateTime.Now;
		//	PostId = guid;
		//	UserId = userId;
		//	Title = title;
		//	Body = body;
		//	Upvotes = 0;
		//	Downvotes = 0;
		//	CommunityId = communityid;	
		//	_score = 0;
		//	UpvotedUserIds = new List<Guid>();
  //          DownvotedUserIds = new List<Guid>();

  //      }

  //      public Post(Guid userId, string title, Guid communityid, string URL)     // create post with image
  //      {
  //          Guid guid = Guid.NewGuid();

  //          DateCreated = DateTime.Now;
  //          PostId = guid;
  //          UserId = userId;
  //          Title = title;
  //          ImageURL = URL;
  //          Upvotes = 0;
  //          Downvotes = 0;
  //          CommunityId = communityid;
  //          _score = 0;
  //          UpvotedUserIds = new List<Guid>();
  //          DownvotedUserIds = new List<Guid>();
  //      }

		public Post (Guid userId, string title, string? body, string? imageUrl ,Guid communityid)
		{
            Guid guid = Guid.NewGuid();

            DateCreated = DateTime.Now;
            PostId = guid;
            UserId = userId;
            Title = title;
            Body = body;
			ImageURL= imageUrl;
            Upvotes = 0;
            Downvotes = 0;
            CommunityId = communityid;
            _score = 0;
            UpvotedUserIds = new List<Guid>();
            DownvotedUserIds = new List<Guid>();
        }


        public Post (DateTime dateCreated, Guid postId, Guid userId, string title, string? body, int upvotes, int downvotes, Guid communityid, string? Url)  // need to add Upvoted/Downvoted UserIds
		{
            DateCreated = dateCreated;
			PostId = postId;
			UserId = userId;
			Title = title;
			Body = body;
			Upvotes = upvotes;
			Downvotes = downvotes;
			CommunityId = communityid;
			ImageURL= Url;
			UpvotedUserIds = new List<Guid>();
			DownvotedUserIds = new List<Guid>();
		}

		public DateTime DateCreated { get; private set; }

		public Guid PostId { get; private set; }

		public Guid UserId { get; private set; }
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
		private string? _body;
		public string? Body
		{
			get
			{
				return _body;
			}
			  private set
			  {
                if (value == null || value.Length <= 750)
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



		public string? ImageURL { get; private set; }


		public List <Guid>UpvotedUserIds { get; private set; }

		public List<Guid> DownvotedUserIds { get; private set; }


		public void AddUpvotedUserId(Guid userId)
		{
			UpvotedUserIds.Add(userId);

        }

		public void RemoveUpvotedUserId(Guid userId)
		{
			UpvotedUserIds.Remove(userId);
		}

		public void AddDownvotedUserId(Guid userId)
		{
			DownvotedUserIds.Add(userId);
		}

		public void RemoveDownvotedUserId(Guid userId)
		{
			DownvotedUserIds.Remove(userId);
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
