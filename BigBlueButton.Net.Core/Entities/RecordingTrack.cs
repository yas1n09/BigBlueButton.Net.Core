using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBlueButton.Net.Core.Entities
{
    public class RecordingTrack
    {
        public string href { get; set; }
        // Bu metin izleme dosyasını indirmek için bir bağlantıdır. Format her zaman WebVTT (text/vtt mime tipi) olacaktır, bu da SRT formatına benzer.
        // İzleme zamanlaması, mevcut kayıt oynatma video ve ses dosyalarıyla eşleşecektir. Kaydın düzenlenmesi durumunda (giriş/çıkış işaretçileri ayarlandığında), canlı veya otomatik kaynaklardan gelen izleme dosyaları yeni zamanlamayla yeniden oluşturulacaktır.
        // Yüklenen izleme dosyaları düzenlenecektir, ancak bu düzenlemeler sırasında kayıt bölümleri kaldırılırsa veri kaybı yaşanabilir.

        public string kind { get; set; }
        // Metin izleme dosyasının kullanım amacını belirtir. Değer aşağıdaki değerlerden biri olacaktır: subtitles (altyazılar) veya captions (başlıklar).
        // Bu değerlerin anlamı HTML5 video öğesi tarafından tanımlanmıştır, ayrıntılar için MDN belgelerine bakınız. HTML5 spesifikasyonu şu anda kullanılmayan ek değerler tanımlamaktadır, ancak bunlar daha sonra eklenebilir.

        public string label { get; set; }
        // Metin izleme dosyasının insana okunabilir etiketidir. Bu, kayıt oynatma sırasında altyazı seçme listesinde görüntülenen dizedir.

        public string lang { get; set; }
        // Metin izleme dosyasının dili, bir dil etiketi olarak belirtilir. Format için RFC 5646'ya bakın ve dil altetiketi araması yaparak yardım alabilirsiniz.
        // Genellikle küçük harflerle yazılmış 2 veya 3 harfli bir dil kodu, isteğe bağlı olarak büyük harflerle yazılmış bir coğrafi bölge kodu (ülke kodu) ile takip edilir.

        public string source { get; set; }
        // İzleme dosyasının kaynağını belirtir. Değer aşağıdaki değerlerden biri olacaktır:
        //   live - BigBlueButton'da yapılan canlı altyazılardan türetilen bir izleme dosyası.
        //   automatic - Bilgisayar sesli tanıma kullanılarak otomatik olarak oluşturulan bir altyazı dosyası.
        //   upload - Üçüncü taraf tarafından yüklenen bir altyazı dosyası.

    }
}
