using IoopProject.CusAppointment;
using IoopProject.CusFeedback;
using IoopProject.CusService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace IoopProject.CusCustomer
{
    public partial class formCustomer : Form
    {
        // ----- Variables ----- //

        private string userID;
        private string username;
        private string email;
        private string password;

        private Customer customer;
        private string customerID;
        private string vehicleNumber;
        private string vehicleModel;
        private string vehicleYear;

        private string PendingUID;
        private string PendingSID;

        private string[] All_Appointment_Array = new string[0];
        private string[] AppointmentPast_Array = new string[0];
        private string[] AppointmentUpcoming_Array = new string[0];
        private string[] AppointmentPending_Array = new string[0];
        private string[] listService = new string[0];

        private static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myCS"].ToString());

        // ----- Form Layout (Default Constructor) for Testing Layout ----- //

        public formCustomer()
        {
            InitializeComponent();
        }

        // ----- Form Preload ----- //

        public formCustomer(string Username)
        {
            username = Username;
            loadTabs(username);

            cmb2_ServiceType.SelectedIndex = 0;
            lsboxPast.Items.Add("Appointment ID");
            lsboxUpcoming.Items.Add("Appointment ID");
            lsboxPending.Items.Add("Appointment ID");
            loadAppointments();
        }

        // ----- Changing Tabs ----- //

        private void tbconMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tbconMain.SelectedIndex == -1)
            {
                tbconMain.SelectedIndex = 2;
            }
            else
            {
                if (tbconMain.SelectedTab.Text.ToString() == "Schedule Appointment")
                {
                    ClearAppointmentDetails();

                    txtread2_AptID.Text = AppointmentIDCount();
                    txtread2_PlateNo.Text = vehicleNumber;
                    txtread2_Model.Text = vehicleModel;
                    txtread2_EstimatedPay.Text = "(none)";
                    txtread2_time.Text = "(none)";
                }

                if (tbconMain.SelectedTab.Text.ToString() == "Appointment Details")
                {
                    txtread2_Model.Clear();
                    txtread2_EstimatedPay.Clear();
                    txtread2_PlateNo.Clear();
                    txtread2_AptID.Clear();
                    datetime2_ServiceDate.Value = DateTime.Now;
                    cmb2_ServiceType.SelectedIndex = 0;
                    txtread2_EstimatedPay.Text = "(none)";
                }
            }
        }

        // ----- Service type respective information ----- //

        private void cmb2_ServiceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            lsbox2_ServiceContent.Items.Clear();
            string service_name = cmb2_ServiceType.Text;
            Service service = new Service(service_name);

            if (string.IsNullOrEmpty(service_name) || service_name == "(none)")
            {
                lsbox2_ServiceContent.Items.Clear();
                txtread2_time.Text = "(none)";
                txtread2_EstimatedPay.Text = "(none)";
            }
            else
            {
                lsbox2_ServiceContent.Items.Add(service.Description);
                txtread2_time.Text = (service.Time);
                txtread2_EstimatedPay.Text = (service.Price);
            }
        }

        // ----- Book Appointment ----- //

        private void btnSchedule_Click(object sender, EventArgs e)
        {
            Service service = new Service(cmb2_ServiceType.Text.Trim());

            string serviceID = service.ServiceID;
            string date = datetime2_ServiceDate.Text.ToString();
            Appointment appointment = new Appointment(date, customerID, serviceID);
            appointment.StaffID = PendingSID;

            if (cmb2_ServiceType.SelectedIndex == 0)
            {
                MessageBox.Show("No service selected.");
            }
            else
            {
                int i = DateTime.Compare(DateTime.Parse(date), DateTime.Now);
                if (i < 0)
                {
                    MessageBox.Show("Invalid time to book!");
                    datetime2_ServiceDate.Text = appointment.AppointmentDate;
                }
                else
                {
                    appointment.AddAppointment();

                    txtread2_AptID.Text = AppointmentIDCount();
                    cmb2_ServiceType.SelectedIndex = 0;
                    txtread2_EstimatedPay.Text = "(none)";
                    txtread2_time.Text = "(none)";
                }
            }
            loadAppointments();
        }

        // ----- Comment ----- //

        private void btnFeedback_Click(object sender, EventArgs e)
        {
            int rating = 0;
            string comment = richtext3_Comment.Text.Trim();
            string serviceID = txtread3_ServiceID.Text.Trim();

            if (radbtn3_Rate1.Checked)
            {
                rating = 1;
            }
            else if (radbtn3_Rate2.Checked)
            {
                rating = 2;
            }
            else if (radbtn3_Rate3.Checked)
            {
                rating = 3;
            }
            else if (radbtn3_Rate4.Checked)
            {
                rating = 4;
            }
            else if (radbtn3_Rate5.Checked)
            {
                rating = 5;
            }

            if (rating == 0)
            {
                MessageBox.Show("Please select a rating.");
            }
            else if (string.IsNullOrEmpty(comment))
            {
                MessageBox.Show("Please enter a comment.");
            }
            else if (string.IsNullOrEmpty(comment) == false && rating != 0 && string.IsNullOrEmpty(serviceID) == false)
            {
                Feedback feedback = new Feedback(comment, rating, int.Parse(customerID.Trim()), int.Parse(serviceID.Trim()));
                string status = feedback.AddFeedback();
                MessageBox.Show(status);
                ClearAppointmentDetails();
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        // ----- Listbox Select Appointments ----- //

        private void lsboxUpcoming_SelectedIndexChanged(object sender, EventArgs e)
        {
            lsboxPast.ClearSelected();
            lsboxPending.ClearSelected();
            if (lsboxUpcoming.SelectedIndex != -1 && lsboxUpcoming.SelectedIndex != 0)
            {
                ClearAppointmentDetails();
                string selectedAppointmentID = AppointmentUpcoming_Array[lsboxUpcoming.SelectedIndex - 1].Substring(0, 2).Trim();
                SearchAppointment(selectedAppointmentID);

                datetime3_ServiceDate.Enabled = true;
                btnCancelApt.Enabled = true;
            }
            else
            {
                lsboxUpcoming.ClearSelected();
            }
        }

        private void lsboxPast_SelectedIndexChanged(object sender, EventArgs e)
        {
            lsboxUpcoming.ClearSelected();
            lsboxPending.ClearSelected();

            if (lsboxPast.SelectedIndex != -1 && lsboxPast.SelectedIndex != 0)
            {
                ClearAppointmentDetails();
                string selectedAppointmentID = AppointmentPast_Array[lsboxPast.SelectedIndex - 1].Substring(0, 2).Trim();
                SearchAppointment(selectedAppointmentID);

                radbtn3_Rate1.Enabled = true;
                radbtn3_Rate2.Enabled = true;
                radbtn3_Rate3.Enabled = true;
                radbtn3_Rate4.Enabled = true;
                radbtn3_Rate5.Enabled = true;
                richtext3_Comment.Enabled = true;
                btnFeedback.Enabled = true;
            }
            else
            {
                lsboxPast.ClearSelected();
            }
        }

        private void lsboxPending_SelectedIndexChanged(object sender, EventArgs e)
        {
            lsboxUpcoming.ClearSelected();
            lsboxPast.ClearSelected();

            if (lsboxPending.SelectedIndex != -1 && lsboxPending.SelectedIndex != 0)
            {
                ClearAppointmentDetails();
                string selectedAppointmentID = AppointmentPending_Array[lsboxPending.SelectedIndex - 1].Substring(0, 2).Trim();
                SearchAppointment(selectedAppointmentID);

                datetime3_ServiceDate.Enabled = true;
                btnRescheduleApt.Enabled = true;
                btnCancelApt.Enabled = true;
            }
            else
            {
                lsboxPending.ClearSelected();
            }
        }

        // ----- Cancel Appointment ----- //

        private void btnCancelApt_Click(object sender, EventArgs e)
        {
            int appointmentID = int.Parse(txtread3_AptID.Text);
            DialogResult confirmation = MessageBox.Show("Are you sure you want to cancel this appointment?", "Cancel Appointment", MessageBoxButtons.YesNo);
            switch (confirmation)
            {
                case DialogResult.Yes:
                    Appointment appointment = new Appointment(customerID, appointmentID.ToString());
                    string status = appointment.DeleteAppointment();
                    MessageBox.Show("Deletd appointment.");
                    loadAppointments();
                    ClearAppointmentDetails();

                    break;
                case DialogResult.No:
                    break;
            }
        }

        // ----- Rescheule Appointment ----- //

        private void btnRescheduleApt_Click(object sender, EventArgs e)
        {
            string aptID = txtread3_AptID.Text;
            string newdate = datetime3_ServiceDate.Text.ToString();
            Appointment appointment = new Appointment(customerID, aptID);

            int i = DateTime.Compare(DateTime.Parse(newdate), DateTime.Now);
            if (i <= 0)
            {
                MessageBox.Show("Invalid time to reschedule");
                datetime3_ServiceDate.Text = appointment.AppointmentDate;
            }
            else
            {
                appointment.AppointmentDate = newdate;
                appointment.UpdateAppointment();
                MessageBox.Show("Appointment updated!");
                loadAppointments();
            }
        }

        // ----- Update Profile ----- //

        private void btnUpdateProfile_Click(object sender, EventArgs e)
        {
            formUpdate form = new formUpdate(username);
            form.Show();
            this.Close();
            form.Focus();
        }

        // ----- Non Object Methods ----- //
        // ----- Load Profile Method ----- //

        private void loadTabs(string Username)
        {
            customer = new Customer(Username);
            userID = customer.UserID;
            username = Username;
            email = customer.Email;
            password = customer.Password;
            customerID = customer.CustomerID;
            vehicleNumber = customer.VehicleNumber;
            vehicleModel = customer.VehicleModel;
            vehicleYear = customer.VehicleYear.ToString();

            listService = ServiceList(listService);

            InitializeComponent();

            txtread1_UserID.Text = userID;
            txtread1_Username.Text = username;
            txtread1_Email.Text = email;
            txtread1_PlateNo.Text = vehicleNumber;
            txtread1_Model.Text = vehicleModel;
            txtread1_Year.Text = vehicleYear;
            txtread1_customerid.Text = customerID;

            foreach (string i in listService)
            {
                cmb2_ServiceType.Items.Add(i.Trim());
            }
        }

        // ----- Load Appointment Method ----- //

        private void loadAppointments()
        {
            lsboxPast.Items.Clear();
            lsboxUpcoming.Items.Clear();
            lsboxPending.Items.Clear();

            lsboxPast.Items.Add("Appointment ID");
            lsboxUpcoming.Items.Add("Appointment ID");
            lsboxPending.Items.Add("Appointment ID");

            Array.Clear(All_Appointment_Array, 0, All_Appointment_Array.Length);
            Array.Clear(AppointmentPast_Array, 0, AppointmentPast_Array.Length);
            Array.Clear(AppointmentPending_Array, 0, AppointmentPending_Array.Length);
            Array.Clear(AppointmentUpcoming_Array, 0, AppointmentUpcoming_Array.Length);

            PendingUserID();
            PendingStaffID();

            All_Appointment_Array = customer.AllAppointmentSearch(All_Appointment_Array, customerID);
            foreach (string i in All_Appointment_Array)
            {
                Appointment appointment = new Appointment(customerID, i);
                appointment.DateCheck();
                appointment.UpdateAppointment();
            }

            AppointmentPast_Array = customer.SearchAppointmentArray(AppointmentPast_Array, customerID, "Passed");
            foreach (string i in AppointmentPast_Array)
            {
                if (i != null)
                {
                    lsboxPast.Items.Add(i);
                }
            }
            AppointmentUpcoming_Array = customer.SearchAppointmentArray(AppointmentUpcoming_Array, customerID, "Confirmed");
            foreach (string i in AppointmentUpcoming_Array)
            {
                if (i != null)
                {
                    lsboxUpcoming.Items.Add(i);
                }
            }
            AppointmentPending_Array = customer.SearchAppointmentArray(AppointmentPending_Array, customerID, "Pending");
            foreach (string i in AppointmentPending_Array)
            {
                if (i != null)
                {
                    lsboxPending.Items.Add(i);

                }
            }
        }

        // ----- Listbox Search Appointment ----- //

        private void SearchAppointment(string selectedAppointmentID)
        {
            Appointment appointment = new Appointment(customerID, selectedAppointmentID);
            Service service = new Service(int.Parse(appointment.ServiceID));

            lsboxPending.ClearSelected();
            lsboxPast.ClearSelected();
            lsboxUpcoming.ClearSelected();
            tbconMain.SelectedIndex = 2;

            string selectedDate = appointment.AppointmentDate;
            string serviceName = service.ServiceName;
            string servicePrice = service.Price;
            string selectedStatus = appointment.AppointmentStatus;
            string serviceID = appointment.ServiceID;
            string serviceTime = service.Time;

            txtread3_PlateNo.Text = vehicleNumber;
            txtread3_AptID.Text = selectedAppointmentID;
            datetime3_ServiceDate.Text = selectedDate;
            txtread3_status.Text = selectedStatus;
            txtread3_ServiceType.Text = serviceName;
            txtread3_TotalPay.Text = servicePrice;
            txtread3_ServiceID.Text = serviceID;
            txtread3_Time.Text = serviceTime;
        }

        // ----- Clear Appointment Detail Tab ----- //

        private void ClearAppointmentDetails()
        {
            txtread3_AptID.Clear();
            txtread3_ServiceType.Clear();
            txtread3_TotalPay.Clear();
            txtread3_PlateNo.Clear();
            txtread3_ServiceID.Clear();
            txtread3_status.Clear();
            txtread3_Time.Clear();

            datetime3_ServiceDate.Value = DateTime.Now;
            datetime3_ServiceDate.Enabled = false;

            radbtn3_Rate1.Enabled = false;
            radbtn3_Rate2.Enabled = false;
            radbtn3_Rate3.Enabled = false;
            radbtn3_Rate4.Enabled = false;
            radbtn3_Rate5.Enabled = false;

            radbtn3_Rate1.Checked = false;
            radbtn3_Rate2.Checked = false;
            radbtn3_Rate3.Checked = false;
            radbtn3_Rate4.Checked = false;
            radbtn3_Rate5.Checked = false;

            richtext3_Comment.Clear();

            btnCancelApt.Enabled = false;
            btnFeedback.Enabled = false;
            btnRescheduleApt.Enabled = false;
        }

        // ----- Non-Object Reliant SQL Commands ----- //
        // ----- Service List SQL ----- //

        private string[] ServiceList(string[] array)
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT Service_Name FROM Service;", con);
            SqlDataReader reader = cmd.ExecuteReader();

            List<String> list = new List<string>();
            while (reader.Read())
            {
                list.Add(reader.GetString(0));
                array = list.ToArray();
            }
            con.Close();

            return array;
        }

        // ----- Appointment ID Count SQL ------ //

        private string AppointmentIDCount()
        {
            string nextAptID = "0";

            con.Open();

            SqlCommand cmd2 = new SqlCommand(
                "SELECT count(*) FROM Appointment", con);
            int count = (int)cmd2.ExecuteScalar();

            if (count != 0)
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT MAX(AppointmentID) FROM appointment AS Count", con);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    nextAptID = (reader.GetInt32(0) + 1).ToString();
                }
                else
                {
                    nextAptID = "Error: AppointmentID not counted.";
                }
            }
            else
            {
                nextAptID = "1";
            }

            con.Close();
            return nextAptID;
        }

        // ----- Instantiate pending if pending status does not exist in database ----- //

        private void PendingUserID()
        {
            PendingUID = "";

            con.Open();

            SqlCommand cmdCount = new SqlCommand(
                "SELECT COUNT(*) FROM Users WHERE Username = 'Pending'; ", con);
            int count = (int)cmdCount.ExecuteScalar();

            if (count == 0)
            {
                SqlCommand cmdCreate = new SqlCommand(
                    "INSERT INTO Users VALUES ('Pending', 'Null', 'Null', 'Mechanic'); ", con);
                cmdCreate.ExecuteNonQuery();
            }

            SqlCommand cmdFind = new SqlCommand(
                "SELECT UserID FROM Users WHERE Username = 'Pending'; ", con);
            SqlDataReader reader = cmdFind.ExecuteReader();

            if (reader.Read())
            {
                PendingUID = reader.GetInt32(0).ToString();
            }
            con.Close();
        }

        private void PendingStaffID()
        {
            PendingSID = "";

            con.Open();

            SqlCommand cmdCount = new SqlCommand(
                "SELECT COUNT(*) FROM Staff WHERE Name = 'Pending'; ", con);
            int count = (int)cmdCount.ExecuteScalar();

            if (count == 0)
            {
                SqlCommand cmdCreate = new SqlCommand(
                    "INSERT INTO Staff(UserID, Position, Name) VALUES (@PendingUID, 'Pending', 'Pending'); ", con);
                cmdCreate.Parameters.AddWithValue("@PendingUID", PendingUID);
                cmdCreate.ExecuteNonQuery();
            }

            SqlCommand cmdFind = new SqlCommand(
                "SELECT StaffID FROM Staff WHERE Name = 'Pending'; ", con);
            SqlDataReader reader = cmdFind.ExecuteReader();

            if (reader.Read())
            {
                PendingSID = reader.GetInt32(0).ToString();
            }
            con.Close();
        }

        private void formCustomer_Load(object sender, EventArgs e)
        {

        }
    }
}