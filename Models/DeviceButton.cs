using System;
using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    public class DeviceButton
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("lastpressedtimestamp")]
        public string LastPressedTimestamp { get; set; }

        [XmlAttribute("identifier")]
        public string Identifier { get; set; }

        [XmlAttribute("id")]
        public int Id { get; set; }
    }
}