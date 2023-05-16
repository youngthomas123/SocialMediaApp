using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Interfaces.IDataAccess
{
	public interface IUserFriendsDataAccess
	{
		void CreateRecord(Guid userId, Guid friendId);

		void DeleteRecord(Guid userId, Guid friendId);

        List<Guid> GetUserFriends(Guid userId);

		bool CheckRecordExists(Guid userId, Guid friendId);
    }
}
