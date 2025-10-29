using System;
using System.Configuration;
using System.Data.SqlClient;

namespace IoopProject
{
    public class DeleteStaff
    {
        static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myCS"].ToString());

        public string DeleteByUsername(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return "Error: Username must be provided.";
            }

            string status;
            try
            {
                con.Open();


                SqlCommand checkCmd = new SqlCommand("SELECT userid FROM users WHERE username = @username", con);
                checkCmd.Parameters.AddWithValue("@username", username);
                object userIdObj = checkCmd.ExecuteScalar();

                if (userIdObj == null)
                {
                    return "Error: Username not found.";
                }

                int userId = (int)userIdObj;


                SqlCommand cmdStaff = new SqlCommand("DELETE FROM Staff WHERE UserId = @userId", con);
                cmdStaff.Parameters.AddWithValue("@userId", userId);
                cmdStaff.ExecuteNonQuery();


                SqlCommand cmdUser = new SqlCommand("DELETE FROM users WHERE userid = @userId", con);
                cmdUser.Parameters.AddWithValue("@userId", userId);
                cmdUser.ExecuteNonQuery();

                status = "Staff deleted successfully.";
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
    }
}