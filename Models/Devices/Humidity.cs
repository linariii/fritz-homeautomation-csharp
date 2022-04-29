using System;
using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models.Devices
{
    [Serializable]
    [XmlRoot(ElementName = "humidity")]
    public class Humidity
    {
        [XmlElement("rel_humidity")]
        public uint RelativeHumidity { get; set; }
    }
}
