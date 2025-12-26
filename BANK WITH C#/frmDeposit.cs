using Banking_DataAccess;
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
    public partial class FrmDeposit : Form
    {

        private string _accountNumber;
        private decimal _currentBalance;


        public FrmDeposit()
        {
            InitializeComponent();
          
        }
        public FrmDeposit(string accountNumber) : this()
        {
            _accountNumber = accountNumber;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      
        
            private void frmDeposit_Load(object sender, EventArgs e)
            {
                lblStatus.Text = "Enter account number and click Search";

                // If account number was provided, auto-search
                if (!string.IsNullOrEmpty(_accountNumber))
                {
                    txtAccountNumber.Text = _accountNumber;
                    btnSearch.PerformClick();
                }
            }

            private void btnSearch_Click(object sender, EventArgs e)
            {
                try
                {
                    string accountNumber = txtAccountNumber.Text.Trim();

                    if (string.IsNullOrEmpty(accountNumber))
                    {
                        MessageBox.Show("Please enter an account number", "Search",
                                      MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Search for customer account
                    DataTable accountData = clsCustomerData.FindCustomer(accountNumber, "AccountNumber");

                    if (accountData.Rows.Count > 0)
                    {
                        DataRow account = accountData.Rows[0];

                        // Display account information
                        txtClientName.Text = $"{account["FirstName"]} {account["LastName"]}";
                        _currentBalance = Convert.ToDecimal(account["AccountBalance"]);
                        txtCurrentBalance.Text = _currentBalance.ToString("C2");

                        lblStatus.Text = "Account found. Enter deposit amount";
                        btnDeposit.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Account not found", "Search",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearAccountInfo();
                        btnDeposit.Enabled = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error searching account: " + ex.Message, "Error");
                }
            }

            private void ClearAccountInfo()
            {
                txtClientName.Text = "Not found";
                txtCurrentBalance.Text = "$0.00";
                _currentBalance = 0;
            }

            private bool ValidateDeposit()
            {
                // Validate amount
                if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
                {
                    MessageBox.Show("Please enter a valid amount greater than zero", "Validation",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAmount.Focus();
                    return false;
                }

                // Validate account is selected
                if (string.IsNullOrEmpty(txtAccountNumber.Text) || txtClientName.Text == "Not found")
                {
                    MessageBox.Show("Please search and select a valid account", "Validation",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAccountNumber.Focus();
                    return false;
                }

                return true;
            }

            private void btnDeposit_Click(object sender, EventArgs e)
            {
                if (!ValidateDeposit())
                    return;

                try
                {
                    decimal amount = decimal.Parse(txtAmount.Text);
                    string description = txtDescription.Text.Trim();
                    string accountNumber = txtAccountNumber.Text;

                    // Get current user ID (you'll need to implement this based on your auth system)
                    int currentUserID = GetCurrentUserID();

                    bool success = clsTransactionData.ProcessDeposit(
                        accountNumber: accountNumber,
                        amount: amount,
                        description: description,
                        userID: currentUserID
                    );

                    if (success)
                    {
                        MessageBox.Show("Deposit completed successfully!", "Success",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Update balance display
                        _currentBalance += amount;
                        txtCurrentBalance.Text = _currentBalance.ToString("C2");

                        // Clear amount for new deposit
                        txtAmount.Text = "";
                        txtDescription.Text = "";

                        lblStatus.Text = "Deposit successful. Ready for next transaction";
                    }
                    else
                    {
                        MessageBox.Show("Deposit failed. Please try again.", "Error",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error during deposit: " + ex.Message, "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            private int GetCurrentUserID()
            {
                try
                {
                    // TODO: Replace with your actual user authentication logic
                    // For now, return a default user ID
                    return 1;
                }
                catch
                {
                    MessageBox.Show("Could not get current user ID. Using default ID 1.", "Warning");
                    return 1;
                }
            }

           

            private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
            {
                // Allow only numbers and decimal point
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }

                // Allow only one decimal point
                if (e.KeyChar == '.' && ((TextBox)sender).Text.IndexOf('.') > -1)
                {
                    e.Handled = true;
                }
            }

            private void txtAmount_TextChanged(object sender, EventArgs e)
            {
                // Enable deposit button only when amount is valid
                btnDeposit.Enabled = !string.IsNullOrEmpty(txtAmount.Text) &&
                                   decimal.TryParse(txtAmount.Text, out decimal amount) &&
                                   amount > 0 &&
                                   txtClientName.Text != "Not found";
            }
    }
}
