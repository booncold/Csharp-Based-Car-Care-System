using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace IoopProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string stat;
            User obj1 = new User(txtUsername.Text, txtPassword.Text);
            stat = obj1.login();

            if (stat != null)
            {
                MessageBox.Show(stat);
            }
            
            txtUsername.Text = String.Empty;
            txtPassword.Text = String.Empty;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public void closeForm()
        {
            this.Close();
        }
    }
}
