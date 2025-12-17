using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace kuleSavunma
{
    public class TopKulesi : Kule
    {
        public TopKulesi() : base(50, 100, 200) // Çok hasar, kısa menzil, pahalı
        {
            Resim = new PictureBox();
            Resim.Size = new Size(90, 90);
            Resim.SizeMode = PictureBoxSizeMode.StretchImage;
            Resim.BackColor = Color.Transparent;

            // DÜZELTİLDİ: Gerçek isim
            Resim.Image = Properties.Resources.TopKulesi;

            this.AtisHizi = 2000; // Çok yavaş vurur
        }

        public override void Saldir(List<Canavar> canavarlar)
        {
            if ((DateTime.Now - SonAtisZamani).TotalMilliseconds < AtisHizi) return;

            foreach (Canavar c in canavarlar)
            {
                double mesafe = Math.Sqrt(Math.Pow(c.Resim.Left - Resim.Left, 2) + Math.Pow(c.Resim.Top - Resim.Top, 2));

                if (mesafe <= Menzil)
                {
                    c.Can -= Hasar;
                    SonAtisZamani = DateTime.Now;
                    break;
                }
            }
        }
    }
}