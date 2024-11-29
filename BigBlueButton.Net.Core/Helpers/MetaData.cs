using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace BigBlueButton.Net.Core.Helpers
{
    public class MetaData : Dictionary<string, string>, IXmlSerializable
    {
        public XmlSchema? GetSchema()
        {
            return null; // Şema döndürülmez, null döner.
        }

        public void ReadXml(XmlReader reader)
        {
            if (reader.IsEmptyElement || reader.Read() == false) return; // Eğer öğe boşsa veya okuma başarısızsa, çık.

            while (reader.NodeType != System.Xml.XmlNodeType.EndElement) // Sonlandırıcı öğeye kadar oku.
            {
                if (reader.NodeType == XmlNodeType.Element) // Eğer öğe türü elementse
                {
                    this[reader.Name] = reader.ReadElementContentAsString(); // Öğenin adını ve içeriğini al.
                }
                else
                {
                    reader.Read(); // Diğer düğümleri atla.
                }
            }
            reader.ReadEndElement(); // Sonlandırıcı öğeyi oku.
        }

        public void WriteXml(XmlWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
