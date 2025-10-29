using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace IoopProject
{
    public class FeedbackDetails
    {
        public int FeedbackID { get; set; }
        public string FeedbackText { get; set; }
        public string Rating { get; set; }
        public int CustomerID { get; set; }
        public int ServiceID { get; set; }
        public string ServiceName { get; set; }
    }

    public class ViewFeedback
    {
        static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myCS"].ToString());

        public List<FeedbackDetails> GetAllFeedback()
        {
            List<FeedbackDetails> feedbackList = new List<FeedbackDetails>();

            try
            {
                con.Open();

                string query = "SELECT f.FeedbackID, f.Feedback_Text, f.Rating, f.CustomerID, f.ServiceID, f.Service_Name " +
                               "FROM feedback f";

                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    FeedbackDetails feedback = new FeedbackDetails
                    {
                        FeedbackID = reader.GetInt32(0),
                        FeedbackText = reader.GetString(1),
                        Rating = reader.GetString(2),
                        CustomerID = reader.GetInt32(3),
                        ServiceID = reader.GetInt32(4),
                        ServiceName = reader.GetString(5)
                    };

                    feedbackList.Add(feedback);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                MessageBox.Show("Error: " + ex.Message); 
            }
            finally
            {
                con.Close();
            }

            return feedbackList;
        }
    }
}
