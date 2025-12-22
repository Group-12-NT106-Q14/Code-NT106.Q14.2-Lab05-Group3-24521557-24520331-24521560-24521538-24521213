using MailKit.Net.Imap;
using MailKit.Security;
using Org.BouncyCastle.Crypto.Macs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Security;
using MimeKit;
namespace Code_NT106.Q14._2_Lab05_Group3_24521557_24520331_24521560_24521538_24521213
{
    public partial class Bai06 : Form
    {
        public Bai06()
        {
            InitializeComponent();
        }

        private void Bai06_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void LoadEmails()
        {

            lstEmail.Items.Clear();

            var email = txtTaiKhoan.Text;
            var pass = txtMatKhau.Text;
            var imapServer = txtIMAP.Text;
            var imapPort = int.Parse(txtIMAPPORT.Text);

            using (var client = new ImapClient())
            {
                client.Connect(imapServer, imapPort, SecureSocketOptions.SslOnConnect);
                client.Authenticate(email, pass);

                var inbox = client.Inbox;
                inbox.Open(MailKit.FolderAccess.ReadOnly);

                for (int i = 0; i < inbox.Count; i++)
                {
                    var message = inbox.GetMessage(i);

                    var item = new ListViewItem((i + 1).ToString());
                    item.SubItems.Add(message.From.ToString());
                    item.SubItems.Add(message.Subject);
                    item.SubItems.Add(message.Date.ToString());

                    item.Tag = i;              // <-- lưu index
                    lstEmail.Items.Add(item);
                }

                client.Disconnect(true);
            }


        }
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            try
            {
                LoadEmails();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnRefesh_Click(object sender, EventArgs e)
        {
            try
            {
                LoadEmails();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi refresh: " + ex.Message);
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            lstEmail.Items.Clear();
            txtTaiKhoan.Clear();
            txtMatKhau.Clear();
            txtIMAP.Clear();
            txtIMAPPORT.Clear();

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (txtTaiKhoan.Text == "")
            {
                MessageBox.Show("Vui lòng nhập email");
            }
            if (txtMatKhau.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mật khẩu");
            }
            if( txtSMTP.Text == "")
            {
                MessageBox.Show("Vui lòng nhập SMTP Server");
            }
            if (txtSMTPPORT.Text == "")
            {
                MessageBox.Show("Vui lòng nhập SMTP Port");
            }

            string smtp = txtSMTP.Text;
            string taiKhoan = txtTaiKhoan.Text;
            var matKhau = txtMatKhau.Text;
            int smtpPort = int.Parse(txtSMTPPORT.Text);
            var sendEmailForm = new SendEmail(smtp, taiKhoan, matKhau, smtpPort);
            sendEmailForm.Show();

        }
        private void lstEmail_DoubleClick(object sender, EventArgs e)
        {
            if (lstEmail.SelectedItems.Count == 0) return;

            int index = (int)lstEmail.SelectedItems[0].Tag;

            var email = txtTaiKhoan.Text;
            var pass = txtMatKhau.Text;
            var imapServer = txtIMAP.Text;
            int imapPort = int.Parse(txtIMAPPORT.Text);

            using (var client = new ImapClient())
            {
                client.Connect(imapServer, imapPort, true);
                client.Authenticate(email, pass);

                var inbox = client.Inbox;
                inbox.Open(MailKit.FolderAccess.ReadOnly);

                var message = inbox.GetMessage(index);

                var f = new EmailDetailForm(message);
                f.ShowDialog();

                client.Disconnect(true);
            }
        }
    }
}
