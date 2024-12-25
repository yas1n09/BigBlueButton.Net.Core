using BigBlueButton.Net.Core.Entities;
using BigBlueButton.Net.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBlueButton.Net.Core.DTOs.RecordingDto
{
    public class RecordingDetailsDto
    {
        public string RecordID { get; set; }
        public string MeetingID { get; set; }
        public string InternalMeetingID { get; set; }
        public string Name { get; set; }
        public bool IsBreakout { get; set; }
        public bool Published { get; set; }
        public string State { get; set; }
        public long StartTime { get; set; }
        public long EndTime { get; set; }
        public int Participants { get; set; }
        public int RawSize { get; set; }
        public int Size { get; set; }
        public List<Playback> Playbacks { get; set; }
        public MetaData Metadata { get; set; }
    }
}
