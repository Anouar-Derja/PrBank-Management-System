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
    public partial class frmAddUser : Form
    {
        public frmAddUser()
        {
            InitializeComponent();
        }

        private void frmAddUser_Load(object sender, EventArgs e)
        {
            // Set default permissions if needed
            chkShowClient.Checked = true; // Most users should at least be able to view clients
        }
        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {

            // Allow only letters and control characters (backspace, delete, etc.)
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }

            // Limit to 20 characters
            if (txtUsername.Text.Length >= 20 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            // Limit to 50 characters
            if (txtPassword.Text.Length >= 50 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private bool ValidateUserInput()
        {
            // Check required fields
            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                MessageBox.Show("Please enter Username", "Validation Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Please enter Password", "Validation Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return false;
            }

            // Validate username length
            if (txtUsername.Text.Length < 3)
            {
                MessageBox.Show("Username must be at least 3 characters long", "Validation Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return false;
            }

            // Validate password length
            if (txtPassword.Text.Length < 4)
            {
                MessageBox.Show("Password must be at least 4 characters long", "Validation Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return false;
            }

            // Check if username already exists (for new users)
            if (clsUserData.IsUsernameExists(txtUsername.Text))
            {
                MessageBox.Show("Username already exists. Please choose a different username.",
                               "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return false;
            }

            // Validate that at least one permission is selected
            if (!chkAddClient.Checked && !chkManageUser.Checked && !chkShowClient.Checked &&
                !chkUpdateClient.Checked && !chktransaction.Checked && !chkFindDeleteClient.Checked)
            {
                MessageBox.Show("Please select at least one permission for the user",
                               "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                chkShowClient.Focus();
                return false;
            }

            return true;
        }

        private void btneAdd_Click(object sender, EventArgs e)
        {// Validate input - REMOVED THE DUPLICATE VALIDATION
            if (!ValidateUserInput())
                return;

            // Add user to database
            bool success = clsUserData.AddUser(
                txtUsername.Text.Trim(),
                txtPassword.Text,
                chkAddClient.Checked,
                chkManageUser.Checked,
                chkShowClient.Checked,
                chkUpdateClient.Checked,
                chktransaction.Checked,
                chkFindDeleteClient.Checked
            );

            if (success)
            {
                MessageBox.Show("User added successfully!", "Success",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();

        }
       

    }

}

