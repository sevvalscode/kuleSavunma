using System;
using System.Drawing;
using System.Windows.Forms;

namespace kuleSavunma
{
    public class Canavar
    {
        public int Can { get; set; }
        public int Hiz { get; set; }
        public int AltinDegeri { get; set; }
        public string TurAdi { get; set; }

        public PictureBox Resim { get; set; }
        public int HedefNoktaIndeksi { get; set; }

        public bool OlduMu { get { return Can <= 0; } }

        public Canavar(string tur)
        {
            this.TurAdi = tur;
            this.HedefNoktaIndeksi = 0;

            Resim = new PictureBox();
            Resim.SizeMode = PictureBoxSizeMode.StretchImage;
            Resim.BackColor = Color.Transparent;

            switch (tur)
            {
                case "AtesRuhu":
                    this.Can = 20;
                    this.Hiz = 6;
                    this.AltinDegeri = 10;
                    Resim.Size = new Size(30, 30);
                    // DÜZELTİLDİ: Resources.Designer.cs içindeki gerçek isim
                    Resim.Image = Properties.Resources.atesruhu;
                    break;

                case "Golem":
                    this.Can = 100;
                    this.Hiz = 2;
                    this.AltinDegeri = 30;
                    Resim.Size = new Size(50, 50);
                    // DÜZELTİLDİ: Resources.Designer.cs içindeki gerçek isim
                    Resim.Image = Properties.Resources.golem;
                    break;

                case "Ejderha":
                    this.Can = 250;
                    this.Hiz = 3;
                    this.AltinDegeri = 100;
                    Resim.Size = new Size(70, 60);
                    // DÜZELTİLDİ: Resources.Designer.cs içindeki gerçek isim
                    Resim.Image = Properties.Resources.ejderha;
                    break;
            }
        }

        public void HasarAl(int hasar)
        {
            this.Can -= hasar;
        }
    }
}