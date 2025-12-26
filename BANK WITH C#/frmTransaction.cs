using Banking_DataAccess;
using BankingBusiness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BANK_WITH_C_
{
    public partial class frmTransaction : Form
    {
        private DataTable _dtTransaction = BankingBusiness.clsBankTransactionData.GetAllBankTransaction();


        private void _RefreshTransactionList()
        {
            // FIXED: Use clsTransactionData instead of clsBankTransaction
            _dtTransaction = clsTransactionData.GetAllBankTransactions();

            if (_dtTransaction != null &&
                _dtTransaction.Columns.Contains("TransactionID") &&
                _dtTransaction.Columns.Contains("TransactionTypeID") &&
                _dtTransaction.Columns.Contains("Amount") &&
                _dtTransaction.Columns.Contains("BalanceAfter") &&
                _dtTransaction.Columns.Contains("TransactionDate") &&
                _dtTransaction.Columns.Contains("Description") &&
                _dtTransaction.Columns.Contains("FromAccount") &&
                _dtTransaction.Columns.Contains("ToAccount"))
            {
                dvgTransaction.DataSource = _dtTransaction;
            }
            else
            {
                MessageBox.Show("Database query is missing required columns or returned no data", "Error");
            }


        }

        public frmTransaction()
        {
            InitializeComponent();
            dvgTransaction.ContextMenuStrip = cmseTransaction;
        }




        private void DgvTransaction_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                if (dvgTransaction.Columns.Count < 10) // Updated to 10 columns
                {
                    MessageBox.Show($"Expected at least 10 columns, but found {dvgTransaction.Columns.Count}");
                    return;
                }

                // Set proper column names
                dvgTransaction.Columns["TransactionID"].HeaderText = "Transaction ID";
                dvgTransaction.Columns["TransactionID"].Width = 80;

                dvgTransaction.Columns["TypeName"].HeaderText = "Type";
                dvgTransaction.Columns["TypeName"].Width = 80;

                dvgTransaction.Columns["Amount"].HeaderText = "Amount";
                dvgTransaction.Columns["Amount"].Width = 80;
                dvgTransaction.Columns["Amount"].DefaultCellStyle.Format = "C2";

                dvgTransaction.Columns["BalanceAfter"].HeaderText = "Balance";
                dvgTransaction.Columns["BalanceAfter"].Width = 80;
                dvgTransaction.Columns["BalanceAfter"].DefaultCellStyle.Format = "C2";

                dvgTransaction.Columns["TransactionDate"].HeaderText = "Date";
                dvgTransaction.Columns["TransactionDate"].Width = 120;
                dvgTransaction.Columns["TransactionDate"].DefaultCellStyle.Format = "g";

                dvgTransaction.Columns["Description"].HeaderText = "Description";
                dvgTransaction.Columns["Description"].Width = 150;

                dvgTransaction.Columns["FromAccount"].HeaderText = "From Account";
                dvgTransaction.Columns["FromAccount"].Width = 100;

                dvgTransaction.Columns["ToAccount"].HeaderText = "To Account";
                dvgTransaction.Columns["ToAccount"].Width = 100;

                dvgTransaction.Columns["UserID"].HeaderText = "User ID";
                dvgTransaction.Columns["UserID"].Width = 60;


                // Remove the event handler to prevent multiple subscriptions
                dvgTransaction.DataBindingComplete -= DgvTransaction_DataBindingComplete;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error configuring grid columns: {ex.Message}");
            }
        }


        private void frmTransaction_Load_1(object sender, EventArgs e)
        {


            dvgTransaction.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dvgTransaction.MultiSelect = false;
            dvgTransaction.ContextMenuStrip = cmseTransaction;

            try
            {
                // This is working - don't change it!
                _dtTransaction = Banking_DataAccess.clsBankTransaction.GetAllBankTransaction();
                dvgTransaction.DataSource = _dtTransaction;

                dvgTransaction.DataBindingComplete += DgvTransaction_DataBindingComplete;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading transactions: {ex.Message}");
            }
        }

        private void cmseTransaction_Opening(object sender, CancelEventArgs e)
        {

        }

        private void tsmDeposit_Click(object sender, EventArgs e)
        {
            try
            {
                string accountNumber = "";

                if (dvgTransaction.CurrentRow != null)
                {
                    // Try to get account number from selected transaction
                    DataGridViewRow row = dvgTransaction.CurrentRow;
                    accountNumber = row.Cells["ToAccount"]?.Value?.ToString() ??
                                   row.Cells["FromAccount"]?.Value?.ToString() ?? "";
                }

                FrmDeposit depositForm = string.IsNullOrEmpty(accountNumber) ?
                    new FrmDeposit() : new    FrmDeposit(accountNumber);

                depositForm.ShowDialog();
                _RefreshTransactionList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening deposit form: " + ex.Message, "Error");
            }
        }

        private void tsmWithdraw_Click(object sender, EventArgs e)
        {
            try
            {
                string accountNumber = "";

                if (dvgTransaction.CurrentRow != null)
                {
                    // Try to get account number from selected transaction
                    DataGridViewRow row = dvgTransaction.CurrentRow;
                    accountNumber = row.Cells["FromAccount"]?.Value?.ToString() ??
                                   row.Cells["ToAccount"]?.Value?.ToString() ?? "";
                }

                frmWithdraw withdrawForm = string.IsNullOrEmpty(accountNumber) ?
                    new frmWithdraw() : new frmWithdraw(accountNumber);

                withdrawForm.ShowDialog();
                _RefreshTransactionList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening withdrawal form: " + ex.Message, "Error");
            }
        }

        private void tsmTransfer_Click(object sender, EventArgs e)
        {
            try
            {
                if (dvgTransaction.CurrentRow != null)
                {
                    DataGridViewRow row = dvgTransaction.CurrentRow;

                    // Get the account number from the selected transaction
                    string fromAccount = row.Cells["FromAccount"]?.Value?.ToString() ?? "";

                    if (string.IsNullOrEmpty(fromAccount))
                    {
                        MessageBox.Show("Selected transaction does not have a valid From Account", "Error");
                        return;
                    }

                    // Open the transfer form with just the fromAccount
                    frmTransfer transferForm = new frmTransfer();
                    transferForm.SetTransferData(fromAccount, "", 0, "");
                    transferForm.ShowDialog();

                    // Refresh the transaction list after transfer
                    _RefreshTransactionList();
                }
                else
                {
                    MessageBox.Show("Please select a transaction first by right-clicking on a row.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening transfer form: " + ex.Message, "Error");
            }
        }

    }
}
