using BigBlueButton.Net.Core.BaseClasses;
using BigBlueButton.Net.Core.Entities;
using BigBlueButton.Net.Core.Enums;
using BigBlueButton.Net.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BigBlueButton.Net.Core.Responses
{
    [XmlRoot("response")]
    public class GetMeetingInfoResponse : MeetingInfo, IBaseResponse
    {
        // Fonksiyonun başarılı olup olmadığını belirten durum kodu.
        public Returncode returncode { get; set; }

        // Mesaj anahtarının, uluslararasılaştırma veya özel mesajlara göre değişebileceğini belirten açıklama.
        public string messageKey { get; set; }

        // İşlem durumuyla ilgili ek bilgi sağlayan açıklama mesajı.
        public string message { get; set; }
    }

}
