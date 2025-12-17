using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace kuleSavunma
{
    // Efekt yapısına RENK özelliğini ekledik
    public class AtisEfekti
    {
        public Point Baslangic;
        public Point Bitis;
        public int Omur;
        public Color Renk; // YENİ: Hangi renk olacak?
    }

    public abstract class Kule : ISaldirabilir
    {
        public int Hasar { get; set; }
        public int Menzil { get; set; }
        public int Fiyat { get; set; }

        public DateTime SonAtisZamani { get; set; }
        public int AtisHizi { get; set; }

        public PictureBox Resim { get; set; }

        public Kule(int hasar, int menzil, int fiyat)
        {
            this.Hasar = hasar;
            this.Menzil = menzil;
            this.Fiyat = fiyat;
            this.SonAtisZamani = DateTime.Now;
        }

        public abstract void Saldir(List<Canavar> canavarlar, List<AtisEfekti> efektler);
    }
}