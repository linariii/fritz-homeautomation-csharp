using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models.Stats
{
    [XmlRoot(ElementName = "stats")]
    public class Statistics
    {
        [XmlAttribute(AttributeName = "count")]
        public string Count { get; set; }

        [XmlAttribute(AttributeName = "grid")]
        public string Grid { get; set; }

        [XmlText]
        public string Text { get; set; }
    }
}