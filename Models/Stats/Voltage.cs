using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models.Stats
{
    /// <summary>
    /// Voltage stats
    /// </summary>
    [XmlRoot(ElementName = "voltage")]
    public class Voltage
    {
        /// <summary>
        /// Stats
        /// </summary>
        [XmlElement(ElementName = "stats")]
        public Statistics Statistics { get; set; }
    }
}