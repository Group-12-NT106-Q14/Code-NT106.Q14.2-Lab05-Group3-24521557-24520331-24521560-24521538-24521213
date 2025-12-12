namespace Code_NT106.Q14._2_Lab05_Group3_24521557_24520331_24521560_24521538_24521213
{
    partial class Bai3
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            txtEmail = new TextBox();
            txtPassword = new TextBox();
            btnlogin = new Button();
            txtRecent = new TextBox();
            txtTotal = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            t = new Label();
            lvMail = new ListView();
            clEmail = new ColumnHeader();
            clForm = new ColumnHeader();
            clTime = new ColumnHeader();
            SuspendLayout();
            // 
            // txtEmail
            // 
            txtEmail.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtEmail.Location = new Point(105, 25);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(476, 27);
            txtEmail.TabIndex = 0;
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPassword.Location = new Point(105, 72);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(476, 27);
            txtPassword.TabIndex = 1;
            // 
            // btnlogin
            // 
            btnlogin.Location = new Point(643, 40);
            btnlogin.Name = "btnlogin";
            btnlogin.Size = new Size(114, 43);
            btnlogin.TabIndex = 2;
            btnlogin.Text = "LOGIN";
            btnlogin.UseVisualStyleBackColor = true;
            btnlogin.Click += btnlogin_Click;
            // 
            // txtRecent
            // 
            txtRecent.Location = new Point(336, 124);
            txtRecent.Name = "txtRecent";
            txtRecent.ReadOnly = true;
            txtRecent.Size = new Size(78, 27);
            txtRecent.TabIndex = 3;
            // 
            // txtTotal
            // 
            txtTotal.Location = new Point(89, 124);
            txtTotal.Name = "txtTotal";
            txtTotal.ReadOnly = true;
            txtTotal.Size = new Size(78, 27);
            txtTotal.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(236, 127);
            label1.Name = "label1";
            label1.Size = new Size(64, 25);
            label1.TabIndex = 5;
            label1.Text = "Recent";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(19, 127);
            label2.Name = "label2";
            label2.Size = new Size(49, 25);
            label2.TabIndex = 6;
            label2.Text = "Total";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(19, 27);
            label3.Name = "label3";
            label3.Size = new Size(54, 25);
            label3.TabIndex = 8;
            label3.Text = "Email";
            // 
            // t
            // 
            t.AutoSize = true;
            t.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            t.Location = new Point(12, 72);
            t.Name = "t";
            t.Size = new Size(87, 25);
            t.TabIndex = 9;
            t.Text = "Password";
            // 
            // lvMail
            // 
            lvMail.Columns.AddRange(new ColumnHeader[] { clEmail, clForm, clTime });
            lvMail.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lvMail.FullRowSelect = true;
            lvMail.GridLines = true;
            lvMail.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            lvMail.Location = new Point(89, 168);
            lvMail.MultiSelect = false;
            lvMail.Name = "lvMail";
            lvMail.Size = new Size(669, 270);
            lvMail.TabIndex = 10;
            lvMail.UseCompatibleStateImageBehavior = false;
            lvMail.View = View.Details;
            lvMail.SelectedIndexChanged += lvMail_SelectedIndexChanged;
            // 
            // clEmail
            // 
            clEmail.Text = "Email";
            clEmail.Width = 220;
            // 
            // clForm
            // 
            clForm.Text = "From";
            clForm.Width = 220;
            // 
            // clTime
            // 
            clTime.Text = "Time";
            clTime.Width = 200;
            // 
            // Bai3
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lvMail);
            Controls.Add(t);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtTotal);
            Controls.Add(txtRecent);
            Controls.Add(btnlogin);
            Controls.Add(txtPassword);
            Controls.Add(txtEmail);
            Name = "Bai3";
            Text = "Bai3";
            Load += Bai3_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtEmail;
        private TextBox txtPassword;
        private Button btnlogin;
        private TextBox txtRecent;
        private TextBox txtTotal;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label t;
        private ListView lvMail;
        private ColumnHeader clEmail;
        private ColumnHeader clForm;
        private ColumnHeader clTime;
    }
}