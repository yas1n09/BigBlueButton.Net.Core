using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBlueButton.Net.Core.DTOs.RecordingDto
{
    public class PauseRecordingDto
    {
        public string MeetingID { get; set; }
        public bool IsPaused { get; set; }
        public string Message { get; set; }
    }
}
