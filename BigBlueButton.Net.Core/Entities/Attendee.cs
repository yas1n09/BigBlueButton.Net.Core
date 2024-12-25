using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBlueButton.Net.Core.Entities
{
    public class Attendee
    {
        public string userID { get; set; } // Bu kullanıcının kimliği, uygulamanızın bu kişiyi tanımlamasına yardımcı olur.

        public string fullName { get; set; } // Bu kullanıcıyı diğer konferans katılımcılarına tanıtmak için kullanılacak tam ad.

        public string role { get; set; } // Kullanıcının rolü: VIEWER veya MODERATOR.

        public bool isPresenter { get; set; } // Kullanıcı bir sunucu mu?
        public bool isListeningOnly { get; set; } // Kullanıcı yalnızca dinleme modunda mı?
        public bool hasJoinedVoice { get; set; } // Kullanıcı sesli görüşmeye katıldı mı?
        public bool hasVideo { get; set; } // Kullanıcı video kullanıyor mu?
        public string clientType { get; set; } // Kullanıcının bağlandığı istemci türü.

        

    }
}
