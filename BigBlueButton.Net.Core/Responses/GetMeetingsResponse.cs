using BigBlueButton.Net.Core.BaseClasses;
using BigBlueButton.Net.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BigBlueButton.Net.Core.Responses
{
    [XmlRoot("response")]
    public class GetMeetingsResponse : BaseResponse
    {
        // Toplanan toplantı bilgilerini içeren bir liste.
        [XmlArrayItem("meeting")]
        public List<MeetingInfo> meetings { get; set; }
    }

}
