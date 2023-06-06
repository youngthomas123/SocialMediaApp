using SocialMedia.BusinessLogic.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Interfaces
{
	public interface IContentFilterAndRanking
	{
		List<PostPageDto> PostsforMainFeed(List<PostPageDto> Loadedposts, Guid userId);

		List<PostPageDto> PostsforCommunityFeed(List<PostPageDto> Loadedposts);

		List<CommentPageDto> CommentsforCommentSection(List<CommentPageDto> Loadedcomments);
	}
}
