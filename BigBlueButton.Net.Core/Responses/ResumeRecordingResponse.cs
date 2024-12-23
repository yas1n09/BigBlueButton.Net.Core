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
    public class ResumeRecordingResponse : BaseResponse
    {
        public string meetingID { get; set; }
        public bool resumed { get; set; }  // Kaydın devam edip etmediğini belirten bayrak
    }
}
