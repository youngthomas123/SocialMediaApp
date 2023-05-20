using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Interfaces.IDataAccess
{
	public interface IUpvotedCommentsDataAccess
	{

		void CreateRecord(Guid userId, Guid commentId);

		void DeleteRecord(Guid userId, Guid commentId);

		bool HasUserUpvoted(Guid userId, Guid commentId);

		List<Guid> GetUpvotedUserIdsByComment(Guid commentId);

		void DeleteRecord(Guid commentId);


    }
}
