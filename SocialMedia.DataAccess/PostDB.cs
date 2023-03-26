using SocialMedia.BusinessLogic;
using SocialMedia.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.DataAccess
{
    public class PostDB : IPostDataAcess
    {
        private string connection = "Server=mssqlstud.fhict.local;Database=dbi511464_i511464fh;User Id=dbi511464_i511464fh;Password=12345;";

        public void DeletePost(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Post> LoadPost()
        {
            throw new NotImplementedException();
        }

        public void SavePost(Post post)
        {
            throw new NotImplementedException();
        }

        public void UpdatePost(Post post)
        {
            throw new NotImplementedException();
        }
    }
}
