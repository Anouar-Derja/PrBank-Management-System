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
    public partial class frmUpdateClients : Form
    {
        private int _customerID = -1;
        public frmUpdateClients()
        {
            InitializeComponent();
        }


        private void DisplayCustomer(DataRow customerRow)
        {
            try
            {
                _customerID = Convert.ToInt32(customerRow["CustomerID"]);

                txteFirstName.Text = customerRow["FirstName"]?.ToString() ?? "";
                txteLastName.Text = customerRow["LastName"]?.ToString() ?? "";
                txtAccountNumber.Text = customerRow["AccountNumber"]?.ToString() ?? "";
                txtPincode.Text = customerRow["PinCode"]?.ToString() ?? "";
                txtePhone.Text = customerRow["Phone"]?.ToString() ?? "";

                // Handle account balance
                if (customerRow["AccountBalance"] != DBNull.Value)
                {
                    txtAccountsalary.Text = Convert.ToDecimal(customerRow["AccountBalance"]).ToString("F2");
                }
                else
                {
                    txtAccountsalary.Text = "0.00";
                }

                // Handle date of birth
                if (customerRow["DateOfBirth"] != DBNull.Value)
                {
                    dtpeDateOfBirth.Value = Convert.ToDateTime(customerRow["DateOfBirth"]);
                }

                lblStatus.Text = $"Editing: {txteFirstName.Text} {txteLastName.Text}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error displaying customer data: " + ex.Message);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrEmpty(txteFirstName.Text))
            {
                MessageBox.Show("Please enter First Name", "Validation Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txteFirstName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txteLastName.Text))
            {
                MessageBox.Show("Please enter Last Name", "Validation Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txteLastName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtPincode.Text) || txtPincode.Text.Length != 4)
            {
                MessageBox.Show("Please enter a 4-digit PIN code", "Validation Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPincode.Focus();
                return false;
            }

            if (!decimal.TryParse(txtAccountsalary.Text, out decimal balance) || balance < 0)
            {
                MessageBox.Show("Please enter a valid account balance", "Validation Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAccountsalary.Focus();
                return false;
            }

            return true;
        }

        private void ClearCustomerFields()
        {
            txteFirstName.Clear();
            txteLastName.Clear();
            txtAccountNumber.Clear();
            txtPincode.Clear();
            txtePhone.Clear();
            txtAccountsalary.Text = "0.00";
            dtpeDateOfBirth.Value = DateTime.Now.AddYears(-18);
            _customerID = -1;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtAccountBalance_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only numbers and decimal point
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // Only allow one decimal point
            if (e.KeyChar == '.' && ((TextBox)sender).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtPincode_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only numbers
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            // Limit to 4 digits
            if (txtPincode.Text.Length >= 4 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtSearchValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow Enter key to trigger Find button
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnfind.PerformClick();
                e.Handled = true;
            }
        }

        private void cmbSearchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Clear search value when search type changes
            txtCustomer.Clear();
            txtCustomer.Focus();
        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (_customerID == -1)
                {
                    MessageBox.Show("Please find a customer first.", "Update Client",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ValidateInput())
                {
                    return;
                }

                // Update customer information
                bool updated = clsCustomerData.UpdateCustomer(
                    CustomerID: _customerID,
                    FirstName: txteFirstName.Text,
                    LastName: txteLastName.Text,
                    Phone: txtePhone.Text,
                    DateOfBirth: dtpeDateOfBirth.Value,
                    AccountStatus: "Active"
                );

                // Update account balance
                decimal newBalance = decimal.Parse(txtAccountsalary.Text);
                bool balanceUpdated = clsCustomerData.UpdateAccountBalance(_customerID, newBalance);

                if (updated || balanceUpdated)
                {
                    MessageBox.Show("Client updated successfully!", "Success",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearCustomerFields();
                    btnUpdate.Enabled = false;
                    lblStatus.Text = "Client updated successfully. Find another client.";
                }
                else
                {
                    MessageBox.Show("Failed to update client.", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating client: " + ex.Message, "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmUpdateClients_Load(object sender, EventArgs e)
        {
            try
            {

                dtpeDateOfBirth.Value = DateTime.Now.AddYears(-18);

                // Initialize combo box
                cmbCustomer.Items.Add("Account Number");
                cmbCustomer.Items.Add("First Name");
                cmbCustomer.Items.Add("Last Name");
                cmbCustomer.SelectedIndex = 0;

                btnfind.Enabled = true;
                btnUpdate.Enabled = false;
                lblStatus.Text = "Enter search criteria and click Find";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading form: " + ex.Message);
            }
        }
        private string GenerateAccountNumber()
        {
            Random random = new Random();
            string prefix = "CHK";
            string numbers = random.Next(100000, 999999).ToString();
            return prefix + numbers;
        }


        private void btnfind_Click(object sender, EventArgs e)
        {
            try
            {
                string searchValue = txtCustomer.Text.Trim();
                string searchType = cmbCustomer.SelectedItem.ToString();

                if (string.IsNullOrEmpty(searchValue))
                {
                    MessageBox.Show($"Please enter a {searchType.ToLower()} to find.", "Find Client",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Find customer by selected search type
                DataTable customerData = clsCustomerData.FindCustomer(searchValue, searchType);

                if (customerData.Rows.Count > 0)
                {
                    // Display the first customer found (for simplicity)
                    DisplayCustomer(customerData.Rows[0]);
                    btnUpdate.Enabled = true;

                    if (customerData.Rows.Count > 1)
                    {
                        lblStatus.Text = $"Multiple customers found. Displaying first result: {txteFirstName.Text} {txteLastName.Text}";
                        MessageBox.Show($"Multiple customers found with this {searchType.ToLower()}. Displaying the first result.",
                                      "Multiple Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        lblStatus.Text = "Customer found. Make changes and click Update.";
                    }
                }
                else
                {
                    MessageBox.Show($"No customer found with this {searchType.ToLower()}.", "Not Found",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearCustomerFields();
                    btnUpdate.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error finding customer: " + ex.Message, "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGenerateAccountNumber_Click(object sender, EventArgs e)
        {
            txtAccountNumber.Text = GenerateAccountNumber();

        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            ClearCustomerFields();

        }
    }

   

}

