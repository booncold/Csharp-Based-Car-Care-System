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
    public partial class AdminFeedbacks : Form
    {
        public AdminFeedbacks()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AdminHome backAdminPage = new AdminHome(AdminHome.name);
            backAdminPage.Show();
            this.Close();
        }

        private void btnViewFeedback_Click(object sender, EventArgs e)
        {
            LoadAllFeedback();
        }

        private void LoadAllFeedback()
        {
            ViewFeedback viewFeedback = new ViewFeedback();
            List<FeedbackDetails> feedbackList = viewFeedback.GetAllFeedback();

            MessageBox.Show("Number of feedbacks retrieved: " + feedbackList.Count);

            dataGridView1.DataSource = feedbackList;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void btnViewReport_Click(object sender, EventArgs e)
        {
            string filterCriteria = comboBoxFilter.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(filterCriteria))
            {
                MessageBox.Show("Please select a filter criteria.");
                return;
            }

            try
            {
                ViewReport viewReport = new ViewReport();
                List<ReportDetails> reportList = viewReport.GetFilteredReports(filterCriteria);

                if (reportList == null || reportList.Count == 0)
                {
                    MessageBox.Show("No reports found for the selected criteria.");
                    dataGridViewReport.DataSource = null;
                }
                else
                {
                    dataGridViewReport.DataSource = reportList;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while retrieving the report: " + ex.Message);
            }
        }
    }
}