using System;
using System.Configuration;
using System.Data.SqlClient;

namespace IoopProject
{
    public class ServiceUpdate
    {
        private string oldServiceName;
        private string serviceName;
        private string price;
        private string time;
        private string serviceDescription;
        static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myCS"].ToString());

        public string OldServiceName { get => oldServiceName; set => oldServiceName = value; }
        public string ServiceName { get => serviceName; set => serviceName = value; }
        public string Price { get => price; set => price = value; }
        public string Time { get => time; set => time = value; }
        public string ServiceDescription { get => serviceDescription; set => serviceDescription = value; }

        public string UpdateServiceDetails()
        {
            string status;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    "UPDATE service SET Service_Name = @serviceName, Price = @price, Estimated_Time = @time, Service_Description = @serviceDescription WHERE Service_Name = @oldServiceName", con);
                cmd.Parameters.AddWithValue("@oldServiceName", oldServiceName);
                cmd.Parameters.AddWithValue("@serviceName", serviceName);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@time", time);
                cmd.Parameters.AddWithValue("@serviceDescription", serviceDescription);

                int rows = cmd.ExecuteNonQuery();
                status = (rows > 0) ? "Service updated successfully." : "Failed to update service.";
                Console.WriteLine($"Updated rows: {rows}");
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
