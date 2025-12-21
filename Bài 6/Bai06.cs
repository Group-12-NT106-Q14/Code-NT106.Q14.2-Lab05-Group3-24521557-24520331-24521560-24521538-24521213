using MailKit.Net.Imap;
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
            
                var email = txtTaiKhoan.Text;
                var pass = txtMatKhau.Text;
                var imapEmail = txtIMAP.Text;
                var imapPort = int.Parse(txtIMAPPORT.Text);
                if (txtTaiKhoan.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập email");
                }
                if (txtMatKhau.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập mật khẩu");
                }
                if (txtIMAP.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập IMAP Server");
                }
                if (txtIMAPPORT.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập IMAP Port");
                }
                using (var client = new ImapClient())
                {
                    client.Connect(imapEmail, imapPort, true);
                    client.Authenticate(email, pass);
                    var inbox = client.Inbox;
                    inbox.Open(MailKit.FolderAccess.ReadOnly);
                    for (int i = 0; i < inbox.Count; i++)
                    {
                        var message = inbox.GetMessage(i);
                        string stt = (i + 1).ToString();
                        string subject = message.Subject;
                        string from = message.From.ToString();
                        string time = message.Date.ToString();

                        var item = new ListViewItem(stt);
                        item.SubItems.Add(from);
                        item.SubItems.Add(subject);
                        item.SubItems.Add(time);
                        lstEmail.Items.Add(item);
                    }

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
    }
}
