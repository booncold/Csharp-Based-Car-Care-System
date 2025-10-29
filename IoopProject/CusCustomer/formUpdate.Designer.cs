namespace IoopProject.CusCustomer
{
    partial class formUpdate
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
            this.btnBack = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.grpbox_User = new System.Windows.Forms.GroupBox();
            this.txt_Password = new System.Windows.Forms.TextBox();
            this.txt_Email = new System.Windows.Forms.TextBox();
            this.txt_Username = new System.Windows.Forms.TextBox();
            this.txtread_UserID = new System.Windows.Forms.TextBox();
            this.lbl1_Password = new System.Windows.Forms.Label();
            this.lbl1_UserID = new System.Windows.Forms.Label();
            this.lbl1_Email = new System.Windows.Forms.Label();
            this.lbl1_Username = new System.Windows.Forms.Label();
            this.grpbox_User.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(218, 254);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(117, 34);
            this.btnBack.TabIndex = 10;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(366, 254);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(117, 34);
            this.btnUpdate.TabIndex = 11;
            this.btnUpdate.Text = "Update Profile";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // grpbox_User
            // 
            this.grpbox_User.Controls.Add(this.txt_Password);
            this.grpbox_User.Controls.Add(this.txt_Email);
            this.grpbox_User.Controls.Add(this.txt_Username);
            this.grpbox_User.Controls.Add(this.txtread_UserID);
            this.grpbox_User.Controls.Add(this.lbl1_Password);
            this.grpbox_User.Controls.Add(this.lbl1_UserID);
            this.grpbox_User.Controls.Add(this.lbl1_Email);
            this.grpbox_User.Controls.Add(this.lbl1_Username);
            this.grpbox_User.Location = new System.Drawing.Point(12, 12);
            this.grpbox_User.Name = "grpbox_User";
            this.grpbox_User.Size = new System.Drawing.Size(471, 221);
            this.grpbox_User.TabIndex = 9;
            this.grpbox_User.TabStop = false;
            this.grpbox_User.Text = "User";
            // 
            // txt_Password
            // 
            this.txt_Password.Location = new System.Drawing.Point(195, 167);
            this.txt_Password.Name = "txt_Password";
            this.txt_Password.Size = new System.Drawing.Size(241, 22);
            this.txt_Password.TabIndex = 16;
            // 
            // txt_Email
            // 
            this.txt_Email.Location = new System.Drawing.Point(195, 125);
            this.txt_Email.Name = "txt_Email";
            this.txt_Email.Size = new System.Drawing.Size(241, 22);
            this.txt_Email.TabIndex = 16;
            // 
            // txt_Username
            // 
            this.txt_Username.Location = new System.Drawing.Point(195, 82);
            this.txt_Username.Name = "txt_Username";
            this.txt_Username.Size = new System.Drawing.Size(241, 22);
            this.txt_Username.TabIndex = 17;
            // 
            // txtread_UserID
            // 
            this.txtread_UserID.Location = new System.Drawing.Point(195, 39);
            this.txtread_UserID.Name = "txtread_UserID";
            this.txtread_UserID.ReadOnly = true;
            this.txtread_UserID.Size = new System.Drawing.Size(241, 22);
            this.txtread_UserID.TabIndex = 19;
            // 
            // lbl1_Password
            // 
            this.lbl1_Password.AutoSize = true;
            this.lbl1_Password.Location = new System.Drawing.Point(39, 170);
            this.lbl1_Password.Name = "lbl1_Password";
            this.lbl1_Password.Size = new System.Drawing.Size(67, 16);
            this.lbl1_Password.TabIndex = 14;
            this.lbl1_Password.Text = "Password";
            // 
            // lbl1_UserID
            // 
            this.lbl1_UserID.AutoSize = true;
            this.lbl1_UserID.Location = new System.Drawing.Point(39, 42);
            this.lbl1_UserID.Name = "lbl1_UserID";
            this.lbl1_UserID.Size = new System.Drawing.Size(52, 16);
            this.lbl1_UserID.TabIndex = 13;
            this.lbl1_UserID.Text = "User ID";
            // 
            // lbl1_Email
            // 
            this.lbl1_Email.AutoSize = true;
            this.lbl1_Email.Location = new System.Drawing.Point(39, 128);
            this.lbl1_Email.Name = "lbl1_Email";
            this.lbl1_Email.Size = new System.Drawing.Size(41, 16);
            this.lbl1_Email.TabIndex = 14;
            this.lbl1_Email.Text = "Email";
            // 
            // lbl1_Username
            // 
            this.lbl1_Username.AutoSize = true;
            this.lbl1_Username.Location = new System.Drawing.Point(39, 85);
            this.lbl1_Username.Name = "lbl1_Username";
            this.lbl1_Username.Size = new System.Drawing.Size(70, 16);
            this.lbl1_Username.TabIndex = 15;
            this.lbl1_Username.Text = "Username";
            // 
            // formUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 320);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.grpbox_User);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "formUpdate";
            this.Text = "Update | Car Care";
            this.Load += new System.EventHandler(this.formUpdate_Load);
            this.grpbox_User.ResumeLayout(false);
            this.grpbox_User.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.GroupBox grpbox_User;
        private System.Windows.Forms.TextBox txt_Email;
        private System.Windows.Forms.TextBox txt_Username;
        private System.Windows.Forms.TextBox txtread_UserID;
        private System.Windows.Forms.Label lbl1_UserID;
        private System.Windows.Forms.Label lbl1_Email;
        private System.Windows.Forms.Label lbl1_Username;
        private System.Windows.Forms.TextBox txt_Password;
        private System.Windows.Forms.Label lbl1_Password;
    }
}