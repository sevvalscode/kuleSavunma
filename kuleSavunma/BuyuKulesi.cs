using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace kuleSavunma
{
    public class BuyuKulesi : Kule
    {
        public BuyuKulesi() : base(25, 130, 200)
        {
            Resim = new PictureBox();
            Resim.Size = new Size(100, 100);
            Resim.SizeMode = PictureBoxSizeMode.StretchImage;
            Resim.BackColor = Color.Transparent;
            Resim.Image = Properties.Resources.BuyuKulesi;
            this.AtisHizi = 1500;
        }

        public override void Saldir(List<Canavar> canavarlar, List<AtisEfekti> efektler)
        {
            if ((DateTime.Now - SonAtisZamani).TotalMilliseconds < AtisHizi) return;

            int vurulan = 0;
            bool atisYapildi = false;

            foreach (Canavar c in canavarlar)
            {
                if (vurulan >= 5) break;
                double mesafe = Math.Sqrt(Math.Pow(c.Resim.Left - Resim.Left, 2) + Math.Pow(c.Resim.Top - Resim.Top, 2));

                if (mesafe <= Menzil)
                {
                    c.Can -= Hasar;
                    // MAVİ LAZER (Elektrik)
                    efektler.Add(new AtisEfekti
                    {
                        Baslangic = new Point(Resim.Left + 45, Resim.Top + 45),
                        Bitis = new Point(c.Resim.Left + 20, c.Resim.Top + 20),
                        Omur = 5,
                        Renk = Color.Cyan
                    });
                    vurulan++;
                    atisYapildi = true;
                }
            }
            if (atisYapildi) SonAtisZamani = DateTime.Now;
        }
    }
}