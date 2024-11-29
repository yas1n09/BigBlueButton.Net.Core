using BigBlueButton.Net.Core.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBlueButton.Net.Core.Requests
{
    public class GetMeetingInfoRequest : BaseRequest
    {
        // Gerekli.
        // Kontrol etmek istediğiniz toplantıyı tanımlayan toplantı kimliği.
        public string meetingID { get; set; }
    }

}
