using System;
using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    public class DeviceHumidity
    {
        [XmlElement("rel_humidity")]
        public byte RelHumidity { get; set; }
    }
}
