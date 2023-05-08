using SocialMedia.BusinessLogic.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Interfaces.IContainer
{
    public interface ICommentContainer
    {
        List<Comment> GetComments();

        void AddComment(Comment comment);

        List<Comment> LoadCommentInPost(Guid PostId);

        List<CommentPageDto> GetCommentPageDtosInPost(Guid postId);

        Comment LoadCommentById(Guid commentId);

        void UpDateComment(Comment comment);
    }
}
