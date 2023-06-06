using SocialMedia.BusinessLogic.Custom_exception;
using SocialMedia.BusinessLogic.Interfaces.IContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Algorithms
{
    public class BotModerator
    {
        private readonly IPostContainer _postContainer;
        private readonly ICommentContainer _commentContainer;
        private readonly IUserContainer _userContainer;
        private readonly IMessageContainer _messageContainer;

		Guid BotModeratorId = new Guid("11111111-1111-1111-1111-111111111111");


		private const int ReportThreshold = 6; // Number of reports required for item to be deleted

		private readonly List<string> offensiveWords; // List of offensive words.


		public BotModerator(IPostContainer postContainer, ICommentContainer commentContainer, IUserContainer userContainer, IMessageContainer messageContainer  ) 
        {
            _postContainer = postContainer;
            _commentContainer = commentContainer;
            _userContainer = userContainer;
            _messageContainer = messageContainer;


            offensiveWords = new List<string>
            {
                "turd",
                "shit",
                "bastard",
                "retard",
                "piss",
                "twat"

            };
        }


        public async Task StartModerationAsync()
        {

            var Posts = _postContainer.LoadAllPosts();

            var Comments = _commentContainer.GetComments();


            foreach (var post in Posts)
            {
                int ReportCount = _postContainer.GetNumberOfReportsInPost(post.PostId);

                
				if (CheckPostForOffensiveWords(post))
				{
					NofityAndRemovePost(post);
				}

				if (ReportCount >= ReportThreshold)
				{
					NotifyAndDeletePost(post);
				}
			}


            foreach (var comment in Comments)
            {
                int ReportCount = _commentContainer.GetNumberOfReportsInComment(comment.CommentId);

				
				if (CheckCommentForOffesiveWords(comment))
                {
					NofityAndRemoveComment(comment);
				}

                if (ReportCount >= ReportThreshold)
                {
                    NotifyAndDeleteComment(comment);
                }
                
            }


            await Task.Delay(TimeSpan.FromMinutes(5));
        }

        public void NotifyAndDeletePost(Post post)
        {
            _messageContainer.CreateAndSaveMessage("Post deletion", "Your post has been deleted by the bot modeator for violating the community guidlines", BotModeratorId, post.UserId);
            try
            {
				_postContainer.DeletePost(post.PostId, post.UserId);
			}
            catch (ItemNotFoundException)
            {
            }
            
        }

        public void NofityAndRemovePost(Post post)
        {
            var isPostAlreadyRemoved = _postContainer.IsPostRemoved(post.PostId);
            if(isPostAlreadyRemoved == false)
            {
				_messageContainer.CreateAndSaveMessage("Post removal", "Your post has been removed and will not be shown in feed because it contained offensive words", BotModeratorId, post.UserId);
                try
                {
					_postContainer.RemovePost(post.PostId, post.CommunityId, BotModeratorId);
				}
                catch(ItemNotFoundException)
                {
                }
			}
           
        }

        public void NotifyAndDeleteComment(Comment comment)
        {
            _messageContainer.CreateAndSaveMessage("Comment deletion", "Your comment has been deleted by the bot moderator for violating the community guidlines", BotModeratorId, comment.UserId);
            try
            {
				_commentContainer.DeleteComment(comment.CommentId, comment.UserId);
			}
            catch(ItemNotFoundException)
            {
            }
            
        }

		public void NofityAndRemoveComment(Comment comment)
		{
            var isCommentAlreadyRemoved = true;
            if(isCommentAlreadyRemoved == false)
            {

            }
		}

		public bool CheckPostForOffensiveWords(Post post)
        {
            return true;
        }

        public bool CheckCommentForOffesiveWords(Comment comment)
        {


            return true;
        }
    }
}
