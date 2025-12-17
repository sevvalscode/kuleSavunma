using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing; // Point ve Color kullanmak için
using System.Windows.Forms; // PictureBox kullanmak için

namespace kuleSavunma // (Senin proje adın neyse o kalabilir)
{
    public abstract class Kule
    {
      // Tüm kulelerin ortak özellikleri (Encapsulation )
        public int Hasar { get; set; }
        public int Menzil { get; set; }
        public int Fiyat { get; set; }

        // Kulene ait resim ve konumu tutacak özellikler
        public PictureBox Resim { get; set; }

        // YAPICI METOT (Constructor): Kule doğduğunda özellikleri ne olsun?
        public Kule(int hasar, int menzil, int fiyat)
        {
            this.Hasar = hasar;
            this.Menzil = menzil;
            this.Fiyat = fiyat;
        }

        // SALDIR METODU (Abstract): Her kule saldırır ama NASIL saldıracağı belli değil.
        // O yüzden bunu boş (abstract) bırakıyoruz, çocuklar kendisi dolduracak.
        public abstract void Saldir();
    }
}