using BigBlueButton.Net.Core.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BigBlueButton.Net.Core.Responses
{
    [XmlRoot("response")]
    public class SetConfigXMLResponse : BaseResponse
    {
        // Bu yanıt, yapılandırma dosyasının token bilgisini içerir.
        public string configToken { get; set; }
    }

}
