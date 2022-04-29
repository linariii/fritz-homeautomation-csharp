using System;
using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models
{
    [Serializable]
    [XmlRoot(ElementName = "humidity")]
    public class DeviceHumidity
    {
        [XmlElement("rel_humidity")]
        public int RelHumidity { get; set; }
    }
}
