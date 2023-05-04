using SocialMedia.BusinessLogic.Dto;
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

        private readonly IUserDataAccess _userDataAccess;

        private readonly ICommunityDataAccess _communityDataAccess;
        
        public PostContainer(IPostDataAccess postDataAcess, IUserDataAccess userDataAccess, ICommunityDataAccess communityDataAccess)
        {
            _postDataAccess = postDataAcess;
            _userDataAccess = userDataAccess;
            _communityDataAccess = communityDataAccess;
        }

        public List<Post> LoadAllPosts()
        {
            List<Post>posts = new List<Post>();
            posts = _postDataAccess.LoadPost();
            return posts;
        }

        public Post? LoadPostById(Guid postId)
        {
      
            var post = _postDataAccess.LoadPostById(postId);    
            return post;
        }

        public void SavePost(Post post)
        {
            _postDataAccess.SavePost(post);
        }

        public void UpdatePost(Post post)
        {
            _postDataAccess.UpdatePost(post);
        }
        public List<PostPageDto>GetPostPageDtos()
        {
            List<PostPageDto>postPageDtos = new List<PostPageDto>();    

            foreach (Post post in _postDataAccess.LoadPost())
            {
                PostPageDto postPageDto = new PostPageDto();

                postPageDto.Author = _userDataAccess.GetUserName(post.UserId);
                postPageDto.CommunityName = _communityDataAccess.GetCommunityName(post.CommunityId);
                postPageDto.DateCreated = post.DateCreated;
                postPageDto.Title = post.Title;
                postPageDto.Body = post.Body;
                postPageDto.Score = post.Score;
                postPageDto.PostId = post.PostId;

                postPageDtos.Add(postPageDto);  
            }
            return postPageDtos;
        }
        public PostPageDto GetPostPageDtoById(Guid id)
        {
            PostPageDto postPageDto = new PostPageDto();    

            var post = _postDataAccess.LoadPostById(id);

            postPageDto.Author = _userDataAccess.GetUserName(post.UserId);
            postPageDto.CommunityName = _communityDataAccess.GetCommunityName(post.CommunityId);
            postPageDto.DateCreated = post.DateCreated;
            postPageDto.Title = post.Title;
            postPageDto.Body = post.Body;
            postPageDto.Score = post.Score;
            postPageDto.PostId = post.PostId;

            return postPageDto; 
        }
       
    }
}
