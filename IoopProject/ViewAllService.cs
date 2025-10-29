using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace IoopProject
{
    public class ServiceDetails
    {
        public string ServiceName { get; set; }
        public string Price { get; set; }
        public string Time { get; set; }
        public string ServiceDescription { get; set; }
    }

    public class ViewAllService
    {
        static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myCS"].ToString());

        public List<ServiceDetails> GetAllServices()
        {
            List<ServiceDetails> serviceList = new List<ServiceDetails>();

            try
            {
                con.Open();

                string query = "SELECT Service_Name, Price, Estimated_Time, Service_Description FROM service";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ServiceDetails service = new ServiceDetails
                    {
                        ServiceName = reader["Service_Name"].ToString(),
                        Price = reader["Price"].ToString(),
                        Time = reader["Estimated_Time"].ToString(),
                        ServiceDescription = reader["Service_Description"].ToString()
                    };

                    serviceList.Add(service);
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

            return serviceList;
        }
    }
}
