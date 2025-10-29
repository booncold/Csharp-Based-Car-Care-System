using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Text;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace IoopProject
{
    class Customer
    {
        public Customer() { }

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myCS"].ToString());

        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string VehicleNumber { get; set; }
        public string VehicleModel { get; set; }
        public int VehicleYear { get; set; }
        public DateTime ArriveDate { get; set; }
        public DateTime ArriveTime { get; set; }
        public DateTime DepartDate { get; set; }
        public DateTime DepartTime { get; set; }
        public string PaymentStatus {  get; set; }
        public DataTable dataTable { get; set; }
        public string serviceName {  get; set; }
        public string appointmentDate {  get; set; }

        // Add & Delete Customer(Add)
        public bool CheckExist()
        {
            bool exists = false;
            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM customer WHERE vehicle_number = @vhn", con);
            cmd.Parameters.AddWithValue("@vhn", VehicleNumber);

            int i = Convert.ToInt32(cmd.ExecuteScalar());
            if (i > 0)
            {
                exists = true;
            }
            else
            {
                SqlCommand cmd2 = new SqlCommand("SELECT COUNT(*) FROM users WHERE email = @em", con);
                cmd2.Parameters.AddWithValue("@em", Email);

                int c = Convert.ToInt32(cmd2.ExecuteScalar());
                if (c > 0)
                {
                    exists = true;
                }
            }

            con.Close();
            return exists;
        }

        public string AddCustomer()
        {
            string status;
            con.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO users(username, email, password, role) values(@name, @em, @pas, 'Customer'); SELECT scope_identity();", con);
            cmd.Parameters.AddWithValue("@name", Username);
            cmd.Parameters.AddWithValue("@em", Email);
            cmd.Parameters.AddWithValue("@pas", Password);
            int userID = Convert.ToInt32(cmd.ExecuteScalar());

            SqlCommand cmd2 = new SqlCommand("INSERT INTO customer(vehicle_number, vehicle_model, vehicle_year, userid) values(@vhn, @vhm, @vhy, @uid)", con);
            cmd2.Parameters.AddWithValue("@vhn", VehicleNumber);
            cmd2.Parameters.AddWithValue("@vhm", VehicleModel);
            cmd2.Parameters.AddWithValue("@vhy", VehicleYear);
            cmd2.Parameters.AddWithValue("@uid", userID);
            int i = cmd2.ExecuteNonQuery();
            if (i != 0)
                status = "Registration Successful.";
            else
                status = "Failed Register.";
            con.Close();
            return status;
        }

        // Add & Delete Customer(Delete)
        public bool CheckAvailability()
        {
            bool exists = false;
            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT userID FROM customer WHERE vehicle_number = @VehicleNumber", con);
            cmd.Parameters.AddWithValue("@VehicleNumber", VehicleNumber);

            object result = cmd.ExecuteScalar();
            if (result != null)
            {
                int UserID = Convert.ToInt32(result);
                SqlCommand cmd2 = new SqlCommand("SELECT COUNT(*) FROM users WHERE userid = @uid AND username = @un", con);
                cmd2.Parameters.AddWithValue("@uid", UserID);
                cmd2.Parameters.AddWithValue("@un", Username);

                int z = Convert.ToInt32(cmd2.ExecuteScalar());
                if (z > 0)
                {
                    exists = true;
                }

            }

            con.Close();
            return exists;
        }

        public string DelCustomer()
        {
            string status;

            con.Open();

            SqlCommand cmd0 = new SqlCommand("SELECT customerid FROM customer WHERE vehicle_number = @VehicleNumber", con);
            cmd0.Parameters.AddWithValue("@VehicleNumber", VehicleNumber);
            object customerIdObj = cmd0.ExecuteScalar();

            if (customerIdObj != null)
            {
                int cusID = Convert.ToInt32(customerIdObj);
                SqlCommand cmd3 = new SqlCommand("DELETE FROM appointment WHERE customerid = @customerID", con);
                cmd3.Parameters.AddWithValue("@customerID", cusID);

                SqlCommand cmd4 = new SqlCommand("DELETE FROM customerstatus WHERE customerid = @customerID", con);
                cmd4.Parameters.AddWithValue("@customerID", cusID);

                SqlCommand cmd1 = new SqlCommand("DELETE FROM customer WHERE vehicle_number = @vhn", con);
                cmd1.Parameters.AddWithValue("@vhn", VehicleNumber);
                int customerDeleted = cmd1.ExecuteNonQuery();

                SqlCommand cmd2 = new SqlCommand("DELETE FROM users WHERE username = @name", con);
                cmd2.Parameters.AddWithValue("@name", Username);
                int usersDeleted = cmd2.ExecuteNonQuery();

                if (usersDeleted > 0 && customerDeleted > 0)
                {
                    status = "All related data has been deleted.";
                }
                else
                {
                    status = "Failed to delete customer.";
                }
            }
            else
            {
                status = "No customer found with the provided vehicle number.";
            }
            con.Close();
            return status;
        }

        // Check in & out
        public void GenBill()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT customerID FROM customer WHERE vehicle_number = @VehicleNumber", con);
            cmd.Parameters.AddWithValue("@VehicleNumber", VehicleNumber);
            object customerIdResult = cmd.ExecuteScalar();

            if (customerIdResult != null)
            {
                int customerId = Convert.ToInt32(customerIdResult);
                SqlCommand searchUsercmd = new SqlCommand("SELECT userID FROM customer WHERE customerID = @CustomerID", con);
                searchUsercmd.Parameters.AddWithValue("@CustomerID", customerId);
                object searchUserResult = searchUsercmd.ExecuteScalar();

                if (searchUserResult != null)
                {
                    int userId = Convert.ToInt32(searchUserResult);
                    SqlCommand getUsercmd = new SqlCommand("SELECT username, email FROM users WHERE userID = @userID", con);
                    getUsercmd.Parameters.AddWithValue("userID", userId);
                    using (SqlDataReader reader = getUsercmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string vehicleNum = Convert.ToString(VehicleNumber);
                            string userName = reader["username"].ToString();
                            string email = reader["email"].ToString();

                            _RecepForm.PrintUser(vehicleNum, userName, email);
                        }
                    }
                    SqlCommand findCusIDcmd = new SqlCommand("SELECT customerID FROM customer WHERE vehicle_number = @VehicleNumber", con);
                    findCusIDcmd.Parameters.AddWithValue("@VehicleNumber", VehicleNumber);
                    object customerIdResult2 = findCusIDcmd.ExecuteScalar();

                    if (customerIdResult2 != null)
                    {
                        int customerId2 = Convert.ToInt32(customerIdResult2);
                        SqlCommand servicecmd = new SqlCommand("SELECT serviceID FROM appointment WHERE customerID = @CustomerID", con);
                        servicecmd.Parameters.AddWithValue("@CustomerID", customerId2);
                        object serviceResult = servicecmd.ExecuteScalar();

                        if (serviceResult != null)
                        {
                            int serviceId = Convert.ToInt32(serviceResult);
                            SqlCommand servicenamecmd = new SqlCommand("SELECT service_name, price FROM service WHERE serviceID = @serviceID", con);
                            servicenamecmd.Parameters.AddWithValue("serviceID", serviceId);
                            using (SqlDataReader reader = servicenamecmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    string ServicePD = reader["service_name"].ToString();
                                    string ServicePrice = reader["price"].ToString();

                                    _RecepForm.ShowServicePD(ServicePD);
                                    _RecepForm.ShowPrice(ServicePrice);

                                    con.Close();
                                    con.Open();

                                    DateTime currentDate = DateTime.Now;
                                    int month = currentDate.Month;
                                    string monthName = currentDate.ToString("MMMM");

                                    SqlCommand cmd2 = new SqlCommand("INSERT INTO report(profit, month, serviceid) values(@pr, @mo, @si)", con);
                                    cmd2.Parameters.AddWithValue("@pr", ServicePrice);
                                    cmd2.Parameters.AddWithValue("@mo", monthName);
                                    cmd2.Parameters.AddWithValue("@si", serviceId);

                                    int i = cmd2.ExecuteNonQuery();
                                    if (i != 0)
                                        MessageBox.Show("Recorded to Report");
                                    else
                                        MessageBox.Show("Failed to Record");
                                    con.Close();
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Vehicle Number not Found!");
                _RecepForm.ClearVehicleNum();
            }
            con.Close();
        }

        public void ShowInv()
        {
            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine("Inventory ID: " + row["inventoryid"]);
                Console.WriteLine("Part Name: " + row["part_name"]);
                Console.WriteLine("Quantity Available: " + row["quantity_available"]);
            }
        }

        public bool CheckVehicle()
        {
            bool exists = false;
            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM customer WHERE vehicle_number = @VehicleNumber", con);
            cmd.Parameters.AddWithValue("@VehicleNumber", VehicleNumber);

            int i = Convert.ToInt32(cmd.ExecuteScalar());
            if (i > 0)
            {
                exists = true;
            }
            con.Close();
            return exists;
        }

        public string UpdateStat()
        {
            string status;
            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT customerid FROM customer WHERE vehicle_number = @VehicleNumber", con);
            cmd.Parameters.AddWithValue("@VehicleNumber", VehicleNumber);
            int cusID = Convert.ToInt32(cmd.ExecuteScalar());

            string formattedArriveTime = ArriveTime.ToString("hh:mm tt");

            SqlCommand cmd2 = new SqlCommand("INSERT INTO customerstatus(arrivedate, arrivetime, departdate, departtime, paymentstatus, customerid) values(@ad, @at, @dd, @dt, @ps, @ci)", con);
            cmd2.Parameters.AddWithValue("@ad", ArriveDate);
            cmd2.Parameters.AddWithValue("@at", formattedArriveTime);
            cmd2.Parameters.AddWithValue("@dd", DepartDate);
            cmd2.Parameters.AddWithValue("@dt", DepartTime);
            cmd2.Parameters.AddWithValue("@ps", PaymentStatus);
            cmd2.Parameters.AddWithValue("@ci", cusID);
            int i = cmd2.ExecuteNonQuery();
            if (i != 0)
                status = "Update Successful.";
            else
                status = "Failed.";
            con.Close();
            return status;
        }

        // Search Appointment
        private RecepHome _RecepForm;

        public Customer(RecepHome RecepForm)
        {
            _RecepForm = RecepForm;
        }
        public void CheckApp()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myCS"].ToString());
            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT customerID FROM customer WHERE vehicle_number = @VehicleNumber", con);
            cmd.Parameters.AddWithValue("@VehicleNumber", VehicleNumber);
            object customerIdResult = cmd.ExecuteScalar();

            if (customerIdResult != null)
            {
                _RecepForm.Found(" ");
                int customerId = Convert.ToInt32(customerIdResult);
                SqlCommand appointmentcmd = new SqlCommand("SELECT appointment_date FROM appointment WHERE customerID = @CustomerID", con);
                appointmentcmd.Parameters.AddWithValue("@CustomerID", customerId);
                object appointmentResult = appointmentcmd.ExecuteScalar();

                if (appointmentResult != null)
                {
                    DateTime appointmentDate = Convert.ToDateTime(appointmentResult);
                    _RecepForm.UpdateAppointmentDate(appointmentDate.ToString("d"));

                    SqlCommand servicecmd = new SqlCommand("SELECT serviceID FROM appointment WHERE customerID = @CustomerID", con);
                    servicecmd.Parameters.AddWithValue("@CustomerID", customerId);
                    object serviceResult = servicecmd.ExecuteScalar();

                    if (serviceResult != null)
                    {
                        int serviceId = Convert.ToInt32(serviceResult);
                        SqlCommand servicenamecmd = new SqlCommand("SELECT service_name FROM service WHERE serviceID = @serviceID", con);
                        servicenamecmd.Parameters.AddWithValue("serviceID", serviceId);
                        object servicenameResult = servicenamecmd.ExecuteScalar();

                        if (servicenameResult != null)
                        {
                            string serviceName = Convert.ToString(servicenameResult);
                            _RecepForm.ShowService(serviceName);
                        }                   
                    }
                }

                else
                {
                    _RecepForm.NotFound("No Appointment Available.");
                }
            }

            else
            {
                _RecepForm.NotFound("Vehicle Not Found.");
            }

            con.Close();
        }

        public string AssignApp()
        {
            string status;
            con.Open();

            SqlCommand cmd2 = new SqlCommand("INSERT INTO assign(vehicle_number, appointment_date, service_name) values(@vn, @ad, @sn)", con);
            cmd2.Parameters.AddWithValue("@vn", VehicleNumber);
            cmd2.Parameters.AddWithValue("@ad", appointmentDate);
            cmd2.Parameters.AddWithValue("@sn", serviceName);

            int i = cmd2.ExecuteNonQuery();
            if (i != 0)
                status = "Assign Successful.";
            else
                status = "Failed.";
            con.Close();
            return status;
        }

        private static string userID;
        private static string userName;
        private static string email;
        private static string password;

        private static string customerID;
        private static string vehicleNumber;
        private static string vehicleModel;
        private static string vehicleYear;

        public string UserID { get => userID; set => userID = value; }
        public string UserName { get => userName; set => userName = value; }
        public string CustomerID { get => customerID; set => customerID = value; }

        // ----- Constructor to find customer details ----- //

        public Customer(string Username)
        {
            UserName = Username;
            con.Open();

            SqlCommand cmdUser = new SqlCommand("SELECT * FROM users WHERE Username = @Username", con);
            cmdUser.Parameters.AddWithValue("@Username", Username);
            SqlDataReader userReader = cmdUser.ExecuteReader();

            if (userReader.Read())
            {
                UserID = userReader.GetInt32(0).ToString();
                Email = userReader.GetString(2);
                Password = userReader.GetString(3);

                userReader.Close();

                SqlCommand cmdCustomer = new SqlCommand("SELECT * FROM Customer WHERE UserId = @userId", con);
                cmdCustomer.Parameters.AddWithValue("@userId", userID);
                SqlDataReader customerReader = cmdCustomer.ExecuteReader();

                if (customerReader.Read())
                {
                    CustomerID = customerReader.GetInt32(0).ToString();
                    VehicleNumber = customerReader.GetString(1);
                    VehicleModel = customerReader.GetString(2);
                    VehicleYear = int.Parse(customerReader.GetString(3));
                }
                else
                {
                    MessageBox.Show("Customer does not exist.");
                }
            }
            else
            {
                MessageBox.Show("User does not exist.");
            }
            con.Close();
        }

        // ----- Update Customer Details ----- //

        public string UpdateCustomerDetails()
        {
            string status = "";

            con.Open();

            SqlCommand cmd = new SqlCommand(
                "UPDATE Users " +
                "SET Username = @new_username, Password = @new_password, Email = @new_email " +
                "WHERE UserID = @uid", con);
            cmd.Parameters.AddWithValue("@uid", int.Parse(UserID));
            cmd.Parameters.AddWithValue("@new_username", UserName);
            cmd.Parameters.AddWithValue("@new_password", Password);
            cmd.Parameters.AddWithValue("@new_email", Email);

            int i = cmd.ExecuteNonQuery();

            status = "Update completed!";

            con.Close();
            return status;
        }

        // ----- Return All Appointment ID Array SQL ----- //

        public string[] AllAppointmentSearch(string[] array, string cusID)
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM Appointment WHERE CustomerID = @customerID;", con);
            cmd.Parameters.AddWithValue("@customerID", int.Parse(cusID));
            SqlDataReader reader = cmd.ExecuteReader();

            List<String> list = new List<string>();
            while (reader.Read())
            {
                list.Add(reader.GetInt32(0).ToString());
                array = list.ToArray();
            }
            con.Close();

            return array;
        }

        // ----- Return Specific Status Appointment ID Array SQL ----- //

        public string[] SearchAppointmentArray(string[] array, string cusID, string status)
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM Appointment WHERE CustomerID = @customerID AND Status = @status;", con);
            cmd.Parameters.AddWithValue("@customerID", int.Parse(cusID));
            cmd.Parameters.AddWithValue("@status", status);
            SqlDataReader reader = cmd.ExecuteReader();

            List<String> list = new List<string>();
            while (reader.Read())
            {
                list.Add(reader.GetInt32(0).ToString() + "\t" + reader.GetString(1));
                array = list.ToArray();
            }
            con.Close();

            return array;
        }
    }
}


        
    

