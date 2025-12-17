namespace kuleSavunma
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            // Týkladýðýn yerin X ve Y sayýsýný al
            string koordinat = "new Point(" + e.X + ", " + e.Y + ");";

            // Ekrana kutu içinde göster
            MessageBox.Show(koordinat);
        }
    }

}
