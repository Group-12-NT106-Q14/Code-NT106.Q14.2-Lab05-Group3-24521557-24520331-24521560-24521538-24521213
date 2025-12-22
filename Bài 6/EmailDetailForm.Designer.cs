namespace Code_NT106.Q14._2_Lab05_Group3_24521557_24520331_24521560_24521538_24521213
{
    partial class EmailDetailForm
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
            lbForm = new Label();
            lbTo = new Label();
            txtFORM = new TextBox();
            txtTO = new TextBox();
            webViewBody = new Microsoft.Web.WebView2.WinForms.WebView2();
            txtSubject = new TextBox();
            ((System.ComponentModel.ISupportInitialize)webViewBody).BeginInit();
            SuspendLayout();
            // 
            // lbForm
            // 
            lbForm.AutoSize = true;
            lbForm.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbForm.Location = new Point(21, 21);
            lbForm.Name = "lbForm";
            lbForm.Size = new Size(54, 21);
            lbForm.TabIndex = 0;
            lbForm.Text = "FORM";
            lbForm.Click += label1_Click;
            // 
            // lbTo
            // 
            lbTo.AutoSize = true;
            lbTo.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbTo.Location = new Point(21, 54);
            lbTo.Name = "lbTo";
            lbTo.Size = new Size(29, 21);
            lbTo.TabIndex = 1;
            lbTo.Text = "TO";
            // 
            // txtFORM
            // 
            txtFORM.Location = new Point(92, 20);
            txtFORM.Name = "txtFORM";
            txtFORM.ReadOnly = true;
            txtFORM.Size = new Size(395, 23);
            txtFORM.TabIndex = 2;
            // 
            // txtTO
            // 
            txtTO.Location = new Point(92, 56);
            txtTO.Name = "txtTO";
            txtTO.ReadOnly = true;
            txtTO.Size = new Size(395, 23);
            txtTO.TabIndex = 3;
            // 
            // webViewBody
            // 
            webViewBody.AllowExternalDrop = true;
            webViewBody.CreationProperties = null;
            webViewBody.DefaultBackgroundColor = Color.White;
            webViewBody.Location = new Point(12, 118);
            webViewBody.Name = "webViewBody";
            webViewBody.Size = new Size(765, 323);
            webViewBody.TabIndex = 4;
            webViewBody.ZoomFactor = 1D;
            // 
            // txtSubject
            // 
            txtSubject.Location = new Point(21, 85);
            txtSubject.Name = "txtSubject";
            txtSubject.ReadOnly = true;
            txtSubject.Size = new Size(756, 23);
            txtSubject.TabIndex = 5;
            txtSubject.TextChanged += textBox1_TextChanged;
            // 
            // EmailDetailForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(txtSubject);
            Controls.Add(webViewBody);
            Controls.Add(txtTO);
            Controls.Add(txtFORM);
            Controls.Add(lbTo);
            Controls.Add(lbForm);
            Name = "EmailDetailForm";
            Text = "EmailDetailForm";
            Load += EmailDetailForm_Load;
            ((System.ComponentModel.ISupportInitialize)webViewBody).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbForm;
        private Label lbTo;
        private TextBox txtFORM;
        private TextBox txtTO;
        private Microsoft.Web.WebView2.WinForms.WebView2 webViewBody;
        private TextBox txtSubject;
    }
}