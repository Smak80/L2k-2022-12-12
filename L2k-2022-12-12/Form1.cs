namespace L2k_2022_12_12
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            a = new Animator(panel1.CreateGraphics());
        }

        private Animator a;
        private void button1_Click(object sender, EventArgs e)
        {
            a.Animate();
        }
    }
}