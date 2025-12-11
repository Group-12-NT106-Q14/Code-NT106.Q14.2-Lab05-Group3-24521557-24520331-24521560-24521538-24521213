using System;
using System.Windows.Forms;
using MailKit.Net.Smtp;
using MimeKit;

namespace Code_NT106.Q14._2_Lab05_Group3_24521557_24520331_24521560_24521538_24521213
{
    public partial class Bai01 : Form
    {
        const string GroupEmail = "group12.nt106.q14@gmail.com";
        const string AppPassword = "tmjx bacw rvsg dybr";

        public Bai01()
        {
            InitializeComponent();
            txtFrom.Text = GroupEmail;
            txtFrom.ReadOnly = true;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                var to = txtTo.Text.Trim();
                var subject = txtSubject.Text;
                var body = txtBody.Text;

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("", GroupEmail));
                message.To.Add(new MailboxAddress("", to));
                message.Subject = subject;
                message.Body = new TextPart("plain")
                {
                    Text = body
                };

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 465, true);
                    client.Authenticate(GroupEmail, AppPassword);
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
    }
}
