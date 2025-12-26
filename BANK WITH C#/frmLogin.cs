using BankingBusiness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BANK_WITH_C_
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
  
            private string settingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BankApp", "user.config");

           
            private void btnLogin_Click(object sender, EventArgs e)
            {
                string username = txtUsername.Text.Trim();
                string password = txtPassword.Text;

                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("Please enter both username and password.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Authenticate user
                clsUser user = clsUser.Authenticate(username, password);

                if (user != null)
                {
                    // Store the current user
                    clsCurrentUser.User = user;

                    if (chkRememberMe.Checked)
                    {
                        // Save username to file
                        SaveUsername(username);
                    }
                    else
                    {
                        // Clear saved username
                        SaveUsername("");
                    }

                    // Open main menu
                    frmMainMenu mainMenu = new frmMainMenu();
                    mainMenu.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid username or password.", "Login Failed",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            private void frmLogin_Load(object sender, EventArgs e)
            {
                // Load saved username if it exists
                string savedUsername = LoadUsername();
                if (!string.IsNullOrEmpty(savedUsername))
                {
                    txtUsername.Text = savedUsername;
                    chkRememberMe.Checked = true;
                }
            }

            private void SaveUsername(string username)
            {
                try
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(settingsPath));
                    File.WriteAllText(settingsPath, username);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error saving username: " + ex.Message);
                }
            }

            private string LoadUsername()
            {
                try
                {
                    if (File.Exists(settingsPath))
                    {
                        return File.ReadAllText(settingsPath);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error loading username: " + ex.Message);
                }
                return "";
            }

            private void btnCancel_Click(object sender, EventArgs e)
            {
                Application.Exit();
            }

            private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
            {
                // Allow login with Enter key in password field
                if (e.KeyChar == (char)Keys.Enter)
                {
                    btnLogin.PerformClick();
                    e.Handled = true;
                }
            }
        
    }
}
