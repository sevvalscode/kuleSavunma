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
            Resim.Size = new Size(100, 100);
            Resim.SizeMode = PictureBoxSizeMode.StretchImage;
            Resim.BackColor = Color.Transparent;
            Resim.Image = Properties.Resources.OkKulesi;
            this.AtisHizi = 1000;
        }

        public override void Saldir(List<Canavar> canavarlar, List<AtisEfekti> efektler)
        {
            if ((DateTime.Now - SonAtisZamani).TotalMilliseconds < AtisHizi) return;

            foreach (Canavar c in canavarlar)
            {
                double mesafe = Math.Sqrt(Math.Pow(c.Resim.Left - Resim.Left, 2) + Math.Pow(c.Resim.Top - Resim.Top, 2));

                if (mesafe <= Menzil)
                {
                    c.Can -= Hasar;
                    SonAtisZamani = DateTime.Now;

                    // SARI LAZER
                    efektler.Add(new AtisEfekti
                    {
                        Baslangic = new Point(Resim.Left + 45, Resim.Top + 45),
                        Bitis = new Point(c.Resim.Left + (c.Resim.Width / 2), c.Resim.Top + (c.Resim.Height / 2)),
                        Omur = 5,
                        Renk = Color.Yellow // <-- Renk atadık
                    });
                    break;
                }
            }
        }
    }
}