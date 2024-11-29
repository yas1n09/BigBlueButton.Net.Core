using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml;
using BigBlueButton.Net.Core.Entities;

namespace BigBlueButton.Net.Core.Helpers
{
    public class PlaybackPreviewImages : List<PlaybackPreviewImage>
    {
        public XmlSchema GetSchema()
        {
            return null; // Şema döndürülmez, null döner.
        }

        public void ReadXml(XmlReader reader)
        {
            if (reader.IsEmptyElement || reader.Read() == false) return; // Eğer öğe boşsa veya okuma başarısızsa, çık.

            var formatter = new XmlSerializer(typeof(PlaybackPreviewImage)); // PlaybackPreviewImage türü için bir XML serileştirici oluşturur.

            while (reader.NodeType != System.Xml.XmlNodeType.EndElement) // Sonlandırıcı öğeye kadar oku.
            {
                if (reader.NodeType == XmlNodeType.Element) // Eğer öğe türü elementse
                {
                    if (reader.Name == "images") // Eğer öğenin adı "images" ise
                    {
                        reader.Read(); // Okumayı bir sonraki öğeye kaydır.
                    }
                    else
                    {
                        this.Add((PlaybackPreviewImage)formatter.Deserialize(reader)); // PlaybackPreviewImage türüne dönüştürüp ekle.
                    }
                }
                else
                {
                    reader.Read(); // Diğer düğümleri atla.
                }
            }

            // images
            reader.ReadEndElement(); // "images" öğesinin sonlandırıcı öğesini oku.
                                     // preview
            reader.ReadEndElement(); // "preview" öğesinin sonlandırıcı öğesini oku.
        }

        public void WriteXml(XmlWriter writer)
        {
            throw new NotImplementedException(); // Yazma işlemi henüz uygulanmamış.
        }

    }
}
