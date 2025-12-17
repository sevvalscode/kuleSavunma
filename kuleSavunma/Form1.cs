using System;
using System.Collections.Generic;
using System.Drawing; // Çizim araçları
using System.Windows.Forms;

namespace kuleSavunma
{
    public partial class Form1 : Form
    {
        // ==================================================
        // 1. OYUN DEĞİŞKENLERİ
        // ==================================================
        List<Point> yolNoktalari = new List<Point>();
        List<Canavar> canavarlar = new List<Canavar>();
        List<string> dalgaKuyrugu = new List<string>();
        List<Kule> kuleler = new List<Kule>();

        System.Windows.Forms.Timer oyunTimer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer dogmaTimer = new System.Windows.Forms.Timer();

        int baslangicCani = 20;
        int baslangicParasi = 600;

        int oyuncuCan;
        int oyuncuPara;
        int dalgaSayisi = 0;
        bool oyunBasladiMi = false;

        string secilenKuleTuru = ""; // Dikmek için seçilen kule

        // YENİ: Üzerine mouse ile gelinen dikili kule
        Kule hoverlananKule = null;

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true; // Titremeyi önle
        }

        // ==================================================
        // 2. OYUN BAŞLANGICI
        // ==================================================
        private void Form1_Load(object sender, EventArgs e)
        {
            // Olayları Bağla
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

            // Başlangıç
            oyuncuCan = baslangicCani;
            oyuncuPara = baslangicParasi;
            ArayuzGuncelle();

            oyunTimer.Interval = 25;
            oyunTimer.Tick += OyunTimer_Tick;

            dogmaTimer.Interval = 1500;
            dogmaTimer.Tick += DogmaTimer_Tick;
        }

        // ==================================================
        // 3. ÇİZİM VE MENZİL GÖSTERGESİ (GÜNCELLENDİ) 🎨
        // ==================================================

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            // Mouse hareket ettikçe ekranı yenile (Daire takibi için)
            if (secilenKuleTuru != "") this.Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // SENARYO 1: Yeni Kule Dikerken (BEYAZ DAİRE)
            if (secilenKuleTuru != "")
            {
                int menzil = 0;
                if (secilenKuleTuru == "OkKulesi") menzil = 150;
                else if (secilenKuleTuru == "BuyuKulesi") menzil = 120; // Varsa
                else if (secilenKuleTuru == "TopKulesi") menzil = 100; // Varsa

                if (menzil > 0)
                {
                    Point mouseYeri = this.PointToClient(Cursor.Position);
                    MenzilCiz(e.Graphics, mouseYeri, menzil, Color.White);
                }
            }

            // SENARYO 2: Dikili Kuleye Bakarken (MAVİ DAİRE)
            if (hoverlananKule != null)
            {
                // Kulenin tam ortasını bul (Kule 90x90 olduğu için yarısı 45)
                Point kuleMerkezi = new Point(
                    hoverlananKule.Resim.Location.X + 45,
                    hoverlananKule.Resim.Location.Y + 45
                );

                // Mavi renkle çiz
                MenzilCiz(e.Graphics, kuleMerkezi, hoverlananKule.Menzil, Color.Cyan);
            }
        }

        // Yardımcı Çizim Metodu (Kod tekrarını önlemek için)
        private void MenzilCiz(Graphics g, Point merkez, int menzil, Color renk)
        {
            Pen kalem = new Pen(renk, 2);
            kalem.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash; // Kesik çizgi

            // Yarı saydam dolgu rengi
            SolidBrush firca = new SolidBrush(Color.FromArgb(60, renk.R, renk.G, renk.B));

            Rectangle alan = new Rectangle(merkez.X - menzil, merkez.Y - menzil, menzil * 2, menzil * 2);

            g.FillEllipse(firca, alan); // İçini boya
            g.DrawEllipse(kalem, alan); // Çerçeve çiz
        }

        // ==================================================
        // 4. KULE DİKME VE ETKİLEŞİM
        // ==================================================

        private void btnKuleOkcu_Click(object sender, EventArgs e) { KuleSec("OkKulesi"); }
        private void btnKuleBuyu_Click(object sender, EventArgs e) { KuleSec("BuyuKulesi"); }
        private void btnKuleTop_Click(object sender, EventArgs e) { KuleSec("TopKulesi"); }

        private void KuleSec(string tur)
        {
            secilenKuleTuru = tur;
            this.Cursor = Cursors.Hand;
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (secilenKuleTuru == "") return;

            Kule yeniKule = null;

            // Fiyat ve Tür Kontrolü
            if (secilenKuleTuru == "OkKulesi" && oyuncuPara >= 100) yeniKule = new OkKulesi();
            else if (secilenKuleTuru == "BuyuKulesi" && oyuncuPara >= 150) yeniKule = new BuyuKulesi();
            else if (secilenKuleTuru == "TopKulesi" && oyuncuPara >= 200) yeniKule = new TopKulesi();

            if (yeniKule != null)
            {
                // Yerleştir
                yeniKule.Resim.Location = new Point(e.X - 45, e.Y - 45);
                this.Controls.Add(yeniKule.Resim);
                yeniKule.Resim.BringToFront();
                kuleler.Add(yeniKule);

                // --- İŞTE SİHİR BURADA: Kuleye dedektör takıyoruz ---
                // Mouse üzerine gelince:
                yeniKule.Resim.MouseEnter += (s, args) => {
                    hoverlananKule = yeniKule; // Bu kuleyi işaretle
                    this.Invalidate();         // Çizimi tetikle
                };

                // Mouse üzerinden gidince:
                yeniKule.Resim.MouseLeave += (s, args) => {
                    hoverlananKule = null;     // İşareti kaldır
                    this.Invalidate();         // Çizimi temizle
                };
                // ----------------------------------------------------

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

        // ==================================================
        // 5. OYUN MOTORU (AYNI)
        // ==================================================
        private void OyunTimer_Tick(object sender, EventArgs e)
        {
            foreach (var kule in kuleler) kule.Saldir(canavarlar);

            for (int i = canavarlar.Count - 1; i >= 0; i--)
            {
                Canavar c = canavarlar[i];
                if (c.Can <= 0)
                {
                    oyuncuPara += c.AltinDegeri;
                    this.Controls.Remove(c.Resim);
                    canavarlar.RemoveAt(i);
                    ArayuzGuncelle();
                    continue;
                }
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

            if (dalgaKuyrugu.Count == 0 && canavarlar.Count == 0 && oyunBasladiMi)
            {
                if (dalgaSayisi < 6) DalgaBaslat(dalgaSayisi + 1);
                else OyunBitti("TEBRİKLER! TÜM DALGALARI PÜSKÜRTTÜN!", "YENİDEN OYNA");
            }
        }

        // ==================================================
        // 6. YARDIMCI METOTLAR
        // ==================================================
        private void btnBaslat_Click(object sender, EventArgs e)
        {
            if (!oyunBasladiMi)
            {
                oyunBasladiMi = true;
                oyuncuCan = baslangicCani;
                oyuncuPara = baslangicParasi;
                dalgaSayisi = 0;
                foreach (var c in canavarlar) this.Controls.Remove(c.Resim);
                canavarlar.Clear();
                dalgaKuyrugu.Clear();
                // Kuleler silinmiyor
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
            try { lblCan.Text = oyuncuCan.ToString(); lblPara.Text = oyuncuPara.ToString(); lblDalga.Text = dalgaSayisi.ToString(); } catch { }
        }

        // --- TASARIM İÇİN BOŞ METOTLAR (SİLME) ---
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void panel2_Paint(object sender, PaintEventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void panel4_Paint(object sender, PaintEventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
    }
}