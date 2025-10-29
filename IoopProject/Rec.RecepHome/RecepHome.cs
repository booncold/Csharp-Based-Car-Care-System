using IoopProject.CusCustomer;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace IoopProject
{
    public partial class RecepHome : Form
    {
        private User user;
        private string StaffID;
        

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myCS"].ToString());

        public static string name;

        public RecepHome(string n)
        {
            InitializeComponent();
            name = n;

            user = new User(n);
            SearchStaffID();
            txtUserIDP.Text = user.UserID;
            txtUsernameP.Text = user.Username;
            txtEmailP.Text = user.Email;
            txtStaffIDP.Text = StaffID;
            
        }

        private void RecepHome_Load(object sender, EventArgs e)
        {
            lblIdentity.Text = "Welcome back " + name + "!";
        }


        // Add & Delete Customer(Add)
        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text) ||
                string.IsNullOrEmpty(txtPassword.Text) ||
                string.IsNullOrEmpty(txtEmail.Text) ||
                string.IsNullOrEmpty(txtVehicleNumber.Text) ||
                string.IsNullOrEmpty(txtVehicleModel.Text) ||
                string.IsNullOrEmpty(txtVehicleYear.Text))
            {
                MessageBox.Show("Please fill all the information.");
                return;
            }

            Customer addCus = new Customer();
            addCus.Username = txtUsername.Text; 
            addCus.Password = txtPassword.Text; 
            addCus.Email = txtEmail.Text;
            addCus.VehicleNumber = txtVehicleNumber.Text.Replace(" ", "");
            addCus.VehicleModel = txtVehicleModel.Text; 
            addCus.VehicleYear = Convert.ToInt32(txtVehicleYear.Text);

            if (addCus.CheckExist())
            {
                MessageBox.Show("Vehicle Number or Email Already Exists.");
                ClearTextBoxesAdd();
                return;
            }

            string status = addCus.AddCustomer();
            MessageBox.Show(status);
        }
        public void ClearTextBoxesAdd()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            txtEmail.Clear();
            txtVehicleNumber.Clear();
            txtVehicleModel.Clear();
            txtVehicleYear.Clear();
        }

        // Add & Delete Customer(Delete)
        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDelUsername.Text) ||
                string.IsNullOrEmpty(txtDelVehicleNum.Text))
            {
                MessageBox.Show("Please fill all the information.");
                return;
            }
            Customer delCus = new Customer();
            delCus.Username = txtDelUsername.Text;
            delCus.VehicleNumber = txtDelVehicleNum.Text.Replace(" ", "");

            if (delCus.CheckAvailability())
            {
                string status = delCus.DelCustomer();
                MessageBox.Show(status);
            }
            else
            {
                MessageBox.Show("No information found.");
                ClearTextBoxesDel();
            }
        }

        public void ClearTextBoxesDel()
        {
            txtDelUsername.Clear();
            txtDelVehicleNum.Clear();
        }

        // Check in & out
        private void btnGenBill_Click(object sender, EventArgs e)
        {
            Customer generateBill = new Customer(this)
            {
                VehicleNumber = txtVehicleNumberPD.Text.Replace(" ", ""),
            };
            generateBill.GenBill();
        }
        public void PrintUser(string vehicleNum, string userName, string email)
        {
            lblVehicleNumberPD.Text = vehicleNum;
            lblUsernamePD.Text = userName;
            lblEmailPD.Text = email;
        }
        public void ClearVehicleNum()
        {
            txtVehicleNumberPD.Text = "";
        }
        public void ShowServicePD(string ServicePD)
        {
            lblServicePD.Text = ServicePD;
        }
        public void ShowPrice(string message)
        {
            lblPrice1.Text = message;
            lblPrice2.Text = message;
            lblTotal.Text = message;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtVehicleNumStat.Text) ||
                comboStat.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill all the information.");
                return;
            }
            Customer upStat = new Customer();
            upStat.VehicleNumber = txtVehicleNumStat.Text.Replace(" ", "");
            upStat.ArriveDate = arriveDate.Value;
            upStat.ArriveTime = arriveTime.Value;
            upStat.DepartDate = departDate.Value;
            upStat.DepartTime = departTime.Value;
            upStat.PaymentStatus = comboStat.SelectedItem.ToString();

            if (upStat.CheckVehicle())
            {
                string status = upStat.UpdateStat();
                MessageBox.Show(status);
            }
            else
            {
                MessageBox.Show("Vehicle Not Found.");
                ClearUpdateText();
            }
        }
        public void ClearUpdateText()
        {
            txtVehicleNumStat.Clear();
        }

        // Search Appointment
        public void UpdateAppointmentDate(string appointmentDate)
        {
            lblAppTime.Text = appointmentDate;
        }
        public void NotFound(string message)
        {
            lblNotFound.Text = message;
            txtVehicleNumberApp.Clear();
        }
        public void Found(string message)
        {
            lblNotFound.Text = message;
        }
        public void ShowService(string serviceName)
        {
            lblShowService.Text = serviceName;
        }

        private void btnSearchApp_Click(object sender, EventArgs e)
        {
            Customer appointment = new Customer(this)
            {
                VehicleNumber = txtVehicleNumberApp.Text.Replace(" ", ""),
            };
            appointment.CheckApp();
        }

        private void btnAssign_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lblAppTime.Text) ||
                string.IsNullOrEmpty(lblShowService.Text))
            {
                MessageBox.Show("Please search the vehicle.");
                return;
            }
            Customer assign = new Customer()
            {
                VehicleNumber = txtVehicleNumberApp.Text,
                serviceName = lblAppTime.Text,
                appointmentDate = lblAppTime.Text
            };

            string status = assign.AssignApp();
            MessageBox.Show(status);
        }

        private void btnViewInventory(object sender, EventArgs e)
        {
            LoadInventoryData();
            if (dataGridViewInv.DataSource != null)
            {
                Customer viewInv = new Customer()
                {
                    dataTable = (DataTable)dataGridViewInv.DataSource
                };
                viewInv.ShowInv();
            }
            else
            {
                MessageBox.Show("DataGridView has no data source.");
            }
        }
        private void LoadInventoryData()
        {
            DataTable dataTable = new DataTable();  // DataTable to hold data from the database
            string query = "SELECT InventoryID, Part_Name, Quantity_Available FROM inventory";

            using (SqlDataAdapter adapter = new SqlDataAdapter(query, con))
            {
                adapter.Fill(dataTable);  // Fill the DataTable with data from the database
            }

            dataGridViewInv.DataSource = dataTable;  // Set DataSource of DataGridView to the DataTable
        }

        private void button1_Click(object sender, EventArgs e)
        {
            formUpdate form = new formUpdate(user.Username);
            this.Close();
            form.Show();
            form.Focus();
        }

        private void SearchStaffID()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT StaffID FROM Staff WHERE UserID = @UID", con);
            cmd.Parameters.AddWithValue("@UID", int.Parse(user.UserID));
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                StaffID = reader.GetInt32(0).ToString();
                reader.Close();
            }
            con.Close();
        }
    }
}
