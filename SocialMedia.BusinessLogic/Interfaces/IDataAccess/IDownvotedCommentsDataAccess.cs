using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Interfaces.IDataAccess
{
	public interface IDownvotedCommentsDataAccess
	{
		void CreateRecord(Guid userId, Guid commentId);

		void DeleteRecord(Guid userId, Guid commentId);

		bool HasUserDownvoted(Guid userId, Guid commentId);




	}
}
