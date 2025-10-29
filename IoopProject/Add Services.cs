using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoopProject
{
    public class AddServices
    {
        private string serviceName;
        private string price;
        private string time;
        private string serviceDescription;
        static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myCS"].ToString());

        public string ServiceName { get => serviceName; set => serviceName = value; }
        public string Price { get => price; set => price = value; }
        public string Time { get => time; set => time = value; }
        public string ServiceDescription { get => serviceDescription; set => serviceDescription = value; }

        public string AddService()
        {
            string status;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Service (Service_Name, Price, Estimated_Time, Service_Description) VALUES (@serviceName, @price, @time, @serviceDescription)", con);
                cmd.Parameters.AddWithValue("@serviceName", serviceName);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@time", time);
                cmd.Parameters.AddWithValue("@serviceDescription", serviceDescription);

                int rows = cmd.ExecuteNonQuery();
                status = (rows > 0) ? "Service added successfully." : "Failed to add service.";
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
