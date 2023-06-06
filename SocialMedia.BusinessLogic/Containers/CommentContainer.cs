using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SocialMedia.BusinessLogic.Algorithms;
using SocialMedia.BusinessLogic.Custom_exception;
using SocialMedia.BusinessLogic.Dto;
using SocialMedia.BusinessLogic.Interfaces;
using SocialMedia.BusinessLogic.Interfaces.IContainer;
using SocialMedia.BusinessLogic.Interfaces.IDataAccess;

namespace SocialMedia.BusinessLogic.Containers
{
    public class CommentContainer : ICommentContainer
    {
		Guid BotModeratorId = new Guid("11111111-1111-1111-1111-111111111111");

		private readonly ICommentDataAccess _commentDataAccess;
        private readonly IUserDataAccess _userDataAccess;
        private readonly IUpvotedCommentsDataAccess _upvotedCommentsDataAccess;
        private readonly IDownvotedCommentsDataAccess _downvotedCommentsDataAccess;

        private readonly IPostDataAccess _postDataAccess;
        
        private readonly IReportedCommentsDataAccess _reportedCommentsDataAccess;

        private readonly IReportReasonsDataAccess _reportReasonsDataAccess;

        private readonly IRemovedCommentsDataAccess _removedCommentsDataAccess;

        private readonly IContentFilterAndRanking _contentFilterAndRanking;

        public CommentContainer(ICommentDataAccess dataAcess, IUserDataAccess userDataAccess, IUpvotedCommentsDataAccess upvotedCommentsDataAccess, IDownvotedCommentsDataAccess downvotedCommentsDataAccess, IPostDataAccess postDataAccess, IReportedCommentsDataAccess reportedCommentsDataAccess, IReportReasonsDataAccess reportReasonsDataAccess, IRemovedCommentsDataAccess removedCommentsDataAccess, IContentFilterAndRanking contentFilterAndRanking)
        {
            _commentDataAccess = dataAcess;
            _userDataAccess = userDataAccess;
			_upvotedCommentsDataAccess = upvotedCommentsDataAccess;
            _downvotedCommentsDataAccess = downvotedCommentsDataAccess;
            _postDataAccess = postDataAccess;
            _reportedCommentsDataAccess = reportedCommentsDataAccess;
            _reportReasonsDataAccess = reportReasonsDataAccess;
			_removedCommentsDataAccess = removedCommentsDataAccess;
			_contentFilterAndRanking = contentFilterAndRanking;


		}

        public void Upvote(Guid commentId, string direction, Guid userId)
        {
			var doesCommentIdExist = _commentDataAccess.DoesCommentIdExist(commentId);

            if(doesCommentIdExist)
            {
				var didUserAlreadyUpvote = IsCommentUpvoted(userId, commentId);

				if (didUserAlreadyUpvote == false)
				{
					var comment = LoadCommentById(commentId);

					if (comment != null)
					{
						comment.Upvote();

						UpdateCommentScore(comment, userId, direction);

					}
					else
					{
						throw new ItemNullException();
					}
				}
				else
				{
					throw new AccessException("User has already upvoted this comment");
				}
			}
            else
            {
                throw new ItemNotFoundException("CommentId not found");
            }

			

           
        }

        public void RemoveUpvote(Guid commentId, string direction, Guid userId)
        {
            var doesCommentIdExist = _commentDataAccess.DoesCommentIdExist(commentId);

            if(doesCommentIdExist)
            {
				var didUserAlreadyUpvote = IsCommentUpvoted(userId, commentId);

				if (didUserAlreadyUpvote == true)
				{
					var comment = LoadCommentById(commentId);

					if (comment != null)
					{
						comment.RemoveUpvote();

						UpdateCommentScore(comment, userId, direction);

					}
					else
					{
						throw new ItemNullException();
					}
				}
				else
				{
					throw new AccessException("User has not upvoted this comment");
				}
			}
            else
            {
                throw new ItemNotFoundException("CommentId not found");
            }

			

			
        }

        public void Downvote(Guid commentId, string direction, Guid userId)
        {

            var doesCommentIdExist = _commentDataAccess.DoesCommentIdExist(commentId);

            if(doesCommentIdExist)
            {
				var didUserAlreadyDownvote = IsCommentDownvoted(userId, commentId);

				if (didUserAlreadyDownvote == false)
				{
					var comment = LoadCommentById(commentId);

					if (comment != null)
					{
						comment.Downvote();

						UpdateCommentScore(comment, userId, direction);

					}
					else
					{
						throw new ItemNullException();
					}
				}
				else
				{
					throw new AccessException("User has already downvoted this comment");
				}
			}
            else
            {
                throw new ItemNotFoundException("CommentId not found");
            }

			

			
        }

        public void RemoveDownvote(Guid commentId, string direction, Guid userId)
        {
			var doesCommentIdExist = _commentDataAccess.DoesCommentIdExist(commentId);

            if(doesCommentIdExist)
            {
				var didUserAlreadyDownvote = IsCommentDownvoted(userId, commentId);

				if (didUserAlreadyDownvote == true)
				{
					var comment = LoadCommentById(commentId);

					if (comment != null)
					{
						comment.Removedownvote();

						UpdateCommentScore(comment, userId, direction);

					}
					else
					{
						throw new ItemNullException();
					}
				}
				else
				{
					throw new AccessException("User has not downvoted this comment");
				}
			}
            else
            {
                throw new ItemNotFoundException("CommentId not found");
            }

        }


        public List<Comment>GetComments() 
        {
           
             var  comments = _commentDataAccess.LoadComments();

              return comments;  

        }
        public void AddComment(Comment comment)
		{
            if(!string.IsNullOrEmpty(comment.Body) && comment.Body.Length <=350)
            {
                _commentDataAccess.SaveComment(comment);
            }
            else
            {
                throw new InvalidInputException("Comment body is too big");
            }
            
        }
        public List<Comment> LoadCommentInPost(Guid PostId)
        {
            var doesPostIdExist = _postDataAccess.DoesPostIdExist(PostId);
            if(doesPostIdExist)
            {
				var comments = _commentDataAccess.LoadCommentsInPost(PostId);
				return comments;
			}
            else
            {
                throw new ItemNotFoundException("Invalid postId");  
            }
            
        }
        public List<CommentPageDto> GetCommentPageDtosInPost(Guid postId, Guid userId)
        {

            var doesPostIdExist = _postDataAccess.DoesPostIdExist(postId);
            var doesUserIdExist = _userDataAccess.DoesUserIdExist(userId);

            if(doesPostIdExist == true && doesUserIdExist == true)
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

                    if(IsCommentReported(userId, comment.CommentId))
                    {
                        commentPageDto.IsReported = true;
                    }
                    else
                    {
						commentPageDto.IsReported = false;
					}

                    commentPageDtos.Add(commentPageDto);
                }

                var FilteredComments = _contentFilterAndRanking.CommentsforCommentSection(commentPageDtos);

                return FilteredComments;
            }
            else
            {
                throw new ItemNotFoundException (); 
            }
           
        }
        public Comment LoadCommentById(Guid commentId)
        {
            var doesCommentIdExist = _commentDataAccess.DoesCommentIdExist(commentId);

            if (doesCommentIdExist == true) 
            {
                var comment = _commentDataAccess.LoadCommentById(commentId);
                return comment;
            }
            else
            {
                throw new ItemNotFoundException("Invalid CommentId");
            }
           
        }

        private void UpDateComment(Comment comment)
        {
            _commentDataAccess.UpdateComment(comment);
        }


		private void UpdateCommentScore(Comment comment, Guid userId, string direction)
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

        public bool IsCommentReported(Guid userId, Guid commentId)
        {
            var isCommentReported = _reportedCommentsDataAccess.CheckRecordExists(commentId, userId);

            return isCommentReported;
        }

        public bool IsCommentRemoved(Guid commentId)
        {
            var isCommentRemoved = _removedCommentsDataAccess.CheckRecordExists(commentId);

            return isCommentRemoved;
        }

        public void UpdateComment(Guid commentId, string body, Guid LoggedInUserId)
        {
            var doesCommentIdExist = _commentDataAccess.DoesCommentIdExist(commentId);

            if (doesCommentIdExist)
            {
                var comment = _commentDataAccess.LoadCommentById(commentId);
                if(comment.UserId == LoggedInUserId)
                {
                    if (!string.IsNullOrEmpty(body) && body.Length <=350)
                    {
					  _commentDataAccess.UpdateComment(commentId, body);
					}
                    else
                    {
                        throw new InvalidInputException("comment body too big");
                    }
                    
                }
                else
                {
                    throw new AccessException("You are not the owner of the comment, hence cannot edit.");
                }
            }
            else
            {
                throw new ItemNotFoundException();
            }

            
        }

        public void DeleteComment(Guid commentId, Guid LoggedInUserId)
        {
            var doesCommentIdExist = _commentDataAccess.DoesCommentIdExist(commentId);
            var doesUserIdExist = _userDataAccess.DoesUserIdExist(LoggedInUserId);


            if(doesCommentIdExist == true && doesUserIdExist == true)
            {
                var comment = _commentDataAccess.LoadCommentById(commentId);

                if(comment.UserId == LoggedInUserId)
                {
                    _removedCommentsDataAccess.DeleteRecord(commentId);
                    _reportedCommentsDataAccess.DeleteRecord(commentId);
                    _upvotedCommentsDataAccess.DeleteRecord(commentId);
                    _downvotedCommentsDataAccess.DeleteRecord(commentId);
                    _commentDataAccess.DeleteComment(commentId);
                }
                else
                {
                    throw new AccessException("You are not the owner of this comment");
                }
             
            }
            else
            {
                throw new ItemNotFoundException();
            }
            
        }

        public List<ReportReasonsDto> LoadReportReasonsDtos()
        {
            var reportReasonsDtos = _reportReasonsDataAccess.LoadReportReasonsDtos();

            return reportReasonsDtos;
        }

        public void ReportComment(Guid commentId, Guid userId, int reasonId)
        {
            var doesCommentExist = _commentDataAccess.DoesCommentIdExist(commentId);
			var doesUserIdExist = _userDataAccess.DoesUserIdExist(userId);

			if (doesCommentExist == true && doesUserIdExist == true)
			{
				var didUserAlreadyReport = IsCommentReported(userId, commentId);

				if (didUserAlreadyReport == false)
				{
                    _reportedCommentsDataAccess.CreateRecord(commentId, userId, reasonId);
					
				}
				else
				{
					throw new AccessException("You have already reported this comment. Cannot report again.");
				}
			}
			else
			{
				throw new ItemNotFoundException("Invalid postId or userId");
			}

		}

        public void RemoveComment(Guid commentId, Guid moderatorId)   // only bot moderator can remove comments
        {
            var doesCommentIdExist = _commentDataAccess.DoesCommentIdExist(commentId);
			var isBotModerator = moderatorId == BotModeratorId;

            if(doesCommentIdExist == true && isBotModerator == true)
            {
                var isCommentAlreadyRemoved = IsCommentRemoved(commentId);
                if(isCommentAlreadyRemoved == false)
                {
                    _removedCommentsDataAccess.CreateRecord(commentId);
                }
                else
                {
                    throw new AccessException("Comment has already been removed");
                }
            }
            else
            {
                throw new ItemNotFoundException("commentId or bot moderator not found");
            }
		}

		public List<Comment>LoadAllReportedComments()
        {
            var ReportedCommentIds = _reportedCommentsDataAccess.LoadAllReportedCommentIds();

            List<Comment> reportedComments = new List<Comment>();

            foreach (var id in ReportedCommentIds)
            {
                var Comment = _commentDataAccess.LoadCommentById(id);

                reportedComments.Add(Comment);
            }

            return reportedComments;
        }

        public int GetNumberOfReportsInComment(Guid commentId)
        {
            var num = _reportedCommentsDataAccess.GetReportCountInComment(commentId);
            return num;
        }
	}
}
