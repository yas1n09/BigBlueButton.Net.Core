using BigBlueButton.Net.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBlueButton.Net.Core.DTOs.RecordingDto
{
    public class RecordingTextTracksDto
    {
        public string RecordID { get; set; }
        public List<RecordingTrack> TextTracks { get; set; }
        public string Message { get; set; }
    }
}
