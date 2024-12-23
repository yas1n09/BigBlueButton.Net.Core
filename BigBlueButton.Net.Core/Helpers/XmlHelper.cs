
using BigBlueButton.Net.Core.DTO;
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
        // XML'den Nesneye Dönüştürme (Deserialize)
        public static T FromXml<T>(string xml)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var reader = new StringReader(xml))
            {
                return (T)serializer.Deserialize(reader);
            }
        }












        // XML Hata Yanıtı Oluşturma
        public static string XmlErrorResponse(string message, string details)
        {
            var errorResponse = new ErrorResponseDto
            {
                Message = message,
                Details = details
            };
            return ToXml(errorResponse);
        }



















        // Nesneyi XML'e Dönüştürme (Serialize)
        public static string ToXml<T>(T value)
        {
            if (value == null) return string.Empty;

            try
            {
                var serializer = new XmlSerializer(typeof(T));
                using (var stringWriter = new StringWriter())
                {
                    serializer.Serialize(stringWriter, value);
                    return stringWriter.ToString();
                }
            }
            catch (InvalidOperationException ex)
            {
                var errorResponse = new ErrorResponseDto
                {
                    Message = "Serialization error",
                    Details = ex.Message
                };
                return ToXml(errorResponse);
            }
        }

    }
}
