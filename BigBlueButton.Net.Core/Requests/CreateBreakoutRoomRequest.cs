using BigBlueButton.Net.Core.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBlueButton.Net.Core.Requests
{
    public class CreateBreakoutRoomRequest : CreateMeetingRequest
    {
        public string parentMeetingID { get; set; }  // Breakout Room’a özel alan
        public int? sequence { get; set; }
    }
}
