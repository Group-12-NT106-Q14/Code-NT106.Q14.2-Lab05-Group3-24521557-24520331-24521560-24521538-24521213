namespace Code_NT106.Q14._2_Lab05_Group3_24521557_24520331_24521560_24521538_24521213
{
    partial class Bai01
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Label lblSubject;
        private System.Windows.Forms.Label lblBody;
        private System.Windows.Forms.TextBox txtFrom;
        private System.Windows.Forms.TextBox txtTo;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.TextBox txtBody;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            btnSend = new Button();
            lblFrom = new Label();
            lblTo = new Label();
            lblSubject = new Label();
            lblBody = new Label();
            txtFrom = new TextBox();
            txtTo = new TextBox();
            txtSubject = new TextBox();
            txtBody = new TextBox();
            SuspendLayout();
            // 
            // btnSend
            // 
            btnSend.Location = new Point(12, 12);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(75, 60);
            btnSend.TabIndex = 0;
            btnSend.Text = "SEND";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click;
            // 
            // lblFrom
            // 
            lblFrom.AutoSize = true;
            lblFrom.Location = new Point(110, 22);
            lblFrom.Name = "lblFrom";
            lblFrom.Size = new Size(38, 15);
            lblFrom.TabIndex = 1;
            lblFrom.Text = "From:";
            // 
            // lblTo
            // 
            lblTo.AutoSize = true;
            lblTo.Location = new Point(110, 51);
            lblTo.Name = "lblTo";
            lblTo.Size = new Size(23, 15);
            lblTo.TabIndex = 2;
            lblTo.Text = "To:";
            // 
            // lblSubject
            // 
            lblSubject.AutoSize = true;
            lblSubject.Location = new Point(110, 90);
            lblSubject.Name = "lblSubject";
            lblSubject.Size = new Size(49, 15);
            lblSubject.TabIndex = 3;
            lblSubject.Text = "Subject:";
            // 
            // lblBody
            // 
            lblBody.AutoSize = true;
            lblBody.Location = new Point(110, 120);
            lblBody.Name = "lblBody";
            lblBody.Size = new Size(37, 15);
            lblBody.TabIndex = 4;
            lblBody.Text = "Body:";
            // 
            // txtFrom
            // 
            txtFrom.Location = new Point(170, 19);
            txtFrom.Name = "txtFrom";
            txtFrom.Size = new Size(450, 23);
            txtFrom.TabIndex = 5;
            // 
            // txtTo
            // 
            txtTo.Location = new Point(170, 48);
            txtTo.Name = "txtTo";
            txtTo.Size = new Size(450, 23);
            txtTo.TabIndex = 6;
            // 
            // txtSubject
            // 
            txtSubject.Location = new Point(170, 87);
            txtSubject.Name = "txtSubject";
            txtSubject.Size = new Size(450, 23);
            txtSubject.TabIndex = 7;
            // 
            // txtBody
            // 
            txtBody.Location = new Point(170, 118);
            txtBody.Multiline = true;
            txtBody.Name = "txtBody";
            txtBody.ScrollBars = ScrollBars.Vertical;
            txtBody.Size = new Size(450, 230);
            txtBody.TabIndex = 8;
            // 
            // Bai01
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(650, 370);
            Controls.Add(txtBody);
            Controls.Add(txtSubject);
            Controls.Add(txtTo);
            Controls.Add(txtFrom);
            Controls.Add(lblBody);
            Controls.Add(lblSubject);
            Controls.Add(lblTo);
            Controls.Add(lblFrom);
            Controls.Add(btnSend);
            Name = "Bai01";
            Text = "Bài 1 - Gửi Email";
            Load += Bai01_Load;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}