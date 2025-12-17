using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace kuleSavunma
{
    public class OkKulesi : Kule
    {
        public OkKulesi() : base(15, 150, 100)
        {
            Resim = new PictureBox();
            Resim.Size = new Size(90, 90);
            Resim.SizeMode = PictureBoxSizeMode.StretchImage;
            Resim.BackColor = Color.Transparent;

            // DÜZELTİLDİ: Gerçek isim
            Resim.Image = Properties.Resources.OkKulesi;

            this.AtisHizi = 1000;
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