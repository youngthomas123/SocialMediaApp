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
        private readonly ICommentDataAccess commentDataAcess;

        public CommentContainer(ICommentDataAccess dataAcess)
        {
            commentDataAcess = dataAcess;
        }

        public List<Comment>GetComments() 
        {
            List <Comment>comments = new List<Comment>();   

              foreach(Comment comment in commentDataAcess.LoadComment())
              {
                comments.Add(comment);
              }

              return comments;  



        }


    }
}
