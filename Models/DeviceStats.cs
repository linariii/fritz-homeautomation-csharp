using System.Xml.Serialization;
using Fritz.HomeAutomation.Models.Stats;

namespace Fritz.HomeAutomation.Models
{
    /// <summary>
    /// Device stats
    /// </summary>
    [XmlRoot(ElementName = "devicestats")]
    public class DeviceStats
    {
        /// <summary>
        /// temperature stats
        /// </summary>
        [XmlElement(ElementName = "temperature")]
        public Temperature Temperature { get; set; }

        /// <summary>
        /// voltage stats
        /// </summary>
        [XmlElement(ElementName = "voltage")]
        public Voltage Voltage { get; set; }

        /// <summary>
        /// Power stats
        /// </summary>
        [XmlElement(ElementName = "power")]
        public Power Power { get; set; }

        /// <summary>
        /// Energie stats
        /// </summary>
        [XmlElement(ElementName = "energy")]
        public Energy Energy { get; set; }
    }
}