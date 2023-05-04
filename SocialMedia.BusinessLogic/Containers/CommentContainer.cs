using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMedia.BusinessLogic.Interfaces.IContainer;
using SocialMedia.BusinessLogic.Interfaces.IDataAccess;

namespace SocialMedia.BusinessLogic.Containers
{
    public class CommentContainer : ICommentContainer
    {
        private readonly ICommentDataAccess _commentDataAccess;

        public CommentContainer(ICommentDataAccess dataAcess)
        {
            _commentDataAccess = dataAcess;
        }

        public List<Comment>GetComments() 
        {
           
             var  comments = _commentDataAccess.LoadComments();

              return comments;  

        }


    }
}
