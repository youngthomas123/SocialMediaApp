using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMedia.BusinessLogic.Dto;
using SocialMedia.BusinessLogic.Interfaces.IContainer;
using SocialMedia.BusinessLogic.Interfaces.IDataAccess;

namespace SocialMedia.BusinessLogic.Containers
{
    public class CommentContainer : ICommentContainer
    {
        private readonly ICommentDataAccess _commentDataAccess;
        private readonly IUserDataAccess _userDataAccess;

        public CommentContainer(ICommentDataAccess dataAcess, IUserDataAccess userDataAccess)
        {
            _commentDataAccess = dataAcess;
            _userDataAccess = userDataAccess;
        }

        public List<Comment>GetComments() 
        {
           
             var  comments = _commentDataAccess.LoadComments();

              return comments;  

        }
        public void AddComment(Comment comment)
        {
            _commentDataAccess.SaveComment(comment);
        }
        public List<Comment> LoadCommentInPost(Guid PostId)
        {
            var comments = _commentDataAccess.LoadCommentsInPost(PostId);
            return comments;
        }
        public List<CommentPageDto> GetCommentPageDtosInPost(Guid postId)
        {
            List<CommentPageDto> commentPageDtos = new List<CommentPageDto>();

            foreach (Comment comment in _commentDataAccess.LoadCommentsInPost(postId))
            {
                CommentPageDto commentPageDto = new CommentPageDto();

                commentPageDto.Author = _userDataAccess.GetUserName(comment.UserId);

                commentPageDto.DateCreated = comment.DateCreated;
                
                commentPageDto.Body = comment.Body;
                commentPageDto.Score = comment.Score;
                commentPageDto.PostId = comment.PostId;
                commentPageDto.CommentId = comment.CommentId;

                commentPageDtos.Add(commentPageDto);
            }
            return commentPageDtos;
        }
        public Comment LoadCommentById(Guid commentId)
        {
            var comment = _commentDataAccess.LoadCommentById(commentId);
            return comment;
        }

        public void UpDateComment(Comment comment)
        {
            _commentDataAccess.UpdateComment(comment);
        }
    }
}
