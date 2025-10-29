using System;
using System.Configuration;
using System.Data.SqlClient;

namespace IoopProject
{
    public class ServiceRetriever
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

        public (string serviceName, string price, string time, string serviceDescription) GetServiceDetails()
        {
            string retrievedServiceName = string.Empty;
            string retrievedPrice = string.Empty;
            string retrievedTime = string.Empty;
            string retrievedServiceDescription = string.Empty;

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    "SELECT Service_Name, Price, Estimated_Time, Service_Description FROM service WHERE Service_Name = @serviceName", con);
                cmd.Parameters.AddWithValue("@serviceName", serviceName);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    retrievedServiceName = reader["Service_Name"].ToString();
                    retrievedPrice = reader["Price"].ToString();
                    retrievedTime = reader["Estimated_Time"].ToString();
                    retrievedServiceDescription = reader["Service_Description"].ToString();
                }
                else
                {
                    Console.WriteLine("Service not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }

            return (retrievedServiceName, retrievedPrice, retrievedTime, retrievedServiceDescription);
        }
    }
}
