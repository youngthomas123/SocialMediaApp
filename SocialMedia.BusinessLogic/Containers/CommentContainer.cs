using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMedia.BusinessLogic.Dto;
using SocialMedia.BusinessLogic.Interfaces.IContainer;
using SocialMedia.BusinessLogic.Interfaces.IDataAccess;

namespace SocialMedia.BusinessLogic.Containers
{
    public class CommentContainer : ICommentContainer
    {
        private readonly ICommentDataAccess _commentDataAccess;
        private readonly IUserDataAccess _userDataAccess;
        private readonly IUpvotedCommentsDataAccess _upvotedCommentsDataAccess;
        private readonly IDownvotedCommentsDataAccess _downvotedCommentsDataAccess;

        public CommentContainer(ICommentDataAccess dataAcess, IUserDataAccess userDataAccess, IUpvotedCommentsDataAccess upvotedCommentsDataAccess, IDownvotedCommentsDataAccess downvotedCommentsDataAccess)
        {
            _commentDataAccess = dataAcess;
            _userDataAccess = userDataAccess;
			_upvotedCommentsDataAccess = upvotedCommentsDataAccess;
            _downvotedCommentsDataAccess = downvotedCommentsDataAccess;

		}

        public List<Comment>GetComments() 
        {
           
             var  comments = _commentDataAccess.LoadComments();

              return comments;  

        }
        public void AddComment(Comment comment)
        {
            _commentDataAccess.SaveComment(comment);
        }
        public List<Comment> LoadCommentInPost(Guid PostId)
        {
            var comments = _commentDataAccess.LoadCommentsInPost(PostId);
            return comments;
        }
        public List<CommentPageDto> GetCommentPageDtosInPost(Guid postId, Guid userId)
        {
            List<CommentPageDto> commentPageDtos = new List<CommentPageDto>();

            foreach (Comment comment in _commentDataAccess.LoadCommentsInPost(postId))
            {
                CommentPageDto commentPageDto = new CommentPageDto();

                commentPageDto.Author = _userDataAccess.GetUserName(comment.UserId);

                commentPageDto.DateCreated = comment.DateCreated;
                
                commentPageDto.Body = comment.Body;
                commentPageDto.Score = comment.Score;
                commentPageDto.PostId = comment.PostId;
                commentPageDto.CommentId = comment.CommentId;

				if (IsCommentUpvoted(userId, comment.CommentId))
				{
					commentPageDto.IsUpvoted = true;
				}
				else
				{
					commentPageDto.IsUpvoted = false;
				}

				if (IsCommentDownvoted(userId, comment.CommentId))
				{
					commentPageDto.IsDownvoted = true;
				}
				else
				{
					commentPageDto.IsDownvoted = false;
				}


				commentPageDtos.Add(commentPageDto);
            }
            return commentPageDtos;
        }
        public Comment LoadCommentById(Guid commentId)
        {
            var comment = _commentDataAccess.LoadCommentById(commentId);
            return comment;
        }

        public void UpDateComment(Comment comment)
        {
            _commentDataAccess.UpdateComment(comment);
        }


		public void UpdateCommentScore(Comment comment, Guid userId, string direction)
		{
			if (direction == "upvoteComment")
			{
				_upvotedCommentsDataAccess.CreateRecord(userId, comment.CommentId);
				UpDateComment(comment);
			}
            else if(direction == "removeUpvoteComment")
            {
                _upvotedCommentsDataAccess.DeleteRecord(userId, comment.CommentId);
				UpDateComment(comment);
			}
			else if (direction == "downvoteComment")
			{
				_downvotedCommentsDataAccess.CreateRecord(userId, comment.CommentId);
				 UpDateComment(comment);
			}
            else if(direction == "removeDownvoteComment")
            {
                _downvotedCommentsDataAccess.DeleteRecord(userId, comment.CommentId);
				UpDateComment(comment);
			}
		}
		public bool IsCommentUpvoted(Guid userId, Guid commentId)
		{
			var IsCommentUpvoted = _upvotedCommentsDataAccess.HasUserUpvoted(userId, commentId);

			return IsCommentUpvoted;
		}

		public bool IsCommentDownvoted(Guid userId, Guid commentId)
		{
			var isCommentDownvoted = _downvotedCommentsDataAccess.HasUserDownvoted(userId, commentId);

			return isCommentDownvoted;
		}

	}
}
