using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Interfaces.IDataAccess
{
    public interface IUpvotedPostsDataAccess
    {
        void CreateRecord(Guid userId, Guid postId);

        void DeleteRecord(Guid userId, Guid postId);

        bool HasUserUpvoted(Guid userId, Guid postId);

    }
}
