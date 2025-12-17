using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing; // Point ve Color kullanmak için
using System.Windows.Forms; // PictureBox kullanmak için


namespace kuleSavunma
{
    public abstract class Kule
    {
        public int Hasar { get; set; }
        public int Menzil { get; set; }
        public int Fiyat { get; set; }

        // YENİ: Kule en son ne zaman ateş etti? (Seri taramayı önlemek için)
        public DateTime SonAtisZamani { get; set; }

        // YENİ: İki atış arası bekleme süresi (Milisaniye)
        public int AtisHizi { get; set; }

        public PictureBox Resim { get; set; }

        public Kule(int hasar, int menzil, int fiyat)
        {
            this.Hasar = hasar;
            this.Menzil = menzil;
            this.Fiyat = fiyat;
            // Kule doğar doğmaz ateş etmeye hazır olsun
            this.SonAtisZamani = DateTime.Now;
        }

        // DEĞİŞİKLİK: Saldir metoduna "düşmanları görebilmesi için" listeyi gönderiyoruz
        public abstract void Saldir(List<Canavar> canavarlar);
    }
}