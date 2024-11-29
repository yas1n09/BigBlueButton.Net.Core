using BigBlueButton.Net.Core.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBlueButton.Net.Core.Requests
{
    public class SetConfigXMLRequest : BaseRequest
    {
        // Aktif bir toplantıya ait meetingID
        public string meetingID { get; set; }

        // Geçerli bir config.xml dosyasının içeriği
        public string configXML { get; set; }
    }

}
