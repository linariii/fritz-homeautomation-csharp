using System;
using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models.Devices
{
    [Serializable]
    [XmlRoot(ElementName = "temperature")]
    public class Temperature
    {
        [XmlElement("celsius")]
        public uint Celsius { get; set; }

        [XmlElement("offset")]
        public uint Offset { get; set; }
    }
}