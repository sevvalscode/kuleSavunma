using System;
using System.Windows.Forms;

namespace kuleSavunma
{
    public class BuyuKulesi : Kule
    {
        // Tablodaki değerler: Hasar 25, Menzil 130, Fiyat 200
        public BuyuKulesi() : base(25, 130, 200)
        {
            Resim = new PictureBox();
            Resim.Size = new Size(50, 50);
            Resim.SizeMode = PictureBoxSizeMode.StretchImage;
            Resim.BackColor = Color.Transparent;

            // Güncelle:
            Resim.Image = Properties.Resources.BuyuKulesi;
        }

        public override void Saldir()
        {
            // Büyü kulesi en yakın 5 kişiye vuracak
        }
    }
}