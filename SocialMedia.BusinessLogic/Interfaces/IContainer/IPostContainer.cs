using SocialMedia.BusinessLogic.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Interfaces.IContainer
{
    public interface IPostContainer
    {
        List<Post> LoadAllPosts();
        Post LoadPostById(Guid postId);
        void SavePost(Post post);

        void UpdatePost(Post post);

        List<PostPageDto> GetPostPageDtos(Guid userId);

        PostPageDto GetPostPageDtoById(Guid postid, Guid userId);

        List<PostPageDto> GetPostPageDtosByCommunity(Guid communityId, Guid userId);

        bool IsPostUpvoted(Guid userId, Guid postId);

        bool IsPostDownvoted(Guid userId, Guid postId);

        void UpdatePostScore(Post post, Guid userId, string UpOrDown);

	}
}
