using Banking_DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BANK_WITH_C_
{
 
   public partial class frmTransfer : Form
    {
        
            private int _transactionID = -1;
            private string _originalFromAccount = "";

            public frmTransfer(int transactionID) : this()
            {
                _transactionID = transactionID;
            }

            public frmTransfer()
            {
                InitializeComponent();
            }

            private void LoadTransactionData(int transactionID)
            {
                try
                {
                    DataRow transactionData = clsTransactionData.GetTransactionByID(transactionID);

                    if (transactionData != null)
                    {
                        DisplayTransactionData(transactionData);
                        this.Text = $"Transfer - Transaction #{transactionData["TransactionID"]}";
                    }
                    else
                    {
                        MessageBox.Show($"Transaction ID {transactionID} not found in database!", "Error",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading transaction: " + ex.Message, "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            private void DisplayTransactionData(DataRow transactionRow)
            {
                try
                {
                    _transactionID = Convert.ToInt32(transactionRow["TransactionID"]);

                    string fromAccount = transactionRow["FromAccount"]?.ToString() ?? "";
                    string toAccount = transactionRow["ToAccount"]?.ToString() ?? "";
                    decimal amount = transactionRow["Amount"] != DBNull.Value ?
                                    Convert.ToDecimal(transactionRow["Amount"]) : 0;
                    string description = transactionRow["Description"]?.ToString() ?? "";

                    _originalFromAccount = fromAccount;

                    if (!string.IsNullOrEmpty(fromAccount))
                    {
                        SetMyAccountInfoFromDatabase(fromAccount);
                    }

                    txtAmount.Text = amount.ToString("F2");
                    txtDescription.Text = description;
                    txtSearchAccount.Text = toAccount;

                    lblStatus.Text = "Transaction data loaded. Search for the account you want to transfer TO.";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error displaying transaction data: " + ex.Message,
                                  "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            private void SetMyAccountInfoFromDatabase(string accountNumber)
            {
                try
                {
                    DataTable accountData = clsCustomerData.FindCustomer(accountNumber, "AccountNumber");

                    if (accountData.Rows.Count > 0)
                    {
                        DataRow account = accountData.Rows[0];
                        txtMyAccount.Text = account["AccountNumber"].ToString();
                        txtMyBalance.Text = Convert.ToDecimal(account["AccountBalance"]).ToString("C2");
                        txtMyName.Text = $"{account["FirstName"]} {account["LastName"]}";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading account information: " + ex.Message, "Error");
                }
            }

            private bool ValidateTransfer()
            {
                if (string.IsNullOrEmpty(txtClientAccount.Text) || txtClientAccount.Text == "N/A")
                {
                    MessageBox.Show("Please search and select a valid account to transfer TO", "Validation",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSearchAccount.Focus();
                    return false;
                }

                // Validate amount
                if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
                {
                    MessageBox.Show("Please enter a valid amount greater than zero", "Validation",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAmount.Focus();
                    return false;
                }

                // Validate sufficient balance
                string balanceText = txtMyBalance.Text.Replace("$", "").Replace(",", "").Replace(" ", "").Trim();
                if (!decimal.TryParse(balanceText, out decimal myBalance))
                {
                    MessageBox.Show("Invalid balance format. Please contact support.", "Validation Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (amount > myBalance)
                {
                    MessageBox.Show($"Insufficient balance. Your balance: {myBalance:C2}, Transfer amount: {amount:C2}", "Validation",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAmount.Focus();
                    return false;
                }

                // Prevent transferring to own account
                if (txtMyAccount.Text == txtClientAccount.Text)
                {
                    MessageBox.Show("Cannot transfer to your own account", "Validation",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                // Check if accounts exist in database
                if (!AccountExists(txtMyAccount.Text))
                {
                    MessageBox.Show("Your account does not exist in the database!", "Validation Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (!AccountExists(txtClientAccount.Text))
                {
                    MessageBox.Show("The recipient account does not exist in the database!", "Validation Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                return true;
            }

            private bool AccountExists(string accountNumber)
            {
                try
                {
                    DataTable accountData = clsCustomerData.FindCustomer(accountNumber, "AccountNumber");
                    return accountData.Rows.Count > 0;
                }
                catch
                {
                    return false;
                }
            }

            private void UpdateMyBalance()
            {
                DataTable myAccountData = clsCustomerData.FindCustomer(txtMyAccount.Text, "AccountNumber");

                if (myAccountData.Rows.Count > 0)
                {
                    DataRow account = myAccountData.Rows[0];
                    decimal newBalance = Convert.ToDecimal(account["AccountBalance"]);
                    txtMyBalance.Text = newBalance.ToString("C2");
                }
            }

            private void ClearClientInfo()
            {
                txtClientAccount.Text = "N/A";
                txtClientBalance.Text = "$0.00";
                txtClientName.Text = "Not found";
            }

            private void UpdateTransferButtonState()
            {
                btnTransfer.Enabled = !string.IsNullOrEmpty(txtClientAccount.Text) &&
                         txtClientAccount.Text != "N/A" &&
                         !string.IsNullOrEmpty(txtAmount.Text) &&
                         decimal.TryParse(txtAmount.Text, out decimal amount) &&
                         amount > 0;
            }

            private void txtAmount_TextChanged(object sender, EventArgs e)
            {
                UpdateTransferButtonState();
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

            private void btntransfer_Click_1(object sender, EventArgs e)
            {
                if (!ValidateTransfer())
                    return;

                try
                {
                    decimal amount = decimal.Parse(txtAmount.Text);
                    string description = txtDescription.Text.Trim();
                    string toAccount = txtClientAccount.Text;
                    string fromAccount = txtMyAccount.Text;

                    // Get the current user ID
                    int currentUserID = GetCurrentUserID();

                    // Debug: Show what's being transferred
                    MessageBox.Show($"Attempting to transfer {amount:C2} from {fromAccount} to {toAccount}",
                                  "Transfer Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    bool success = clsTransactionData.TransferFunds(
                        fromAccount: fromAccount,
                        toAccount: toAccount,
                        amount: amount,
                        description: description,
                        userID: currentUserID
                    );

                    if (success)
                    {
                        MessageBox.Show("Transfer completed successfully", "Transfer",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                        UpdateMyBalance();
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        // More specific error message
                        MessageBox.Show("Transfer failed. This could be due to:\n\n" +
                                      "1. Database connection issues\n" +
                                      "2. Insufficient permissions\n" +
                                      "3. Account validation problems\n" +
                                      "4. Transaction recording errors\n\n" +
                                      "Please check the database and try again.", "Error",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error during transfer: " + ex.Message +
                                  "\n\nStack Trace: " + ex.StackTrace, "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            private int GetCurrentUserID()
            {
                try
                {
                    // TODO: Implement this based on your authentication system
                    // For now, return a valid user ID from your database
                    return 1; // This should be replaced with actual user ID logic
                }
                catch
                {
                    MessageBox.Show("Could not get current user ID. Using default ID 1.", "Warning");
                    return 1;
                }
            }

            private void frmTransfer_Load(object sender, EventArgs e)
            {
                lblStatus.Text = "Please, Enter an Account you want transfer to";

                if (_transactionID != -1)
                {
                    LoadTransactionData(_transactionID);
                }
                else if (!string.IsNullOrEmpty(_originalFromAccount))
                {
                    SetMyAccountInfoFromDatabase(_originalFromAccount);
                }
            }

            private void btnCancel_Click_1(object sender, EventArgs e)
            {
                this.Close();
            }

            private void btnSearch_Click_1(object sender, EventArgs e)
            {
                try
                {
                    string accountNumber = txtSearchAccount.Text.Trim();

                    if (string.IsNullOrEmpty(accountNumber))
                    {
                        MessageBox.Show("Please enter an account number to search", "Search",
                                      MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (accountNumber == txtMyAccount.Text)
                    {
                        MessageBox.Show("Cannot transfer to your own account. Please enter a different account number.",
                                      "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtSearchAccount.Focus();
                        return;
                    }

                    DataTable accountData = clsCustomerData.FindCustomer(accountNumber, "AccountNumber");

                    if (accountData.Rows.Count > 0)
                    {
                        DataRow account = accountData.Rows[0];
                        txtClientAccount.Text = account["AccountNumber"].ToString();
                        txtClientBalance.Text = Convert.ToDecimal(account["AccountBalance"]).ToString("C2");
                        txtClientName.Text = $"{account["FirstName"]} {account["LastName"]}";

                        lblStatus.Text = "Account found. Enter amount and click Transfer";
                        btnTransfer.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Account not found", "Search",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearClientInfo();
                        btnTransfer.Enabled = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error searching account: " + ex.Message, "Error");
                }
            }

            public void SetTransferData(string fromAccount, string toAccount, decimal amount, string description)
            {
                _originalFromAccount = fromAccount;

                if (!string.IsNullOrEmpty(fromAccount))
                {
                    SetMyAccountInfoFromDatabase(fromAccount);
                }

                // Only pre-fill if values are provided
                if (amount > 0)
                {
                    txtAmount.Text = amount.ToString("F2");
                }

                if (!string.IsNullOrEmpty(description))
                {
                    txtDescription.Text = description;
                }

                if (!string.IsNullOrEmpty(toAccount))
                {
                    txtSearchAccount.Text = toAccount;
                }

                lblStatus.Text = "Transaction data loaded. Search for the account you want to transfer TO.";
            }
        }
    }



       