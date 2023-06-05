using SocialMedia.BusinessLogic.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Interfaces.IContainer
{
    public interface IPostContainer
    {
        void CreateAndSavePost(Guid userId, string title, string? body, string? imageUrl, Guid communityid);

        //old
        void Upvote(Guid postId, string direction, Guid userId);

        void RemoveUpvote(Guid postId, string direction, Guid userId);

        void Downvote(Guid postId, string direction, Guid userId);

        void RemoveDownvote(Guid postId, string direction, Guid userId);

        // old
        List<Post> LoadAllPosts();
        Post LoadPostById(Guid postId);
        void SavePost(Post post);

        void UpdatePost(Post post);

        List<PostPageDto> GetPostPageDtos(Guid userId);

        PostPageDto GetPostPageDtoById(Guid postid, Guid userId);

        List<PostPageDto> GetPostPageDtosByCommunity(Guid communityId, Guid userId);

        bool IsPostUpvoted(Guid userId, Guid postId);

        bool IsPostDownvoted(Guid userId, Guid postId);

        bool IsPostReported(Guid userId, Guid postId);


		void UpdatePostScore(Post post, Guid userId, string UpOrDown);

        void SetVoteUserIds(Post post);

        void UpdatePost(Guid postId, string title, string? body, string? imageUrl, Guid LoggedInUserId);

        void DeletePost(Guid PostId, Guid LoggedInUserId);

        void ReportPost(Guid postId, Guid userId, int reasonId);

        List<ReportReasonsDto> LoadReportReasonsDtos();

        List<Post> LoadAllReportedPosts();

        int GetNumberOfReportsInPost(Guid postId);



    }
}
