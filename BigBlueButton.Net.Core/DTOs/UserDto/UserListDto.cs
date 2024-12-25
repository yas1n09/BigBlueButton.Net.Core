using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBlueButton.Net.Core.DTOs.UserDto
{
    public class UserListDto
    {
        public string MeetingID { get; set; }
        public List<UserDto> Attendees { get; set; }
        public string Message { get; set; }
    }
}
