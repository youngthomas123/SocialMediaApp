using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMedia.BusinessLogic.Interfaces;

namespace SocialMedia.BusinessLogic.Containers
{
    public class CommentContainer
    {
        private readonly ICommentDataAcess commentDataAcess;

        public CommentContainer(ICommentDataAcess dataAcess)
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
