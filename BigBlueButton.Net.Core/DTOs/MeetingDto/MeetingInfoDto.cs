using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBlueButton.Net.Core.DTOs.MeetingDto
{
    public class MeetingInfoDto
    {
        public string MeetingName { get; set; }
        public string MeetingID { get; set; }
        public string InternalMeetingID { get; set; }
        public long? CreateTime { get; set; }
        public string CreateDate { get; set; }
        public int? VoiceBridge { get; set; }
        public string DialNumber { get; set; }
        public string AttendeePW { get; set; }
        public string ModeratorPW { get; set; }
        public bool? Running { get; set; }
        public int? Duration { get; set; }
        public bool? HasUserJoined { get; set; }
        public bool? Recording { get; set; }
        public bool? HasBeenForciblyEnded { get; set; }
        public long? StartTime { get; set; }
        public long? EndTime { get; set; }
        public int? ParticipantCount { get; set; }
        public int? ListenerCount { get; set; }
        public int? VoiceParticipantCount { get; set; }
        public int? VideoCount { get; set; }
        public int? MaxUsers { get; set; }
        public int? ModeratorCount { get; set; }
        public bool? IsBreakout { get; set; }
        public List<string> BreakoutRooms { get; set; }
        public string ParentMeetingID { get; set; }
        public int? Sequence { get; set; }
        public bool? FreeJoin { get; set; }
        public string Message { get; set; }
    }
}
