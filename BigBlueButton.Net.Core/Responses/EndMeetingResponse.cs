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
    public class EndMeetingResponse : BaseResponse
    {
        // Bu sınıf, toplantıyı sonlandırma işleminin yanıtını temsil eder.
        // Yanıt, temel BaseResponse sınıfından miras alınmıştır ve herhangi bir ek alan içermez.
    }

}
