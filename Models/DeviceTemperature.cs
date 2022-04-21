using System;
using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    public partial class DeviceTemperature
    {
        [XmlElement("celsius")]
        public byte Celsius { get; set; }

        [XmlElement("offset")]
        public byte Offset { get; set; }
    }
}