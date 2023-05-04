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

        List<PostPageDto> GetPostPageDtos();

        PostPageDto GetPostPageDtoById(Guid id);
    }
}
