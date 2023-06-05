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

        private const int ReportThreshold = 6; // Number of reports required to trigger moderation

        public BotModerator(IPostContainer postContainer, ICommentContainer commentContainer, IUserContainer userContainer, IMessageContainer messageContainer  ) 
        {
            _postContainer = postContainer;
            _commentContainer = commentContainer;
            _userContainer = userContainer;
            _messageContainer = messageContainer;
        }


        public async Task StartModerationAsync()
        {
            var reportedPosts = _postContainer.LoadAllReportedPosts();

            var reportedComments = _commentContainer.LoadAllReportedComments();


            foreach ( var post in reportedPosts )
            {
                int ReportCount = _postContainer.GetNumberOfReportsInPost(post.PostId);

                if(ReportCount>= ReportThreshold && post.Downvotes > post.Upvotes)
                {
                    NotifyUserAndDeletePost(post);
                }
                else if(ReportCount >= ReportThreshold && post.Downvotes <= post.Upvotes)
                {
                    AlertCommunityModerators();
                }
            }


            foreach (var comment in reportedComments)
            {
                int ReportCount = _commentContainer.GetNumberOfReportsInComment(comment.CommentId);

                if (ReportCount >= ReportThreshold && comment.Downvotes > comment.Upvotes)
                {
                    NotifyUserAndDeleteComment(comment);
                }
                else if (ReportCount >= ReportThreshold && comment.Downvotes <= comment.Upvotes)
                {
                    AlertCommunityModerators();
                }
            }


            await Task.Delay(TimeSpan.FromMinutes(5));
        }

        public void NotifyUserAndDeletePost(Post post)
        {

        }

        public void NotifyUserAndDeleteComment(Comment comment)
        {

        }

        public void AlertCommunityModerators()
        {

        }
    }
}
