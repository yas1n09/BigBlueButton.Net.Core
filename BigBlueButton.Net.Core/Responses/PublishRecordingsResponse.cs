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
    public class PublishRecordingsResponse : BaseResponse
    {
        // Kaydın yayınlanıp yayınlanmadığını belirten durum.
        public bool? published { get; set; }
    }

}
