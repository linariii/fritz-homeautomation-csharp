using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models.Stats
{
    /// <summary>
    /// Stats
    /// </summary>
    [XmlRoot(ElementName = "stats")]
    public class Statistics
    {
        /// <summary>
        /// Count
        /// </summary>
        [XmlAttribute(AttributeName = "count")]
        public string Count { get; set; }

        /// <summary>
        /// Grid of values
        /// </summary>
        [XmlAttribute(AttributeName = "grid")]
        public string Grid { get; set; }

        /// <summary>
        /// Text
        /// </summary>
        [XmlText]
        public string Text { get; set; }
    }
}