using System.Xml.Serialization;
using Fritz.HomeAutomation.Models.Stats;

namespace Fritz.HomeAutomation.Models
{
    [XmlRoot(ElementName = "devicestats")]
    public class DeviceStats
    {
        [XmlElement(ElementName = "temperature")]
        public Temperature Temperature { get; set; }

        [XmlElement(ElementName = "voltage")]
        public Voltage Voltage { get; set; }

        [XmlElement(ElementName = "power")]
        public Power Power { get; set; }

        [XmlElement(ElementName = "energy")]
        public Energy Energy { get; set; }
    }
}