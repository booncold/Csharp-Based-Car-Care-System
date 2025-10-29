using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace IoopProject
{
    public class AddStaff
    {
        private string userName;
        private string email;
        private string password;
        private string name;
        private string position;
        private DateTime dateJoin;
        private string gender;
        private byte[] image;
        static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myCS"].ToString());

        public string UserName { get => userName; set => userName = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public string Name { get => name; set => name = value; }
        public string Position { get => position; set => position = value; }
        public DateTime DateJoin { get => dateJoin; set => dateJoin = value; }
        public string Gender { get => gender; set => gender = value; }
        public byte[] Image { get => image; set => image = value; }

        public string AddNewStaff(string role)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(name) || string.IsNullOrEmpty(position) || dateJoin == default(DateTime) || string.IsNullOrEmpty(gender))
            {
                return "Error: All fields must be filled.";
            }

            if (image == null)
            {
                return "Error: An image must be uploaded.";
            }

            string status;
            try
            {
                con.Open();

                SqlCommand checkCmdName = new SqlCommand("SELECT COUNT(*) FROM Staff WHERE Name = @name", con);
                checkCmdName.Parameters.AddWithValue("@name", name);
                int nameExists = (int)checkCmdName.ExecuteScalar();

                if (nameExists > 0)
                {
                    return "Error: Staff with this name already exists.";
                }

                SqlCommand checkCmdUsername = new SqlCommand("SELECT COUNT(*) FROM users WHERE username = @username", con);
                checkCmdUsername.Parameters.AddWithValue("@username", userName);
                int usernameExists = (int)checkCmdUsername.ExecuteScalar();

                if (usernameExists > 0)
                {
                    return "Error: Username already exists.";
                }

                SqlCommand cmdUser = new SqlCommand(
                    "INSERT INTO users (username, email, password, role) VALUES (@username, @email, @password, @role); " +
                    "SELECT SCOPE_IDENTITY();", con);
                cmdUser.Parameters.AddWithValue("@username", userName);
                cmdUser.Parameters.AddWithValue("@role", role);
                cmdUser.Parameters.AddWithValue("@email", email);
                cmdUser.Parameters.AddWithValue("@password", password);

                int userId = Convert.ToInt32(cmdUser.ExecuteScalar());

                SqlCommand cmdStaff = new SqlCommand(
                    "INSERT INTO Staff (Name, Position, DateJoin, Gender, Image, UserId) VALUES (@name, @position, @dateJoin, @gender, @image, @userId)", con);
                cmdStaff.Parameters.AddWithValue("@name", name);
                cmdStaff.Parameters.AddWithValue("@position", position);
                cmdStaff.Parameters.AddWithValue("@dateJoin", dateJoin);
                cmdStaff.Parameters.AddWithValue("@gender", gender);
                cmdStaff.Parameters.AddWithValue("@image", (object)image ?? DBNull.Value);
                cmdStaff.Parameters.AddWithValue("@userId", userId);

                int rows = cmdStaff.ExecuteNonQuery();

                status = (rows > 0) ? "Staff added successfully." : "Failed to add staff.";
            }
            catch (Exception ex)
            {
                status = "Error: " + ex.Message;
            }
            finally
            {
                con.Close();
            }
            return status;
        }

        public void LoadImage(string imagePath)
        {
            if (!string.IsNullOrEmpty(imagePath))
            {
                using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        image = br.ReadBytes((int)fs.Length);
                    }
                }
            }
        }
    }
}