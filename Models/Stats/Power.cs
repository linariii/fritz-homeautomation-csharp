using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models.Stats
{
    /// <summary>
    /// Power stats
    /// </summary>
    [XmlRoot(ElementName = "power")]
    public class Power
    {
        /// <summary>
        /// Stats
        /// </summary>
        [XmlElement(ElementName = "stats")]
        public Statistics Statistics { get; set; }
    }
}