using BankingBusiness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BANK_WITH_C_
{
    public partial class frmFindAndDeleteClient : Form
    {
       
        private int _currentCustomerID = -1;

        public frmFindAndDeleteClient()
        {
            InitializeComponent();
        }

        private void DisplayCustomer(DataRow customerRow)
        {
            try
            {
                _currentCustomerID = Convert.ToInt32(customerRow["CustomerID"]);

                // Set textbox values
                txteFirstName.Text = customerRow["FirstName"]?.ToString() ?? "";
                txtelastName.Text = customerRow["LastName"]?.ToString() ?? "";
                textePhone.Text = customerRow["Phone"]?.ToString() ?? "";
                txteAccountNumber.Text = customerRow["AccountNumber"]?.ToString() ?? "";
                txtePincode.Text = customerRow["PinCode"]?.ToString() ?? "";

                // Handle Account Balance
                if (customerRow["AccountBalance"] != DBNull.Value)
                {
                    decimal balance = Convert.ToDecimal(customerRow["AccountBalance"]);
                    texteAccountSalary.Text = balance.ToString("C");
                }
                else
                {
                    texteAccountSalary.Text = "$0.00";
                }

                // Handle Date of Birth
                if (customerRow["DateOfBirth"] != DBNull.Value)
                {
                    dtpeDateofBirth.Value = Convert.ToDateTime(customerRow["DateOfBirth"]);
                }

                // Enable delete button
                btunDelete.Enabled = true;

                // Show success message
                lbleStatus.Text = $"Customer found: {txteFirstName.Text} {txtelastName.Text}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error displaying customer data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       

        private void ClearForm()
        {
            txteFirstName.Clear();
            txtelastName.Clear();
            textePhone.Clear();
            txteAccountNumber.Clear();
            texteAccountSalary.Clear();
            txtePincode.Clear();
            dtpeDateofBirth.Value = DateTime.Now;
            lbleStatus.Text = "Ready";
        }

        private void frmFindAndDeleteClient_Load_1(object sender, EventArgs e)
        {

            // Add search options to combobox
            cmbeCustomer.Items.AddRange(new string[] {
            "AccountNumber", "FirstName", "LastName", "Phone", "CustomerID"   });
            cmbeCustomer.SelectedIndex = 0;

            btunDelete.Enabled = false;
        }

        private void btunDelete_Click(object sender, EventArgs e)
        {
            if (_currentCustomerID == -1)
            {
                MessageBox.Show("Please select a customer first.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string customerName = $"{txteFirstName.Text} {txtelastName.Text}";
            string accountNumber = txteAccountNumber.Text;

            DialogResult result = MessageBox.Show(
                $"Are you sure you want to delete this customer?\n\n" +
                $"Name: {customerName}\n" +
                $"Account: {accountNumber}\n\n" +
                "This action cannot be undone!",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {
                bool isDeleted = clsCustomerData.DeleteCustomer(_currentCustomerID);

                if (isDeleted)
                {
                    MessageBox.Show("Customer deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    _currentCustomerID = -1;
                    btunDelete.Enabled = false;
                    lbleStatus.Text = "Customer deleted successfully";
                }
                else
                {
                    MessageBox.Show("Failed to delete customer. They may have existing transactions.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btunFind_Click(object sender, EventArgs e)
        {

            try
            {
                string searchValue = texteCustomer.Text.Trim();
                string searchBy = cmbeCustomer.SelectedItem?.ToString() ?? "FirstName";



                if (string.IsNullOrEmpty(searchValue))
                {
                    MessageBox.Show("Please enter a search value.");
                    return;
                }

                DataTable searchResults = clsCustomerData.FindCustomer(searchValue, searchBy);



                if (searchResults.Rows.Count > 0)
                {
                    // Display the first found customer
                    DisplayCustomer(searchResults.Rows[0]);
                }
                else
                {
                    MessageBox.Show("No customers found with the specified criteria.");
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching for customer: {ex.Message}\n\n{ex.StackTrace}");
            }
        }

        private void BtunClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    
    

}

