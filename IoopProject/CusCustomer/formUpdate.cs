using System;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace IoopProject.CusCustomer
{
    public partial class formUpdate : Form
    {
        // ----- Variables ----- //

        private User user;

        private static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myCS"].ToString());

        // ----- Form Layout (Default Constructor) ----- //

        public formUpdate()
        {
            InitializeComponent();
        }

        // ----- Form Preload ----- //

        public formUpdate(string Username)
        {
            user = new User(Username);

            InitializeComponent();

            txtread_UserID.Text = user.UserID;
            txt_Username.Text = Username;
            txt_Email.Text = user.Email;
            txt_Password.Text = user.Password;
            this.Focus();
        }

        // ----- Cancel Update ----- //
        private void btnBack_Click(object sender, EventArgs e)
        {
            if (user.Role == "Customer")
            {
                formCustomer form = new formCustomer(user.Username);
                form.Show();
                this.Close();
                form.Focus();
            }
            else if (user.Role == "Receptionist")
            {
                RecepHome form = new RecepHome(user.Username);
                form.Show();
                this.Close();
                form.Focus();
            }
            else if (user.Role == "Mechanic")
            {
                MecHome form = new MecHome(user.Username);
                form.Show();
                this.Close();
                form.Focus();
            }
        }

        // ----- Update Details ----- //

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string status = "Incomplete.";

            string username = txt_Username.Text;
            User user = new User(username);

            user.Username = txt_Username.Text;
            user.Email = txt_Email.Text;
            user.Password = txt_Password.Text;

            if (user.Username.Length < 50 && user.Password.Length < 50)
            {
                if (user.Email.Contains("@") && user.Email.Contains("."))
                {
                    status = user.updateProfile();
                    MessageBox.Show(status);

                    if (user.Role == "Customer")
                    {
                        formCustomer form = new formCustomer(user.Username);
                        form.Show();
                        this.Close();
                        form.Focus();
                    }
                    else if (user.Role == "Receptionist")
                    {
                        RecepHome form = new RecepHome(user.Username);
                        form.Show();
                        this.Close();
                        form.Focus();
                    }
                    else if (user.Role == "Mechanic")
                    {
                        MecHome form = new MecHome(user.Username);
                        form.Show();
                        this.Close();
                        form.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Email invalid.");
                }
            }
            else
            {
                MessageBox.Show("Username or Password too long.");
            }
        }

        private void formUpdate_Load(object sender, EventArgs e)
        {

        }
    }
}