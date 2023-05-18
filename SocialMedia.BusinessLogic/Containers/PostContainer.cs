﻿using SocialMedia.BusinessLogic.Dto;
using SocialMedia.BusinessLogic.Interfaces.IContainer;
using SocialMedia.BusinessLogic.Interfaces.IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Containers
{
    public class PostContainer : IPostContainer
    {

        private readonly IPostDataAccess _postDataAccess;

        private readonly IUserDataAccess _userDataAccess;

        private readonly ICommunityDataAccess _communityDataAccess;

        private readonly IUpvotedPostsDataAccess _upvotedPostsDataAccess;

        private readonly IDownvotedPostsDataAccess _downvotedPostsDataAccess;
        
        public PostContainer(IPostDataAccess postDataAcess, IUserDataAccess userDataAccess, ICommunityDataAccess communityDataAccess, IUpvotedPostsDataAccess upvotedPostsDataAccess, IDownvotedPostsDataAccess downvotedPostsDataAccess)
        {
            _postDataAccess = postDataAcess;
            _userDataAccess = userDataAccess;
            _communityDataAccess = communityDataAccess;
            _upvotedPostsDataAccess = upvotedPostsDataAccess;
            _downvotedPostsDataAccess = downvotedPostsDataAccess;
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
      
            var post = _postDataAccess.LoadPostById(postId);

            SetVoteUserIds(post);
            return post;
        }

        public void SavePost(Post post)
        {
            _postDataAccess.SavePost(post);
        }

        public void UpdatePost(Post post)
        {

            _postDataAccess.UpdatePost(post);
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


				postPageDtos.Add(postPageDto);  
            }
            return postPageDtos;
        }
        public PostPageDto GetPostPageDtoById(Guid postid, Guid userId)  
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


			return postPageDto; 
        }
        public List<PostPageDto> GetPostPageDtosByCommunity(Guid communityId, Guid userId)
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


                postPageDtos.Add(postPageDto);
            }
            return postPageDtos;
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

        public void UpdatePostScore(Post post,Guid userId ,string direction)
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
       
    }
}
