using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBlueButton.Net.Core.DTOs.UserDto
{
    public class RemoveUserDto
    {
        public string MeetingID { get; set; }
        public string UserID { get; set; }
        public bool IsRemoved { get; set; }
        public string Message { get; set; }
    }
}
