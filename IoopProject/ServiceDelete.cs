using System;
using System.Configuration;
using System.Data.SqlClient;

namespace IoopProject
{
    public class DeleteService
    {
        private string serviceName;
        static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myCS"].ToString());

        public string ServiceName { get => serviceName; set => serviceName = value; }

        public string DeleteServiceAndDependencies()
        {
            string status;
            try
            {
                con.Open();

                SqlTransaction transaction = con.BeginTransaction();

                try
                {
                    SqlCommand deleteFeedbackCmd = new SqlCommand(
                        "DELETE FROM Feedback WHERE ServiceID IN (SELECT ServiceID FROM Service WHERE Service_Name = @serviceName)", con, transaction);
                    deleteFeedbackCmd.Parameters.AddWithValue("@serviceName", serviceName);
                    int feedbackRows = deleteFeedbackCmd.ExecuteNonQuery();
                    Console.WriteLine($"Deleted rows in Feedback: {feedbackRows}");

                    SqlCommand deleteServiceCmd = new SqlCommand(
                        "DELETE FROM Service WHERE Service_Name = @serviceName", con, transaction);
                    deleteServiceCmd.Parameters.AddWithValue("@serviceName", serviceName);
                    int serviceRows = deleteServiceCmd.ExecuteNonQuery();
                    status = (serviceRows > 0) ? "Service deleted successfully." : "Failed to delete service. Service not found.";
                    Console.WriteLine($"Deleted rows in Service: {serviceRows}");

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                status = "Error: " + ex.Message;
            }
            finally
            {
                con.Close();
            }
            return status;
        }
    }
}
