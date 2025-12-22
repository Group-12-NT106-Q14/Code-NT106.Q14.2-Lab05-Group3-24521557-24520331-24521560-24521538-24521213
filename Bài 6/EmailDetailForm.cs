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
using MimeKit;
using System.Net;
namespace Code_NT106.Q14._2_Lab05_Group3_24521557_24520331_24521560_24521538_24521213
{
    public partial class EmailDetailForm : Form
    {
        private readonly MimeMessage _message;

        public EmailDetailForm(MimeMessage message)
        {
            InitializeComponent();  
            _message = message;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private async void EmailDetailForm_Load(object sender, EventArgs e)
        {
            txtFORM.Text = _message.From.ToString();
            txtTO.Text = _message.To.ToString();
            txtSubject.Text = _message.Subject;



            await webViewBody.EnsureCoreWebView2Async();

            var html = _message.HtmlBody;
            if (!string.IsNullOrEmpty(html))
            {
                webViewBody.NavigateToString(html);
            }
            else
            {
                var text = _message.TextBody ?? "";
                var safe = WebUtility.HtmlEncode(text).Replace("\n", "<br/>");
                webViewBody.NavigateToString($"<html><body style='font-family:Segoe UI'>{safe}</body></html>");
            }
            webViewBody.NavigationCompleted += async (s, e) =>
            {
                await webViewBody.ExecuteScriptAsync(@"
        (function(){
            document.documentElement.style.backgroundColor = '#ffffff';
            document.body.style.backgroundColor = '#ffffff';
            document.body.style.color = '#000000';
        })();
    ");
            };
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
