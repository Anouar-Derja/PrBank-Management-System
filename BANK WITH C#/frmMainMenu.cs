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
    public partial class frmMainMenu : Form
    {
            public frmMainMenu()
            {
                InitializeComponent();

                // Set up UI based on user permissions
                SetupMenuBasedOnPermissions();
            }

            private void SetupMenuBasedOnPermissions()
            {
                // Enable/disable buttons based on user permissions
                btnAdd.Enabled = clsCurrentUser.HasPermission("CanAddClient");
                btnUpdate.Enabled = clsCurrentUser.HasPermission("CanUpdateClient");
                btnShowClient.Enabled = clsCurrentUser.HasPermission("CanShowClient");
                btnFind.Enabled = clsCurrentUser.HasPermission("CanFindDeleteClient");
                btnTrans.Enabled = clsCurrentUser.HasPermission("CanTransaction");
                btnManage.Enabled = clsCurrentUser.HasPermission("CanManageUser");

                // Update button text to show status if needed
                if (!btnAdd.Enabled) btnAdd.Text += " (No Permission)";
                if (!btnUpdate.Enabled) btnUpdate.Text += " (No Permission)";
                if (!btnShowClient.Enabled) btnShowClient.Text += " (No Permission)";
                if (!btnFind.Enabled) btnFind.Text += " (No Permission)";
                if (!btnTrans.Enabled) btnTrans.Text += " (No Permission)";
                if (!btnManage.Enabled) btnManage.Text += " (No Permission)";

                // Display current user info
                lblCurrentUser.Text = $"Logged in as: {clsCurrentUser.User.Username}";
            }

            private void btnShowClient_Click(object sender, EventArgs e)
            {
                if (clsCurrentUser.HasPermission("CanShowClient"))
                {
                    frmShowClients frmShowClient = new frmShowClients();
                    frmShowClient.ShowDialog();
                }
                else
                {
                    MessageBox.Show("You don't have permission to show clients.", "Access Denied",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            private void btnFind_Click(object sender, EventArgs e)
            {
                if (clsCurrentUser.HasPermission("CanFindDeleteClient"))
                {
                    frmFindAndDeleteClient frmFindClient = new frmFindAndDeleteClient();
                    frmFindClient.ShowDialog();
                }
                else
                {
                    MessageBox.Show("You don't have permission to find/delete clients.", "Access Denied",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            private void btnAdd_Click(object sender, EventArgs e)
            {
                if (clsCurrentUser.HasPermission("CanAddClient"))
                {
                    frmAddUpdateClients frmAddClients = new frmAddUpdateClients();
                    frmAddClients.ShowDialog();
                }
                else
                {
                    MessageBox.Show("You don't have permission to add clients.", "Access Denied",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            private void btnUpdate_Click(object sender, EventArgs e)
            {
                if (clsCurrentUser.HasPermission("CanUpdateClient"))
                {
                    frmUpdateClients frmUpdateClients = new frmUpdateClients();
                    frmUpdateClients.ShowDialog();
                }
                else
                {
                    MessageBox.Show("You don't have permission to update clients.", "Access Denied",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            private void btnManage_Click(object sender, EventArgs e)
            {
                if (clsCurrentUser.HasPermission("CanManageUser"))
                {
                    frmManageUsers frmManageUsers = new frmManageUsers();
                    frmManageUsers.ShowDialog();
                }
                else
                {
                    MessageBox.Show("You don't have permission to manage users.", "Access Denied",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            private void btnTrans_Click(object sender, EventArgs e)
            {
                if (clsCurrentUser.HasPermission("CanTransaction"))
                {
                    frmTransaction frmTransaction = new frmTransaction();
                    frmTransaction.ShowDialog();
                }
                else
                {
                    MessageBox.Show("You don't have permission to perform transactions.", "Access Denied",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            private void btnLogout_Click(object sender, EventArgs e)
            {
                // Logout
                clsCurrentUser.Logout();

                // Show login form
                frmLogin loginForm = new frmLogin();
                loginForm.Show();
                this.Close();
            }

        private void frmMainMenu_Load(object sender, EventArgs e)
        {

        }
    }
}

