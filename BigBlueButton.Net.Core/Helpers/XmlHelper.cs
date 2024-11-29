using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BigBlueButton.Net.Core.Helpers
{
    public class XmlHelper
    {
        public static T FromXml<T>(string xml)
        {
            var serializer = new XmlSerializer(typeof(T)); // Serileştirici oluşturuluyor.

            using (var reader = new StringReader(xml)) // XML verisi StringReader ile okunuyor.
            {
                return (T)serializer.Deserialize(reader); // XML verisi, belirtilen türdeki objeye dönüştürülüp döndürülüyor.
            }
        }

    }
}
