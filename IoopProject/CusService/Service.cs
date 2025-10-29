using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace IoopProject.CusService
{
    internal class Service
    {

        private static string serviceID;
        private static string serviceName;
        private static string price;
        private static string time;
        private static string description;

        static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myCS"].ToString());

        public string ServiceID { get => serviceID; set => serviceID = value; }
        public string ServiceName { get => serviceName; set => serviceName = value; }
        public string Price { get => price; set => price = value; }
        public string Time { get => time; set => time = value; }
        public string Description { get => description; set => description = value; }

        // ----- Full Service Constructor (For Admin or Mechanic) ----- //

        public Service(string serviceName, string price, string time, string description)
        {
            ServiceName = serviceName;
            Price = price;
            Time = time;
            Description = description;
        }

        // ----- Service Constructor for Details ----- //

        public Service(string serviceName)
        {
            ServiceName = serviceName;
            con.Open();

            SqlCommand cmd = new SqlCommand(
                "SELECT ServiceID, Price, Estimated_Time, Service_Description FROM Service " +
                "WHERE Service_Name = @sName", con);
            cmd.Parameters.AddWithValue("@sName", serviceName);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                ServiceID = reader.GetInt32(0).ToString();
                Price = reader.GetString(1);
                Time = reader.GetString(2);
                Description = reader.GetString(3);
                reader.Close();
            }
            con.Close();
        }

        // ----- Service ID Check ----- //

        public Service(int SID)
        {
            ServiceID = SID.ToString();
            con.Open();

            SqlCommand cmd = new SqlCommand(
                "SELECT Service_Name, Price, Estimated_Time, Description FROM Service " +
                "WHERE ServiceID = @SID", con);
            cmd.Parameters.AddWithValue("@SID", SID);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                ServiceName = reader.GetString(0);
                Price = reader.GetString(1);
                Time = reader.GetString(2);
                Description = reader.GetString(3);
                reader.Close();
            }
            else
            {
                MessageBox.Show("Service does not exist.");
            }
            con.Close();
        }

        // ----- Add Service ----- //

        public string AddService()
        {
            string status;
            con.Open();

            if (string.IsNullOrEmpty(ServiceName) || string.IsNullOrEmpty(Price) || string.IsNullOrEmpty(Time))
            {
                con.Close();
                status = "Please enter all the information.";
            }
            else
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Service WHERE Service_Name = @serviceName", con);
                cmd.Parameters.AddWithValue("@serviceName", serviceName);
                int serviceCount = (int)cmd.ExecuteScalar();
                if (serviceCount > 0)
                {
                    con.Close();
                    status = "Service Exists";
                }
                else
                {
                    SqlCommand cmd2 = new SqlCommand(
                        "INSERT INTO service (Service_Name, Price, Estimated_Time, Description) VALUES (@serviceName, @price, @time, @desc);", con);
                    cmd2.Parameters.AddWithValue("@serviceName", ServiceName);
                    cmd2.Parameters.AddWithValue("@price", Price);
                    cmd2.Parameters.AddWithValue("@time", Time);
                    cmd2.Parameters.AddWithValue("@desc", Description);
                    int i = Convert.ToInt32(cmd2.ExecuteScalar());
                    if (i != 0)
                    {
                        status = "Service Added.";
                    }
                    else
                    {
                        status = "Error";
                    }
                }
            }
            con.Close();
            return status;
        }
    }
}