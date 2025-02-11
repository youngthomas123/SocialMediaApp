﻿using SocialMedia.BusinessLogic.Custom_exception;
using SocialMedia.BusinessLogic.Dto;
using SocialMedia.BusinessLogic.Interfaces;
using SocialMedia.BusinessLogic.Interfaces.IContainer;
using SocialMedia.BusinessLogic.Interfaces.IDataAccess;
using SocialMedia.BusinessLogic.PostRendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SocialMedia.BusinessLogic.Containers
{
    public class PostContainer : IPostContainer
    {
		Guid BotModeratorId = new Guid("11111111-1111-1111-1111-111111111111");

		private readonly IPostDataAccess _postDataAccess;

        private readonly IUserDataAccess _userDataAccess;

        private readonly ICommunityDataAccess _communityDataAccess;

        private readonly IUpvotedPostsDataAccess _upvotedPostsDataAccess;

        private readonly IDownvotedPostsDataAccess _downvotedPostsDataAccess;

        private readonly ICommentDataAccess _commentDataAccess;

        private readonly IUpvotedCommentsDataAccess _upvotedCommentsDataAccess;

        private readonly IDownvotedCommentsDataAccess _downvotedCommentsDataAccess;

        private readonly IReportedPostsDataAccess _reportedPostsDataAccess;

        private readonly IReportedCommentsDataAccess _reportedCommentsDataAccess;

        private readonly IReportReasonsDataAccess _reportReasonsDataAccess;

        private readonly IRemovedPostsDataAccess _removedPostsDataAccess;

        private readonly IRemovedCommentsDataAccess _removedCommentsDataAccess;

        private readonly ICommunityModeratorsDataAccess _communityModeratorsDataAccess;

        private readonly IContentFilterAndRanking _contentFilterAndRanking;
        
        public PostContainer(IPostDataAccess postDataAcess, IUserDataAccess userDataAccess, ICommunityDataAccess communityDataAccess, IUpvotedPostsDataAccess upvotedPostsDataAccess, IDownvotedPostsDataAccess downvotedPostsDataAccess, ICommentDataAccess commentDataAccess, IUpvotedCommentsDataAccess upvotedCommentsDataAccess, IDownvotedCommentsDataAccess downvotedCommentsDataAccess, IReportedPostsDataAccess reportedPostsDataAccess, IReportReasonsDataAccess reportReasonsDataAccess, IReportedCommentsDataAccess reportedCommentsDataAccess, IRemovedPostsDataAccess removedPostsDataAccess, ICommunityModeratorsDataAccess communityModeratorsDataAccess, IRemovedCommentsDataAccess removedCommentsDataAccess, IContentFilterAndRanking contentFilterAndRanking)
        {
            _postDataAccess = postDataAcess;
            _userDataAccess = userDataAccess;
            _communityDataAccess = communityDataAccess;
            _upvotedPostsDataAccess = upvotedPostsDataAccess;
            _downvotedPostsDataAccess = downvotedPostsDataAccess;
            _commentDataAccess = commentDataAccess;
            _upvotedCommentsDataAccess = upvotedCommentsDataAccess;
            _downvotedCommentsDataAccess = downvotedCommentsDataAccess;
			_reportedPostsDataAccess = reportedPostsDataAccess;
			_reportedCommentsDataAccess = reportedCommentsDataAccess;
			_reportReasonsDataAccess = reportReasonsDataAccess;
			_removedPostsDataAccess = removedPostsDataAccess;
			_communityModeratorsDataAccess = communityModeratorsDataAccess;
			_removedCommentsDataAccess = removedCommentsDataAccess;
			_contentFilterAndRanking = contentFilterAndRanking;


		}

        public void CreateAndSavePost(Guid userId, string title, string? body, string? imageUrl, Guid communityid)
        {
            if(body!=null && imageUrl == null)
            {
                if(!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(body) && title.Length <=250 && body.Length <=750)
                {
                    Post post = new Post(userId, title, body, null, communityid);
                    SavePost(post);
                }
                else
                {
                    throw new InvalidInputException();
                }
            }
            else if(body == null && imageUrl != null)
            {
                if(!string.IsNullOrEmpty(title) && title.Length <=250)
                {
                    Post post = new Post(userId, title, null, imageUrl, communityid);
                    SavePost(post);
                }
                else
                {
                    throw new InvalidInputException();
                }
            }
        }

        public void Upvote(Guid postId, string direction, Guid userId)
        {
            // check if user has already upvoted

            var doesPostIdExist = _postDataAccess.DoesPostIdExist(postId);

            if(doesPostIdExist)
            {
				var didUserAlreadyUpvote = IsPostUpvoted(userId, postId);

				if (didUserAlreadyUpvote == false)
				{
					var post = LoadPostById(postId);

					if (post != null)
					{
						post.Upvote();

						UpdatePostScore(post, userId, direction);

					}
					else
					{
						throw new ItemNullException();
					}
				}
				else
				{
					throw new AccessException("User has already upvoted this post");
				}
			}
            else
            {
                throw new ItemNotFoundException("PostId not found");
            }
           
           

        }

        public void RemoveUpvote(Guid postId, string direction, Guid userId)
        {

			var doesPostIdExist = _postDataAccess.DoesPostIdExist(postId);

            if(doesPostIdExist)
            {
				var didUserAlreadyUpvote = IsPostUpvoted(userId, postId);

				if (didUserAlreadyUpvote == true)
				{
					var post = LoadPostById(postId);

					if (post != null)
					{
						post.RemoveUpvote();

						UpdatePostScore(post, userId, direction);

					}
					else
					{
						throw new ItemNullException();
					}
				}
				else
				{
					throw new AccessException("User has not upvoted this post");
				}
			}
            else
            {
                throw new ItemNotFoundException("PostId not found");
            }

			

			

        }

        public void Downvote(Guid postId, string direction, Guid userId)
        {
            var doesPostIdExist = _postDataAccess.DoesPostIdExist(postId);

            if(doesPostIdExist)
            {
				var didUserAlreadyDownvote = IsPostDownvoted(userId, postId);

				if (didUserAlreadyDownvote == false)
				{
					var post = LoadPostById(postId);

					if (post != null)
					{
						post.Downvote();

						UpdatePostScore(post, userId, direction);

					}
					else
					{
						throw new ItemNullException();
					}
				}
				else
				{
					throw new AccessException("User has already downvoted this post");
				}
			}
            else
            {
                throw new ItemNotFoundException("PostId not found");
            }

			
           
        }

        public void RemoveDownvote(Guid postId, string direction, Guid userId)
        {

            var doesPostIdExist = _postDataAccess.DoesPostIdExist(postId);

            if(doesPostIdExist)
            {
				var didUserAlreadyDownvote = IsPostDownvoted(userId, postId);

				if (didUserAlreadyDownvote == true)
				{
					var post = LoadPostById(postId);

					if (post != null)
					{
						post.RemoveDownvote();

						UpdatePostScore(post, userId, direction);

					}
					else
					{
						throw new ItemNullException();
					}
				}
				else
				{
					throw new AccessException("User has not downvoted this post");
				}
			}
            else
            {
                throw new ItemNotFoundException("PostId not found");
            }

			
        }

        public List<Post> LoadAllPosts()
        {
            List<Post>posts = new List<Post>();
            posts = _postDataAccess.LoadPost();


            foreach (Post post in posts)
            {
				SetVoteUserIds(post);
			}
            return posts;
        }

        public Post? LoadPostById(Guid postId)
        {
            var doesPostIdExist = _postDataAccess.DoesPostIdExist(postId);

            if(doesPostIdExist)
            {
				var post = _postDataAccess.LoadPostById(postId);

				SetVoteUserIds(post);
				return post;
			}
            else
            {
                throw new ItemNotFoundException("PostId not found");
            }
      
            
        }

        private void SavePost(Post post)
        {
            _postDataAccess.SavePost(post);

        }

        public void UpdatePost(Post post)
        {
            if(post.Body !=null && post.ImageURL == null)
			{
                if(!string.IsNullOrEmpty(post.Title) && !string.IsNullOrEmpty(post.Body) && post.Title.Length <=250 && post.Body.Length <=750)
                {
                    _postDataAccess.UpdatePost(post);
                }
                else
                {
                    throw new InvalidInputException();
                }
            }
            else if(post.Body == null && post.ImageURL!=null)
            {
                if (!string.IsNullOrEmpty(post.Title) && post.Title.Length <= 250 )
                {
                    _postDataAccess.UpdatePost(post);
                }
                else
                {
                    throw new InvalidInputException();
                }
            }
            else
            {
                throw new InvalidInputException();
            }
           
        }
        public List<PostPageDto>GetPostPageDtos(Guid userId)
        {
            List<PostPageDto>postPageDtos = new List<PostPageDto>();    

            foreach (Post post in _postDataAccess.LoadPost())
            {
                PostPageDto postPageDto = new PostPageDto();

                postPageDto.Author = _userDataAccess.GetUserName(post.UserId);
                postPageDto.CommunityName = _communityDataAccess.GetCommunityName(post.CommunityId);
                postPageDto.DateCreated = post.DateCreated;
                postPageDto.Title = post.Title;
                postPageDto.Body = post.Body;
                postPageDto.Score = post.Score;
                postPageDto.PostId = post.PostId;
                postPageDto.ImageUrl = post.ImageURL;
                postPageDto.UpvotedUserIds = post.UpvotedUserIds;
                postPageDto.DownvotedUserIds= post.DownvotedUserIds;


				if (IsPostUpvoted(userId, post.PostId))
				{
					postPageDto.IsUpvoted = true;
				}
				else
				{
					postPageDto.IsUpvoted = false;
				}

				if (IsPostDownvoted(userId, post.PostId))
				{
					postPageDto.IsDownvoted = true;
				}
				else
				{
					postPageDto.IsDownvoted = false;
				}

                if(IsPostReported(userId, post.PostId))
                {
                    postPageDto.IsReported = true;
                }
                else
                {
					postPageDto.IsReported = false;
				}

                if(post.Body !=null && post.ImageURL == null)
                {
                    postPageDto.Content = new TextContent { Body = post.Body };

				}
                else if(post.Body == null && post.ImageURL != null)
                {
					postPageDto.Content = new ImageContent { Image = post.ImageURL };

				}


				postPageDtos.Add(postPageDto);  
            }

            var filteredPosts = _contentFilterAndRanking.PostsforMainFeed(postPageDtos, userId);

            return filteredPosts;
        }
        public PostPageDto GetPostPageDtoById(Guid postid, Guid userId)  
        {
            // check if ids are valid
            bool doesPostIdExist = _postDataAccess.DoesPostIdExist(postid);
            bool doesUserIdExist = _userDataAccess.DoesUserIdExist(userId);

            if(doesPostIdExist == true && doesUserIdExist == true)
            {

                PostPageDto postPageDto = new PostPageDto();

                var post = LoadPostById(postid);



                postPageDto.Author = _userDataAccess.GetUserName(post.UserId);
                postPageDto.CommunityName = _communityDataAccess.GetCommunityName(post.CommunityId);
                postPageDto.DateCreated = post.DateCreated;
                postPageDto.Title = post.Title;
                postPageDto.Body = post.Body;
                postPageDto.Score = post.Score;
                postPageDto.PostId = post.PostId;
                postPageDto.ImageUrl = post.ImageURL;

                if (IsPostUpvoted(userId, post.PostId))
                {
                    postPageDto.IsUpvoted = true;
                }
                else
                {
                    postPageDto.IsUpvoted = false;
                }

                if (IsPostDownvoted(userId, post.PostId))
                {
                    postPageDto.IsDownvoted = true;
                }
                else
                {
                    postPageDto.IsDownvoted = false;
                }

				if (IsPostReported(userId, post.PostId))
				{
					postPageDto.IsReported = true;
				}
				else
				{
					postPageDto.IsReported = false;
				}

				if (post.Body != null && post.ImageURL == null)
				{
					postPageDto.Content = new TextContent { Body = post.Body };

				}
				else if (post.Body == null && post.ImageURL != null)
				{
					postPageDto.Content = new ImageContent { Image = post.ImageURL };

				}

				return postPageDto;
            }
            else
            {
                throw new ItemNotFoundException("Invalid PostId or UserId");
            }
            
        }
        public List<PostPageDto> GetPostPageDtosByCommunity(Guid communityId, Guid userId)
        {
            var doesCommunityIdExist = _communityDataAccess.DoesCommunityIdExist(communityId);
            var doesUserIdExist = _userDataAccess.DoesUserIdExist(userId);

            if(doesCommunityIdExist == true && doesUserIdExist == true)
            {
                List<PostPageDto> postPageDtos = new List<PostPageDto>();

                foreach (Post post in _postDataAccess.LoadPostsByCommunity(communityId))
                {
                    PostPageDto postPageDto = new PostPageDto();

                    postPageDto.Author = _userDataAccess.GetUserName(post.UserId);
                    postPageDto.CommunityName = _communityDataAccess.GetCommunityName(post.CommunityId);
                    postPageDto.DateCreated = post.DateCreated;
                    postPageDto.Title = post.Title;
                    postPageDto.Body = post.Body;
                    postPageDto.Score = post.Score;
                    postPageDto.PostId = post.PostId;
                    postPageDto.ImageUrl = post.ImageURL;

                    if (IsPostUpvoted(userId, post.PostId))
                    {
                        postPageDto.IsUpvoted = true;
                    }
                    else
                    {
                        postPageDto.IsUpvoted = false;
                    }

                    if (IsPostDownvoted(userId, post.PostId))
                    {
                        postPageDto.IsDownvoted = true;
                    }
                    else
                    {
                        postPageDto.IsDownvoted = false;
                    }

					if (IsPostReported(userId, post.PostId))
					{
						postPageDto.IsReported = true;
					}
					else
					{
						postPageDto.IsReported = false;
					}

					if (post.Body != null && post.ImageURL == null)
					{
						postPageDto.Content = new TextContent { Body = post.Body };

					}
					else if (post.Body == null && post.ImageURL != null)
					{
						postPageDto.Content = new ImageContent { Image = post.ImageURL };

					}

					postPageDtos.Add(postPageDto);
                }

                var FilteredPosts = _contentFilterAndRanking.PostsforCommunityFeed(postPageDtos);

                return FilteredPosts;
            }
            else
            {
                throw new ItemNotFoundException("Invalid community id");
            }
            
        }

        public bool IsPostUpvoted(Guid userId, Guid postId)
        {
            var isPostUpvoted = _upvotedPostsDataAccess.HasUserUpvoted(userId, postId);

            return isPostUpvoted;
        }

        public bool IsPostDownvoted(Guid userId, Guid postId)
        {
            var isPostDownvoted = _downvotedPostsDataAccess.HasUserDownvoted(userId, postId);

            return isPostDownvoted;
        }

        public bool IsPostReported(Guid userId, Guid postId)
        {
            var isPostReported = _reportedPostsDataAccess.CheckRecordExists(postId, userId);

            return isPostReported;
        }

        public bool IsPostRemoved(Guid postId)
        {
            var isPostRemoved = _removedPostsDataAccess.CheckRecordExists(postId);

            return isPostRemoved;
        }

        private void UpdatePostScore(Post post,Guid userId ,string direction)
        {
            if(direction == "upvotePost")
            {
                _upvotedPostsDataAccess.CreateRecord(userId, post.PostId);
                UpdatePost(post);
            }
            else if (direction == "downvotePost")
            {
                _downvotedPostsDataAccess.CreateRecord(userId, post.PostId);
                UpdatePost(post);
            }
            else if (direction == "removeUpvotePost")
            {
				_upvotedPostsDataAccess.DeleteRecord(userId, post.PostId);
				UpdatePost(post);

			}
            else if (direction == "removeDownvotePost")
            {
                _downvotedPostsDataAccess.DeleteRecord(userId, post.PostId);
				UpdatePost(post);
			}
        }

        public void SetVoteUserIds(Post post)
        {
            var upvotedUserIds = _upvotedPostsDataAccess.GetUpvotedUserIdsByPost(post.PostId);

            foreach (var upvotedUserId in upvotedUserIds)
            {
                post.AddUpvotedUserId(upvotedUserId);
            }

            var downvotedUserIds = _downvotedPostsDataAccess.GetDownvotedUserIdsByPost(post.PostId);

            foreach(var downvotedUserId in downvotedUserIds)
            {
                post.AddUpvotedUserId(downvotedUserId);
            }
        }
        public void UpdatePost(Guid postId, string title, string? body, string? imageUrl, Guid LoggedInUserId)
        {
            bool doesPostIdExist = _postDataAccess.DoesPostIdExist(postId);

            if(doesPostIdExist == true)
            {
                var post = _postDataAccess.LoadPostById(postId);
                if(post.UserId == LoggedInUserId)
                {
                    _postDataAccess.UpdatePost(postId, title, body, imageUrl);
                }
                else
                {
                    throw new AccessException("You are not the owner of this post, hence cannot edit it.");
                }
               

            }
            else
            {
                throw new ItemNotFoundException();
            }
        }

        public void DeletePost(Guid PostId, Guid LoggedInUserId)
        {
            // validation - does postid/userId exist and is the logged in user the creator of the post

            
            //delete all comments in post
            //get all comment ids in post (in loop)
            //delete all the reported comment recored which are part of the post
            // delete upvotedComents with commentId
            // delete downvotedComments with commentId
            //delete comment

            var doesPostIdExist = _postDataAccess.DoesPostIdExist(PostId);
            var doesUserIdExist = _userDataAccess.DoesUserIdExist(LoggedInUserId);

            if(doesPostIdExist && doesUserIdExist)
            {
                var post = _postDataAccess.LoadPostById(PostId);
                if(post.UserId == LoggedInUserId)
                {
                    var commentIds = _commentDataAccess.LoadCommentIdsInPost(PostId);

                    foreach (var commentId in commentIds)
                    {
                        _removedCommentsDataAccess.DeleteRecord(commentId);
                        _reportedCommentsDataAccess.DeleteRecord(commentId);
                        _upvotedCommentsDataAccess.DeleteRecord(commentId);
                        _downvotedCommentsDataAccess.DeleteRecord(commentId);
                        _commentDataAccess.DeleteComment(commentId);
                    }
                    _removedPostsDataAccess.DeleteRecord(PostId);
                    _reportedPostsDataAccess.DeleteRecord(PostId);
                    _upvotedPostsDataAccess.DeleteRecord(PostId);
                    _downvotedPostsDataAccess.DeleteRecord(PostId);
                    _postDataAccess.DeletePost(PostId);
                }
                else
                {
                    throw new AccessException("You are not the owner of this post");
                }

             
            }
            else
            {
                throw new ItemNotFoundException();
            }
           
           
        }

        public void ReportPost(Guid postId, Guid userId, int reasonId)
        {
			// check if post is already reported

			var doesPostIdExist = _postDataAccess.DoesPostIdExist(postId);
            var doesUserIdExist = _userDataAccess.DoesUserIdExist(userId);
			
            if(doesPostIdExist == true && doesUserIdExist == true)
            {
				var didUserAlreadyReport = IsPostReported(userId, postId);

				if (didUserAlreadyReport == false)
				{
					_reportedPostsDataAccess.CreateRecord(postId, userId, reasonId);
				}
				else
				{
					throw new AccessException("You have already reported this post. Cannot report again.");
				}
			}
            else
            {
                throw new ItemNotFoundException("Invalid postId or userId");
            }
			
        }

        public void RemovePost(Guid postId, Guid communityId, Guid moderatorId)
        {
			

			var doesPostIdExist = _postDataAccess.DoesPostIdExist(postId);
			var doesModeratorIdExist = _userDataAccess.DoesUserIdExist(moderatorId);

            var isModeratorValid = _communityModeratorsDataAccess.CheckRecordExists(communityId, moderatorId);

            if(doesPostIdExist == true)
            {
                if ((doesModeratorIdExist == true && isModeratorValid == true) || moderatorId == BotModeratorId)
                {
                    var isPostAlreadyRemoved = IsPostRemoved(postId);
                    if (isPostAlreadyRemoved == false)
                    {
                        _removedPostsDataAccess.CreateRecord(postId);
                    }
                    else
                    {
                        throw new AccessException("Post has already been removed");
                    }
                }
                else
                {
                    throw new AccessException("You dont have access to remove post");
                }
			}
            else
            {
                throw new ItemNotFoundException("Invalid postId");
            }


        }

        public List<ReportReasonsDto>LoadReportReasonsDtos()
        {
            var reportReasonsDtos = _reportReasonsDataAccess.LoadReportReasonsDtos();

            return reportReasonsDtos;
        }

        public List<Post>LoadAllReportedPosts()
        {
            var ReportedPostIds = _reportedPostsDataAccess.LoadAllReportedPostIds();

            List<Post> reportedPosts = new List<Post>();

            foreach(var id in ReportedPostIds)
            {
                var Post = _postDataAccess.LoadPostById(id);

                reportedPosts.Add(Post);
            }

            return reportedPosts;


        }

        public int GetNumberOfReportsInPost(Guid postId)
        {
            var num = _reportedPostsDataAccess.GetReportCountInPost(postId);
            return num;
        }

    }
}
