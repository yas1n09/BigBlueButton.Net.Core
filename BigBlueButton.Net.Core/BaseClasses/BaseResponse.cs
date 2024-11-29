using BigBlueButton.Net.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BigBlueButton.Net.Core.BaseClasses
{
    [XmlRoot("response")]
    public class BaseResponse
    {
        public Returncode returncode { get; set; }
        // İşlevin başarılı olup olmadığını belirten durum kodu.

        public string messageKey { get; set; }
        // Mesajla benzer işlevsellik sağlar ve aynı kuralları takip eder. 
        // Ancak, mesaj anahtarı çok daha kısa olacak ve genellikle API'nin yaşamı boyunca aynı kalacaktır, oysa mesaj zamanla değişebilir.
        // Üçüncü taraf uygulamanız standart mesajları uluslararasılaştırmak veya değiştirmek isterse, bu messageKey'e dayalı olarak kendi özel mesajlarını arayabilirsiniz.

        public string message { get; set; }
        // Çağrının durumu hakkında ek bilgi veren bir mesaj.
        // Eğer returncode "FAILED" (başarısız) ise, her zaman bir mesaj parametresi döndürülecektir.
        // Ayrıca, bazı durumlarda returncode "SUCCESS" (başarılı) olsa bile ek bilgi verilmesi yararlı olursa bir mesaj döndürülebilir.

    }
}
