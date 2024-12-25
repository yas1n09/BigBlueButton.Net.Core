using System;
using System.IO;
using System.Xml.Serialization;

namespace BigBlueButton.Net.Core.Helpers
{
    public class XmlHelper
    {
        // XML'den Nesneye Dönüştürme (Deserialize)
        public static T FromXml<T>(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                throw new ArgumentNullException(nameof(xml), "XML string cannot be null or empty.");
            }

            try
            {
                var serializer = new XmlSerializer(typeof(T));
                using (var reader = new StringReader(xml))
                {
                    return (T)serializer.Deserialize(reader);
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Deserialization error: {ex.InnerException?.Message ?? ex.Message}");
                throw;
            }
        }

        // XML Hata Yanıtı Oluşturma (Generic)
        public static string XmlErrorResponse<T>(string message, string details) where T : new()
        {
            dynamic errorResponse = new T();
            errorResponse.Message = message;
            errorResponse.Details = details;

            return ToXml(errorResponse);
        }

        // Nesneyi XML'e Dönüştürme (Serialize)
        public static string ToXml<T>(T value)
        {
            if (value == null)
            {
                return $"<ErrorResponse><Message>Serialization error</Message><Details>Value cannot be null.</Details></ErrorResponse>";
            }

            try
            {
                var serializer = new XmlSerializer(typeof(T));
                using (var stringWriter = new StringWriter())
                {
                    serializer.Serialize(stringWriter, value);
                    Console.WriteLine($"Serialized XML: {stringWriter.ToString()}"); // Loglama
                    return stringWriter.ToString();
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Serialization error: {ex.InnerException?.Message ?? ex.Message}");
                return $"<ErrorResponse><Message>Serialization error</Message><Details>{ex.InnerException?.Message ?? ex.Message}</Details></ErrorResponse>";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error during serialization: {ex.Message}");
                return $"<ErrorResponse><Message>Unexpected error</Message><Details>{ex.Message}</Details></ErrorResponse>";
            }
        }
    }
}
