using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BigBlueButton.Net.Core.DTOs.BreakoutRoomDto
{
    [XmlRoot("BreakoutRoomParticipantDto")]
    public class BreakoutRoomParticipantDto
    {

        public string ParticipantID { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
    }
}
