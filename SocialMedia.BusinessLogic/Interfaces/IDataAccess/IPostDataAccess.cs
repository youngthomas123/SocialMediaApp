using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Interfaces.IDataAccess
{
    public interface IPostDataAccess
    {
        void SavePost(Post post);

        void UpdatePost(Post post);

        List<Post> LoadPost();

        void DeletePost(Guid id);
        List<string> GetPostIds(Guid communityId);

        Post LoadPostById(Guid postId);

        List<Post> LoadPostsByCommunity(Guid communityId);
    }
}
