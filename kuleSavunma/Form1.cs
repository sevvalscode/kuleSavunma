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

        List<AtisEfekti> aktifEfektler = new List<AtisEfekti>();

        System.Windows.Forms.Timer oyunTimer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer dogmaTimer = new System.Windows.Forms.Timer();

        int baslangicCani = 10;
        int baslangicParasi = 350;
        const int TOPLAM_DALGA = 6;
        int oyuncuCan;
        int oyuncuPara;
        int dalgaSayisi = 0;
        bool oyunBasladiMi = false;

        string secilenKuleTuru = "";
        Kule hoverlananKule = null;
        int skor = 0;
        bool oyunDuraklatildi = false;

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
            // Kule yerleştirme modu varsa zaten ekran yenileniyor
            if (secilenKuleTuru != "")
            {
                this.Invalidate();
                return;
            }

            // Mouse'un altında bir kule var mı diye kontrol et
            bool birKuleyeDegdi = false;
            Kule bulunanKule = null;

            foreach (var kule in kuleler)
            {
                if (kule.Resim.Bounds.Contains(e.Location))
                {
                    birKuleyeDegdi = true;
                    bulunanKule = kule;
                    break;
                }
            }

            if (birKuleyeDegdi)
            {
                this.Cursor = Cursors.Hand;
                hoverlananKule = bulunanKule;
            }
            else
            {
                this.Cursor = Cursors.Default;
                hoverlananKule = null;
            }

            // Sadece durum değiştiyse ekranı yenile (Performans için)
            this.Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // --- 1. ÇİZİM LİSTESİ HAZIRLA ---
            // Tüm kuleleri ve canavarları ortak bir listeye alıyoruz
            var cizilecekler = new List<PictureBox>();

            foreach (var k in kuleler) cizilecekler.Add(k.Resim);
            foreach (var c in canavarlar) cizilecekler.Add(c.Resim);

            // --- 2. SIRALAMA (Z-ORDER) ---
            // Y (Top) değerine göre sırala. Yukarıdakiler önce, aşağıdakiler sonra çizilsin.
            // Böylece aşağıda duran nesne, yukarıdakinin önünde görünür (2.5D Efekti).
            cizilecekler.Sort((p1, p2) =>
            {
                int y1 = p1.Location.Y + p1.Height;
                int y2 = p2.Location.Y + p2.Height;
                return y1.CompareTo(y2);
            });

            // --- 3. ÇİZİM ---
            foreach (var p in cizilecekler)
            {
                // DrawImage gerçek şeffaflığı destekler!
                if (p.Image != null)
                {
                    g.DrawImage(p.Image, p.Bounds);
                }
            }

            // --- 4. MENZİL GÖSTERGELERİ (Mevcut kodun) ---
            if (secilenKuleTuru != "")
            {
                // ... (Senin mevcut menzil kodların buraya) ...
                int menzil = 100; // Örnek varsayılan
                if (secilenKuleTuru == "OkKulesi") menzil = 150;
                else if (secilenKuleTuru == "BuyuKulesi") menzil = 130;
                else if (secilenKuleTuru == "TopKulesi") menzil = 120;

                Point mouseYeri = this.PointToClient(Cursor.Position);
                MenzilCiz(g, mouseYeri, menzil, Color.White);
            }

            // Yere konmuş kule menzili
            if (hoverlananKule != null)
            {
                Point kuleMerkezi = new Point(
                    hoverlananKule.Resim.Location.X + (hoverlananKule.Resim.Width / 2),
                    hoverlananKule.Resim.Location.Y + (hoverlananKule.Resim.Height / 2));
                MenzilCiz(g, kuleMerkezi, hoverlananKule.Menzil, Color.Cyan);
            }

            // --- 5. EFEKTLER (Mevcut kodun) ---
            foreach (var efekt in aktifEfektler)
            {
                using (Pen lazerKalemi = new Pen(efekt.Renk, 3))
                {
                    g.DrawLine(lazerKalemi, efekt.Baslangic, efekt.Bitis);
                }
            }

            // --- 6. CAN BARLARI (Canavarların üstüne çizilmeli) ---
            foreach (var c in canavarlar)
            {
                int barX = c.Resim.Location.X;
                int barY = c.Resim.Location.Y - 10;
                int barGenislik = c.Resim.Width;
                int barYukseklik = 5;

                g.FillRectangle(Brushes.Red, barX, barY, barGenislik, barYukseklik);
                float oran = (float)c.Can / (float)c.BaslangicCani;
                if (oran < 0) oran = 0;
                int yesilGenislik = (int)(barGenislik * oran);
                g.FillRectangle(Brushes.LimeGreen, barX, barY, yesilGenislik, barYukseklik);
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
                    oyuncuPara += c.AltinDegeri;
                    skor += c.SkorDegeri;

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
            // YENİ: SAĞ TIK İLE İPTAL ETME
            if (e.Button == MouseButtons.Right)
            {
                if (secilenKuleTuru != "")
                {
                    secilenKuleTuru = "";
                    this.Cursor = Cursors.Default;
                    this.Invalidate(); // Ekranı temizle (menzil dairesi gitsin)
                    return;
                }
            }

            // Sol tık işlemleri (Mevcut mantık)
            if (secilenKuleTuru == "" || e.Button != MouseButtons.Left) return;

            Kule yeniKule = null;
            // Kule oluşturma mantığı aynı kalacak...
            if (secilenKuleTuru == "OkKulesi" && oyuncuPara >= 100) yeniKule = new OkKulesi();
            else if (secilenKuleTuru == "BuyuKulesi" && oyuncuPara >= 200) yeniKule = new BuyuKulesi(); 
            else if (secilenKuleTuru == "TopKulesi" && oyuncuPara >= 250) yeniKule = new TopKulesi();       
            else if (secilenKuleTuru == "LazerKulesi" && oyuncuPara >= 350) yeniKule = new LazerKulesi();

            if (yeniKule != null)
            {
                // Kuleyi ortalayarak koymak için -45 yaptık (resim boyutu 90-100 varsayılıyor)
                yeniKule.Resim.Location = new Point(e.X - 45, e.Y - 45);
               
                kuleler.Add(yeniKule);

                // Hover olayları
                yeniKule.Resim.MouseEnter += (s, args) => { hoverlananKule = yeniKule; this.Invalidate(); };
                yeniKule.Resim.MouseLeave += (s, args) => { hoverlananKule = null; this.Invalidate(); };

                oyuncuPara -= yeniKule.Fiyat;
                ArayuzGuncelle();

                // Kuleyi koyduktan sonra seçim devam etsin mi? Genelde etmez.
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
            // --- DURUM 1: OYUN BİTTİ VE RESETLEMEK İSTİYORSUN ---
            // Bu blok çalıştığında oyun hemen başlamaz, sadece "Hazırlık Moduna" döner.
            if (btnBaslat.Text == "TEKRAR DENE" || btnBaslat.Text == "YENİDEN OYNA")
            {
                // 1. Değişkenleri ve Parayı Sıfırla
                oyuncuCan = baslangicCani;
                oyuncuPara = baslangicParasi;
                dalgaSayisi = 0;
                oyunBasladiMi = false; // Oyun akışını durduruyoruz.

                // 2. Ekrandaki Eski Kuleleri Temizle
                foreach (var kule in kuleler)
                {
                    this.Controls.Remove(kule.Resim);
                }
                kuleler.Clear();

                // 3. Ekrandaki Eski Canavarları Temizle
                foreach (var c in canavarlar)
                {
                    this.Controls.Remove(c.Resim);
                }
                canavarlar.Clear();
                dalgaKuyrugu.Clear();
                aktifEfektler.Clear();

                // 4. Timer'ları Durdur (Garanti olsun)
                oyunTimer.Stop();
                dogmaTimer.Stop();

                // 5. Arayüzü Güncelle (Paranın geri geldiğini gör)
                ArayuzGuncelle();

                // 6. Butonu Hazır Hale Getir
                // Artık buton "BAŞLAT" oldu. Sen kulelerini koyduktan sonra buna basacaksın.
                btnBaslat.Text = "BAŞLAT";
                btnBaslat.Visible = true;
            }

            // --- DURUM 2: HAZIRLIK BİTTİ, OYUNU BAŞLATIYORSUN ---
            // Burası sadece buton "BAŞLAT" ise çalışır.
            else if (!oyunBasladiMi)
            {
                oyunBasladiMi = true;

                // Butonu gizle veya "Oyun Başladı" yap
                btnBaslat.Visible = false;

                // Zamanlayıcıları ve Dalgayı Başlat
                oyunTimer.Start();
                dogmaTimer.Start();
                DalgaBaslat(1); // 1. Dalga başlasın
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

            // BU SATIRI SİLDİK: this.Controls.Add(yeni.Resim); 
            // BU SATIRI DA SİLEBİLİRSİN: yeni.Resim.BringToFront(); (Artık gerek yok)

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
            if (lblCan != null) lblCan.Text = oyuncuCan.ToString();
            if (lblPara != null) lblPara.Text = oyuncuPara.ToString();

            if (lblDalga != null) lblDalga.Text = dalgaSayisi.ToString() + " / " + TOPLAM_DALGA.ToString();

            if (lblSkor != null) lblSkor.Text = skor.ToString();
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
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // P tuşuna veya ESC tuşuna basınca durdur/devam et
            if (e.KeyCode == Keys.P || e.KeyCode == Keys.Escape)
            {
                OyunDurdurDevamEt();
            }
        }

        private void OyunDurdurDevamEt()
        {
            if (!oyunBasladiMi) return; // Oyun başlamadıysa durdurma çalışmasın

            if (oyunDuraklatildi)
            {
                // Devam Et
                oyunTimer.Start();
                dogmaTimer.Start();
                oyunDuraklatildi = false;
                // Eğer buton eklediysen metnini güncelle: btnDurdur.Text = "DURDUR";
            }
            else
            {
                // Durdur
                oyunTimer.Stop();
                dogmaTimer.Stop();
                oyunDuraklatildi = true;
                // Eğer buton eklediysen metnini güncelle: btnDurdur.Text = "DEVAM ET";
            }
            this.Invalidate(); // "OYUN DURAKLATILDI" yazısını çizmek/silmek için
        }
        // Bu kod titremeyi (flickering) kökten çözer
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // WS_EX_COMPOSITED: Tüm kontrolleri ve formu birlikte çizer
                return cp;
            }
        }
     
    }
}