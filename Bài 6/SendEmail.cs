using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MailKit.Net.Smtp;
using MimeKit;

namespace Code_NT106.Q14._2_Lab05_Group3_24521557_24520331_24521560_24521538_24521213
{
    public partial class SendEmail : Form
    {
        private string smtpServer;
        private int port;
        private string email;
        private string password;
        public SendEmail(string smtp, string taiKhoan, string matKhau, int smtpPort)
        {
            InitializeComponent();
            this.smtpServer = smtp;
            this.email = taiKhoan;
            this.password = matKhau;
            this.port = smtpPort;

            txtForm.Text = this.email.ToString();

        }

        private void SendEmail_Load(object sender, EventArgs e)
        {
            
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                var to = txtTo.Text.Trim();
                var subject = txtSubject.Text;
                var body = txtBody.Text;
                var isHtml = cbHTML.Checked;
                var attachmentPath = txtBrowse.Text.Trim();
                var message = new MimeKit.MimeMessage();
                message.From.Add(new MimeKit.MailboxAddress(txtName.Text.Trim(), txtForm.Text.Trim()));
                message.To.Add(new MimeKit.MailboxAddress("", to));
                message.Subject = subject;
                if (!string.IsNullOrEmpty(attachmentPath))
                {
                    var builder = new MimeKit.BodyBuilder();
                    if (isHtml)
                    {
                        builder.HtmlBody = body;
                    }
                    else
                    {
                        builder.TextBody = body;
                    }
                    builder.Attachments.Add(attachmentPath);
                    message.Body = builder.ToMessageBody();
                }
                else
                {
                    if (isHtml)
                    {
                        message.Body = new MimeKit.TextPart("html") { Text = body };
                    }
                    else
                    {
                        message.Body = new MimeKit.TextPart("plain") { Text = body };
                    }
                }
                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.Connect(smtpServer, port, true);
                    client.Authenticate(email, password);
                    client.Send(message);
                    client.Disconnect(true);
                }
                MessageBox.Show("Gửi email thành công");


            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi gửi email: " + ex.Message);
            }

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtBrowse.Text = ofd.FileName;
                }
            }
        }

        private void txtForm_TextChanged(object sender, EventArgs e)
        {
       
        }
    }
}
    

