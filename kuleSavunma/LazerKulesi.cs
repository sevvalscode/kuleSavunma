using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace kuleSavunma
{
    // 4. KULE: Lazer Kulesi (Keskin Nişancı)
    // Özellikleri: Çok Yüksek Menzil, Yüksek Hasar, ama Yavaş Atış Hızı.
    public class LazerKulesi : Kule
    {
        public LazerKulesi() : base(60, 220, 250) // Hasar: 60 (Yüksek), Menzil: 220 (Çok Yüksek), Fiyat: 250 (Pahalı)
        {
            Resim = new PictureBox();
            Resim.Size = new Size(90, 90);
            Resim.SizeMode = PictureBoxSizeMode.StretchImage;
            // Şimdilik resmi yoksa arka planı mor olsun ki belli olsun

            // Eğer bir resim bulursan üstteki satırı silip bunu aç:
            Resim.Image = Properties.Resources.LazerKulesiResmi; 

            // 2 saniyede bir ateş etsin (Diğerlerinden çok daha yavaş)
            this.AtisHizi = 2000;
        }

        // Ana sınıftaki (Kule) ve Arabirimdeki (ISaldirgan) metodu eziyoruz (Override)
        public override void Saldir(List<Canavar> canavarlar, List<AtisEfekti> efektler)
        {
            // Atış zamanı gelmediyse bekle
            if ((DateTime.Now - SonAtisZamani).TotalMilliseconds < AtisHizi) return;

            // Menzildeki en yakın canavarı bulalım (Sniper mantığı)
            Canavar enYakinHedef = null;
            double enKisaMesafe = Menzil + 1; // Başlangıçta menzilden büyük bir değer

            foreach (Canavar c in canavarlar)
            {
                double mesafe = Math.Sqrt(Math.Pow(c.Resim.Left - Resim.Left, 2) + Math.Pow(c.Resim.Top - Resim.Top, 2));
                if (mesafe <= Menzil && mesafe < enKisaMesafe)
                {
                    enKisaMesafe = mesafe;
                    enYakinHedef = c;
                }
            }

            // Eğer bir hedef bulduysak ateş edelim
            if (enYakinHedef != null)
            {
                enYakinHedef.Can -= Hasar;
                SonAtisZamani = DateTime.Now;

                // --- İŞTE İSTEDİĞİN MOR LAZER EFEKTİ ---
                efektler.Add(new AtisEfekti
                {
                    Baslangic = new Point(Resim.Left + 45, Resim.Top + 45), // Kulenin tam ortası
                    Bitis = new Point(enYakinHedef.Resim.Left + (enYakinHedef.Resim.Width / 2), enYakinHedef.Resim.Top + (enYakinHedef.Resim.Height / 2)), // Hedefin tam ortası
                    Omur = 7, // Lazer ekranda biraz daha uzun kalsın, güçlü görünsün
                    Renk = Color.Purple // MOR RENK!
                });
            }
        }
    }
}