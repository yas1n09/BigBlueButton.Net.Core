using BigBlueButton.Net.Core.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBlueButton.Net.Core.Requests
{
    public class GetRecordingTextTracksRequest : BaseRequest
    {
        // Tek bir kayıt ID'si sağlanarak, o kaydın mevcut altyazıları alınır. (Diğer kayıt API'lerinin aksine, burada virgülle ayrılmış birden fazla kayıt verilemez.)
        public string recordID { get; set; }
    }

}
