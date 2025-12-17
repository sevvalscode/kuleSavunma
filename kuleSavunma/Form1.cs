using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace kuleSavunma
{
    public partial class Form1 : Form
    {
        // ==================================================
        // 1. OYUNUN HAFIZASI (DEÐÝÞKENLER)
        // ==================================================
        List<Point> yolNoktalari = new List<Point>();
        List<Canavar> canavarlar = new List<Canavar>();
        List<string> dalgaKuyrugu = new List<string>(); // Doðacak canavarlar listesi

        // Timer Çatýþmasýný Önlemek Ýçin Tam Ýsimleri
        System.Windows.Forms.Timer oyunTimer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer dogmaTimer = new System.Windows.Forms.Timer();

        // Oyun Ayarlarý
        int baslangicCani = 20;
        int baslangicParasi = 600;

        // Anlýk Durum
        int oyuncuCan;
        int oyuncuPara;
        int dalgaSayisi = 0;
        bool oyunBasladiMi = false;

        public Form1()
        {
            InitializeComponent();
        }

        // ==================================================
        // 2. OYUN YÜKLENÝRKEN (AYARLAR)
        // ==================================================
        private void Form1_Load(object sender, EventArgs e)
        {
            // --- HARÝTA KOORDÝNATLARI (Senin Kaðýdýn) ---
            yolNoktalari.Clear();
            yolNoktalari.Add(new Point(989, 241)); // Baþlangýç
            yolNoktalari.Add(new Point(800, 240));
            yolNoktalari.Add(new Point(754, 196));
            yolNoktalari.Add(new Point(730, 129));
            yolNoktalari.Add(new Point(638, 87));
            yolNoktalari.Add(new Point(549, 136));
            yolNoktalari.Add(new Point(524, 215));
            yolNoktalari.Add(new Point(480, 246));
            yolNoktalari.Add(new Point(300, 255));
            yolNoktalari.Add(new Point(228, 309));
            yolNoktalari.Add(new Point(260, 378));
            yolNoktalari.Add(new Point(376, 403));
            yolNoktalari.Add(new Point(583, 376));
            yolNoktalari.Add(new Point(659, 402));
            yolNoktalari.Add(new Point(657, 479));
            yolNoktalari.Add(new Point(600, 509));
            yolNoktalari.Add(new Point(506, 522));
            yolNoktalari.Add(new Point(453, 549));
            yolNoktalari.Add(new Point(444, 694)); // Bitiþ

            // Deðiþkenleri Hazýrla
            oyuncuCan = baslangicCani;
            oyuncuPara = baslangicParasi;
            ArayuzGuncelle();

            // Zamanlayýcý Ayarlarý (Ama START demiyoruz)
            oyunTimer.Interval = 25;
            oyunTimer.Tick += OyunTimer_Tick;

            dogmaTimer.Interval = 1500;
            dogmaTimer.Tick += DogmaTimer_Tick;
        }

        // ==================================================
        // 3. BAÞLAT BUTONU (SIFIRLAMA MANTIÐI BURADA)
        // ==================================================
        private void btnBaslat_Click(object sender, EventArgs e)
        {
            if (oyunBasladiMi == false)
            {
                // --- OYUNU SIFIRLA VE BAÞLAT ---
                oyunBasladiMi = true;

                // 1. Deðerleri Baþa Sar
                oyuncuCan = baslangicCani;
                oyuncuPara = baslangicParasi;
                dalgaSayisi = 0;

                // 2. Ortada kalan canavar varsa sil (Temizlik)
                foreach (var c in canavarlar)
                {
                    this.Controls.Remove(c.Resim);
                }
                canavarlar.Clear();
                dalgaKuyrugu.Clear();

                // 3. Arayüzü Güncelle
                ArayuzGuncelle();
                this.Text = "Kule Savunma"; // Baþlýðý düzelt

                // 4. Butonu Gizle
                btnBaslat.Visible = false;

                // 5. Motorlarý Çalýþtýr
                oyunTimer.Start();
                dogmaTimer.Start();

                // 6. Ýlk Dalgayý Çaðýr
                DalgaBaslat(1);
            }
        }

        // ==================================================
        // 4. DALGA YÖNETÝMÝ (SENARYOLAR)
        // ==================================================
        private void DalgaBaslat(int dalga)
        {
            dalgaSayisi = dalga;
            dalgaKuyrugu.Clear();

            // SENARYOLAR
            if (dalga == 1)
            {
                // 5 Ateþ Ruhu
                for (int i = 0; i < 5; i++) dalgaKuyrugu.Add("AtesRuhu");
            }
            else if (dalga == 2)
            {
                // 5 Ateþ Ruhu + 2 Golem
                for (int i = 0; i < 5; i++) dalgaKuyrugu.Add("AtesRuhu");
                dalgaKuyrugu.Add("Golem");
                dalgaKuyrugu.Add("Golem");
            }
            else if (dalga == 3)
            {
                // 5 Golem
                for (int i = 0; i < 5; i++) dalgaKuyrugu.Add("Golem");
            }
            else if (dalga == 4)
            {
                // Karýþýk: Golem ve Ejderha
                dalgaKuyrugu.Add("Golem"); dalgaKuyrugu.Add("Ejderha");
                dalgaKuyrugu.Add("Golem"); dalgaKuyrugu.Add("Ejderha");
                dalgaKuyrugu.Add("Golem"); dalgaKuyrugu.Add("Ejderha");
            }
            else if (dalga == 5)
            {
                // 4 Ejderha
                for (int i = 0; i < 4; i++) dalgaKuyrugu.Add("Ejderha");
            }
            else if (dalga == 6)
            {
                // Final Dalgasý: Hepsi
                for (int i = 0; i < 5; i++) dalgaKuyrugu.Add("AtesRuhu");
                for (int i = 0; i < 3; i++) dalgaKuyrugu.Add("Golem");
                for (int i = 0; i < 2; i++) dalgaKuyrugu.Add("Ejderha");
            }

            ArayuzGuncelle();
        }

        // ==================================================
        // 5. CANAVAR DOÐURMA MOTORU
        // ==================================================
        private void DogmaTimer_Tick(object sender, EventArgs e)
        {
            // Kuyruk boþsa iþlem yapma
            if (dalgaKuyrugu.Count <= 0) return;

            // Sýradakini al ve yarat
            string tur = dalgaKuyrugu[0];
            Canavar yeni = new Canavar(tur);
            yeni.Resim.Location = yolNoktalari[0];

            this.Controls.Add(yeni.Resim);
            yeni.Resim.BringToFront();
            canavarlar.Add(yeni);

            // Kuyruktan sil
            dalgaKuyrugu.RemoveAt(0);
        }

        // ==================================================
        // 6. OYUN MOTORU (HAREKET & KAZANMA/KAYBETME)
        // ==================================================
        private void OyunTimer_Tick(object sender, EventArgs e)
        {
            for (int i = canavarlar.Count - 1; i >= 0; i--)
            {
                Canavar c = canavarlar[i];

                // Yolun sonuna geldi mi?
                if (c.HedefNoktaIndeksi >= yolNoktalari.Count)
                {
                    oyuncuCan--;
                    if (oyuncuCan < 0) oyuncuCan = 0;

                    this.Controls.Remove(c.Resim);
                    canavarlar.RemoveAt(i);
                    ArayuzGuncelle();

                    // --- KAYBETME KONTROLÜ ---
                    if (oyuncuCan == 0)
                    {
                        OyunBitti("KRALLIK DÜÞTÜ! KAYBETTÝNÝZ.", "TEKRAR DENE");
                    }
                    continue;
                }

                // --- YÜRÜME MANTIÐI ---
                Point hedef = yolNoktalari[c.HedefNoktaIndeksi];

                if (c.Resim.Left < hedef.X) c.Resim.Left += c.Hiz;
                else if (c.Resim.Left > hedef.X) c.Resim.Left -= c.Hiz;

                if (c.Resim.Top < hedef.Y) c.Resim.Top += c.Hiz;
                else if (c.Resim.Top > hedef.Y) c.Resim.Top -= c.Hiz;

                if (Math.Abs(c.Resim.Left - hedef.X) < 15 && Math.Abs(c.Resim.Top - hedef.Y) < 15)
                {
                    c.HedefNoktaIndeksi++;
                }
            }

            // --- KAZANMA VE DALGA KONTROLÜ ---
            // Eðer doðacak kimse kalmadýysa VE sahnedeki herkes öldüyse
            if (dalgaKuyrugu.Count == 0 && canavarlar.Count == 0 && oyunBasladiMi)
            {
                if (dalgaSayisi < 6)
                {
                    // Sýradaki dalgaya geç
                    DalgaBaslat(dalgaSayisi + 1);
                }
                else
                {
                    // Oyun Bitti (Zafer)
                    OyunBitti("TEBRÝKLER! TÜM DALGALARI PÜSKÜRTTÜN!", "YENÝDEN OYNA");
                }
            }
        }

        // Oyun Bitince Çalýþan Yardýmcý Metot
        private void OyunBitti(string mesaj, string butonYazisi)
        {
            oyunTimer.Stop();
            dogmaTimer.Stop();

            oyunBasladiMi = false;          // Oyun bitti durumu
            btnBaslat.Text = butonYazisi;   // Butonun yazýsýný deðiþtir
            btnBaslat.Visible = true;       // Butonu geri getir!
            btnBaslat.Enabled = true;

            MessageBox.Show(mesaj);
        }

        // Arayüz Güncelleyici
        private void ArayuzGuncelle()
        {
            try
            {
                lblCan.Text = oyuncuCan.ToString();
                lblPara.Text = oyuncuPara.ToString();
                lblDalga.Text = dalgaSayisi.ToString();
            }
            catch { }
        }

        // ==================================================
        // 7. TASARIM KORUYUCU BOÞ METOTLAR (SÝLME!)
        // ==================================================
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void panel2_Paint(object sender, PaintEventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void panel4_Paint(object sender, PaintEventArgs e) { }
        private void Form1_MouseClick(object sender, MouseEventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
    }
}