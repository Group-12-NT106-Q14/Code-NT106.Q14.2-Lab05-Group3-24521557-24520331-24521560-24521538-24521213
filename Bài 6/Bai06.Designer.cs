namespace Code_NT106.Q14._2_Lab05_Group3_24521557_24520331_24521560_24521538_24521213
{
    partial class Bai06
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
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            lbDangNhap = new Label();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            txtTaiKhoan = new TextBox();
            txtMatKhau = new TextBox();
            txtIMAP = new TextBox();
            txtIMAPPORT = new TextBox();
            txtSMTP = new TextBox();
            txtSMTPPORT = new TextBox();
            btnDangNhap = new Button();
            btnSend = new Button();
            btnRefesh = new Button();
            btnLogout = new Button();
            lstEmail = new ListView();
            STT = new ColumnHeader();
            FROM = new ColumnHeader();
            SUBJECT = new ColumnHeader();
            DATETIME = new ColumnHeader();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 18);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(300, 104);
            textBox1.TabIndex = 0;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(319, 19);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(478, 104);
            textBox2.TabIndex = 1;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // lbDangNhap
            // 
            lbDangNhap.AutoSize = true;
            lbDangNhap.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbDangNhap.Location = new Point(16, 10);
            lbDangNhap.Name = "lbDangNhap";
            lbDangNhap.Size = new Size(89, 21);
            lbDangNhap.TabIndex = 2;
            lbDangNhap.Text = "Dang Nhap";
            lbDangNhap.Click += label1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(332, 10);
            label1.Name = "label1";
            label1.Size = new Size(60, 21);
            label1.TabIndex = 3;
            label1.Text = "Cai Dat";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(91, 170);
            label2.Name = "label2";
            label2.Size = new Size(0, 21);
            label2.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(16, 42);
            label3.Name = "label3";
            label3.Size = new Size(65, 17);
            label3.TabIndex = 5;
            label3.Text = "Tai Khoan";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(372, 231);
            label4.Name = "label4";
            label4.Size = new Size(0, 21);
            label4.TabIndex = 6;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(380, 239);
            label5.Name = "label5";
            label5.Size = new Size(0, 21);
            label5.TabIndex = 7;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(16, 71);
            label6.Name = "label6";
            label6.Size = new Size(64, 17);
            label6.TabIndex = 8;
            label6.Text = "Mat Khau";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.Location = new Point(545, 71);
            label7.Name = "label7";
            label7.Size = new Size(39, 17);
            label7.TabIndex = 9;
            label7.Text = "PORT";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.Location = new Point(545, 42);
            label8.Name = "label8";
            label8.Size = new Size(41, 17);
            label8.TabIndex = 10;
            label8.Text = "SMTP";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label9.Location = new Point(328, 71);
            label9.Name = "label9";
            label9.Size = new Size(39, 17);
            label9.TabIndex = 11;
            label9.Text = "PORT";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label10.Location = new Point(328, 42);
            label10.Name = "label10";
            label10.Size = new Size(38, 17);
            label10.TabIndex = 12;
            label10.Text = "IMAP";
            // 
            // txtTaiKhoan
            // 
            txtTaiKhoan.Location = new Point(86, 42);
            txtTaiKhoan.Name = "txtTaiKhoan";
            txtTaiKhoan.Size = new Size(226, 23);
            txtTaiKhoan.TabIndex = 13;
            // 
            // txtMatKhau
            // 
            txtMatKhau.Location = new Point(87, 71);
            txtMatKhau.Name = "txtMatKhau";
            txtMatKhau.Size = new Size(226, 23);
            txtMatKhau.TabIndex = 14;
            // 
            // txtIMAP
            // 
            txtIMAP.Location = new Point(372, 42);
            txtIMAP.Name = "txtIMAP";
            txtIMAP.Size = new Size(167, 23);
            txtIMAP.TabIndex = 15;
            // 
            // txtIMAPPORT
            // 
            txtIMAPPORT.Location = new Point(372, 71);
            txtIMAPPORT.Name = "txtIMAPPORT";
            txtIMAPPORT.Size = new Size(167, 23);
            txtIMAPPORT.TabIndex = 16;
            // 
            // txtSMTP
            // 
            txtSMTP.Location = new Point(592, 42);
            txtSMTP.Name = "txtSMTP";
            txtSMTP.Size = new Size(196, 23);
            txtSMTP.TabIndex = 17;
            // 
            // txtSMTPPORT
            // 
            txtSMTPPORT.Location = new Point(592, 71);
            txtSMTPPORT.Name = "txtSMTPPORT";
            txtSMTPPORT.Size = new Size(196, 23);
            txtSMTPPORT.TabIndex = 18;
            // 
            // btnDangNhap
            // 
            btnDangNhap.Location = new Point(212, 97);
            btnDangNhap.Name = "btnDangNhap";
            btnDangNhap.Size = new Size(100, 23);
            btnDangNhap.TabIndex = 19;
            btnDangNhap.Text = "Dang Nhap";
            btnDangNhap.UseVisualStyleBackColor = true;
            btnDangNhap.Click += btnDangNhap_Click;
            // 
            // btnSend
            // 
            btnSend.Location = new Point(332, 97);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(100, 23);
            btnSend.TabIndex = 20;
            btnSend.Text = "Gui mail";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click;
            // 
            // btnRefesh
            // 
            btnRefesh.Location = new Point(439, 98);
            btnRefesh.Name = "btnRefesh";
            btnRefesh.Size = new Size(100, 23);
            btnRefesh.TabIndex = 21;
            btnRefesh.Text = "Refesh";
            btnRefesh.UseVisualStyleBackColor = true;
            btnRefesh.Click += btnRefesh_Click;
            // 
            // btnLogout
            // 
            btnLogout.Location = new Point(545, 98);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(100, 23);
            btnLogout.TabIndex = 22;
            btnLogout.Text = "Dang Xuat";
            btnLogout.UseVisualStyleBackColor = true;
            btnLogout.Click += btnLogout_Click;
            // 
            // lstEmail
            // 
            lstEmail.Columns.AddRange(new ColumnHeader[] { STT, FROM, SUBJECT, DATETIME });
            lstEmail.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lstEmail.FullRowSelect = true;
            lstEmail.GridLines = true;
            lstEmail.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            lstEmail.Location = new Point(10, 129);
            lstEmail.MultiSelect = false;
            lstEmail.Name = "lstEmail";
            lstEmail.Size = new Size(787, 320);
            lstEmail.TabIndex = 2;
            lstEmail.UseCompatibleStateImageBehavior = false;
            lstEmail.View = View.Details;
            lstEmail.SelectedIndexChanged += listView1_SelectedIndexChanged;
            lstEmail.DoubleClick += lstEmail_DoubleClick;
            // 
            // STT
            // 
            STT.Text = "STT";
            // 
            // FROM
            // 
            FROM.Text = "FROM";
            FROM.Width = 240;
            // 
            // SUBJECT
            // 
            SUBJECT.Text = "SUBJECT";
            SUBJECT.Width = 240;
            // 
            // DATETIME
            // 
            DATETIME.Text = "DATETIME";
            DATETIME.Width = 250;
            // 
            // Bai06
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lstEmail);
            Controls.Add(btnLogout);
            Controls.Add(btnRefesh);
            Controls.Add(btnSend);
            Controls.Add(btnDangNhap);
            Controls.Add(txtSMTPPORT);
            Controls.Add(txtSMTP);
            Controls.Add(txtIMAPPORT);
            Controls.Add(txtIMAP);
            Controls.Add(txtMatKhau);
            Controls.Add(txtTaiKhoan);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(lbDangNhap);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Name = "Bai06";
            Text = "Bai06";
            Load += Bai06_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private TextBox textBox2;
        private Label lbDangNhap;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private TextBox txtTaiKhoan;
        private TextBox txtMatKhau;
        private TextBox txtIMAP;
        private TextBox txtIMAPPORT;
        private TextBox txtSMTP;
        private TextBox txtSMTPPORT;
        private Button btnDangNhap;
        private Button btnSend;
        private Button btnRefesh;
        private Button btnLogout;
        private ListView lstEmail;
        private ColumnHeader STT;
        private ColumnHeader FROM;
        private ColumnHeader SUBJECT;
        private ColumnHeader DATETIME;
    }
}