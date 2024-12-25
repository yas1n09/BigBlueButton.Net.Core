using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBlueButton.Net.Core.DTOs.MeetingDto
{
    public class MeetingCreateDto
    {
        public string MeetingID { get; set; }
        public string InternalMeetingID { get; set; }
        public string ParentMeetingID { get; set; }
        public string AttendeePW { get; set; }
        public string ModeratorPW { get; set; }
        public long? CreateTime { get; set; }
        public int? VoiceBridge { get; set; }
        public string DialNumber { get; set; }
        public string CreateDate { get; set; }
        public bool? HasUserJoined { get; set; }
        public int? Duration { get; set; }
        public bool? HasBeenForciblyEnded { get; set; }
        public string Message { get; set; }
    }
}
