using SocialMedia.BusinessLogic.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Interfaces.IDataAccess
{
    public interface IReportReasonsDataAccess
    {
        List<ReportReasonsDto> LoadReportReasonsDtos();
    }
}
