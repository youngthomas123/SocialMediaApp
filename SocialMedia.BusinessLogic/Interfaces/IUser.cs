using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Interfaces
{
    public interface IUser
    {
        void UpdateProfilePic(byte[] pic);

        void UpdateProfile(string? bio, string? gender, string? location, byte[]? pic);

        void SetSalt(string salt);

        void SetHashedPassword(string password);

        void AddToUserCreatedComments(Comment comment);

        void AddToUserCreatedPosts(Post post);

        void AddToUserFollowingCommunities(string communityName);

        void AddToUserModeratingCommunities(string communityName);

        void AddToUserFriends(string friendName);

        void AddToReceivedMessages(Message message);

        // properties

        Guid UserId { get; }
        string UserName { get; }
        string Password { get; }
        string Salt { get; }
        string? Email { get; }
        DateTime DateCreated { get; }
        string? Bio { get; }
        string? Gender { get; }
        string? Location { get; }
        byte[]? ProfilePicture { get; }
        List<Post>? UserCreatedPosts { get; }
        List<Comment>? UserCreatedComments { get; }

        List<string>? UserFollowingCommunities { get; }

        List<string>? UserModeratingCommunities { get; }

        List<string>? UserFriends { get; }

        List<Message>? ReceivedMessages { get;  }






    }
}
