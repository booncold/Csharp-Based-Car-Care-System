using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace IoopProject
{
    public class ViewAllStaffDetails
    {
        public int StaffId { get; set; } 
        public string StaffName { get; set; }
        public string StaffPosition { get; set; }
        public DateTime StaffDateJoin { get; set; }
        public string StaffGender { get; set; }
        public string StaffEmail { get; set; }
        public string StaffPassword { get; set; }
        public byte[] StaffImage { get; set; }
        public string StaffUsername { get; set; }
    }

    public class ViewAllStaff
    {
        static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myCS"].ToString());

        public List<ViewAllStaffDetails> GetAllStaff()
        {
            List<ViewAllStaffDetails> staffList = new List<ViewAllStaffDetails>();

            try
            {
                con.Open();

                string query = "SELECT u.userid, u.username, u.email, u.password, s.Name, s.Position, s.DateJoin, s.Gender, s.Image " +
                               "FROM users u " +
                               "JOIN Staff s ON u.userid = s.UserId";

                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ViewAllStaffDetails staff = new ViewAllStaffDetails
                    {
                        StaffId = reader.GetInt32(0),
                        StaffUsername = reader.GetString(1),
                        StaffEmail = reader.GetString(2),
                        StaffPassword = reader.GetString(3),
                        StaffName = reader.GetString(4),
                        StaffPosition = reader.GetString(5),
                        StaffDateJoin = reader.GetDateTime(6),
                        StaffGender = reader.GetString(7),
                        StaffImage = reader[8] as byte[]
                    };

                    staffList.Add(staff);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }

            return staffList;
        }
    }
}