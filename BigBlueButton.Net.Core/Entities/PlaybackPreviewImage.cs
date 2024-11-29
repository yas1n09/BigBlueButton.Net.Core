using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BigBlueButton.Net.Core.Entities
{
    [XmlRoot("image")]
    public class PlaybackPreviewImage
    {
        [XmlAttribute]
        public string alt { get; set; } // Görüntünün alternatif metni.

        [XmlAttribute]
        public int height { get; set; } // Görüntünün yüksekliği (piksel cinsinden).

        [XmlAttribute]
        public int width { get; set; } // Görüntünün genişliği (piksel cinsinden).

        [XmlText]
        public string url { get; set; } // Görüntünün URL adresi.

    }
}
