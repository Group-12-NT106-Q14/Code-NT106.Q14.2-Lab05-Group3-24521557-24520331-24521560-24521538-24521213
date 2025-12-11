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
    }
}
