using System;
using System.Windows.Forms;

namespace kuleSavunma
{
    public class TopKulesi : Kule
    {
        // Tablodaki değerler: Hasar 50, Menzil 120, Fiyat 250
        public TopKulesi() : base(50, 120, 250)
        {
            Resim = new PictureBox();
            Resim.Size = new Size(50, 50);
            Resim.SizeMode = PictureBoxSizeMode.StretchImage;
            Resim.BackColor = Color.Transparent;

            // Güncelle:
            Resim.Image = Properties.Resources.TopKulesi;
        }

        public override void Saldir()
        {
            // Top kulesi menzildeki HERKESE vuracak (Alan hasarı)
        }
    }
}