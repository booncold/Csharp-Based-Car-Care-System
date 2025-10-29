using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace IoopProject
{
    public partial class AdminStaff : Form
    {
        public AdminStaff()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Create an instance of the AdminHome form
            AdminHome backAdminPage = new AdminHome(AdminHome.name);
            // Show the AdminHome form
            backAdminPage.Show();
            // Close the current AdminService form
            this.Close();
        }

        private void btnUploadPic_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    picBoxAddStaff.Image = Image.FromFile(filePath);
                    txtImagePath.Text = filePath;
                }
            }
        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string userName = txtBoxUserName.Text;
            string email = txtBoxEmail.Text;
            string password = txtBoxPw.Text;
            string name = txtBoxStaffName.Text;

            if (cboRole.SelectedItem == null)
            {
                MessageBox.Show("Please select a role.");
                return;
            }

            string position = cboRole.SelectedItem.ToString();
            DateTime dateJoin = dtpDateJoin.Value;
            string gender = rbMale.Checked ? "Male" : "Female";
            string imagePath = txtImagePath.Text;
            string role = cboRole.SelectedItem.ToString();

            AddStaff newStaff = new AddStaff
            {
                UserName = userName,
                Email = email,
                Password = password,
                Name = name,
                Position = position,
                DateJoin = dateJoin,
                Gender = gender
            };

            newStaff.LoadImage(imagePath);
            string result = newStaff.AddNewStaff(role);
            MessageBox.Show(result);
        }


        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtBoxStaffName_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void cboRole_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnViewStaff_Click(object sender, EventArgs e)
        {
            string username = txtBoxViewStaff.Text;

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Please enter a username.");
                return;
            }

            ViewStaff viewStaff = new ViewStaff();
            StaffDetails staff = viewStaff.GetStaffByUsername(username);

            if (staff != null)
            {
                lblName.Text = $"Name: {staff.Name}";
                lblPosition.Text = $"Position: {staff.Position}";
                lblDateJoin.Text = $"Date Joined: {staff.DateJoin.ToShortDateString()}";
                lblGender.Text = $"Gender: {staff.Gender}";
                lblEmail.Text = $"Email: {staff.Email}";
                lblPw.Text = $"Password: {staff.Password}";
                lblUsername.Text = $"Username: {staff.Username}";

                if (staff.Image != null)
                {
                    using (MemoryStream ms = new MemoryStream(staff.Image))
                    {
                        picBoxViewStaff.Image = Image.FromStream(ms);
                    }
                }
                else
                {
                    picBoxViewStaff.Image = null;
                }
            }
            else
            {
                MessageBox.Show("Staff not found.");
                lblName.Text = string.Empty;
                lblPosition.Text = string.Empty;
                lblDateJoin.Text = string.Empty;
                lblGender.Text = string.Empty;
                lblEmail.Text = string.Empty;
                lblPw.Text = string.Empty;
                lblUsername.Text = string.Empty;
                picBoxViewStaff.Image = null;
            }
        }


        private void btnClearStaff_Click(object sender, EventArgs e)
        {
            txtBoxUserName.Text = string.Empty;
            txtBoxEmail.Text = string.Empty;
            txtBoxPw.Text = string.Empty;
            txtBoxStaffName.Text = string.Empty;
            txtImagePath.Text = string.Empty;

            cboRole.SelectedIndex = -1;

            dtpDateJoin.Value = DateTime.Now;

            rbMale.Checked = false;
            rbFemale.Checked = false;

            picBoxAddStaff.Image = null;
        }

        private void btnDeleteStaff_Click(object sender, EventArgs e)
        {
            string username = txtBoxDeleteStaff.Text;

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Please enter a username to delete.");
                return;
            }

            DeleteStaff deleteStaff = new DeleteStaff();
            string result = deleteStaff.DeleteByUsername(username);
            MessageBox.Show(result);
        }

        private void LoadAllStaff()
        {
            ViewAllStaff viewAllStaff = new ViewAllStaff();
            List<ViewAllStaffDetails> staffList = viewAllStaff.GetAllStaff();

            dataGridViewAllStaff.DataSource = staffList;
        }

        private void btnViewAllStaff_Click(object sender, EventArgs e)
        {
            LoadAllStaff();
        }

        private void dataGridViewAllStaff_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}