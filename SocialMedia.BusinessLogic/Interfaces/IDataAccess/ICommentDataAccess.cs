using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Interfaces.IDataAccess
{
    public interface ICommentDataAccess
    {
        void SaveComment(Comment comment);

        void UpdateComment(Comment comment);

        List<Comment> LoadComments();

        void DeleteComment(Guid id);

        List<Comment> LoadCommentsInPost(Guid postId);

        Comment LoadCommentById(Guid commentId);


    }
}
