using BigBlueButton.Net.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BigBlueButton.Net.Core.Entities
{
    public class Playback
    {
        public string type { get; set; } // İçeriğin türü.

        public string url { get; set; } // İçeriğin URL adresi.

        public int processingTime { get; set; } // İşleme süresi (milisaniye cinsinden).

        public int length { get; set; } // İçeriğin uzunluğu (örneğin, saniye cinsinden).

        public int size { get; set; } // İçeriğin boyutu (byte cinsinden).

        [XmlElement("preview")]
        public PlaybackPreviewImages previewImages { get; set; } // Önizleme görüntüleri.

    }
}
