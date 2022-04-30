using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models.Stats
{
    /// <summary>
    /// Temperature stats
    /// </summary>
    [XmlRoot(ElementName = "temperature")]
    public class Temperature
    {
        /// <summary>
        /// Stats
        /// </summary>
        [XmlElement(ElementName = "stats")]
        public Statistics Statistics { get; set; }
    }
}