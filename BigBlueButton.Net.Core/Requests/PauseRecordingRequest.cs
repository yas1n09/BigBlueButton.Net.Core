using BigBlueButton.Net.Core.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBlueButton.Net.Core.Requests
{
    public class PauseRecordingRequest : BaseRequest
    {
        public string meetingID { get; set; }
        public string moderatorPW { get; set; }
    }
}
