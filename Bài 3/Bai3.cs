using MailKit;
using MailKit.Net.Pop3;
using MailKit.Security;
using MimeKit;
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
    public partial class Bai3 : Form
    {
        public Bai3()
        {
            InitializeComponent();
        }



        private void btnlogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Nếu dùng Gmail POP thì luôn là imap.gmail.com
            string server = "pop.gmail.com";
            int port = 995;

            using (var client = new Pop3Client())
            {
                // Kết nối POP3 (SSL)
                //client.Connect(server, port, true);

                // Đăng nhập bằng Email + App Password
                //client.Authenticate(email, password);\

                client.Connect("pop.gmail.com", 995, SecureSocketOptions.SslOnConnect);
                client.Authenticate(email, password);


                int total = client.Count;        // tổng số email
                txtTotal.Text = total.ToString();

                // Xóa dữ liệu cũ trên listview
                lvMail.Items.Clear();

                // Chọn số email muốn xem gần đây
                int recentCount = Math.Min(5, total);
                txtRecent.Text = recentCount.ToString();

                // Duyệt email từ mới nhất → cũ nhất
                for (int i = total - 1; i >= total - recentCount; i--)
                {
                    MimeMessage message = client.GetMessage(i);
                    string subject = message.Subject;
                    string from = message.From.ToString();
                    string time = message.Date.ToString();
                    AddToListView(subject, from, time);


                }

                client.Disconnect(true);
            }
        }

        private void lvMail_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AddToListView(string subject, string from, string time)
        {
            ListViewItem item = new ListViewItem(subject);
            item.SubItems.Add(from);
            item.SubItems.Add(time);
            lvMail.Items.Add(item);
        }
        private void Bai3_Load(object sender, EventArgs e)
        {

        }
    }
}
