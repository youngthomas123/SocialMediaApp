using SocialMedia.BusinessLogic.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Interfaces.IDataAccess
{
    public interface IProfileDataAccess
    {
        void CreateRecord(Guid userId, string username);

        void DeleteRecord(string userId);

        void UpdateRecord(Guid userId, string username, string bio, string gender, byte[] picture, string location);

        void UpdateProfilePicture(Guid UserId, byte[] picture);

        byte[] GetProfilePicture(Guid UserId);

        ProfileDto LoadProfileRecord(Guid userId);

        void UpdateRecord(Guid userId, string username, string bio, string gender, string location);

    }
}
