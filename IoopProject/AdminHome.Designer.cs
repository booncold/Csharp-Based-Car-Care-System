namespace IoopProject
{
    partial class AdminHome
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
            this.lblIdentity = new System.Windows.Forms.Label();
            this.btnViewFbReportPage = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnStaffPage = new System.Windows.Forms.Button();
            this.btnServicePage = new System.Windows.Forms.Button();
            this.grpBoxAdminHome = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.grpBoxAdminHome.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblIdentity
            // 
            this.lblIdentity.AutoSize = true;
            this.lblIdentity.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.lblIdentity.Location = new System.Drawing.Point(444, 176);
            this.lblIdentity.Name = "lblIdentity";
            this.lblIdentity.Size = new System.Drawing.Size(39, 39);
            this.lblIdentity.TabIndex = 7;
            this.lblIdentity.Text = "--";
            this.lblIdentity.Click += new System.EventHandler(this.lblIdentity_Click);
            // 
            // btnViewFbReportPage
            // 
            this.btnViewFbReportPage.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnViewFbReportPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewFbReportPage.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnViewFbReportPage.Location = new System.Drawing.Point(240, 187);
            this.btnViewFbReportPage.Name = "btnViewFbReportPage";
            this.btnViewFbReportPage.Size = new System.Drawing.Size(407, 44);
            this.btnViewFbReportPage.TabIndex = 9;
            this.btnViewFbReportPage.Text = "View Report and Feedback";
            this.btnViewFbReportPage.UseVisualStyleBackColor = false;
            this.btnViewFbReportPage.Click += new System.EventHandler(this.btnView_Click);
            // 
            // button6
            // 
            this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button6.Location = new System.Drawing.Point(367, 558);
            this.button6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(204, 58);
            this.button6.TabIndex = 20;
            this.button6.Text = "Exit";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnStaffPage);
            this.groupBox1.Controls.Add(this.btnServicePage);
            this.groupBox1.Controls.Add(this.btnViewFbReportPage);
            this.groupBox1.Location = new System.Drawing.Point(36, 215);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(894, 281);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Page Links";
            // 
            // btnStaffPage
            // 
            this.btnStaffPage.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnStaffPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStaffPage.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnStaffPage.Location = new System.Drawing.Point(351, 107);
            this.btnStaffPage.Name = "btnStaffPage";
            this.btnStaffPage.Size = new System.Drawing.Size(169, 48);
            this.btnStaffPage.TabIndex = 14;
            this.btnStaffPage.Text = "Staff Page";
            this.btnStaffPage.UseVisualStyleBackColor = false;
            this.btnStaffPage.Click += new System.EventHandler(this.btnStaffPage_Click);
            // 
            // btnServicePage
            // 
            this.btnServicePage.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnServicePage.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnServicePage.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnServicePage.Location = new System.Drawing.Point(331, 30);
            this.btnServicePage.Name = "btnServicePage";
            this.btnServicePage.Size = new System.Drawing.Size(200, 48);
            this.btnServicePage.TabIndex = 13;
            this.btnServicePage.Text = "Service Page";
            this.btnServicePage.UseVisualStyleBackColor = false;
            this.btnServicePage.Click += new System.EventHandler(this.btnServicePage_Click);
            // 
            // grpBoxAdminHome
            // 
            this.grpBoxAdminHome.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.grpBoxAdminHome.Controls.Add(this.label1);
            this.grpBoxAdminHome.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.grpBoxAdminHome.Location = new System.Drawing.Point(24, 40);
            this.grpBoxAdminHome.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpBoxAdminHome.Name = "grpBoxAdminHome";
            this.grpBoxAdminHome.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpBoxAdminHome.Size = new System.Drawing.Size(935, 134);
            this.grpBoxAdminHome.TabIndex = 23;
            this.grpBoxAdminHome.TabStop = false;
            this.grpBoxAdminHome.Text = "Admin HomePage";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 26F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(88, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(684, 52);
            this.label1.TabIndex = 0;
            this.label1.Text = "Welcome to Admin\'s Homepage!";
            // 
            // AdminHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(1004, 646);
            this.Controls.Add(this.grpBoxAdminHome);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.lblIdentity);
            this.Name = "AdminHome";
            this.Text = "AdminHome";
            this.Load += new System.EventHandler(this.AdminHome_Load);
            this.groupBox1.ResumeLayout(false);
            this.grpBoxAdminHome.ResumeLayout(false);
            this.grpBoxAdminHome.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblIdentity;
        private System.Windows.Forms.Button btnViewFbReportPage;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox grpBoxAdminHome;
        private System.Windows.Forms.Button btnServicePage;
        private System.Windows.Forms.Button btnStaffPage;
        private System.Windows.Forms.Label label1;
    }
}