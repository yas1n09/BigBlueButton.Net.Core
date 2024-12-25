using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBlueButton.Net.Core.DTOs.ReportingDto
{
    public class ExportMeetingDataDto
    {
        public string MeetingID { get; set; }
        public byte[] Data { get; set; }
        public string FileName { get; set; }
        public string Message { get; set; }
    }
}
