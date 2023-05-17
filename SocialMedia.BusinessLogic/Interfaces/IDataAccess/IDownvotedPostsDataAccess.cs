using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Interfaces.IDataAccess
{
    public interface IDownvotedPostsDataAccess
    {
        void CreateRecord(Guid userId, Guid postId);

        void DeleteRecord(Guid userId, Guid postId);

        bool HasUserDownvoted(Guid userId, Guid postId);

        List<Guid> GetDownvotedUserIdsByPost(Guid postId);
    }
}
