using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBlueButton.Net.Core.DTOs.ReportingDto
{
    public class MeetingStatsDto
    {
        public string MeetingID { get; set; }
        public int ParticipantCount { get; set; }
        public int ListenerCount { get; set; }
        public int VoiceParticipantCount { get; set; }
        public int VideoCount { get; set; }
        public int ModeratorCount { get; set; }
        public long StartTime { get; set; }
        public long EndTime { get; set; }
        public bool IsRunning { get; set; }
        public string Message { get; set; }
    }
}
