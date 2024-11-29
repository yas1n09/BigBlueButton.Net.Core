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
    public class IsMeetingRunningResponse : BaseResponse
    {
        // Toplantının devam edip etmediğini belirten durum.
        public bool? running { get; set; }
    }

}
