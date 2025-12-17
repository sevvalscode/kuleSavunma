using System;
using System.Drawing;       // Resim ve Boyut işlemleri için
using System.Windows.Forms; // PictureBox için

namespace kuleSavunma
{
    public class Canavar
    {
        // --- ÖZELLİKLER ---
        public int Can { get; set; }
        public int Hiz { get; set; }
        public int AltinDegeri { get; set; } // Ölünce kazandıracağı para
        public string TurAdi { get; set; }   // "Golem", "Ejderha" vb.

        public PictureBox Resim { get; set; } // Ekrandaki görüntüsü
        public int HedefNoktaIndeksi { get; set; } // Yolda kaçıncı virajda?

        // Ölü kontrolü için pratik özellik
        public bool OlduMu { get { return Can <= 0; } }

        // --- SİHİRLİ YAPICI METOT (Constructor) ---
        // Biz sadece türünü söyleyeceğiz (örn: "Golem"), o geri kalanı halledecek.
        public Canavar(string tur)
        {
            this.TurAdi = tur;
            this.HedefNoktaIndeksi = 0; // Yolun başından başla

            // Görsel Kutuyu Hazırla
            Resim = new PictureBox();
            Resim.SizeMode = PictureBoxSizeMode.StretchImage; // Resim kutuya sığsın
            Resim.BackColor = Color.Transparent; // Arka plan şeffaf olsun

            // TÜRÜNE GÖRE AYARLARI YAP (Switch-Case)
            switch (tur)
            {
                case "AtesRuhu": // Hızlı, Zayıf, Küçük
                    this.Can = 20;
                    this.Hiz = 6;     // Çok hızlı
                    this.AltinDegeri = 10;
                    Resim.Size = new Size(30, 30); // Küçük
                    // Eğer resmin yoksa hata vermemesi için kontrol:
                    // Resim.Image = Properties.Resources.atesruhu; 
                    // (Resimlerin Resources içinde ekli olduğunu varsayıyorum, değilse renk verebiliriz)
                    Resim.Image = Properties.Resources.atesruhu;
                   
                  break;

                case "Golem": // Yavaş, Tank, Orta Boy
                    this.Can = 100;
                    this.Hiz = 2;     // Yavaş
                    this.AltinDegeri = 30;
                    Resim.Size = new Size(50, 50);
                    Resim.Image = Properties.Resources.golem;
                    break;
                case "Ejderha": // BOSS, Büyük
                    this.Can = 250;
                    this.Hiz = 3;     // Normal
                    this.AltinDegeri = 100;
                    Resim.Size = new Size(70, 60); // Büyük
                    Resim.Image = Properties.Resources.ejderha;
                    break;
            }
        }

        // Kule vurunca can azaltma
        public void HasarAl(int hasar)
        {
            this.Can -= hasar;
        }

        // Yardımcı Metot: Resim var mı diye kontrol eder (Hata almanı engeller)
        private bool KutuphaneVarMi(string ad)
        {
            var kaynak = Properties.Resources.ResourceManager.GetObject(ad);
            return kaynak != null;
        }
    }
}