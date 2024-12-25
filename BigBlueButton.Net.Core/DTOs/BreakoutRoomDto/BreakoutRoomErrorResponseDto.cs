using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BigBlueButton.Net.Core.DTOs.BreakoutRoomDto
{
    [XmlRoot("BreakoutRoomErrorResponseDto")]
    public class BreakoutRoomErrorResponseDto
    {
        public string Message { get; set; }
        public string Details { get; set; }
    }
}
