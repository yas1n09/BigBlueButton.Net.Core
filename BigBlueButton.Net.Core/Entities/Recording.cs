using BigBlueButton.Net.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BigBlueButton.Net.Core.Entities
{
    public class Recording
    {
        public string recordID { get; set; } // Kaydın kimliği.

        public string meetingID { get; set; } // Toplantı kimliği.

        public string internalMeetingID { get; set; } // Dahili toplantı kimliği.

        public string name { get; set; } // Kaydın adı.

        public bool isBreakout { get; set; } // Bu kayıt bir grup odasına mı ait?

        public bool published { get; set; } // Kayıt yayımlandı mı?

        public string state { get; set; } // Kayıt durumu.

        public long startTime { get; set; } // Kayıt başlangıç zamanı.

        public long endTime { get; set; } // Kayıt bitiş zamanı.

        public int participants { get; set; } // Kayda katılan toplam katılımcı sayısı.

        public int rawSize { get; set; } // Kaydın ham boyutu (byte cinsinden).

        public MetaData metadata { get; set; } // Kayda ait meta veriler.

        public int size { get; set; } // Kaydın boyutu (byte cinsinden).

        [XmlArray("playback")]
        [XmlArrayItem("format")]
        public List<Playback> playbacks { get; set; } // Kaydın oynatma formatlarının listesi.

        public RecordingData data { get; set; } // Kayda ait ek veriler.

    }
}
