using IoopProject.CusCustomer;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
//pay attention to the below 2 lines to include Database access (Conenction object) and configuration manager
using System.Configuration;   // this is for configuration manager object library
using System.Data.SqlClient;  // this one is the connection object library
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace IoopProject
{
    public class User
    {
        private string username;
        private string password;

        public User(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string login()
        {
            //username = un;
            //password = pw;

            string status = null;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myCS"].ToString());
            con.Open();

            SqlCommand cmd = new SqlCommand("Select count(*) from users where username = '" + username + "' and password='" + password + "'", con);

            int count = Convert.ToInt32(cmd.ExecuteScalar().ToString());

            if (count > 0)
            {
                SqlCommand cmd2 = new SqlCommand("Select role from users where username = '" + username + "' and password='" + password + "'", con);
                string userRole = cmd2.ExecuteScalar().ToString();

                if (userRole == "Admin")
                {
                    AdminHome a = new AdminHome(Username);
                    a.Show();
                    
                }
                else if (userRole == "Receptionist")
                {
                    RecepHome r = new RecepHome(Username);
                    r.Show();
                    
                }

                else if (userRole == "Customer")
                {
                    formCustomer c = new formCustomer(Username);
                    c.Show();
                }

                else if (userRole == "Mechanic")
                {
                    MecHome m = new MecHome(Username);
                    m.Show();
                }
                status = null;

            }
            else
            {
                status = "Incorrect username/password";
            }

            con.Close();
            return status;
        }

        private static string userID;
        private static string email;
        private static string role;
        public string Email { get => email; set => email = value; }
        public string UserID { get => userID; set => userID = value; }
        public string Role { get => role; set => role = value; }


        public User(string username)
        {
            Username = username;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myCS"].ToString());
            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT UserID, Password, Email, Role FROM Users WHERE Username = @username; ", con);
            cmd.Parameters.AddWithValue("@username", Username);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                UserID = reader.GetInt32(0).ToString();
                Password = reader.GetString(1);
                Email = reader.GetString(2);
                Role = reader.GetString(3);
                reader.Close();
            }
            else
            {
            }
            con.Close();
        }

        public string updateProfile()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myCS"].ToString());

            string status = "";

            con.Open();

            SqlCommand cmd = new SqlCommand(
                "UPDATE Users " +
                "SET Username = @new_username, Password = @new_password, Email = @new_email " +
                "WHERE UserID = @uid", con);
            cmd.Parameters.AddWithValue("@uid", int.Parse(UserID));
            cmd.Parameters.AddWithValue("@new_username", Username);
            cmd.Parameters.AddWithValue("@new_password", Password);
            cmd.Parameters.AddWithValue("@new_email", Email);

            int i = cmd.ExecuteNonQuery();

            status = "Update completed!";

            con.Close();
            return status;
        }
    }
}