using BigBlueButton.Net.Core.BaseClasses;
using BigBlueButton.Net.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBlueButton.Net.Core.Requests
{
    public class GetRecordingsRequest : BaseRequest
    {
        // Opsiyonel: Kaydın alınacağı toplantı ID'si. Birden fazla toplantı ID'si virgülle ayrılabilir. Eğer belirtilmezse, tüm kayıtlar alınır.
        public string meetingID { get; set; }

        // Opsiyonel: Kayıt ID'si ile kayıtlar alınır. Virgülle ayrılmış birden fazla kayıt ID'si olabilir.
        public string recordID { get; set; }

        // Opsiyonel: Kayıtların durumunu filtrelemek için kullanılan parametre. Belirtilmezse, sadece yayınlanmış ve yayınlanmamış durumdaki kayıtlar alınır.
        public string state { get; set; }

        // Opsiyonel: Kayıtların filtrelenmesi için bir veya birden fazla meta veri değeri.
        public MetaData meta { get; set; }
    }

}
