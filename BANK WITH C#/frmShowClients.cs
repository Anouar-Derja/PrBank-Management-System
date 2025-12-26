using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingBusiness;

using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace BANK_WITH_C_
{
    public partial class frmShowClients : Form
    {
        private DataTable _dtClients = clsCustomer.GetAllCustomers();
        public frmShowClients()
        {
            InitializeComponent();
        }

        private void _RefreshPeoplList()
        {
            _dtClients = clsCustomer.GetAllCustomers();
            _dtClients = _dtClients.DefaultView.ToTable(false, "CustomerID", "AccountNumber",
                                                       "PinCode", "FirstName", "LastName", "Phone",
                                                       "DateOfBirth", "AccountBalance", "AccountStatus",
                                                       "CreatedAt", "UserID");

            dgvClient.DataSource = _dtClients;

          
        }

       

        private void DgvClients_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // This event fires AFTER data binding is complete
            // Now we can safely access the columns

            try
            {
                // Check if we have enough columns
                if (dgvClient.Columns.Count < 9)
                {
                    MessageBox.Show($"Expected at least 9 columns, but found {dgvClient.Columns.Count}");
                    return;
                }

                dgvClient.Columns[0].HeaderText = "CustomerID";
                dgvClient.Columns[0].Width = 110;

                dgvClient.Columns[1].HeaderText = "AccountNumber";
                dgvClient.Columns[1].Width = 120;

                dgvClient.Columns[2].HeaderText = "PinCode";
                dgvClient.Columns[2].Width = 350;

                dgvClient.Columns[3].HeaderText = "FirstName";
                dgvClient.Columns[3].Width = 120;

                dgvClient.Columns[4].HeaderText = "LastName";
                dgvClient.Columns[4].Width = 120;

                dgvClient.Columns[5].HeaderText = "Phone";
                dgvClient.Columns[5].Width = 120;

                dgvClient.Columns[6].HeaderText = "DateOfBirth";
                dgvClient.Columns[6].Width = 120;

                dgvClient.Columns[7].HeaderText = "AccountBalance";
                dgvClient.Columns[7].Width = 120;

                dgvClient.Columns[8].HeaderText = "AccountStatus";
                dgvClient.Columns[8].Width = 120; // Added width for consistency

                // Optional: Format specific columns
                dgvClient.Columns[6].DefaultCellStyle.Format = "yyyy-MM-dd"; // Format DateOfBirth
                dgvClient.Columns[7].DefaultCellStyle.Format = "C2"; // Format AccountBalance as currency

                // Optional: Hide sensitive columns like PinCode
                dgvClient.Columns[2].Visible = false; // Hide PinCode column

                // Remove the event handler to prevent multiple subscriptions
                dgvClient.DataBindingComplete -= DgvClients_DataBindingComplete;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error configuring grid columns: {ex.Message}");
            }
        }

        private void frmShowClients_Load_1(object sender, EventArgs e)
        {
            try
            {
                _dtClients = clsCustomer.GetAllCustomers();
                dgvClient.DataSource = _dtClients;

                // Wait for data binding to complete before accessing columns
                dgvClient.DataBindingComplete += DgvClients_DataBindingComplete;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading clients: {ex.Message}");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
