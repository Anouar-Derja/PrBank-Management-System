namespace BANK_WITH_C_
{
    partial class frmTransaction
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTransaction));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cmseTransaction = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmDeposit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmWithdraw = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmTransfer = new System.Windows.Forms.ToolStripMenuItem();
            this.dvgTransaction = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.cmseTransaction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvgTransaction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // cmseTransaction
            // 
            this.cmseTransaction.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmDeposit,
            this.tsmWithdraw,
            this.tsmTransfer});
            this.cmseTransaction.Name = "contextMenuStrip1";
            this.cmseTransaction.Size = new System.Drawing.Size(158, 166);
            this.cmseTransaction.Opening += new System.ComponentModel.CancelEventHandler(this.cmseTransaction_Opening);
            // 
            // tsmDeposit
            // 
            this.tsmDeposit.Image = ((System.Drawing.Image)(resources.GetObject("tsmDeposit.Image")));
            this.tsmDeposit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsmDeposit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmDeposit.Name = "tsmDeposit";
            this.tsmDeposit.Size = new System.Drawing.Size(157, 54);
            this.tsmDeposit.Text = "Deposit";
            this.tsmDeposit.Click += new System.EventHandler(this.tsmDeposit_Click);
            // 
            // tsmWithdraw
            // 
            this.tsmWithdraw.Image = ((System.Drawing.Image)(resources.GetObject("tsmWithdraw.Image")));
            this.tsmWithdraw.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsmWithdraw.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmWithdraw.Name = "tsmWithdraw";
            this.tsmWithdraw.Size = new System.Drawing.Size(157, 54);
            this.tsmWithdraw.Text = "Withdraw";
            this.tsmWithdraw.Click += new System.EventHandler(this.tsmWithdraw_Click);
            // 
            // tsmTransfer
            // 
            this.tsmTransfer.Image = ((System.Drawing.Image)(resources.GetObject("tsmTransfer.Image")));
            this.tsmTransfer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsmTransfer.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmTransfer.Name = "tsmTransfer";
            this.tsmTransfer.Size = new System.Drawing.Size(157, 54);
            this.tsmTransfer.Text = "Transfer";
            this.tsmTransfer.Click += new System.EventHandler(this.tsmTransfer_Click);
            // 
            // dvgTransaction
            // 
            this.dvgTransaction.AllowUserToAddRows = false;
            this.dvgTransaction.AllowUserToDeleteRows = false;
            this.dvgTransaction.AllowUserToResizeRows = false;
            this.dvgTransaction.BackgroundColor = System.Drawing.Color.White;
            this.dvgTransaction.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvgTransaction.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dvgTransaction.Location = new System.Drawing.Point(0, 192);
            this.dvgTransaction.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dvgTransaction.MultiSelect = false;
            this.dvgTransaction.Name = "dvgTransaction";
            this.dvgTransaction.ReadOnly = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dvgTransaction.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dvgTransaction.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dvgTransaction.Size = new System.Drawing.Size(1053, 269);
            this.dvgTransaction.TabIndex = 3;
            this.dvgTransaction.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.Font = new System.Drawing.Font("Impact", 24F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label4.Location = new System.Drawing.Point(479, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(174, 39);
            this.label4.TabIndex = 4;
            this.label4.Text = "Transaction";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(0, 0);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(1053, 461);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 1;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(539, 90);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(72, 72);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // frmTransaction
            // 
            this.ClientSize = new System.Drawing.Size(1053, 461);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dvgTransaction);
            this.Controls.Add(this.pictureBox3);
            this.Name = "frmTransaction";
            this.Load += new System.EventHandler(this.frmTransaction_Load_1);
            this.cmseTransaction.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dvgTransaction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ContextMenuStrip cmsTransaction;
        private System.Windows.Forms.ToolStripMenuItem depositToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem withdrawToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem transferToolStripMenuItem;
        private System.Windows.Forms.DataGridView dgvTransaction;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.ContextMenuStrip cmseTransaction;
        private System.Windows.Forms.ToolStripMenuItem tsmDeposit;
        private System.Windows.Forms.ToolStripMenuItem tsmWithdraw;
        private System.Windows.Forms.ToolStripMenuItem tsmTransfer;
        private System.Windows.Forms.DataGridView dvgTransaction;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}