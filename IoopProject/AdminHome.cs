using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IoopProject
{
    public partial class AdminHome : Form
    {
        public static string name;
        public AdminHome(string n)
        {
            InitializeComponent();
            name = n;
        }

        private void AdminHome_Load(object sender, EventArgs e)
        {
            lblIdentity.Text = "Hello, " + name;
        }

        private void lblIdentity_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //AddStudent ad = new AddStudent();
            //ad.ShowDialog();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            // Create an instance of the AdminFeedback form
            AdminFeedbacks feedbackPage = new AdminFeedbacks();
            // Show the AdminFeedback form
            feedbackPage.Show();
            // Close the current AdminHomeForm
            this.Close();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnServicePage_Click(object sender, EventArgs e)
        {
            // Create an instance of the AdminService form
            AdminService servicePage = new AdminService();
            // Show the AdminService form
            servicePage.Show();
            //Close the current AdminHomeForm
            this.Close();
        }

        private void btnStaffPage_Click(object sender, EventArgs e)
        {
            // Create an instance of the AdminService form
            AdminStaff staffPage = new AdminStaff();
            // Show the AdminService form
            staffPage.Show();
            //Close the current AdminHomeForm
            this.Close();
        }
    }
}