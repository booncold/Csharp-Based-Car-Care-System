using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IoopProject
{
    internal class Service2
    {

        public Service2()
        {

        }
        string staffId;
        public Service2(string staffid)
        {
            this.staffId = staffid; 
        }

        public ArrayList ListPartName()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myCS"].ToString());
            ArrayList nm = new ArrayList();
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT Part_Name FROM Inventory", con);

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                nm.Add(rd["Part_Name"].ToString());
            }
            con.Close();
            return nm;
        }
        public ArrayList ListPart()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myCS"].ToString());
            ArrayList nm = new ArrayList();
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Inventory", con);

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                nm.Add($"Name: {rd["Part_Name"].ToString()} | Quantity: {rd["Quantity_Available"].ToString()} | Stuats: {rd["Part_Status"].ToString()}");
            }
            con.Close();
            return nm;
        }
        public ArrayList ListService()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myCS"].ToString());
            ArrayList nm = new ArrayList();
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Assign", con);

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                nm.Add($"Service: {rd["service_name"].ToString()} Vehicle Number: {rd["vehicle_number"].ToString()} | Date: {rd["appointment_date"].ToString()}");
            }
            con.Close();
            return nm;
        }
        public ArrayList ListServiceName()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myCS"].ToString());
            ArrayList nm = new ArrayList();
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Assign", con);

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                nm.Add(rd["service_name"].ToString());
            }
            con.Close();
            return nm;
        }
        public void UpdatePart(string selectedPartName, string newStatus)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["myCS"].ToString()))
            {
                string query = "UPDATE Inventory SET Part_Status = @Status WHERE Part_Name = @PartName";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Status", newStatus);
                    command.Parameters.AddWithValue("@PartName", selectedPartName);
                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Part status updated successfully.");
                        }
                        else
                        {
                            MessageBox.Show("No matching part found.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred: " + ex.Message);
                    }
                }
            }

        }
        private int GetStaffeID(string staffName)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myCS"].ToString());
            string query = "SELECT StaffID FROM Staff WHERE name = @staffName";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@staffName", staffName);
            conn.Open();
            int result = (int)command.ExecuteScalar();
            return result;
        }
        public void AddPart(string namePart, int numPart, string name, string stat = "Requested")
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myCS"].ToString());
            string status;
            conn.Open();
            int staffID = GetStaffeID(name);
            SqlCommand cmd2 = new SqlCommand("INSERT INTO Inventory (Part_Name, Quantity_Available, Quantity_Used, Shortage_Status, Part_Status, StaffID) VALUES (@Part_Name,@Quantity_Available, @Quantity_Used,@Shortage_Status, @Part_Status, @StaffID)", conn);
            cmd2.Parameters.AddWithValue("@Part_Name", namePart);
            cmd2.Parameters.AddWithValue("@Part_Status", stat);
            cmd2.Parameters.AddWithValue("@Quantity_Available", numPart);
            cmd2.Parameters.AddWithValue("@Quantity_Used", "0");
            cmd2.Parameters.AddWithValue("@Shortage_Status", "Nothing");
            cmd2.Parameters.AddWithValue("@StaffID", staffID);
            int result = cmd2.ExecuteNonQuery();
            status = result > 0 ? "Add Part Done.." : "Unable to Add.";
            MessageBox.Show(status);
            conn.Close();
            return;

        }
        private int GetServiceID(string serviceName)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myCS"].ToString());
            string query = "SELECT ServiceID FROM Service WHERE Service_Name = @ServiceName";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@ServiceName", serviceName);
            conn.Open();
            int result = (int)command.ExecuteScalar();
            return result;
        }
       
        public void AddRecord(string taskName, string taskStatus, string addPrice, DateTime taskDate, string name)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myCS"].ToString());
            string status;
            conn.Open();
            int taskID = GetServiceID(taskName);
            int staffID = GetStaffeID(name);
            SqlCommand cmd2 = new SqlCommand("INSERT INTO record (completionStatus, collectionTime, additionalRepairs, StaffID, ServiceID, ServiceName) VALUES (@completionStatus, @collectionTime, @additionalRepairs, @StaffID, @ServiceID, @ServiceName)", conn);
            cmd2.Parameters.AddWithValue("@ServiceID", taskID);
            cmd2.Parameters.AddWithValue("@ServiceName", taskName);
            cmd2.Parameters.AddWithValue("@completionStatus", taskStatus);
            cmd2.Parameters.AddWithValue("@collectionTime", taskDate);
            cmd2.Parameters.AddWithValue("@additionalRepairs", addPrice);
            cmd2.Parameters.AddWithValue("@StaffID", staffID);
            int result = cmd2.ExecuteNonQuery();
            status = result > 0 ? "Add Record Done.." : "Unable to Add.";
            MessageBox.Show(status);
            conn.Close();
            return;

        }

    }

}
