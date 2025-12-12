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
    public partial class Bai02 : Form
    {
        public Bai02()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {

                var email = "group12.nt106.q14@gmail.com";
                var pass = "tmjx bacw rvsg dybr";
                string username = txtUserName.Text;
                string password = txtPassWord.Text;
                if (txtUserName.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập email");
                }
                if (txtPassWord.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập mật khẩu");

                }
                using (var client = new ImapClient())
                {
                    client.Connect("imap.gmail.com", 993, true);
                    client.Authenticate(username, password);
                    var inbox = client.Inbox;
                    inbox.Open(MailKit.FolderAccess.ReadOnly);
                    for (int i = 0; i < inbox.Count; i++)
                    {
                        var message = inbox.GetMessage(i);
                        string subject = message.Subject;
                        string from = message.From.ToString();
                        string time = message.Date.ToString();
                        AddToListView(subject, from, time);
                    }
                    txtToTal.Text = inbox.Count.ToString();
                    txtRecent.Text = inbox.Recent.ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đăng nhập: " + ex.Message);
            }

        }
        private void AddToListView(string subject, string from, string time)
        {
            ListViewItem item = new ListViewItem(subject);
            item.SubItems.Add(from);
            item.SubItems.Add(time);
            lstEmail.Items.Add(item);
        }
        private void Bai02_Load(object sender, EventArgs e)
        {

        }

        private void lstEmail_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
