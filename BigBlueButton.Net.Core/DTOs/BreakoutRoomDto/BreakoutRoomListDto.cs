using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BigBlueButton.Net.Core.DTOs.BreakoutRoomDto
{
    [XmlRoot("BreakoutRoomListDto")]
    public class BreakoutRoomListDto
    {
        public string Message { get; set; }
        public List<BreakoutRoomInfoDto> BreakoutRooms { get; set; }
    }
}
