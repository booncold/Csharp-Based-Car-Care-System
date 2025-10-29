using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IoopProject
{
    public partial class AdminService : Form
    {
        int time;
        double price;

        public AdminService()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            AdminHome backAdminPage = new AdminHome(AdminHome.name);

            backAdminPage.Show();

            this.Close();
        }

        private void btnAddService_Click(object sender, EventArgs e)
        {
            double price;
            int time;
            string serviceDescription = txtBoxAddDescription.Text;

            try
            {
                price = double.Parse(txtBoxAddPrice.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid input for price: " + ex.Message);
                return;
            }

            lblShowPrice.Text = price.ToString("C");

            try
            {
                time = int.Parse(txtBoxAddTime.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid input for time: " + ex.Message);
                return;
            }

            lblShowTime.Text = time.ToString() + " hours";
            lblShowServ.Text = txtBoxAddServ.Text;
            lblShowDescription.Text = serviceDescription;

            string serviceName = txtBoxAddServ.Text;
            if (ServiceNameExists(serviceName))
            {
                MessageBox.Show("Service name already exists. Please choose a different name.");
                return;
            }

            AddServices newService = new AddServices
            {
                ServiceName = serviceName,
                Price = price.ToString(),
                Time = time.ToString(),
                ServiceDescription = serviceDescription
            };

            string result = newService.AddService();

            MessageBox.Show(result);
        }

        private bool ServiceNameExists(string serviceName)
        {
            bool exists = false;

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myCS"].ToString()))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM service WHERE Service_Name = @serviceName", con);
                    cmd.Parameters.AddWithValue("@serviceName", serviceName);
                    int count = (int)cmd.ExecuteScalar();
                    exists = (count > 0);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error checking service name: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }

            return exists;
        }

        private void btnClearAddService_Click(object sender, EventArgs e)
        {
            txtBoxAddServ.Clear();
            txtBoxAddPrice.Clear();
            txtBoxAddTime.Clear();
            txtBoxAddDescription.Clear();
            lblShowServ.Text = string.Empty;
            lblShowPrice.Text = string.Empty;
            lblShowTime.Text = string.Empty;
            lblShowDescription.Text = string.Empty;
        }

        private void btnCheckService_Click(object sender, EventArgs e)
        {
            ServiceRetriever serviceRetriever = new ServiceRetriever
            {
                ServiceName = txtBoxCheckService.Text
            };
            MessageBox.Show("Checking service: " + serviceRetriever.ServiceName);

            if (!string.IsNullOrEmpty(serviceRetriever.ServiceName))
            {
                var serviceDetails = serviceRetriever.GetServiceDetails();

                MessageBox.Show("Retrieved service: " + serviceDetails.serviceName);

                if (!string.IsNullOrEmpty(serviceDetails.serviceName))
                {
                    lblOldName.Text = serviceDetails.serviceName;
                    lblOldPrice.Text = $"RM{serviceDetails.price}";
                    lblOldTime.Text = $"{serviceDetails.time} hours";
                    lblOldDescription.Text = serviceDetails.serviceDescription;
                }
                else
                {
                    lblOldName.Text = "Service not found.";
                    lblOldPrice.Text = string.Empty;
                    lblOldTime.Text = string.Empty;
                    lblOldDescription.Text = string.Empty;
                }
            }
            else
            {
                lblOldName.Text = string.Empty;
                lblOldPrice.Text = string.Empty;
                lblOldTime.Text = string.Empty;
                lblOldDescription.Text = string.Empty;
            }
        }
        private void btnEnter_Click(object sender, EventArgs e)
        {
            double price;
            int time;

            try
            {
                price = double.Parse(txtBoxEditPrice.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid input for price: " + ex.Message);
                return;
            }

            try
            {
                time = int.Parse(txtBoxEditTime.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid input for time: " + ex.Message);
                return;
            }

            string newServiceName = txtBoxEditService.Text;
            string newServiceDescription = txtBoxEditDescription.Text;

            if (ServiceNameExists(newServiceName))
            {
                MessageBox.Show("Service name already exists. Please choose a different name.");
                return;
            }

            ServiceUpdate serviceUpdate = new ServiceUpdate
            {
                OldServiceName = txtBoxCheckService.Text,
                ServiceName = newServiceName,
                Price = price.ToString(),
                Time = time.ToString(),
                ServiceDescription = newServiceDescription
            };

            string result = serviceUpdate.UpdateServiceDetails();
            MessageBox.Show(result);
        }


        private void btnShowNewService_Click(object sender, EventArgs e)
        {
            ServiceRetriever serviceRetriever = new ServiceRetriever
            {
                ServiceName = txtBoxEditService.Text
            };

            if (!string.IsNullOrEmpty(serviceRetriever.ServiceName))
            {
                var serviceDetails = serviceRetriever.GetServiceDetails();

                if (!string.IsNullOrEmpty(serviceDetails.serviceName))
                {
                    lblNewName.Text = serviceDetails.serviceName;
                    lblNewPrice.Text = $"RM{serviceDetails.price}";
                    lblNewTime.Text = $"{serviceDetails.time} hours";
                    lblNewDescription.Text = serviceDetails.serviceDescription;
                    txtBoxEditService.Text = serviceDetails.serviceName;
                    txtBoxEditPrice.Text = serviceDetails.price;
                    txtBoxEditTime.Text = serviceDetails.time;
                    txtBoxEditDescription.Text = serviceDetails.serviceDescription;
                }
                else
                {
                    lblNewName.Text = "Service not found.";
                    lblNewPrice.Text = string.Empty;
                    lblNewTime.Text = string.Empty;
                    lblNewDescription.Text = string.Empty;
                }
            }
            else
            {
                lblNewName.Text = string.Empty;
                lblNewPrice.Text = string.Empty;
                lblNewTime.Text = string.Empty;
                lblNewDescription.Text = string.Empty;
                MessageBox.Show("Please enter a service name to proceed.");
            }
        }


        private void btnClearEditService_Click(object sender, EventArgs e)
        {
            txtBoxCheckService.Clear();
            txtBoxEditService.Clear();
            txtBoxEditPrice.Clear();
            txtBoxEditTime.Clear();
            txtBoxEditDescription.Clear();
            lblNewName.Text = string.Empty;
            lblNewPrice.Text = string.Empty;
            lblNewTime.Text = string.Empty;
            lblNewDescription.Text = string.Empty;
            lblOldName.Text = string.Empty;
            lblOldPrice.Text = string.Empty;
            lblOldTime.Text = string.Empty;
            lblOldDescription.Text = string.Empty;
        }
        private void button6_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnDeteteService_Click(object sender, EventArgs e)
        {
            DeleteService deleteService = new DeleteService
            {
                ServiceName = txtBoxDeleteService.Text
            };

            string result = deleteService.DeleteServiceAndDependencies();
            MessageBox.Show(result);
        }

        private void btnClearDeleteService_Click(object sender, EventArgs e)
        {
            txtBoxDeleteService.Clear();
        }

        private void btnViewService_Click(object sender, EventArgs e)
        {
            ServiceRetriever serviceRetriever = new ServiceRetriever
            {
                ServiceName = txtBoxViewService.Text
            };
            var serviceDetails = serviceRetriever.GetServiceDetails();

            if (!string.IsNullOrEmpty(serviceDetails.serviceName))
            {
                lblViewName.Text = serviceDetails.serviceName;
                lblViewPrice.Text = $"RM{serviceDetails.price}";
                lblViewTime.Text = $"{serviceDetails.time} hours";
                lblViewDescription.Text = serviceDetails.serviceDescription;
            }
            else
            {
                lblViewName.Text = "Service not found.";
                lblViewPrice.Text = string.Empty;
                lblViewTime.Text = string.Empty;
                lblViewDescription.Text = string.Empty;
            }
        }


        private void btnClearViewService_Click(object sender, EventArgs e)
        {
            txtBoxViewService.Clear();
            lblViewName.Text = string.Empty;
            lblViewPrice.Text = string.Empty;
            lblViewTime.Text = string.Empty;
        }
        private void LoadAllServices()
        {
            ViewAllService viewAllService = new ViewAllService();
            List<ServiceDetails> serviceList = viewAllService.GetAllServices();

            dataGridViewAllServices.DataSource = serviceList;
        }

        private void btnViewAllService_Click(object sender, EventArgs e)
        {
            LoadAllServices();
        }
    }
}