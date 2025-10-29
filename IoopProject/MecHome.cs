using IoopProject;
using IoopProject.CusCustomer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace IoopProject
{
    public partial class MecHome : Form
    {
        private User user;
        private string StaffID;
        string name;

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myCS"].ToString());

        public MecHome(string n)
        {
            InitializeComponent();
            this.name = n;

            user = new User(n);
            SearchStaffID();
            txtUserIDP.Text = user.UserID;
            txtUsernameP.Text = user.Username;
            txtEmailP.Text = user.Email;
            txtStaffIDP.Text = StaffID;
        }

        private void MecHome_Load(object sender, EventArgs e)
        {
            lblIdentity.Text = "Welcome back " + name + "!";
        }

        public MecHome()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {


        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox4.SelectedItem == null || comboBox5.SelectedItem == null)
            {
                MessageBox.Show("Please select a part and a status.");
                return;
            }

            string selectedPartName = comboBox5.SelectedItem.ToString();
            string newStatus = comboBox4.SelectedItem.ToString();

            Service2 service = new Service2(name);
            service.UpdatePart(selectedPartName, newStatus);
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void tabPage7_Click(object sender, EventArgs e)
        {
            Service2 obj1 = new Service2();
            ArrayList name = new ArrayList();
            name = obj1.ListPartName();
            foreach (var item in name)
            {
                comboBox5.Items.Add(item);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            Service2 obj1 = new Service2();
            ArrayList name = new ArrayList();
            name = obj1.ListPart();
            foreach (var item in name)
            {
                listBox2.Items.Add(item);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string namePart = Partname_txt.Text;
            int numPart = int.Parse(num_btn.Value.ToString());
            string staffname = staffname2_txt.Text;
            if (string.IsNullOrEmpty(namePart))
            {
                MessageBox.Show("Please Write Name Of Part");
                return;

            }
            if (numPart <= 0)
            {
                MessageBox.Show("Please Put Number Of Part You Want");
                return;
            }
            Service2 service = new Service2(name);
            service.AddPart(namePart, numPart, staffname);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string taskName = service_name.Text;
            string taskStatus = stats_cobom.SelectedItem.ToString();
            string addPrice = addPrice_txt.Text;
            string staffname = staffname_txt.Text;
            DateTime taskDate = date_service.Value;
            Service2 service = new Service2();
            service.AddRecord(taskName, taskStatus, addPrice, taskDate, staffname);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            service_name.Items.Clear();
            Service2 obj1 = new Service2();
            ArrayList name = new ArrayList();
            name = obj1.ListServiceName();
            foreach (var item in name)
            {
                service_name.Items.Add(item);
            }
        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            service_name.SelectedItem = null;
            addPrice_txt.Clear();
            stats_cobom.SelectedItem = null;
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            Service2 obj1 = new Service2();
            ArrayList name = new ArrayList();
            name = obj1.ListService();
            foreach (var item in name)
            {
                listBox1.Items.Add(item);
            }
        }

        private void service_name_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void staffname_txt_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnUpdateProfile_Click(object sender, EventArgs e)
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



