using System;
using System.Drawing;
using System.Windows.Forms;

namespace kuleSavunma
{
    public class Canavar
    {
        public int Can { get; set; }
        public int BaslangicCani { get; set; } 
        public int Hiz { get; set; }
        public int AltinDegeri { get; set; }
        public string TurAdi { get; set; }
        public int SkorDegeri { get; set; }

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
                    this.BaslangicCani = 20; 
                    this.Hiz = 7;
                    this.AltinDegeri = 10;
                    Resim.Size = new Size(40, 40);
                    this.SkorDegeri = 10;
                    Resim.Image = Properties.Resources.atesruhu;
                    break;

                case "Golem":
                    this.Can = 100;
                    this.BaslangicCani = 100; // YENİ
                    this.Hiz = 2;
                    this.AltinDegeri = 30;
                    this.SkorDegeri = 50;
                    Resim.Size = new Size(50, 50);
                    Resim.Image = Properties.Resources.golem;
                    break;

                case "Ejderha":
                    this.Can = 250;
                    this.BaslangicCani = 250; // YENİ
                    this.Hiz = 3;
                    this.AltinDegeri = 75;
                    this.SkorDegeri = 100;
                    Resim.Size = new Size(80, 80);
                    Resim.Image = Properties.Resources.ejderha;
                    break;
            }
        }
    }
}