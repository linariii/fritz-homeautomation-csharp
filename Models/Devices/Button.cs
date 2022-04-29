using System;
using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models.Devices
{
    [Serializable]
    [XmlRoot(ElementName = "button")]
    public class Button
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("lastpressedtimestamp")]
        public ulong LastPressedTimestamp { get; set; }

        [XmlAttribute("identifier")]
        public string Identifier { get; set; }

        [XmlAttribute("id")]
        public uint Id { get; set; }
    }
}