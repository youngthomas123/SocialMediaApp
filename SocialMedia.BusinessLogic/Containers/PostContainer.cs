using SocialMedia.BusinessLogic.Interfaces.IContainer;
using SocialMedia.BusinessLogic.Interfaces.IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Containers
{
    public class PostContainer : IPostContainer
    {

        private readonly IPostDataAccess _postDataAccess;
        
        public PostContainer(IPostDataAccess postDataAcess)
        {
            _postDataAccess = postDataAcess;
        }

        public List<Post> LoadAllPosts()
        {
            List<Post>posts = new List<Post>();
            posts = _postDataAccess.LoadPost();
            return posts;
        }

        public Post? LoadPostById(Guid postId)
        {
            Post ThePost = null;
            foreach (Post post in _postDataAccess.LoadPost())
            {
                if(post.PostId == postId)
                {
                    ThePost = post;
                    break;
                    
                }
            }
            return ThePost;
        }

        public void SavePost(Post post)
        {
            _postDataAccess.SavePost(post);
        }

        public void UpdatePost(Post post)
        {
            _postDataAccess.UpdatePost(post);
        }

       
    }
}
