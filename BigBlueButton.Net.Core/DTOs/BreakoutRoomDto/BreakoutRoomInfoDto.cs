using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BigBlueButton.Net.Core.DTOs.BreakoutRoomDto
{
    [XmlRoot("BreakoutRoomInfoDto")]
    public class BreakoutRoomInfoDto
    {
        public string MeetingID { get; set; }
        public string Name { get; set; }
        public int ParticipantCount { get; set; }
        public bool IsRunning { get; set; }
        public long? StartTime { get; set; }
        public long? EndTime { get; set; }
        public int MaxUsers { get; set; }
        public int ModeratorCount { get; set; }
        public int VoiceParticipantCount { get; set; }
        public int VideoCount { get; set; }
        public int ListenerCount { get; set; }
        public List<string> BreakoutRooms { get; set; }
    }
}
