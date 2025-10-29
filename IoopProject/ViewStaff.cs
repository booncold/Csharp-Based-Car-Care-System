using System;
using System.Configuration;
using System.Data.SqlClient;

namespace IoopProject
{
    public class StaffDetails
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public DateTime DateJoin { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public byte[] Image { get; set; }
        public string Username { get; set; }
    }

    public class ViewStaff
    {
        static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myCS"].ToString());

        public StaffDetails GetStaffByUsername(string username)
        {
            StaffDetails staff = null;
            try
            {
                con.Open();

                SqlCommand cmdUser = new SqlCommand("SELECT userid, email, password FROM users WHERE username = @Username", con);
                cmdUser.Parameters.AddWithValue("@Username", username);
                SqlDataReader userReader = cmdUser.ExecuteReader();

                if (userReader.Read())
                {
                    int userId = userReader.GetInt32(0);
                    string email = userReader.GetString(1);
                    string password = userReader.GetString(2);

                    userReader.Close();


                    System.Windows.Forms.MessageBox.Show($"User Info - ID: {userId}, Email: {email}, Password: {password}", "User Info Retrieved");


                    SqlCommand cmdStaff = new SqlCommand("SELECT UserId, Name, Position, DateJoin, Gender, Image FROM Staff WHERE UserId = @userId", con);
                    cmdStaff.Parameters.AddWithValue("@userId", userId);
                    SqlDataReader staffReader = cmdStaff.ExecuteReader();

                    if (staffReader.Read())
                    {
                        staff = new StaffDetails
                        {
                            UserId = staffReader.GetInt32(0),
                            Name = staffReader.GetString(1),
                            Position = staffReader.GetString(2),
                            DateJoin = staffReader.GetDateTime(3),
                            Gender = staffReader.GetString(4),
                            Image = staffReader[5] as byte[],
                            Email = email,
                            Password = password,
                            Username = username
                        };


                        System.Windows.Forms.MessageBox.Show($"Staff Info - ID: {staff.UserId}, Name: {staff.Name}, Position: {staff.Position}, DateJoin: {staff.DateJoin}, Gender: {staff.Gender}", "Staff Info Retrieved");
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Staff details not found.", "Error");
                    }
                    staffReader.Close();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("User not found.", "Error");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Error: {ex.Message}", "Error");
            }
            finally
            {
                con.Close();
            }
            return staff;
        }
    }
}