using BigBlueButton.Net.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BigBlueButton.Net.Core.Entities
{
    public class MeetingInfo
    {
        public string meetingName { get; set; } // Toplantının adı.

        public string meetingID { get; set; } // Toplantı kimliği.

        public string internalMeetingID { get; set; } // Sistem tarafından kullanılan dahili toplantı kimliği.

        public long? createTime { get; set; } // Toplantı oluşturma zamanı.
        public string createDate { get; set; } // Toplantı oluşturma tarihi.

        public int? voiceBridge { get; set; }
        // Toplantıya bağlı FreeSWITCH ses konferansı için ses köprüsü numarası.
        // Bu, 10000 ile 99999 arasında 5 basamaklı bir sayı olmalıdır.
        // FreeSWITCH'te arama planını düzenleyerek bu aralığı değiştirebilirsiniz.
        // Her toplantı için bu numaranın farklı olması gerekir.

        public string dialNumber { get; set; }
        // Katılımcıların düzenli telefon ile bağlanabileceği erişim numarası.
        // Varsayılan bir numara `bigbluebutton.properties` dosyasından ayarlanabilir.

        public string attendeePW { get; set; }
        // Katılımcıların toplantıya izleyici olarak katılması için sağlanacak parola.
        // Eğer belirtilmezse, sistem rastgele bir parola oluşturur.

        public string moderatorPW { get; set; } // Moderatörlerin toplantıya katılmak için kullandığı parola.

        public bool? running { get; set; } // Toplantı şu anda çalışıyor mu?
        public int? duration { get; set; } // Toplantının süresi (dakika).
        public bool? hasUserJoined { get; set; } // Kullanıcı toplantıya katıldı mı?
        public bool? recording { get; set; } // Toplantı kaydediliyor mu?
        public bool? hasBeenForciblyEnded { get; set; } // Toplantı zorla sona erdirildi mi?
        public long? startTime { get; set; } // Toplantının başlama zamanı.
        public long? endTime { get; set; } // Toplantının bitiş zamanı.
        public int? participantCount { get; set; } // Katılımcı sayısı.
        public int? listenerCount { get; set; } // Sadece dinleyici modunda olanların sayısı.
        public int? voiceParticipantCount { get; set; } // Sesli katılımcı sayısı.
        public int? videoCount { get; set; } // Video kullanan katılımcı sayısı.
        public int? maxUsers { get; set; } // Toplantıya katılabilecek maksimum kullanıcı sayısı.
        public int? moderatorCount { get; set; } // Moderatör sayısı.

        [XmlArrayItem("attendee")]
        public List<Attendee> attendees { get; set; } // Katılımcıların listesi.

        public MetaData metadata { get; set; } // Toplantıya ait meta veriler.

        public bool? isBreakout { get; set; } // Toplantı bir grup odası mı?

        public List<string> breakoutRooms { get; set; }
        // Toplantı grup odaları oluşturduysa, grup odalarına ait kimliklerin listesi.

        public string parentMeetingID { get; set; }
        // Toplantı bir grup odasıysa, ana toplantının dahili kimliği.

        public int? sequence { get; set; }
        // Grup odası ise, grup odasının sırası.

        public bool? freeJoin { get; set; }
        // Grup odası ise, serbest katılıma izin verilip verilmediği.

    }
}
