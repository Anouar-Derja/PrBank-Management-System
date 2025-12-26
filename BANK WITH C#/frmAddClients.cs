using BANK_WITH_C_.Properties;
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
    public partial class frmAddUpdateClients : Form
    {

        public frmAddUpdateClients()
        {
            InitializeComponent();
        }

        private bool ValidateInput()
        {
            // Check required fields
            if (string.IsNullOrEmpty(txteFirstName.Text))
            {
                MessageBox.Show("Please enter First Name", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFirstName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txteLastName.Text))
            {
                MessageBox.Show("Please enter Last Name", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLastName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtePhone.Text))
            {
                MessageBox.Show("Please enter Phone Number", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtPincode.Text) || txtPincode.Text.Length != 4)
            {
                MessageBox.Show("Please enter a 4-digit PIN code", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPincode.Focus();
                return false;
            }

            // Validate account balance
            if (!decimal.TryParse(txtAccountBalance.Text, out decimal balance) || balance < 0)
            {
                MessageBox.Show("Please enter a valid account balance", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAccountBalance.Focus();
                return false;
            }

            // Validate age (at least 18 years old)
            int age = DateTime.Now.Year - dtpeDateOfBirth.Value.Year;
            if (DateTime.Now.DayOfYear < dtpeDateOfBirth.Value.DayOfYear)
                age--;

            if (age < 18)
            {
                MessageBox.Show("Client must be at least 18 years old", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpeDateOfBirth.Focus();
                return false;
            }

            return true;
        }

        private string GenerateAccountNumber()
        {
            // Generate a unique account number (e.g., CHK + random digits)
            Random random = new Random();
            string prefix = "CHK";
            string numbers = random.Next(100000, 999999).ToString();
            return prefix + numbers;
        }


        private void ClearForm()
        {
            txteFirstName.Clear();
            txteLastName.Clear();
            txtePhone.Clear();
            txtAccountNumber.Clear();
            txtAccountBalance.Text = "0.00";
            txtPincode.Clear();
            dtpeDateOfBirth.Value = DateTime.Now.AddYears(-18);
            lblStatus.Text = "Form cleared";
            txteFirstName.Focus();
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

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only numbers, spaces, and hyphens
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                e.KeyChar != ' ' && e.KeyChar != '-')
            {
                e.Handled = true;
            }
        }



        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Validate input
                if (!ValidateInput())
                {
                    return;
                }

                // Generate a unique account number if not provided
                string accountNumber = string.IsNullOrEmpty(txtAccountNumber.Text) ?
                    GenerateAccountNumber() : txtAccountNumber.Text;

                // Create new customer
                int newCustomerID = clsCustomerData.AddNewCustomer(
                    AccountNumber: txtAccountNumber.Text,
                    PinCode: txtPincode.Text,
                    FirstName: txteFirstName.Text,
                    LastName: txteLastName.Text,
                    Phone: txtePhone.Text,
                    DateOfBirth: dtpeDateOfBirth.Value,
                    AccountBalance: decimal.Parse(txtAccountBalance.Text),
                    AccountStatus: "Active",
                    CreatedAt: DateTime.Now,
                    UserID: 1 // You might want to get this from current user session
                );

                if (newCustomerID != -1)
                {
                    MessageBox.Show($"Client added successfully!\nClient ID: {newCustomerID}\nAccount Number: {accountNumber}",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearForm();
                    lblStatus.Text = "Client added successfully!";
                }
                else
                {
                    MessageBox.Show("Failed to add client. Please try again.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding client: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnclear_Click_1(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void frmAddUpdateClients_Load(object sender, EventArgs e)
        {
            dtpeDateOfBirth.Value = DateTime.Now.AddYears(-18); // Default to 18 years old
            txtAccountBalance.Text = "0.00";
            lblStatus.Text = "Ready to add new client";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGenerateAccountNumber_Click_1(object sender, EventArgs e)
        {
            txtAccountNumber.Text = GenerateAccountNumber();

        }


    }
}

