namespace Code_NT106.Q14._2_Lab05_Group3_24521557_24520331_24521560_24521538_24521213
{
    partial class SendEmail
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
            txtForm = new TextBox();
            txtSubject = new TextBox();
            txtTo = new TextBox();
            txtName = new TextBox();
            lbFrom = new Label();
            lbName = new Label();
            lbTo = new Label();
            lbSuject = new Label();
            lbBody = new Label();
            label2 = new Label();
            cbHTML = new CheckBox();
            txtBody = new TextBox();
            lbAttackment = new Label();
            txtBrowse = new TextBox();
            btnBrowse = new Button();
            btnSend = new Button();
            SuspendLayout();
            // 
            // txtForm
            // 
            txtForm.Location = new Point(130, 12);
            txtForm.Name = "txtForm";
            txtForm.ReadOnly = true;
            txtForm.Size = new Size(317, 23);
            txtForm.TabIndex = 0;
            txtForm.TextChanged += txtForm_TextChanged;
            // 
            // txtSubject
            // 
            txtSubject.Location = new Point(130, 119);
            txtSubject.Name = "txtSubject";
            txtSubject.Size = new Size(317, 23);
            txtSubject.TabIndex = 1;
            // 
            // txtTo
            // 
            txtTo.Location = new Point(130, 82);
            txtTo.Name = "txtTo";
            txtTo.Size = new Size(317, 23);
            txtTo.TabIndex = 2;
            // 
            // txtName
            // 
            txtName.Location = new Point(130, 49);
            txtName.Name = "txtName";
            txtName.Size = new Size(317, 23);
            txtName.TabIndex = 3;
            // 
            // lbFrom
            // 
            lbFrom.AutoSize = true;
            lbFrom.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbFrom.Location = new Point(40, 14);
            lbFrom.Name = "lbFrom";
            lbFrom.Size = new Size(47, 21);
            lbFrom.TabIndex = 4;
            lbFrom.Text = "From";
            // 
            // lbName
            // 
            lbName.AutoSize = true;
            lbName.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbName.Location = new Point(40, 51);
            lbName.Name = "lbName";
            lbName.Size = new Size(52, 21);
            lbName.TabIndex = 5;
            lbName.Text = "Name";
            // 
            // lbTo
            // 
            lbTo.AutoSize = true;
            lbTo.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbTo.Location = new Point(40, 84);
            lbTo.Name = "lbTo";
            lbTo.Size = new Size(25, 21);
            lbTo.TabIndex = 6;
            lbTo.Text = "To";
            // 
            // lbSuject
            // 
            lbSuject.AutoSize = true;
            lbSuject.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbSuject.Location = new Point(40, 121);
            lbSuject.Name = "lbSuject";
            lbSuject.Size = new Size(61, 21);
            lbSuject.TabIndex = 7;
            lbSuject.Text = "Subject";
            // 
            // lbBody
            // 
            lbBody.AutoSize = true;
            lbBody.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbBody.Location = new Point(40, 155);
            lbBody.Name = "lbBody";
            lbBody.Size = new Size(45, 21);
            lbBody.TabIndex = 8;
            lbBody.Text = "Body";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(159, 155);
            label2.Name = "label2";
            label2.Size = new Size(0, 21);
            label2.TabIndex = 9;
            // 
            // cbHTML
            // 
            cbHTML.AutoSize = true;
            cbHTML.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cbHTML.Location = new Point(130, 151);
            cbHTML.Name = "cbHTML";
            cbHTML.Size = new Size(70, 25);
            cbHTML.TabIndex = 10;
            cbHTML.Text = "HTML";
            cbHTML.UseVisualStyleBackColor = true;
            // 
            // txtBody
            // 
            txtBody.Location = new Point(40, 179);
            txtBody.Multiline = true;
            txtBody.Name = "txtBody";
            txtBody.Size = new Size(733, 209);
            txtBody.TabIndex = 11;
            // 
            // lbAttackment
            // 
            lbAttackment.AutoSize = true;
            lbAttackment.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbAttackment.Location = new Point(40, 407);
            lbAttackment.Name = "lbAttackment";
            lbAttackment.Size = new Size(89, 21);
            lbAttackment.TabIndex = 12;
            lbAttackment.Text = "Attackment";
            // 
            // txtBrowse
            // 
            txtBrowse.Location = new Point(135, 405);
            txtBrowse.Name = "txtBrowse";
            txtBrowse.ReadOnly = true;
            txtBrowse.Size = new Size(425, 23);
            txtBrowse.TabIndex = 13;
            // 
            // btnBrowse
            // 
            btnBrowse.Location = new Point(566, 408);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(75, 23);
            btnBrowse.TabIndex = 14;
            btnBrowse.Text = "Browse";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // btnSend
            // 
            btnSend.Location = new Point(698, 407);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(75, 23);
            btnSend.TabIndex = 15;
            btnSend.Text = "Send";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click;
            // 
            // SendEmail
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnSend);
            Controls.Add(btnBrowse);
            Controls.Add(txtBrowse);
            Controls.Add(lbAttackment);
            Controls.Add(txtBody);
            Controls.Add(cbHTML);
            Controls.Add(label2);
            Controls.Add(lbBody);
            Controls.Add(lbSuject);
            Controls.Add(lbTo);
            Controls.Add(lbName);
            Controls.Add(lbFrom);
            Controls.Add(txtName);
            Controls.Add(txtTo);
            Controls.Add(txtSubject);
            Controls.Add(txtForm);
            Name = "SendEmail";
            Text = "SendEmail";
            Load += SendEmail_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtForm;
        private TextBox txtSubject;
        private TextBox txtTo;
        private TextBox txtName;
        private Label lbFrom;
        private Label lbName;
        private Label lbTo;
        private Label lbSuject;
        private Label lbBody;
        private Label label2;
        private CheckBox cbHTML;
        private TextBox txtBody;
        private Label lbAttackment;
        private TextBox txtBrowse;
        private Button btnBrowse;
        private Button btnSend;
    }
}