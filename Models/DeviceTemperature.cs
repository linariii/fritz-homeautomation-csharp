using System;
using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    public class DeviceTemperature
    {
        [XmlElement("celsius")]
        public int Celsius { get; set; }

        [XmlElement("offset")]
        public int Offset { get; set; }
    }
}