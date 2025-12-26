namespace BANK_WITH_C_
{
    partial class frmUpdateUsers
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUpdateUsers));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblCurrentUser = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btneUpdate = new System.Windows.Forms.Button();
            this.chkAddClient = new System.Windows.Forms.CheckBox();
            this.chkUpdateClient = new System.Windows.Forms.CheckBox();
            this.chkShowClient = new System.Windows.Forms.CheckBox();
            this.chkManageUser = new System.Windows.Forms.CheckBox();
            this.chkTransaction = new System.Windows.Forms.CheckBox();
            this.chkFindDeleteClient = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(800, 450);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // lblCurrentUser
            // 
            this.lblCurrentUser.AutoSize = true;
            this.lblCurrentUser.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblCurrentUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblCurrentUser.Location = new System.Drawing.Point(41, 99);
            this.lblCurrentUser.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCurrentUser.Name = "lblCurrentUser";
            this.lblCurrentUser.Size = new System.Drawing.Size(91, 20);
            this.lblCurrentUser.TabIndex = 187;
            this.lblCurrentUser.Text = "Customer ";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Impact", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnCancel.Location = new System.Drawing.Point(661, 384);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(118, 32);
            this.btnCancel.TabIndex = 197;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btneUpdate
            // 
            this.btneUpdate.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btneUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btneUpdate.Font = new System.Drawing.Font("Impact", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btneUpdate.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btneUpdate.Location = new System.Drawing.Point(516, 384);
            this.btneUpdate.Name = "btneUpdate";
            this.btneUpdate.Size = new System.Drawing.Size(118, 32);
            this.btneUpdate.TabIndex = 196;
            this.btneUpdate.Text = "Update";
            this.btneUpdate.UseVisualStyleBackColor = false;
            this.btneUpdate.Click += new System.EventHandler(this.btneUpdate_Click);
            // 
            // chkAddClient
            // 
            this.chkAddClient.AutoSize = true;
            this.chkAddClient.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.chkAddClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAddClient.ForeColor = System.Drawing.Color.White;
            this.chkAddClient.Location = new System.Drawing.Point(149, 265);
            this.chkAddClient.Name = "chkAddClient";
            this.chkAddClient.Size = new System.Drawing.Size(91, 19);
            this.chkAddClient.TabIndex = 195;
            this.chkAddClient.Text = "Add Client";
            this.chkAddClient.UseVisualStyleBackColor = false;
            // 
            // chkUpdateClient
            // 
            this.chkUpdateClient.AutoSize = true;
            this.chkUpdateClient.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.chkUpdateClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkUpdateClient.ForeColor = System.Drawing.Color.White;
            this.chkUpdateClient.Location = new System.Drawing.Point(149, 305);
            this.chkUpdateClient.Name = "chkUpdateClient";
            this.chkUpdateClient.Size = new System.Drawing.Size(113, 19);
            this.chkUpdateClient.TabIndex = 194;
            this.chkUpdateClient.Text = "Update Client";
            this.chkUpdateClient.UseVisualStyleBackColor = false;
            // 
            // chkShowClient
            // 
            this.chkShowClient.AutoSize = true;
            this.chkShowClient.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.chkShowClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShowClient.ForeColor = System.Drawing.Color.White;
            this.chkShowClient.Location = new System.Drawing.Point(402, 265);
            this.chkShowClient.Name = "chkShowClient";
            this.chkShowClient.Size = new System.Drawing.Size(102, 19);
            this.chkShowClient.TabIndex = 192;
            this.chkShowClient.Text = "Show Client";
            this.chkShowClient.UseVisualStyleBackColor = false;
            // 
            // chkManageUser
            // 
            this.chkManageUser.AutoSize = true;
            this.chkManageUser.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.chkManageUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkManageUser.ForeColor = System.Drawing.Color.White;
            this.chkManageUser.Location = new System.Drawing.Point(271, 265);
            this.chkManageUser.Name = "chkManageUser";
            this.chkManageUser.Size = new System.Drawing.Size(112, 19);
            this.chkManageUser.TabIndex = 191;
            this.chkManageUser.Text = "Manage User";
            this.chkManageUser.UseVisualStyleBackColor = false;
            // 
            // chkTransaction
            // 
            this.chkTransaction.AutoSize = true;
            this.chkTransaction.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.chkTransaction.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTransaction.ForeColor = System.Drawing.Color.White;
            this.chkTransaction.Location = new System.Drawing.Point(271, 305);
            this.chkTransaction.Name = "chkTransaction";
            this.chkTransaction.Size = new System.Drawing.Size(101, 19);
            this.chkTransaction.TabIndex = 189;
            this.chkTransaction.Text = "Transaction";
            this.chkTransaction.UseVisualStyleBackColor = false;
            // 
            // chkFindDeleteClient
            // 
            this.chkFindDeleteClient.AutoSize = true;
            this.chkFindDeleteClient.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.chkFindDeleteClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkFindDeleteClient.ForeColor = System.Drawing.Color.White;
            this.chkFindDeleteClient.Location = new System.Drawing.Point(402, 305);
            this.chkFindDeleteClient.Name = "chkFindDeleteClient";
            this.chkFindDeleteClient.Size = new System.Drawing.Size(173, 19);
            this.chkFindDeleteClient.TabIndex = 188;
            this.chkFindDeleteClient.Text = "Find and Delete  Client";
            this.chkFindDeleteClient.UseVisualStyleBackColor = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label3.Location = new System.Drawing.Point(41, 263);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 20);
            this.label3.TabIndex = 186;
            this.label3.Text = "Permission";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label8.Location = new System.Drawing.Point(41, 160);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(93, 20);
            this.label8.TabIndex = 177;
            this.label8.Text = "UserName";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(149, 212);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPassword.MaxLength = 50;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(167, 20);
            this.txtPassword.TabIndex = 183;
            this.txtPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPassword_KeyPress);
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(149, 162);
            this.txtUsername.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtUsername.MaxLength = 50;
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(167, 20);
            this.txtUsername.TabIndex = 182;
            this.txtUsername.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUsername_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label4.Location = new System.Drawing.Point(41, 212);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 20);
            this.label4.TabIndex = 175;
            this.label4.Text = "Password";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Font = new System.Drawing.Font("Impact", 26.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label2.Location = new System.Drawing.Point(311, 9);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(193, 43);
            this.label2.TabIndex = 201;
            this.label2.Text = "Update User";
            // 
            // frmUpdateUsers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblCurrentUser);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btneUpdate);
            this.Controls.Add(this.chkAddClient);
            this.Controls.Add(this.chkUpdateClient);
            this.Controls.Add(this.chkShowClient);
            this.Controls.Add(this.chkManageUser);
            this.Controls.Add(this.chkTransaction);
            this.Controls.Add(this.chkFindDeleteClient);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pictureBox1);
            this.Name = "frmUpdateUsers";
            this.Text = "frmUpdateUsers";
            this.Load += new System.EventHandler(this.frmUpdateUsers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblCurrentUser;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btneUpdate;
        private System.Windows.Forms.CheckBox chkAddClient;
        private System.Windows.Forms.CheckBox chkUpdateClient;
        private System.Windows.Forms.CheckBox chkShowClient;
        private System.Windows.Forms.CheckBox chkManageUser;
        private System.Windows.Forms.CheckBox chkTransaction;
        private System.Windows.Forms.CheckBox chkFindDeleteClient;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
    }
}