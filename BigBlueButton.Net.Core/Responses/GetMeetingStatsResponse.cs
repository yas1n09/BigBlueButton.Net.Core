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
    public class GetMeetingStatsResponse : BaseResponse
    {
        

        public string returncode { get; set; }
        public string message { get; set; }
        public int participantCount { get; set; }


        public int listenerCount { get; set; }
        public int voiceParticipantCount { get; set; }
        public int videoCount { get; set; }
        public long startTime { get; set; }
        public long endTime { get; set; }
        public bool running { get; set; }
        

        public int moderatorCount { get; set; }
        public string meetingDuration { get; set; }

    }
}
