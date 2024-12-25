using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBlueButton.Net.Core.DTOs.MeetingDto
{
    public class MeetingStatusDto
    {
        public string MeetingID { get; set; }
        public bool IsRunning { get; set; }
        public string Message { get; set; }
    }
}
