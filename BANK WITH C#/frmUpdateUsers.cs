using Banking_DataAccess;
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
    public partial class frmUpdateUsers : Form
    {

        private int _userID;

        public frmUpdateUsers(int userID)
        {
            InitializeComponent();
            _userID = userID;
           
        }

        private void frmUpdateUsers_Load(object sender, EventArgs e)
        {
            // Load user data if editing existing user
            LoadUserData(_userID);
        }
        private void LoadUserData(int userID)
        {
            clsUser user = clsUserData.GetUserByID(userID);
            if (user != null)
            {
                txtUsername.Text = user.Username;
                // Don't display the actual password - it's hashed anyway
                // If you want to allow password changes, use an empty textbox
                txtPassword.Text = "";
               

                chkAddClient.Checked = user.CanAddClient;
                chkManageUser.Checked = user.CanManageUser;
                chkShowClient.Checked = user.CanShowClient;
                chkUpdateClient.Checked = user.CanUpdateClient;
                chkTransaction.Checked = user.CanTransaction;
                chkFindDeleteClient.Checked = user.CanFindDeleteClient;

                this.Text = $"Update User - {user.Username}";
            }
            else
            {
                MessageBox.Show("User not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

   
        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
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

            // Validate username contains only letters
            if (!txtUsername.Text.All(char.IsLetter))
            {
                MessageBox.Show("Username must contain only letters", "Validation Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return false;
            }

            // Validate username length
            if (txtUsername.Text.Length < 3)
            {
                MessageBox.Show("Username must be at least 3 letters long", "Validation Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return false;
            }

            // Validate password contains only numbers
            if (!txtPassword.Text.All(char.IsDigit))
            {
                MessageBox.Show("Password must contain only numbers", "Validation Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return false;
            }

            // Validate password length
            if (txtPassword.Text.Length < 4)
            {
                MessageBox.Show("Password must be at least 4 digits long", "Validation Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return false;
            }

            // Check if username already exists (excluding current user)
            if (clsUserData.IsUsernameExists(txtUsername.Text, _userID))
            {
                MessageBox.Show("Username already exists. Please choose a different username.",
                               "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return false;
            }

            // Validate that at least one permission is selected
            if (!chkAddClient.Checked && !chkManageUser.Checked && !chkShowClient.Checked &&
                !chkUpdateClient.Checked && !chkTransaction.Checked && !chkFindDeleteClient.Checked)
            {
                MessageBox.Show("Please select at least one permission for the user",
                               "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                chkShowClient.Focus();
                return false;
            }

            return true;
        }
        private void ClearForm()
        {
                txtUsername.Clear();
                txtPassword.Clear();
                chkAddClient.Checked = false;
                chkManageUser.Checked = false;
                chkShowClient.Checked = false;
                chkUpdateClient.Checked = false;
                chkTransaction.Checked = false;
                chkFindDeleteClient.Checked = false;
                lblCurrentUser.Text = "No user selected";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void btneUpdate_Click(object sender, EventArgs e)
        {
            if (!ValidateUserInput())
                return;

            // Update user in database
            bool success = clsUserData.UpdateUser(
                _userID,
                txtUsername.Text.Trim(),
                txtPassword.Text,
                chkAddClient.Checked,
                chkManageUser.Checked,
                chkShowClient.Checked,
                chkUpdateClient.Checked,
                chkTransaction.Checked,
                chkFindDeleteClient.Checked
            );

            if (success)
            {
                MessageBox.Show("User updated successfully!", "Success",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

      
        
    }
}


