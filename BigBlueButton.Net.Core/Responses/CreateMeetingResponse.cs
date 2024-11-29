using BigBlueButton.Net.Core.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BigBlueButton.Net.Core.Responses
{
    [XmlRoot("response")]
    public class CreateMeetingResponse : BaseResponse
    {
        // Toplantı kimliği.
        public string meetingID { get; set; }

        // Sistem tarafından kullanılan dahili toplantı kimliği.
        public string internalMeetingID { get; set; }

        // Ana toplantı kimliği.
        public string parentMeetingID { get; set; }

        // Katılımcıların, URL üzerinden katılacağı şifreyi belirten şifre.
        public string attendeePW { get; set; }

        // Moderatör şifresi.
        public string moderatorPW { get; set; }

        // Toplantının oluşturulma zamanı.
        public long? createTime { get; set; }

        // FreeSWITCH ses konferans numarası.
        public int? voiceBridge { get; set; }

        // Katılımcıların arayabileceği telefon numarası.
        public string dialNumber { get; set; }

        // Toplantının oluşturulma tarihi.
        public string createDate { get; set; }

        // Kullanıcıların katılıp katılmadığı durumu.
        public bool? hasUserJoined { get; set; }

        // Toplantının süresi.
        public int? duration { get; set; }

        // Toplantının zorla sonlandırılıp sonlandırılmadığı.
        public bool? hasBeenForciblyEnded { get; set; }
    }

}
