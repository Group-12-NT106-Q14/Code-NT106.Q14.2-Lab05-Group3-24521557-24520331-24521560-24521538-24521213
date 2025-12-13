using Microsoft.Web.WebView2.WinForms;
using System.Windows.Forms;
using System.Drawing;

namespace Code_NT106.Q14._2_Lab05_Group3_24521557_24520331_24521560_24521538_24521213
{
    partial class Bai04
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
            flpMovies = new FlowLayoutPanel();
            webViewDetail = new WebView2();
            panelTop = new Panel();
            btnBook = new Button();
            btnLoadFromJson = new Button();
            btnCrawl = new Button();
            progressBar = new ProgressBar();
            lblStatus = new Label();
            ((System.ComponentModel.ISupportInitialize)webViewDetail).BeginInit();
            panelTop.SuspendLayout();
            SuspendLayout();
            // 
            // flpMovies
            // 
            flpMovies.AutoScroll = true;
            flpMovies.BackColor = Color.White;
            flpMovies.BorderStyle = BorderStyle.FixedSingle;
            flpMovies.Dock = DockStyle.Left;
            flpMovies.Location = new Point(0, 79);
            flpMovies.Margin = new Padding(3, 4, 3, 4);
            flpMovies.Name = "flpMovies";
            flpMovies.Padding = new Padding(6, 7, 6, 7);
            flpMovies.Size = new Size(400, 854);
            flpMovies.TabIndex = 0;
            // 
            // webViewDetail
            // 
            webViewDetail.AllowExternalDrop = true;
            webViewDetail.CreationProperties = null;
            webViewDetail.DefaultBackgroundColor = Color.White;
            webViewDetail.Dock = DockStyle.Fill;
            webViewDetail.Location = new Point(400, 79);
            webViewDetail.Margin = new Padding(3, 4, 3, 4);
            webViewDetail.Name = "webViewDetail";
            webViewDetail.Size = new Size(949, 854);
            webViewDetail.TabIndex = 1;
            webViewDetail.ZoomFactor = 1D;
            // 
            // panelTop
            // 
            panelTop.BorderStyle = BorderStyle.FixedSingle;
            panelTop.Controls.Add(btnBook);
            panelTop.Controls.Add(btnLoadFromJson);
            panelTop.Controls.Add(btnCrawl);
            panelTop.Controls.Add(progressBar);
            panelTop.Controls.Add(lblStatus);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Margin = new Padding(3, 4, 3, 4);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(1349, 79);
            panelTop.TabIndex = 2;
            // 
            // btnBook
            // 
            btnBook.BackColor = Color.LightGreen;
            btnBook.Font = new Font("Arial", 9F, FontStyle.Bold);
            btnBook.Location = new Point(411, 13);
            btnBook.Margin = new Padding(3, 4, 3, 4);
            btnBook.Name = "btnBook";
            btnBook.Size = new Size(160, 47);
            btnBook.TabIndex = 2;
            btnBook.Text = "Đặt vé";
            btnBook.UseVisualStyleBackColor = false;
            btnBook.Click += btnBook_Click;
            // 
            // btnLoadFromJson
            // 
            btnLoadFromJson.BackColor = Color.LightYellow;
            btnLoadFromJson.Font = new Font("Arial", 9F, FontStyle.Bold);
            btnLoadFromJson.Location = new Point(206, 13);
            btnLoadFromJson.Margin = new Padding(3, 4, 3, 4);
            btnLoadFromJson.Name = "btnLoadFromJson";
            btnLoadFromJson.Size = new Size(194, 47);
            btnLoadFromJson.TabIndex = 1;
            btnLoadFromJson.Text = "Đọc phim từ JSON";
            btnLoadFromJson.UseVisualStyleBackColor = false;
            btnLoadFromJson.Click += btnLoadFromJson_Click;
            // 
            // btnCrawl
            // 
            btnCrawl.BackColor = Color.LightSkyBlue;
            btnCrawl.Font = new Font("Arial", 9F, FontStyle.Bold);
            btnCrawl.Location = new Point(11, 13);
            btnCrawl.Margin = new Padding(3, 4, 3, 4);
            btnCrawl.Name = "btnCrawl";
            btnCrawl.Size = new Size(183, 47);
            btnCrawl.TabIndex = 0;
            btnCrawl.Text = "Crawl phim từ web";
            btnCrawl.UseVisualStyleBackColor = false;
            btnCrawl.Click += btnCrawl_Click;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(594, 13);
            progressBar.Margin = new Padding(3, 4, 3, 4);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(320, 20);
            progressBar.TabIndex = 3;
            progressBar.Visible = false;
            // 
            // lblStatus
            // 
            lblStatus.AutoEllipsis = true;
            lblStatus.Font = new Font("Arial", 9F);
            lblStatus.Location = new Point(594, 37);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(731, 27);
            lblStatus.TabIndex = 4;
            lblStatus.Text = "...";
            // 
            // Bai04
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1349, 933);
            Controls.Add(webViewDetail);
            Controls.Add(flpMovies);
            Controls.Add(panelTop);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Bai04";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Bài 04 – Quản lý phòng vé (phiên bản số 5)";
            ((System.ComponentModel.ISupportInitialize)webViewDetail).EndInit();
            panelTop.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flpMovies;
        private Microsoft.Web.WebView2.WinForms.WebView2 webViewDetail;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Button btnBook;
        private System.Windows.Forms.Button btnLoadFromJson;
        private System.Windows.Forms.Button btnCrawl;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblStatus;
    }
}