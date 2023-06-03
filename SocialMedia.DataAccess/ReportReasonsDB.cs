using SocialMedia.BusinessLogic;
using SocialMedia.BusinessLogic.Dto;
using SocialMedia.BusinessLogic.Interfaces.IDataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.DataAccess
{
    public class ReportReasonsDB : IReportReasonsDataAccess
    {
        private string connection = "Server=mssqlstud.fhict.local;Database=dbi511464_i511464fh;User Id=dbi511464_i511464fh;Password=12345;";


        public List<ReportReasonsDto> LoadReportReasonsDtos()
        {
            List <ReportReasonsDto> reportReasonsDtos = new List<ReportReasonsDto>();

            SqlConnection conn = new SqlConnection(connection);

            conn.Open();

            string sql = $"select * " +
                         $"from ReportReasons ";


            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            int IdIndex = dr.GetOrdinal("Id");
            int ReasonIndex = dr.GetOrdinal("Reason");
            int ExplanationIndex = dr.GetOrdinal("Explanation");
           


            while (dr.Read())
            {
                var Id = (int)dr[IdIndex];
                var Reason = (string)dr[ReasonIndex];
                var Explanation = (string)dr[ExplanationIndex];

                ReportReasonsDto reportReasonsDto = new ReportReasonsDto();

                reportReasonsDto.Id = Id;
                reportReasonsDto.Reason = Reason;
                reportReasonsDto.Explanation = Explanation;

                reportReasonsDtos.Add(reportReasonsDto);    
            }


            dr.Close();

            conn.Close();

            return reportReasonsDtos;

        }

    }
}
