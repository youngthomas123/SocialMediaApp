using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Dto
{
    public class ReportReasonsDto
    {
        public ReportReasonsDto() { }

        public int Id { get; set; }

        public string Reason { get; set; }

        public string Explanation { get; set; }
    }
}
