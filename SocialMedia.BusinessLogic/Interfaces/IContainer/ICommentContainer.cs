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

        List<CommentPageDto> GetCommentPageDtosInPost(Guid postId, Guid userId);

        Comment LoadCommentById(Guid commentId);

        void UpDateComment(Comment comment);

        void UpdateCommentScore(Comment comment, Guid userId, string UpOrDown);

        bool IsCommentUpvoted(Guid userId, Guid commentId);

        bool IsCommentDownvoted(Guid userId, Guid commentId);

        void UpdateComment(Guid commentId, string body, Guid LoggedInUserId);

        void DeleteComment(Guid commentId);


    }
}
