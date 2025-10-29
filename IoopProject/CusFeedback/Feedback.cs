using IoopProject.CusFeedback;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace IoopProject.CusFeedback
{
    internal class Feedback
    {
        private static string feedback_text;
        private static int rating;
        private static int customer_ID;
        private static int serviceID;

        public string Feedback_text { get => feedback_text; set => feedback_text = value; }
        public int Rating { get => rating; set => rating = value; }
        public int Customer_ID { get => customer_ID; set => customer_ID = value; }
        public int ServiceID { get => serviceID; set => serviceID = value; }

        private static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myCS"].ToString());

        // ----- Full Feedback Constructor ----- //
        public Feedback(string comment, int rating, int customer_id, int serviceID)
        {
            Feedback_text = comment;
            Rating = rating;
            Customer_ID = customer_id;
            ServiceID = serviceID;
        }

        // ----- Add Feedback ----- //
        public string AddFeedback()
        {
            string status;
            con.Open();

            SqlCommand cmd = new SqlCommand(
                "INSERT INTO feedback (Feedback_Text, Rating, CustomerID, ServiceID) VALUES " +
                "(@comment, @rating, @cid, @sid); ", con);
            cmd.Parameters.AddWithValue("@comment", Feedback_text);
            cmd.Parameters.AddWithValue("@rating", Rating);
            cmd.Parameters.AddWithValue("@cid", Customer_ID);
            cmd.Parameters.AddWithValue("@sid", ServiceID);

            int i = cmd.ExecuteNonQuery();
            if (i != 0)
            {
                status = "Feedback submitted!";
            }
            else
            {
                status = "Unable to submit.";
            }
            con.Close();
            return status;
        }
    }
}