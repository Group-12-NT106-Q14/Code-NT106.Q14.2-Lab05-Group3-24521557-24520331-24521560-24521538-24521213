namespace Code_NT106.Q14._2_Lab05_Group3_24521557_24520331_24521560_24521538_24521213
{
    partial class Bai02
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
            txtUserName = new TextBox();
            txtPassWord = new TextBox();
            lstEmail = new ListView();
            clEmail = new ColumnHeader();
            clForm = new ColumnHeader();
            clTime = new ColumnHeader();
            lbEmail = new Label();
            lbPassword = new Label();
            lbTotal = new Label();
            lbRecent = new Label();
            btnLogin = new Button();
            txtToTal = new TextBox();
            txtRecent = new TextBox();
            SuspendLayout();
            // 
            // txtUserName
            // 
            txtUserName.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtUserName.Location = new Point(105, 25);
            txtUserName.Name = "txtUserName";
            txtUserName.Size = new Size(476, 27);
            txtUserName.TabIndex = 0;
            // 
            // txtPassWord
            // 
            txtPassWord.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPassWord.Location = new Point(105, 72);
            txtPassWord.Name = "txtPassWord";
            txtPassWord.Size = new Size(476, 27);
            txtPassWord.TabIndex = 1;
            // 
            // lstEmail
            // 
            lstEmail.Columns.AddRange(new ColumnHeader[] { clEmail, clForm, clTime });
            lstEmail.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lstEmail.FullRowSelect = true;
            lstEmail.GridLines = true;
            lstEmail.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            lstEmail.Location = new Point(89, 168);
            lstEmail.MultiSelect = false;
            lstEmail.Name = "lstEmail";
            lstEmail.Size = new Size(669, 270);
            lstEmail.TabIndex = 2;
            lstEmail.UseCompatibleStateImageBehavior = false;
            lstEmail.View = View.Details;
            lstEmail.SelectedIndexChanged += lstEmail_SelectedIndexChanged;
            // 
            // clEmail
            // 
            clEmail.Text = "Email";
            clEmail.Width = 240;
            // 
            // clForm
            // 
            clForm.Text = "From";
            clForm.Width = 240;
            // 
            // clTime
            // 
            clTime.Text = "Time";
            clTime.Width = 240;
            // 
            // lbEmail
            // 
            lbEmail.AutoSize = true;
            lbEmail.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbEmail.Location = new Point(19, 27);
            lbEmail.Name = "lbEmail";
            lbEmail.Size = new Size(54, 25);
            lbEmail.TabIndex = 3;
            lbEmail.Text = "Email";
            // 
            // lbPassword
            // 
            lbPassword.AutoSize = true;
            lbPassword.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbPassword.Location = new Point(12, 72);
            lbPassword.Name = "lbPassword";
            lbPassword.Size = new Size(87, 25);
            lbPassword.TabIndex = 4;
            lbPassword.Text = "Password";
            // 
            // lbTotal
            // 
            lbTotal.AutoSize = true;
            lbTotal.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbTotal.Location = new Point(19, 127);
            lbTotal.Name = "lbTotal";
            lbTotal.Size = new Size(49, 25);
            lbTotal.TabIndex = 5;
            lbTotal.Text = "Total";
            // 
            // lbRecent
            // 
            lbRecent.AutoSize = true;
            lbRecent.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbRecent.Location = new Point(236, 127);
            lbRecent.Name = "lbRecent";
            lbRecent.Size = new Size(67, 25);
            lbRecent.TabIndex = 6;
            lbRecent.Text = "Recent";
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(643, 40);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(114, 43);
            btnLogin.TabIndex = 7;
            btnLogin.Text = "LOGIN";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // txtToTal
            // 
            txtToTal.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtToTal.Location = new Point(89, 124);
            txtToTal.Name = "txtToTal";
            txtToTal.ReadOnly = true;
            txtToTal.Size = new Size(78, 27);
            txtToTal.TabIndex = 8;
            // 
            // txtRecent
            // 
            txtRecent.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtRecent.Location = new Point(336, 124);
            txtRecent.Name = "txtRecent";
            txtRecent.ReadOnly = true;
            txtRecent.Size = new Size(78, 27);
            txtRecent.TabIndex = 9;
            // 
            // Bai02
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(txtRecent);
            Controls.Add(txtToTal);
            Controls.Add(btnLogin);
            Controls.Add(lbRecent);
            Controls.Add(lbTotal);
            Controls.Add(lbPassword);
            Controls.Add(lbEmail);
            Controls.Add(lstEmail);
            Controls.Add(txtPassWord);
            Controls.Add(txtUserName);
            Name = "Bai02";
            Text = "Bai02";
            Load += Bai02_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtUserName;
        private TextBox txtPassWord;
        private ListView lstEmail;
        private Label lbEmail;
        private Label lbPassword;
        private Label lbTotal;
        private Label lbRecent;
        private Button btnLogin;
        private TextBox txtToTal;
        private TextBox txtRecent;
        private ColumnHeader clEmail;
        private ColumnHeader clForm;
        private ColumnHeader clTime;
    }
}