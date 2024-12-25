using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBlueButton.Net.Core.DTOs.RecordingDto
{
    public class ResumeRecordingDto
    {
        public string MeetingID { get; set; }
        public bool IsResumed { get; set; }
        public string Message { get; set; }
    }
}
