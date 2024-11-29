using BigBlueButton.Net.Core.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBlueButton.Net.Core.Requests
{
    public class EndMeetingRequest : BaseRequest
    {
        // Bitişini istediğiniz toplantıyı tanımlayan toplantı ID'si.
        public string meetingID { get; set; }

        // Bu toplantının moderatör şifresi. Toplantıyı bitirmek için katılımcı şifresi kullanılamaz.
        public string password { get; set; }
    }

}
