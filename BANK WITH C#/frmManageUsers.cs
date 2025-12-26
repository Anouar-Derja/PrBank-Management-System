using BankingBusiness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BANK_WITH_C_
{
    public partial class frmManageUsers : Form
    {
            private DataTable _dtUsers;

            public frmManageUsers()
            {
                InitializeComponent();
                dgvUser.ContextMenuStrip = cmsManageUsers;
            }

            private void _RefreshUserList()
            {
                _dtUsers = clsUser.GetAllUsers();
                dgvUser.DataSource = _dtUsers;
            }

            private void DgvUsers_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
            {
                try
                {
                    // Check if we have enough columns
                    if (dgvUser.Columns.Count >= 11) // Updated to check for 11 columns
                    {
                        dgvUser.Columns[0].HeaderText = "UserID";
                        dgvUser.Columns[0].Width = 60;
                        dgvUser.Columns[0].Visible = false;

                        dgvUser.Columns[1].HeaderText = "Username";
                        dgvUser.Columns[1].Width = 110;

                        dgvUser.Columns[2].HeaderText = "Password";
                        dgvUser.Columns[2].Width = 120;
                        dgvUser.Columns[2].Visible = false; // Hide password for security

                        dgvUser.Columns[3].HeaderText = "Created At";
                        dgvUser.Columns[3].Width = 150;

                        dgvUser.Columns[4].HeaderText = "Is Active";
                        dgvUser.Columns[4].Width = 80;

                        dgvUser.Columns[5].HeaderText = "Can Add Client";
                        dgvUser.Columns[5].Width = 100;

                        dgvUser.Columns[6].HeaderText = "Can Manage User";
                        dgvUser.Columns[6].Width = 120;

                        dgvUser.Columns[7].HeaderText = "Can Show Client";
                        dgvUser.Columns[7].Width = 110;

                        dgvUser.Columns[8].HeaderText = "Can Update Client";
                        dgvUser.Columns[8].Width = 120;

                        dgvUser.Columns[9].HeaderText = "Can Transaction";
                        dgvUser.Columns[9].Width = 110;

                        dgvUser.Columns[10].HeaderText = "Can Find/Delete Client";
                        dgvUser.Columns[10].Width = 140;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error configuring grid columns: {ex.Message}");
                }
            }

            private void frmManageUsers_Load_1(object sender, EventArgs e)
            {
                dgvUser.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvUser.MultiSelect = false;
                dgvUser.ContextMenuStrip = cmsManageUsers;

                try
                {
                    _RefreshUserList();
                    dgvUser.DataBindingComplete += DgvUsers_DataBindingComplete;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading users: {ex.Message}");
                }
            }

            private void button2_Click_1(object sender, EventArgs e)
            {
                this.Close();
            }

            private void tmsUpdate_Click(object sender, EventArgs e)
            {
                if (dgvUser.CurrentRow != null && dgvUser.CurrentRow.Cells["UserID"].Value != null)
                {
                    int userID = Convert.ToInt32(dgvUser.CurrentRow.Cells["UserID"].Value);
                    frmUpdateUsers frmUpdateUser = new frmUpdateUsers(userID);
                    frmUpdateUser.ShowDialog();
                    _RefreshUserList();
                }
                else
                {
                    MessageBox.Show("Please select a user first by right-clicking on a row.");
                }
            }

            private void TsmDelete_Click(object sender, EventArgs e)
            {
                if (dgvUser.CurrentRow != null && dgvUser.CurrentRow.Cells["UserID"].Value != null)
                {
                    int userID = Convert.ToInt32(dgvUser.CurrentRow.Cells["UserID"].Value);
                    string username = dgvUser.CurrentRow.Cells["Username"].Value.ToString();

                    if (MessageBox.Show($"Are you sure you want to delete user '{username}'?",
                        "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        if (clsUserData.DeleteUser(userID))
                        {
                            MessageBox.Show("User deleted successfully.");
                            _RefreshUserList();
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete user.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a user first by clicking on a row.");
                }
            }

            private void btnAdd_Click(object sender, EventArgs e)
            {
                frmAddUser frmAddUser = new frmAddUser();
                if (frmAddUser.ShowDialog() == DialogResult.OK)
                {
                    _RefreshUserList();
                }
            }
        
    }
}

