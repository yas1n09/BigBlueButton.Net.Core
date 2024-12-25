using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBlueButton.Net.Core.DTOs.UserDto
{
    public class AddUserDto
    {
        public string MeetingID { get; set; }
        public string UserID { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
        public string JoinUrl { get; set; }
        public string Message { get; set; }
    }
}
