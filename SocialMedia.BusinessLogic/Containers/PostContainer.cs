using SocialMedia.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Containers
{
    public class PostContainer : IPostContainer
    {

        private readonly IPostDataAcess _postDataAcess;
        
        public PostContainer(IPostDataAcess postDataAcess)
        {
            _postDataAcess = postDataAcess;
        }

        public List<Post> LoadAllPosts()
        {
            List<Post>posts = new List<Post>();
            posts = _postDataAcess.LoadPost();
            return posts;
        }

        public Post? LoadPostById(Guid postId)
        {
            Post ThePost = null;
            foreach (Post post in _postDataAcess.LoadPost())
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
            _postDataAcess.SavePost(post);
        }

        public void UpdatePost(Post post)
        {
            _postDataAcess.UpdatePost(post);
        }

       
    }
}
