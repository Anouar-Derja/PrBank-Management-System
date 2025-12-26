namespace BANK_WITH_C_
{
    partial class frmTransfer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTransfer));
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblMyAccount = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblAmount = new System.Windows.Forms.Label();
            this.txtSearchAccount = new System.Windows.Forms.TextBox();
            this.txtMyAccount = new System.Windows.Forms.TextBox();
            this.txtMyBalance = new System.Windows.Forms.TextBox();
            this.btnTransfer = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblClientAccount = new System.Windows.Forms.Label();
            this.lblClientBalance = new System.Windows.Forms.Label();
            this.txtClientBalance = new System.Windows.Forms.TextBox();
            this.txtClientName = new System.Windows.Forms.TextBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.Desc = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblClientName = new System.Windows.Forms.Label();
            this.lblMyBalance = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.lblMyName = new System.Windows.Forms.Label();
            this.txtMyName = new System.Windows.Forms.TextBox();
            this.txtClientAccount = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblStatus.Location = new System.Drawing.Point(35, 90);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(373, 20);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "Please,Enter an Account you want transfer to\r\n";
            // 
            // lblMyAccount
            // 
            this.lblMyAccount.AutoSize = true;
            this.lblMyAccount.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblMyAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMyAccount.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblMyAccount.Location = new System.Drawing.Point(38, 210);
            this.lblMyAccount.Name = "lblMyAccount";
            this.lblMyAccount.Size = new System.Drawing.Size(97, 20);
            this.lblMyAccount.TabIndex = 5;
            this.lblMyAccount.Text = "MyAccount";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.SystemColors.ControlText;
            this.label10.Font = new System.Drawing.Font("Impact", 26.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label10.Location = new System.Drawing.Point(360, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(141, 43);
            this.label10.TabIndex = 6;
            this.label10.Text = "Transfer";
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmount.ForeColor = System.Drawing.Color.White;
            this.lblAmount.Location = new System.Drawing.Point(36, 349);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(199, 20);
            this.lblAmount.TabIndex = 7;
            this.lblAmount.Text = "Enter Withdraw Amount";
            // 
            // txtSearchAccount
            // 
            this.txtSearchAccount.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtSearchAccount.ForeColor = System.Drawing.Color.Black;
            this.txtSearchAccount.Location = new System.Drawing.Point(445, 84);
            this.txtSearchAccount.Name = "txtSearchAccount";
            this.txtSearchAccount.Size = new System.Drawing.Size(196, 26);
            this.txtSearchAccount.TabIndex = 8;
            // 
            // txtMyAccount
            // 
            this.txtMyAccount.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtMyAccount.ForeColor = System.Drawing.Color.Black;
            this.txtMyAccount.Location = new System.Drawing.Point(156, 207);
            this.txtMyAccount.Name = "txtMyAccount";
            this.txtMyAccount.Size = new System.Drawing.Size(271, 26);
            this.txtMyAccount.TabIndex = 9;
            // 
            // txtMyBalance
            // 
            this.txtMyBalance.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtMyBalance.ForeColor = System.Drawing.Color.Black;
            this.txtMyBalance.Location = new System.Drawing.Point(156, 250);
            this.txtMyBalance.Name = "txtMyBalance";
            this.txtMyBalance.Size = new System.Drawing.Size(271, 26);
            this.txtMyBalance.TabIndex = 10;
            this.txtMyBalance.TextChanged += new System.EventHandler(this.txtAmount_TextChanged);
            this.txtMyBalance.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAmount_KeyPress);
            // 
            // btnTransfer
            // 
            this.btnTransfer.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnTransfer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTransfer.Font = new System.Drawing.Font("Franklin Gothic Medium", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTransfer.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnTransfer.Location = new System.Drawing.Point(281, 401);
            this.btnTransfer.Name = "btnTransfer";
            this.btnTransfer.Size = new System.Drawing.Size(99, 38);
            this.btnTransfer.TabIndex = 11;
            this.btnTransfer.Text = "Transfer";
            this.btnTransfer.UseVisualStyleBackColor = false;
            this.btnTransfer.Click += new System.EventHandler(this.btntransfer_Click_1);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Franklin Gothic Medium", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnSearch.Location = new System.Drawing.Point(662, 79);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(99, 38);
            this.btnSearch.TabIndex = 12;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click_1);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Franklin Gothic Medium", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnCancel.Location = new System.Drawing.Point(511, 401);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(99, 38);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click_1);
            // 
            // lblClientAccount
            // 
            this.lblClientAccount.AutoSize = true;
            this.lblClientAccount.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblClientAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClientAccount.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblClientAccount.Location = new System.Drawing.Point(484, 216);
            this.lblClientAccount.Name = "lblClientAccount";
            this.lblClientAccount.Size = new System.Drawing.Size(126, 20);
            this.lblClientAccount.TabIndex = 14;
            this.lblClientAccount.Text = "Account Client";
            // 
            // lblClientBalance
            // 
            this.lblClientBalance.AutoSize = true;
            this.lblClientBalance.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblClientBalance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClientBalance.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblClientBalance.Location = new System.Drawing.Point(484, 299);
            this.lblClientBalance.Name = "lblClientBalance";
            this.lblClientBalance.Size = new System.Drawing.Size(74, 20);
            this.lblClientBalance.TabIndex = 15;
            this.lblClientBalance.Text = "Balance";
            // 
            // txtClientBalance
            // 
            this.txtClientBalance.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtClientBalance.ForeColor = System.Drawing.Color.Black;
            this.txtClientBalance.Location = new System.Drawing.Point(627, 293);
            this.txtClientBalance.Name = "txtClientBalance";
            this.txtClientBalance.Size = new System.Drawing.Size(175, 26);
            this.txtClientBalance.TabIndex = 16;
            // 
            // txtClientName
            // 
            this.txtClientName.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtClientName.ForeColor = System.Drawing.Color.Black;
            this.txtClientName.Location = new System.Drawing.Point(627, 253);
            this.txtClientName.Name = "txtClientName";
            this.txtClientName.Size = new System.Drawing.Size(175, 26);
            this.txtClientName.TabIndex = 17;
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackColor = System.Drawing.Color.Black;
            this.pictureBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
            this.pictureBox5.Location = new System.Drawing.Point(0, 0);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(835, 473);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox5.TabIndex = 3;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.Black;
            this.pictureBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(0, 0);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(835, 473);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 2;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(0, 0);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(835, 473);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 1;
            this.pictureBox3.TabStop = false;
            // 
            // Desc
            // 
            this.Desc.AutoSize = true;
            this.Desc.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Desc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Desc.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.Desc.Location = new System.Drawing.Point(32, 299);
            this.Desc.Name = "Desc";
            this.Desc.Size = new System.Drawing.Size(100, 20);
            this.Desc.TabIndex = 18;
            this.Desc.Text = "Description";
            // 
            // txtDescription
            // 
            this.txtDescription.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtDescription.ForeColor = System.Drawing.Color.Black;
            this.txtDescription.Location = new System.Drawing.Point(156, 293);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(271, 26);
            this.txtDescription.TabIndex = 19;
            // 
            // lblClientName
            // 
            this.lblClientName.AutoSize = true;
            this.lblClientName.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblClientName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClientName.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblClientName.Location = new System.Drawing.Point(484, 256);
            this.lblClientName.Name = "lblClientName";
            this.lblClientName.Size = new System.Drawing.Size(101, 20);
            this.lblClientName.TabIndex = 20;
            this.lblClientName.Text = "ClientName";
            // 
            // lblMyBalance
            // 
            this.lblMyBalance.AutoSize = true;
            this.lblMyBalance.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblMyBalance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMyBalance.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblMyBalance.Location = new System.Drawing.Point(36, 253);
            this.lblMyBalance.Name = "lblMyBalance";
            this.lblMyBalance.Size = new System.Drawing.Size(96, 20);
            this.lblMyBalance.TabIndex = 21;
            this.lblMyBalance.Text = "MyBalance";
            // 
            // txtAmount
            // 
            this.txtAmount.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtAmount.ForeColor = System.Drawing.Color.Black;
            this.txtAmount.Location = new System.Drawing.Point(304, 349);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(271, 26);
            this.txtAmount.TabIndex = 22;
            this.txtAmount.TextChanged += new System.EventHandler(this.txtAmount_TextChanged);
            this.txtAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAmount_KeyPress);
            // 
            // lblMyName
            // 
            this.lblMyName.AutoSize = true;
            this.lblMyName.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblMyName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMyName.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblMyName.Location = new System.Drawing.Point(39, 167);
            this.lblMyName.Name = "lblMyName";
            this.lblMyName.Size = new System.Drawing.Size(77, 20);
            this.lblMyName.TabIndex = 23;
            this.lblMyName.Text = "MyName";
            // 
            // txtMyName
            // 
            this.txtMyName.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtMyName.ForeColor = System.Drawing.Color.Black;
            this.txtMyName.Location = new System.Drawing.Point(156, 164);
            this.txtMyName.Name = "txtMyName";
            this.txtMyName.Size = new System.Drawing.Size(271, 26);
            this.txtMyName.TabIndex = 24;
            // 
            // txtClientAccount
            // 
            this.txtClientAccount.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtClientAccount.ForeColor = System.Drawing.Color.Black;
            this.txtClientAccount.Location = new System.Drawing.Point(627, 213);
            this.txtClientAccount.Name = "txtClientAccount";
            this.txtClientAccount.Size = new System.Drawing.Size(175, 26);
            this.txtClientAccount.TabIndex = 25;
            // 
            // frmTransfer
            // 
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(835, 473);
            this.Controls.Add(this.txtClientAccount);
            this.Controls.Add(this.txtMyName);
            this.Controls.Add(this.lblMyName);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.lblMyBalance);
            this.Controls.Add(this.lblClientName);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.Desc);
            this.Controls.Add(this.txtClientName);
            this.Controls.Add(this.txtClientBalance);
            this.Controls.Add(this.lblClientBalance);
            this.Controls.Add(this.lblClientAccount);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnTransfer);
            this.Controls.Add(this.txtMyBalance);
            this.Controls.Add(this.txtMyAccount);
            this.Controls.Add(this.txtSearchAccount);
            this.Controls.Add(this.lblAmount);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lblMyAccount);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox3);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmTransfer";
            this.Text = "frmTransfer";
            this.Load += new System.EventHandler(this.frmTransfer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblMyAccount;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.TextBox txtSearchAccount;
        private System.Windows.Forms.TextBox txtMyAccount;
        private System.Windows.Forms.TextBox txtMyBalance;
        private System.Windows.Forms.Button btnTransfer;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblClientAccount;
        private System.Windows.Forms.Label lblClientBalance;
        private System.Windows.Forms.TextBox txtClientBalance;
        private System.Windows.Forms.TextBox txtClientName;
        private System.Windows.Forms.Label Desc;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblClientName;
        private System.Windows.Forms.Label lblMyBalance;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label lblMyName;
        private System.Windows.Forms.TextBox txtMyName;
        private System.Windows.Forms.TextBox txtClientAccount;
    }
}