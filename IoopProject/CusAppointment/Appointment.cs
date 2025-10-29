using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace IoopProject.CusAppointment
{
    internal class Appointment
    {
        // ----- Properties ----- //

        private static string appointmentID;
        private static string appointmentDate;
        private static string appointmentStatus;
        private static string customerID;
        private static string staffID;
        private static string serviceID;

        public string AppointmentID { get => appointmentID; set => appointmentID = value; }
        public string AppointmentDate { get => appointmentDate; set => appointmentDate = value; }
        public string AppointmentStatus { get => appointmentStatus; set => appointmentStatus = value; }
        public string CustomerID { get => customerID; set => customerID = value; }
        public string StaffID { get => staffID; set => staffID = value; }
        public string ServiceID { get => serviceID; set => serviceID = value; }

        private static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myCS"].ToString());

        // ----- Full Constructor (For Admin) ----- //

        public Appointment(string appointmentDate, string appointmentStatus, string customerID, string staffID, string serviceID)
        {
            AppointmentDate = appointmentDate;
            AppointmentStatus = appointmentStatus;
            CustomerID = customerID;
            StaffID = staffID;
            ServiceID = serviceID;

            con.Open();

            SqlCommand cmd = new SqlCommand(
                "SELECT Service_Name, Price FROM Service " +
                "WHERE ServiceID = @serviceID", con);
            cmd.Parameters.AddWithValue("@serviceID", int.Parse(ServiceID));

            con.Close();
        }

        // ----- Appointment Constructor (For Customer) ----- //


        public Appointment(string appointmentDate, string customerID, string serviceID)
        {
            AppointmentDate = appointmentDate;
            AppointmentStatus = "Pending";
            CustomerID = customerID;
            ServiceID = serviceID;
            StaffID = "20";
        }

        // ----- Appointment constructor for searching via ID ------ //

        public Appointment(string cusID, string aptID)
        {
            CustomerID = cusID;
            AppointmentID = aptID;

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM Appointment WHERE AppointmentID = @aptID", con);
            cmd.Parameters.AddWithValue("@aptID", int.Parse(aptID));
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                AppointmentDate = reader.GetString(1);
                AppointmentStatus = reader.GetString(2);
                ServiceID = reader.GetInt32(4).ToString();
                StaffID = reader.GetInt32(5).ToString();
                reader.Close();
            }
            else
            {
                MessageBox.Show("Appointment does not exist.");
            }

            con.Close();
        }

        // ----- Add appointment into database ----- //

        public string AddAppointment()
        {
            string status;

            con.Open();

            SqlCommand cmd = new SqlCommand(
            "INSERT INTO Appointment (Appointment_Date, Status, CustomerID, StaffID, ServiceID)" +
            " VALUES (@appointmentDate, @appointmentStatus, @customerID, @staffID, @serviceID);", con);

            cmd.Parameters.AddWithValue("@appointmentDate", AppointmentDate);
            cmd.Parameters.AddWithValue("@appointmentStatus", AppointmentStatus);
            cmd.Parameters.AddWithValue("@customerID", CustomerID);
            cmd.Parameters.AddWithValue("@staffID", int.Parse(StaffID));
            cmd.Parameters.AddWithValue("@serviceID", ServiceID);

            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i != 0)
            {
                status = "Appointment scheduled!";
            }
            else
            {
                status = "Unable to schedule.";
            }
            MessageBox.Show(status);
            return status;
        }

        // ----- Update Appointment ----- //

        public string UpdateAppointment()
        {
            string status;
            con.Open();

            SqlCommand cmd = new SqlCommand(
            "UPDATE Appointment " +
            "SET Appointment_Date = @aptDate, Status = @aptStatus, CustomerID = @cusID, StaffID = @staffID, ServiceID = @serveID " +
            "WHERE AppointmentID = @aptID;", con);
            cmd.Parameters.AddWithValue("@aptDate", AppointmentDate);
            cmd.Parameters.AddWithValue("@aptStatus", AppointmentStatus);
            cmd.Parameters.AddWithValue("@cusID", CustomerID);
            cmd.Parameters.AddWithValue("@staffID", StaffID);
            cmd.Parameters.AddWithValue("@serveID", ServiceID);
            cmd.Parameters.AddWithValue("@aptID", AppointmentID);

            int i = cmd.ExecuteNonQuery();

            con.Close();
            if (i != 0)
            {
                status = "Appointment updated!";
            }
            else
            {
                status = "Unable to update.";
            }
            return status;
        }

        // ----- Delete Appointment ----- //

        public string DeleteAppointment()
        {
            string status;
            con.Open();

            SqlCommand cmd = new SqlCommand(
            "DELETE FROM Appointment WHERE CustomerID = @cusID AND AppointmentID = @aptID;", con);

            cmd.Parameters.AddWithValue("@cusID", CustomerID);
            cmd.Parameters.AddWithValue("@aptID", AppointmentID);

            int i = cmd.ExecuteNonQuery();

            con.Close();
            if (i != 0)
            {
                status = "Appointment deleted!";
            }
            else
            {
                status = "Unable to delete.";
            }
            return status;
        }

        // ----- Check whether date passed ----- //

        public string DateCheck()
        {
            string status = "";
            DateTime aptDate = DateTime.Parse(AppointmentDate);
            int i = DateTime.Compare(aptDate, DateTime.Now);

            if (i < 0)
            {
                if (AppointmentStatus != "Pending")
                {
                    status = "Date Passed";
                    AppointmentStatus = "Passed";
                }
                else
                {
                    this.DeleteAppointment();
                }
            }
            else
            {
                if (AppointmentStatus != "Pending")
                {
                    status = "Date Not Passed";
                    AppointmentStatus = "Confirmed";
                }
                else
                {
                    this.AppointmentStatus = "Pending";
                    status = "Pending";
                }
            }
            return status;
        }
    }
}