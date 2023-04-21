using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMedia.BusinessLogic.Interfaces.IDataAccess;

namespace SocialMedia.BusinessLogic.Containers
{
    public class CommentContainer
    {
        private readonly ICommentDataAccess commentDataAccess;

        public CommentContainer(ICommentDataAccess dataAcess)
        {
            commentDataAccess = dataAcess;
        }

        public List<Comment>GetComments() 
        {
            List <Comment>comments = new List<Comment>();   

              foreach(Comment comment in commentDataAccess.LoadComment())
              {
                comments.Add(comment);
              }

              return comments;  



        }


    }
}
