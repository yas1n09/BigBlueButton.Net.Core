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
    public class JoinMeetingResponse : BaseResponse
    {
        
        [XmlElement("meeting_id")]
        public string internalMeetingID { get; set; }

        
        [XmlElement("user_id")]
        public string userID { get; set; }

        
        public string JoinUrl { get; set; }

        public bool Redirect { get; set; }

        public string Message { get; set; }
    }

}
