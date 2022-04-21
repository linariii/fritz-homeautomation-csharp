using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Extensions
{
    internal static class XmlExtensions
    {
        public static T Deserialize<T>(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return default;

            var xmlSerializer = new XmlSerializer(typeof(T));

            return (T)xmlSerializer.Deserialize(new StringReader(value));
        }

        public static string Serialize<T>(this T value)
        {
            if (value == null)
                return string.Empty;

            var xmlSerializer = new XmlSerializer(typeof(T));

            using (var stringWriter = new StringWriter())
            {
                using (var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings { Indent = true }))
                {
                    xmlSerializer.Serialize(xmlWriter, value);
                    return stringWriter.ToString();
                }
            }
        }
    }
}
