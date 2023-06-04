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

        void UpdateProfile(string bio, string gender, string location, byte[] pic);

        void SetSalt(string salt);

        void SetHashedPassword(string password);

        void AddToUserCreatedComments(Comment comment);

        void AddToUserCreatedPosts(Post post);


    }
}
