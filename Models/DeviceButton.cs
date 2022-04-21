using System;
using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    public partial class DeviceButton
    {
        [XmlElement("name")]
        public string name { get; set; }

        [XmlElement("lastpressedtimestamp")]
        public string Lastpressedtimestamp { get; set; }

        [XmlAttribute("identifier")]
        public string Identifier { get; set; }

        [XmlAttribute("id")]
        public int Id { get; set; }
    }
}