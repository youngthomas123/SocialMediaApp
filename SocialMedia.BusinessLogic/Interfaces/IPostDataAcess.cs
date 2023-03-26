using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Interfaces
{
    public interface IPostDataAcess
    {
        void SavePost(Post post);

        void UpdatePost(Post post);

        List<Post> LoadPost();

        void DeletePost(Guid id);


    }
}
