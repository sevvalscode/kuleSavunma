using System;
using System.Windows.Forms;

namespace kuleSavunma
{
    // Artık OkKulesi, babası olan Kule'nin tüm özelliklerine (Hasar, Fiyat vb.) sahip oldu.
    public class OkKulesi : Kule
    {
        // Yapıcı Metot: Ok kulesi yaratılırken babasına (base) şu değerleri gönderiyoruz:
        public OkKulesi() : base(15, 150, 100)
        {
            Resim = new PictureBox();
            Resim.Size = new Size(50, 50);
            Resim.SizeMode = PictureBoxSizeMode.StretchImage;
            Resim.BackColor = Color.Transparent;

            // İŞTE BURASI: Artık yorum satırı değil, gerçek kod!
            Resim.Image = Properties.Resources.OkKulesi;
        }

        // Babada "abstract" olan Saldir metodunu burada doldurmak ZORUNDAYIZ.
        // "override" kelimesi "Babadaki kuralı uyguluyorum" demektir.
        public override void Saldir()
        {
            // BURAYA SONRA KOD YAZACAĞIZ.
            // Şimdilik boş kalsın, hata vermesin yeter.
        }
    }
}
