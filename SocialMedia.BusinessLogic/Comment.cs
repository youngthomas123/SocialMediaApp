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

		public Comment(string creator, string body, Guid postId)
		{
            Guid guid = Guid.NewGuid();

            DateCreated = DateTime.Now;
			CommentId = guid;
            Creator = creator;
			Body = body;
			Upvotes = 0;
			Downvotes = 0;
			_score = 0;
			PostId = postId;
		}

		public Comment(DateTime dateTime, Guid commentID, string creator, string body, Guid postId, int upvotes, int downvotes)
		{
            DateCreated = dateTime;
			CommentId = commentID;
			Creator = creator;
			Body = body;
            PostId = postId;
            Upvotes = upvotes;
			Downvotes = downvotes;


        }

		public DateTime DateCreated { get; private set; }

		public Guid CommentId { get; private set; }

		public string Creator { get; private set; }

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
