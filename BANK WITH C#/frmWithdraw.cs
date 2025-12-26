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
    public partial class frmWithdraw : Form
    {
        private string _accountNumber;
        private decimal _currentBalance;

        public frmWithdraw()
        {
            InitializeComponent();
        }

        public frmWithdraw(string accountNumber) : this()
        {
            _accountNumber = accountNumber;
        }

        private void frmWithdraw_Load(object sender, EventArgs e)
        {
            lblStatus.Text = "Enter account number and click Search";

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

                DataTable accountData = clsCustomerData.FindCustomer(accountNumber, "AccountNumber");

                if (accountData.Rows.Count > 0)
                {
                    DataRow account = accountData.Rows[0];
                    txtClientName.Text = $"{account["FirstName"]} {account["LastName"]}";
                    _currentBalance = Convert.ToDecimal(account["AccountBalance"]);
                    txtCurrentBalance.Text = _currentBalance.ToString("C2");

                    lblStatus.Text = "Account found. Enter withdrawal amount";
                    btnWithdraw.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Account not found", "Search",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearAccountInfo();
                    btnWithdraw.Enabled = false;
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

        private bool ValidateWithdrawal()
        {
            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid amount greater than zero", "Validation",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAmount.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtAccountNumber.Text) || txtClientName.Text == "Not found")
            {
                MessageBox.Show("Please search and select a valid account", "Validation",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAccountNumber.Focus();
                return false;
            }

            if (amount > _currentBalance)
            {
                MessageBox.Show($"Insufficient funds. Current balance: {_currentBalance:C2}", "Validation",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAmount.Focus();
                return false;
            }

            if (amount > 10000)
            {
                MessageBox.Show("Withdrawal amount cannot exceed $10,000", "Validation",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAmount.Focus();
                return false;
            }

            return true;
        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            if (!ValidateWithdrawal())
                return;

            try
            {
                decimal amount = decimal.Parse(txtAmount.Text);
                string description = txtDescription.Text.Trim();
                string accountNumber = txtAccountNumber.Text;

                int currentUserID = GetCurrentUserID();

                bool success = clsTransactionData.ProcessWithdrawal(
                    accountNumber: accountNumber,
                    amount: amount,
                    description: description,
                    userID: currentUserID
                );

                if (success)
                {
                    MessageBox.Show("Withdrawal completed successfully!", "Success",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _currentBalance -= amount;
                    txtCurrentBalance.Text = _currentBalance.ToString("C2");

                    txtAmount.Text = "";
                    txtDescription.Text = "";

                    lblStatus.Text = "Withdrawal successful. Ready for next transaction";
                }
                else
                {
                    MessageBox.Show("Withdrawal failed. Please try again.", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during withdrawal: " + ex.Message, "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetCurrentUserID()
        {
            try
            {
                return 1; // Replace with actual user ID logic
            }
            catch
            {
                MessageBox.Show("Could not get current user ID. Using default ID 1.", "Warning");
                return 1;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && ((TextBox)sender).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            // FIXED: Corrected variable scope issue
            decimal amount = 0;
            bool amountValid = !string.IsNullOrEmpty(txtAmount.Text) &&
                             decimal.TryParse(txtAmount.Text, out amount) &&
                             amount > 0;

            bool sufficientFunds = amountValid && amount <= _currentBalance;
            bool accountValid = txtClientName.Text != "Not found";

            btnWithdraw.Enabled = amountValid && sufficientFunds && accountValid;

            // Update status message
            if (amountValid && amount > _currentBalance)
            {
                lblStatus.Text = "Insufficient funds for this withdrawal";
            }
            else if (amountValid && accountValid)
            {
                lblStatus.Text = "Amount valid. Click Withdraw to proceed";
            }
            else if (!amountValid && !string.IsNullOrEmpty(txtAmount.Text))
            {
                lblStatus.Text = "Please enter a valid amount";
            }
            else
            {
                lblStatus.Text = "Enter withdrawal amount";
            }
        }

        private void txtAccountNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            if (e.KeyChar == (char)Keys.Enter)
            {
                btnSearch.PerformClick();
                e.Handled = true;
            }
        }
    }
}
