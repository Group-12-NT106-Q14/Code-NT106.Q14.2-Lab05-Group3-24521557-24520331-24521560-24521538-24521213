using System.Runtime.Intrinsics.X86;

namespace Code_NT106.Q14._2_Lab05_Group3_24521557_24520331_24521560_24521538_24521213
{
    public partial class dashboard : Form
    {
        public dashboard()
        {
            InitializeComponent();
        }

        private void btnBai01_Click(object sender, EventArgs e)
        {
            this.Hide();
            Bai01 bai01 = new Bai01();
            bai01.ShowDialog();
            this.Show();
        }

        private void btnBai02_Click(object sender, EventArgs e)
        {
            this.Hide();
            Bai02 bai02 = new Bai02();
            bai02.ShowDialog();
            this.Show();
        }

        private void btnBai03_Click(object sender, EventArgs e)
        {
            this.Hide();
            Bai3 bai3 = new Bai3();
            bai3.ShowDialog();
            this.Show();
        }

        private void btnBai04_Click(object sender, EventArgs e)
        {
            this.Hide();
            Bai04 bai04 = new Bai04();
            bai04.ShowDialog();
            this.Show();
        }

        private void btnBai05_Click(object sender, EventArgs e)
        {
            this.Hide();
            Bai05 bai05 = new Bai05();
            bai05.ShowDialog();
            this.Show();
        }

        private void btnBai06_Click(object sender, EventArgs e)
        {
            this.Hide();
            Bai06 bai6 = new Bai06();
            bai6.ShowDialog();
            this.Show();
        }
    }
}