using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace IoopProject
{
    public class ReportDetails
    {
        public int ReportID { get; set; }
        public string TotalProfit { get; set; }
        public string Month { get; set; }
        public string ServiceName { get; set; }
        public string CompletionStatus { get; set; }
    }

    public class ViewReport
    {
        static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myCS"].ToString());

        public List<ReportDetails> GetFilteredReports(string filterCriteria)
        {
            List<ReportDetails> reportList = new List<ReportDetails>();

            try
            {
                con.Open();

                string query = @"
                    SELECT r.ReportID, r.Profit, r.Month, s.Service_Name, rec.completionStatus
                    FROM Report r
                    JOIN Service s ON r.ServiceID = s.ServiceID
                    JOIN Record rec ON r.ServiceID = rec.ServiceID
                    WHERE r.Month = @filterCriteria";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@filterCriteria", filterCriteria);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ReportDetails report = new ReportDetails
                    {
                        ReportID = reader.GetInt32(0),
                        TotalProfit = reader.GetString(1),
                        Month = reader.GetString(2),
                        ServiceName = reader.GetString(3),
                        CompletionStatus = reader.GetString(4)
                    };

                    reportList.Add(report);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }

            return reportList;
        }
    }
}
