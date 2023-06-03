using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Interfaces.IDataAccess
{
	public interface IReportedCommentsDataAccess
	{
		 void CreateRecord(Guid commentId, Guid userId, int reasonId);

		 void DeleteRecord(Guid commentId, Guid userId);

		 void DeleteRecord(Guid commentId);

		 bool CheckRecordExists(Guid commentId, Guid userId);
	}
}
