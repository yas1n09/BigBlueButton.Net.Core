using BigBlueButton.Net.Core.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBlueButton.Net.Core.Requests
{
    public class PutRecordingTextTrackRequest : BasePostFileRequest
    {
        // Tek bir kayıt ID'si, kullanılabilir altyazıları almak için. (Diğer kayıt API'lerinden farklı olarak, virgülle ayrılmış bir kayıt listesi sağlanamaz.)
        public string recordID { get; set; }

        // Metin izinin kullanım amacını belirtir. Detaylar için getRecordingTextTracks açıklamasına bakın. Bu belgede listelenmeyen bir değer kullanmak hata döndürecektir.
        public string kind { get; set; }

        // Metin izinin kullanım amacını belirtir. Detaylar için getRecordingTextTracks açıklamasına bakın. Bu belgede listelenmeyen bir değer kullanmak hata döndürecektir.
        public string lang { get; set; }

        // Metin izinin okunabilir bir etiketi. Belirtilmezse, sistem lang parametresinde belirtilen dilin adını içeren bir etiket otomatik olarak oluşturur.
        public string label { get; set; }
    }

}
