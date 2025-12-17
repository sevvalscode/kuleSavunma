using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace kuleSavunma
{
    public partial class Form1 : Form
    {
        // ==================================================
        // 1. DEĞİŞKENLER
        // ==================================================
        List<Point> yolNoktalari = new List<Point>();
        List<Canavar> canavarlar = new List<Canavar>();
        List<string> dalgaKuyrugu = new List<string>();
        List<Kule> kuleler = new List<Kule>();

        // YENİ: Ekranda görünen mermi/lazer efektleri
        List<AtisEfekti> aktifEfektler = new List<AtisEfekti>();

        System.Windows.Forms.Timer oyunTimer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer dogmaTimer = new System.Windows.Forms.Timer();

        int baslangicCani = 20;
        int baslangicParasi = 600;

        int oyuncuCan;
        int oyuncuPara;
        int dalgaSayisi = 0;
        bool oyunBasladiMi = false;

        string secilenKuleTuru = "";
        Kule hoverlananKule = null;
        int skor = 0; // Skor değişkeni

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true; // Titremeyi önler
        }

        // ==================================================
        // 2. YÜKLEME VE ÇİZİM (PAINT)
        // ==================================================
        private void Form1_Load(object sender, EventArgs e)
        {
            this.MouseMove += new MouseEventHandler(Form1_MouseMove);
            this.Paint += new PaintEventHandler(Form1_Paint);

            // KOORDİNATLAR
            yolNoktalari.Clear();
            yolNoktalari.Add(new Point(989, 241));
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
            yolNoktalari.Add(new Point(444, 694));

            oyuncuCan = baslangicCani;
            oyuncuPara = baslangicParasi;
            ArayuzGuncelle();

            oyunTimer.Interval = 25;
            oyunTimer.Tick += OyunTimer_Tick;

            dogmaTimer.Interval = 1500;
            dogmaTimer.Tick += DogmaTimer_Tick;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            // Sadece bir kule seçiliyse ekranı yenile, aksi takdirde işlem yapma.
            // Ayrıca performans için sadece kule yerleştirme modundayken çalışsın.
            if (secilenKuleTuru != "")
            {
                this.Invalidate();
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // 1. MENZİL GÖSTERGELERİ
            if (secilenKuleTuru != "")
            {
                int menzil = (secilenKuleTuru == "OkKulesi") ? 150 : (secilenKuleTuru == "BuyuKulesi" ? 120 : 100);
                Point mouseYeri = this.PointToClient(Cursor.Position);
                MenzilCiz(g, mouseYeri, menzil, Color.White);
            }
            if (hoverlananKule != null)
            {
                Point kuleMerkezi = new Point(hoverlananKule.Resim.Location.X + 45, hoverlananKule.Resim.Location.Y + 45);
                MenzilCiz(g, kuleMerkezi, hoverlananKule.Menzil, Color.Cyan);
            }


            // GÜNCEL KOD (Her efektin kendi rengini kullanır)
            foreach (var efekt in aktifEfektler)
            {
                // efekt.Renk özelliğini kullanarak kalemi oluşturuyoruz
                using (Pen lazerKalemi = new Pen(efekt.Renk, 3))
                {
                    g.DrawLine(lazerKalemi, efekt.Baslangic, efekt.Bitis);
                }
            }

            // 3. YENİ: CAN BARLARINI ÇİZ
            foreach (var c in canavarlar)
            {
                // Canavarın hemen üstüne
                int barX = c.Resim.Location.X;
                int barY = c.Resim.Location.Y - 10;
                int barGenislik = c.Resim.Width;
                int barYukseklik = 5;

                // Arkaplan (Kırmızı)
                g.FillRectangle(Brushes.Red, barX, barY, barGenislik, barYukseklik);

                // Kalan Can (Yeşil)
                float oran = (float)c.Can / (float)c.BaslangicCani;
                if (oran < 0) oran = 0;
                int yesilGenislik = (int)(barGenislik * oran);

                g.FillRectangle(Brushes.LimeGreen, barX, barY, yesilGenislik, barYukseklik);

                // İnce siyah çerçeve
                g.DrawRectangle(Pens.Black, barX, barY, barGenislik, barYukseklik);
            }
        }

        private void MenzilCiz(Graphics g, Point merkez, int menzil, Color renk)
        {
            Pen kalem = new Pen(renk, 2);
            kalem.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            SolidBrush firca = new SolidBrush(Color.FromArgb(60, renk.R, renk.G, renk.B));
            Rectangle alan = new Rectangle(merkez.X - menzil, merkez.Y - menzil, menzil * 2, menzil * 2);
            g.FillEllipse(firca, alan);
            g.DrawEllipse(kalem, alan);
        }

        // ==================================================
        // 3. OYUN MANTIĞI (GÜNCELLENDİ)
        // ==================================================
        private void OyunTimer_Tick(object sender, EventArgs e)
        {
            // A. KULELER SALDIRSIN
            foreach (var kule in kuleler)
            {
                // Artık efekt listesini de gönderiyoruz
                kule.Saldir(canavarlar, aktifEfektler);
            }

            // B. EFEKTLERİ TEMİZLE (Kısa süre sonra kaybolsunlar)
            for (int i = aktifEfektler.Count - 1; i >= 0; i--)
            {
                aktifEfektler[i].Omur--;
                if (aktifEfektler[i].Omur <= 0) aktifEfektler.RemoveAt(i);
            }

            // C. CANAVARLAR
            for (int i = canavarlar.Count - 1; i >= 0; i--)
            {
                Canavar c = canavarlar[i];

                // ÖLÜM KONTROLÜ
                if (c.Can <= 0)
                {
                    // ALTIN BUG'I ÇÖZÜMÜ: Sadece burada ve bir kere eklenir.
                    oyuncuPara += c.AltinDegeri;
                    skor += 10;

                    this.Controls.Remove(c.Resim);
                    canavarlar.RemoveAt(i);
                    ArayuzGuncelle();
                    continue;
                }

                // HAREKET
                if (c.HedefNoktaIndeksi >= yolNoktalari.Count)
                {
                    oyuncuCan--;
                    if (oyuncuCan < 0) oyuncuCan = 0;
                    this.Controls.Remove(c.Resim);
                    canavarlar.RemoveAt(i);
                    ArayuzGuncelle();
                    if (oyuncuCan == 0) OyunBitti("KRALLIK DÜŞTÜ! KAYBETTİNİZ.", "TEKRAR DENE");
                    continue;
                }

                Point hedef = yolNoktalari[c.HedefNoktaIndeksi];
                if (c.Resim.Left < hedef.X) c.Resim.Left += c.Hiz; else if (c.Resim.Left > hedef.X) c.Resim.Left -= c.Hiz;
                if (c.Resim.Top < hedef.Y) c.Resim.Top += c.Hiz; else if (c.Resim.Top > hedef.Y) c.Resim.Top -= c.Hiz;
                if (Math.Abs(c.Resim.Left - hedef.X) < 15 && Math.Abs(c.Resim.Top - hedef.Y) < 15) c.HedefNoktaIndeksi++;
            }

            // Ekrana çizilenleri (Lazer, Can Barı) yenilemek için
            this.Invalidate();

            // D. DALGA
            if (dalgaKuyrugu.Count == 0 && canavarlar.Count == 0 && oyunBasladiMi)
            {
                if (dalgaSayisi < 6) DalgaBaslat(dalgaSayisi + 1);
                else OyunBitti("TEBRİKLER! TÜM DALGALARI PÜSKÜRTTÜN!", "YENİDEN OYNA");
            }
        }

        // ==================================================
        // 4. ETKİLEŞİM VE YARDIMCI METOTLAR
        // ==================================================
        private void btnKuleOkcu_Click(object sender, EventArgs e) { KuleSec("OkKulesi"); }
        private void btnKuleBuyu_Click(object sender, EventArgs e) { KuleSec("BuyuKulesi"); }
        private void btnKuleTop_Click(object sender, EventArgs e) { KuleSec("TopKulesi"); }
        private void KuleSec(string tur) { secilenKuleTuru = tur; this.Cursor = Cursors.Hand; }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (secilenKuleTuru == "") return;
            Kule yeniKule = null;
            if (secilenKuleTuru == "OkKulesi" && oyuncuPara >= 100) yeniKule = new OkKulesi();
            else if (secilenKuleTuru == "BuyuKulesi" && oyuncuPara >= 150) yeniKule = new BuyuKulesi();
            else if (secilenKuleTuru == "TopKulesi" && oyuncuPara >= 200) yeniKule = new TopKulesi();
            else if (secilenKuleTuru == "LazerKulesi" && oyuncuPara >= 250) yeniKule = new LazerKulesi();
                if (yeniKule != null)
            {
                yeniKule.Resim.Location = new Point(e.X - 45, e.Y - 45);
                this.Controls.Add(yeniKule.Resim);
                yeniKule.Resim.BringToFront();
                kuleler.Add(yeniKule);

                yeniKule.Resim.MouseEnter += (s, args) => { hoverlananKule = yeniKule; this.Invalidate(); };
                yeniKule.Resim.MouseLeave += (s, args) => { hoverlananKule = null; this.Invalidate(); };

                oyuncuPara -= yeniKule.Fiyat;
                ArayuzGuncelle();
                secilenKuleTuru = "";
                this.Cursor = Cursors.Default;
                this.Invalidate();
            }
           
          
            else
            {
                MessageBox.Show("Yeterli paran yok!");
                secilenKuleTuru = "";
                this.Cursor = Cursors.Default;
                this.Invalidate();
            }
        }

        private void btnBaslat_Click(object sender, EventArgs e)
        {
            if (!oyunBasladiMi)
            {
                oyunBasladiMi = true;

                // --- DÜZELTME BAŞLANGICI ---
                // Eğer bu bir "YENİDEN BAŞLATMA" ise her şeyi sıfırla.
                if (btnBaslat.Text == "TEKRAR DENE" || btnBaslat.Text == "YENİDEN OYNA")
                {
                    oyuncuCan = baslangicCani;
                    oyuncuPara = baslangicParasi;

                    // Yeniden başlatırken ekrandaki eski kuleleri de silmeliyiz!
                    foreach (var kule in kuleler)
                    {
                        this.Controls.Remove(kule.Resim);
                    }
                    kuleler.Clear();
                }
                else
                {
                    // Oyun İLK KEZ başlıyorsa parayı SIFIRLAMA. 
                    // Kullanıcı kule koyup parayı harcamış olabilir, olduğu gibi kalsın.
                }
                // --- DÜZELTME BİTİŞİ ---

                dalgaSayisi = 0;

                foreach (var c in canavarlar) this.Controls.Remove(c.Resim);
                canavarlar.Clear();
                dalgaKuyrugu.Clear();
                aktifEfektler.Clear();

                ArayuzGuncelle();
                btnBaslat.Visible = false;
                oyunTimer.Start();
                dogmaTimer.Start();
                DalgaBaslat(1);
            }
        }

        private void DalgaBaslat(int dalga)
        {
            dalgaSayisi = dalga;
            dalgaKuyrugu.Clear();
            if (dalga == 1) for (int i = 0; i < 5; i++) dalgaKuyrugu.Add("AtesRuhu");
            else if (dalga == 2) { for (int i = 0; i < 5; i++) dalgaKuyrugu.Add("AtesRuhu"); dalgaKuyrugu.Add("Golem"); dalgaKuyrugu.Add("Golem"); }
            else if (dalga == 3) for (int i = 0; i < 5; i++) dalgaKuyrugu.Add("Golem");
            else if (dalga == 4) { dalgaKuyrugu.Add("Golem"); dalgaKuyrugu.Add("Ejderha"); dalgaKuyrugu.Add("Golem"); dalgaKuyrugu.Add("Ejderha"); dalgaKuyrugu.Add("Golem"); dalgaKuyrugu.Add("Ejderha"); }
            else if (dalga == 5) for (int i = 0; i < 4; i++) dalgaKuyrugu.Add("Ejderha");
            else if (dalga == 6) { for (int i = 0; i < 5; i++) dalgaKuyrugu.Add("AtesRuhu"); for (int i = 0; i < 3; i++) dalgaKuyrugu.Add("Golem"); for (int i = 0; i < 2; i++) dalgaKuyrugu.Add("Ejderha"); }
            ArayuzGuncelle();
        }

        private void DogmaTimer_Tick(object sender, EventArgs e)
        {
            if (dalgaKuyrugu.Count <= 0) return;
            Canavar yeni = new Canavar(dalgaKuyrugu[0]);
            yeni.Resim.Location = yolNoktalari[0];
            this.Controls.Add(yeni.Resim);
            yeni.Resim.BringToFront();
            canavarlar.Add(yeni);
            dalgaKuyrugu.RemoveAt(0);
        }

        private void OyunBitti(string mesaj, string butonYazisi)
        {
            oyunTimer.Stop();
            dogmaTimer.Stop();
            oyunBasladiMi = false;
            btnBaslat.Text = butonYazisi;
            btnBaslat.Visible = true;
            btnBaslat.Enabled = true;
            MessageBox.Show(mesaj);
        }

        private void ArayuzGuncelle()
        {
            try
            {
                lblCan.Text = oyuncuCan.ToString();
                lblPara.Text = oyuncuPara.ToString();
                lblDalga.Text = dalgaSayisi.ToString();
                lblSkor.Text = "Skor: " + skor.ToString();
            }
            catch { }
        }

        // --- BOŞ METOTLAR (SİLME) ---
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void panel2_Paint(object sender, PaintEventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void panel4_Paint(object sender, PaintEventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            KuleSec("LazerKulesi");
        }
    }
}