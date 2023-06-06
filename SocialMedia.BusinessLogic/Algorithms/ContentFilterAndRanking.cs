﻿using SocialMedia.BusinessLogic.Dto;
using SocialMedia.BusinessLogic.Interfaces;
using SocialMedia.BusinessLogic.Interfaces.IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Algorithms
{
	public class ContentFilterAndRanking : IContentFilterAndRanking
	{
		private readonly IRemovedPostsDataAccess _removedPostsDataAccess;
		private readonly IRemovedCommentsDataAccess _removedCommentsAccess;
		private readonly ICommunityMembersDataAccess _communityMembersAccess;
		private readonly ICommunityDataAccess _communityDataAccess;





		public ContentFilterAndRanking(IRemovedPostsDataAccess removedPostsDataAccess, IRemovedCommentsDataAccess removedCommentsDataAccess, ICommunityMembersDataAccess communityMembersDataAccess, ICommunityDataAccess communityDataAccess) 
		{
			_removedPostsDataAccess = removedPostsDataAccess;
			_removedCommentsAccess = removedCommentsDataAccess;
			_communityMembersAccess = communityMembersDataAccess;
			_communityDataAccess = communityDataAccess;
		}


		private List<PostPageDto> FilterRemovedPostsFromList(List<PostPageDto>Posts, List<Guid> RemovedPostIds)
		{
			List<PostPageDto> FilteredPosts = new List<PostPageDto>();

			foreach(var post in Posts)
			{
				if(!RemovedPostIds.Contains(post.PostId))
				{
					FilteredPosts.Add(post);
				}
			}
			return FilteredPosts;
		}



		private List<PostPageDto>OnlyPostsFromUserFollowingCommunities(List<PostPageDto> Posts, List<Guid>FollowingCommunityIds)
		{
			List<PostPageDto> FilteredPosts = new List<PostPageDto>();

			foreach(var post in Posts)
			{
				var communityId = new Guid(_communityDataAccess.GetCommunityId(post.CommunityName));
				if(FollowingCommunityIds.Contains(communityId))
				{
					FilteredPosts.Add(post);
				}
			}
			return FilteredPosts;

		}

		private List<PostPageDto>RankPostsOnScore(List<PostPageDto> Posts)
		{
			List<PostPageDto> rankedPosts = Posts.OrderByDescending(p => p.Score).ToList();
			return rankedPosts;

		}

		public List<PostPageDto>PostsforMainFeed(List<PostPageDto>Loadedposts, Guid userId)
		{
			List<PostPageDto> posts = Loadedposts;

			var RemovedPostIds = _removedPostsDataAccess.GetRemovedPostIds();
			var UserFollowingCommunityIds = _communityMembersAccess.LoadCommunityIdsByMember(userId);

			posts = FilterRemovedPostsFromList(posts, RemovedPostIds);

			posts = OnlyPostsFromUserFollowingCommunities(posts, UserFollowingCommunityIds);

			posts = RankPostsOnScore(posts);

			return posts;	
		}

	}
}
