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

		public Comment(Guid userId, string body, Guid postId)
		{
            Guid guid = Guid.NewGuid();

            DateCreated = DateTime.Now;
			CommentId = guid;
            UserId = userId;
			Body = body;
			Upvotes = 0;
			Downvotes = 0;
			_score = 0;
			PostId = postId;
			UpvotedUserIds = new List<Guid>();
			DownvotedUserIds = new List<Guid>();
		}

		public Comment(DateTime dateTime, Guid commentID, Guid userId, string body, Guid postId, int upvotes, int downvotes)  // add upvotedUserids/downvotedUserIds
		{
            DateCreated = dateTime;
			CommentId = commentID;
			UserId = userId;
			Body = body;
            PostId = postId;
            Upvotes = upvotes;
			Downvotes = downvotes;
            UpvotedUserIds = new List<Guid>();
            DownvotedUserIds = new List<Guid>();

        }

		public DateTime DateCreated { get; private set; }

		public Guid CommentId { get; private set; }

		public Guid UserId { get; private set; }

		private string _body;
		public string Body
		{
			get
			{
				return _body;
			}
			 set
			 {
                if (value.Length <= 350)
                {
					_body = value;
                }
                else
                {
                    throw new Exception("The comment body is too big");
                }

             }
        }

		public int Upvotes { get; private set; }

		public int Downvotes { get; private set; } 

		public Guid PostId { get; private set; }

		public List <Guid> UpvotedUserIds { get; private set; }

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

        private int _score;
        public int Score
        {
            get
            {
                CalculateScore();
                return _score;
            }
           
        }

       
		public void Upvote()
		{
			Upvotes = Upvotes + 1;
			CalculateScore();

        }

		public void RemoveUpvote()
		{
			Upvotes = Upvotes - 1;
			CalculateScore();
        }
		public void Downvote()
		{
			Downvotes = Downvotes + 1;
			CalculateScore();
		}
		public void Removedownvote()
		{
			Downvotes = Downvotes - 1;
			CalculateScore();
		}

		public void CalculateScore()
		{
			_score = Upvotes - Downvotes;
		}

	}
}
