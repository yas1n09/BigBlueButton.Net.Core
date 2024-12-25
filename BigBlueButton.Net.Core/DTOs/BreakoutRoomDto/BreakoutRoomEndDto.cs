using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BigBlueButton.Net.Core.DTOs.BreakoutRoomDto
{
    [XmlRoot("BreakoutRoomEndDto")]
    public class BreakoutRoomEndDto
    {
        public string MeetingID { get; set; }
        public string Message { get; set; }
    }
}
