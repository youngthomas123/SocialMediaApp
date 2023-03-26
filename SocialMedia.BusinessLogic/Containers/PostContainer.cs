using SocialMedia.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Containers
{
    public class PostContainer
    {

        private readonly IPostDataAcess postDataAcess;
        
        public PostContainer(IPostDataAcess dataAcess)
        {
            postDataAcess = dataAcess;
        }
    }
}
